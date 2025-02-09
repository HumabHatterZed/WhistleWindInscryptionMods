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
    [HarmonyPatch]
    public static class SpeedPatch
    {
        private static bool ModifyBySpeed = true;
        private static readonly List<CardSlot> AttackedThisRound = new();

        public static List<CardSlot> HandleSpeedModifications(List<CardSlot> result, bool playerIsAttacker)
        {
            if (!ModifyBySpeed || result.Count == 0) // prevent recursion
                return result;

            ModifyBySpeed = false;
            List<CardSlot> cardsAttackingThisTurn = new();
            List<CardSlot> playerCardsResult = DoCombatPhasePatches.ModifyAttackingSlots(true);
            List<CardSlot> opponentCardsResult = DoCombatPhasePatches.ModifyAttackingSlots(false);
            ModifyBySpeed = true;

            List<CardSlot> allCardsResult = playerCardsResult.Concat(opponentCardsResult).ToList();
            allCardsResult.RemoveAll(x => x.Card == null || x.Card.Attack == 0 || AttackedThisRound.Contains(x));

            AbnormalPlugin.Log.LogDebug($"[SpeedLogic] Results: {allCardsResult.Count} AttackedThisRound: {AttackedThisRound.Count}");
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

            int highestPlayerSpeed = CardSpeed(distinctResults.FirstOrDefault(x => x.IsPlayerSlot));
            int lowestOpponentSpeed = CardSpeed(distinctResults.LastOrDefault(x => !x.IsPlayerSlot));

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
                List<CardSlot> positiveOutspeed = lowestPlayerSpeed < 1 ? cardsOutspeedSlowest.FindAll(x => CardSpeed(x) > 0) : cardsOutspeedSlowest.FindAll(x => CardSpeed(x) >= 0);
                cardsOutspeedSlowest.RemoveAll(positiveOutspeed.Contains);

                if (positiveOutspeed.Count > 0) // only add positive speed cards
                {
                    cardsAttackingThisTurn.AddRange(positiveOutspeed);
                }
                else // lowest player is negative, add any cards that outspeed it
                {
                    cardsAttackingThisTurn.AddRange(cardsOutspeedSlowest);
                }

                bool outspeedsAreNegative = cardsOutspeedSlowest.All(x => CardSpeed(x) < 1);

                // if slowest player is neutral or all cards outspeeding it are at most neutral, add slowest speed
                if (lowestPlayerSpeed > -1)
                {
                    AbnormalPlugin.Log.LogDebug("[SpeedLogic] Add slowest player slots");

                    List<CardSlot> playerNeutralSpeed = allCardsResult.FindAll(x => CardSpeed(x) == lowestPlayerSpeed);
                    allCardsResult.RemoveAll(playerNeutralSpeed.Contains);

                    cardsAttackingThisTurn.AddRange(playerNeutralSpeed);
                }
            }

            cardsAttackingThisTurn.Sort((CardSlot a, CardSlot b) => CardSpeed(b) - CardSpeed(a));
            return cardsAttackingThisTurn;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.PlayerTurn))]
        private static bool ResetAttackedThisRound()
        {
            AttackedThisRound.Clear();
            return true;
        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.CleanupPhase))]
        private static void CleanUpSlotsQueuedForTurn()
        {
            ModifyBySpeed = true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(DoCombatPhasePatches), nameof(DoCombatPhasePatches.ModifyAttackingSlots))]
        private static void SortByCardSpeed(ref List<CardSlot> __result, bool playerIsAttacker)
        {
            if (ModifyBySpeed)
            {
                int or = __result.Count;
                AbnormalPlugin.Log.LogDebug("[SpeedLogic] Start: " + playerIsAttacker);
                __result = HandleSpeedModifications(__result, playerIsAttacker);
                if (playerIsAttacker)
                {
                    AttackedThisRound.AddRange(__result);
                }

                AbnormalPlugin.Log.LogDebug($"[SpeedLogic] Old: {or} New: {__result.Count}");
            }
            return;
        }

        /// <summary>
        /// Gets a Card's speed value, used to determine attack order
        /// </summary>
        public static int CardSpeed(CardSlot slot)
        {
            if (slot?.Card == null)
                return -9000;

            int cardSpeed = slot.Card.GetAbilityStacks(Haste.iconId) + slot.Card.GetAbilityStacks(Bind.iconId);
            return cardSpeed;
        }
    }
}
