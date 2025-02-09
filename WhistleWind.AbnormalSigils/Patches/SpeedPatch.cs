using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WhistleWind.AbnormalSigils.Patches
{
    public class SpeedQueueSlot
    {
        public CardSlot slot;
        public int numSlotsToAdd;
        public bool attackedThisTurn;
    }
    [HarmonyPatch]
    public static class SpeedPatch
    {
        /// <summary>
        /// If a card's Speed is high or low enough for it to change whose turn it attacks on, we need to keep track of that slot so we can re-add it.
        /// This is where these slot's are cached, with true corresponding to cards that will be re-added 
        /// </summary>
        private static readonly Dictionary<bool, List<Tuple<int, int>>> SlotsQueuedForTurn = new()
        {
            { true, new() },
            { false, new() }
        };

        public static readonly Dictionary<CardSlot, int> QueuedSlots = new();

        [HarmonyPostfix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.CleanupPhase))]
        private static void CleanUpSlotsQueuedForTurn()
        {
            CheckSpeed = true;
            SlotsQueuedForTurn[true].Clear();
            SlotsQueuedForTurn[false].Clear();
        }

        private static bool CheckSpeed = true;
        private static readonly List<CardSlot> AlreadyAttacked = new();

        public static List<CardSlot> HandleSpeedModifications(List<CardSlot> result, bool playerIsAttacker)
        {
            if (!CheckSpeed || result.Count == 0) // prevent recursion
                return result;

            CheckSpeed = false;
            List<CardSlot> cardsAttackingThisTurn = new();
            List<CardSlot> playerCardsResult = DoCombatPhasePatches.ModifyAttackingSlots(true);
            List<CardSlot> opponentCardsResult = DoCombatPhasePatches.ModifyAttackingSlots(false);
            CheckSpeed = true;

            List<CardSlot> allCardsResult = playerCardsResult.Concat(opponentCardsResult).ToList();
            allCardsResult.RemoveAll(x => x.Card == null || x.Card.Attack == 0 || AlreadyAttacked.Contains(x));

            AbnormalPlugin.Log.LogDebug($"[SpeedLogic] Results: {allCardsResult.Count} AlreadyAttacked: {AlreadyAttacked.Count}");
            if (allCardsResult.Count == 0)
            {
                AbnormalPlugin.Log.LogDebug("[SpeedLogic] Zero attackers");
                return cardsAttackingThisTurn;
            }

            allCardsResult.Sort((CardSlot a, CardSlot b) => CardSpeed(b) - CardSpeed(a));

            // cards already played have been filtered out, so return the remainder for the opponent's turn
            if (!playerIsAttacker)
            {
                AbnormalPlugin.Log.LogDebug("[SpeedLogic] Opponent's turn.");
                return allCardsResult;
            }

            List<CardSlot> distinctResults = allCardsResult.Distinct().ToList();

            int lowestPlayerSpeed = CardSpeed(distinctResults.LastOrDefault(x => x.IsPlayerSlot));
            int highestOpponentSpeed = CardSpeed(distinctResults.FirstOrDefault(x => !x.IsPlayerSlot));

            // if the slowest player matches the fastest opponent or it faster, return vanilla order
            // also captures results where cards are all the same speed
            if (lowestPlayerSpeed >= highestOpponentSpeed)
            {
                AbnormalPlugin.Log.LogDebug("[SpeedLogic] Player faster/equal than opponent");
                return result;
            }

            int highestPlayerSpeed = CardSpeed(distinctResults.First(x => x.IsPlayerSlot));
            int lowestOpponentSpeed = CardSpeed(distinctResults.Last(x => !x.IsPlayerSlot));

            // if all opponents are faster than the player, reverse the attacker order
            if (lowestOpponentSpeed > highestPlayerSpeed)
            {
                AbnormalPlugin.Log.LogDebug("[SpeedLogic] Opponent outspeeds player");
                return allCardsResult.Where(x => !x.IsPlayerSlot).ToList();
            }

            List<CardSlot> opponentsOutspeedFastest = allCardsResult.FindAll(x => !x.IsPlayerSlot && CardSpeed(x) > highestPlayerSpeed);
            allCardsResult.RemoveAll(opponentsOutspeedFastest.Contains);

            List<CardSlot> fastestPlayers = allCardsResult.FindAll(x => x.IsPlayerSlot && CardSpeed(x) == highestPlayerSpeed);
            allCardsResult.RemoveAll(fastestPlayers.Contains);

            List<CardSlot> cardsOutspeedSlowest = allCardsResult.FindAll(x => CardSpeed(x) > lowestPlayerSpeed);
            allCardsResult.RemoveAll(cardsOutspeedSlowest.Contains);

            cardsAttackingThisTurn.AddRange(opponentsOutspeedFastest);
            cardsAttackingThisTurn.AddRange(fastestPlayers);

            if (cardsOutspeedSlowest.Count > 0)
            {
                List<CardSlot> positiveOutspeed = cardsOutspeedSlowest.FindAll(x => CardSpeed(x) >= 0);
                cardsOutspeedSlowest.RemoveAll(positiveOutspeed.Contains);

                if (positiveOutspeed.Count > 0) // only add positive speed cards
                {
                    cardsAttackingThisTurn.AddRange(positiveOutspeed);
                }
                else // lowest player is negative, add any cards that outspeed it
                {
                    cardsAttackingThisTurn.AddRange(cardsOutspeedSlowest);
                }

                bool outspeedsAreNegative = cardsOutspeedSlowest.All(x => CardSpeed(x) <= 0);

                // if slowest player is neutral or all cards outspeeding it are at most neutral, add slowest speed
                if (lowestPlayerSpeed >= 0)
                {
                    AbnormalPlugin.Log.LogDebug("[SpeedLogic] Add slowest player slots");

                    List<CardSlot> playerNeutralSpeed = allCardsResult.FindAll(x => CardSpeed(x) == lowestPlayerSpeed);
                    allCardsResult.RemoveAll(playerNeutralSpeed.Contains);

                    cardsAttackingThisTurn.AddRange(playerNeutralSpeed);
                }



                /*List<CardSlot> opponentsOutspeedPositive = cardsOutspeedSlowest.FindAll(x => !x.IsPlayerSlot && CardSpeed(x) > 0);
                if (opponentsOutspeedPositive.Count > 0)
                {
                    AbnormalPlugin.Log.LogDebug("[SpeedLogic] Opponents positive outspeed slowest");
                    cardsAttackingThisTurn.AddRange(opponentsOutspeedPositive);
                }

                if (lowestPlayerSpeed >= 0)
                {
                    List<CardSlot> playerNeutralSpeed = allCardsResult.FindAll(x => x.IsPlayerSlot && CardSpeed(x) == lowestPlayerSpeed);
                    allCardsResult.RemoveAll(playerNeutralSpeed.Contains);

                    cardsAttackingThisTurn.AddRange(playerNeutralSpeed);
                }*/
            }

            /*List<int> distinctSpeeds = distinctResults.Select(CardSpeed).ToList(); // already sorted from high to low
            List<CardSlot> fastestCards = allCardsResult.Where(x => CardSpeed(x) == distinctSpeeds[0]).ToList();

            List<CardSlot> secondFastestCards = allCardsResult.Where(x => CardSpeed(x) == distinctSpeeds[1]).ToList();
            if (false && distinctSpeeds.Count == 2)
            {
                if (distinctSpeeds[0] <= 0)
                {
                    AbnormalPlugin.Log.LogDebug("[SpeedLogic] Fastest cards neutral or negative");
                    return fastestCards.Where(x => x.IsPlayerSlot).ToList();
                }
                cardsAttackingThisTurn.AddRange(fastestCards);
                if (distinctSpeeds[1] >= 0)
                {
                    AbnormalPlugin.Log.LogDebug("[SpeedLogic] Second fastest neutral or positive");
                    cardsAttackingThisTurn.AddRange(secondFastestCards.Where(x => x.IsPlayerSlot));
                }
            }
            else // greater than 2
            {

            }*/

            cardsAttackingThisTurn.Sort((CardSlot a, CardSlot b) => CardSpeed(b) - CardSpeed(a));
            return cardsAttackingThisTurn;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.PlayerTurn))]
        private static bool ResetCardsAlreadyAttacked()
        {
            AlreadyAttacked.Clear();
            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(DoCombatPhasePatches), nameof(DoCombatPhasePatches.ModifyAttackingSlots))]
        private static void SortByCardSpeed(ref List<CardSlot> __result, bool playerIsAttacker)
        {
            if (CheckSpeed)
            {
                int or = __result.Count;
                AbnormalPlugin.Log.LogDebug("[SpeedLogic] Start: " + playerIsAttacker);
                __result = HandleSpeedModifications(__result, playerIsAttacker);
                if (playerIsAttacker)
                {
                    //AbnormalPlugin.Log.LogDebug("[SpeedLogic] AlreadyAttacked");
                    AlreadyAttacked.AddRange(__result);
                }

                AbnormalPlugin.Log.LogDebug($"[SpeedLogic] Old: {or} New: {__result.Count}");
            }
            return;
        }

        /// <summary>
        /// Used to determine a card's Speed. Player cards have a base of 3 while opponents have a base of 0.
        /// This is to simulate how Inscryption normally plays, with the player always going first.
        /// Bind decreases speed while Haste increases it.
        /// </summary>
        /// <param name="slot"></param>
        public static int CardSpeed(CardSlot slot)
        {
            if (slot?.Card == null)
                return -9000;

            int cardSpeed = 0;
            cardSpeed += slot.Card.GetAbilityStacks(Haste.iconId);
            cardSpeed -= slot.Card.GetAbilityStacks(Bind.iconId);
            return cardSpeed;
        }
    }
}
