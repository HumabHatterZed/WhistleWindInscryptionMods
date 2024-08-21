using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils.Patches
{
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
        [HarmonyPostfix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.CleanupPhase))]
        private static void CleanUpSlotsQueuedForTurn()
        {
            SlotsQueuedForTurn[true].Clear();
            SlotsQueuedForTurn[false].Clear();
        }
        [HarmonyPostfix, HarmonyPatch(typeof(DoCombatPhasePatches), nameof(DoCombatPhasePatches.ModifyAttackingSlots))]
        private static void SortByCardSpeed(List<CardSlot> __result, bool playerIsAttacker)
        {
            if (playerIsAttacker)
            {
                foreach (CardSlot slot in BoardManager.Instance.PlayerSlotsCopy)
                {
                    if (CardSpeed(slot) < 0) // if a player card has negative Speed, queue them for the opponent's turn
                    {
                        int numToAddToOpponentTurn = __result.RemoveAll(x => x == slot);
                        if (numToAddToOpponentTurn > 0)
                        {
                            AbnormalPlugin.Log.LogDebug($"[SpeedLogic] Queuing player slot [{slot.Index}] for opponent's turn, amount [{numToAddToOpponentTurn}]");
                            SlotsQueuedForTurn[false].Add(new(slot.Index, numToAddToOpponentTurn));
                        }
                    }
                }
                foreach (Tuple<int, int> slotQueuedForTurn in SlotsQueuedForTurn[true])
                {
                    CardSlot slot = BoardManager.Instance.OpponentSlotsCopy.Find(x => x.Index == slotQueuedForTurn.Item1);
                    if (!__result.Contains(slot))
                    {
                        AbnormalPlugin.Log.LogDebug($"Adding queued opponent slot {slot.Index} to player's turn, amount {slotQueuedForTurn.Item2}");
                        for (int i = 0; i < slotQueuedForTurn.Item2; i++)
                            __result.Add(slot);
                    }
                }
                SlotsQueuedForTurn[true].Clear();
            }
            else
            {
                foreach (CardSlot slot in BoardManager.Instance.OpponentSlotsCopy)
                {
                    if (CardSpeed(slot) > 3) // if an opponent has positive speed, queue them for the player's turn
                    {
                        int numToAddToPlayerTurn = __result.RemoveAll(x => x == slot);
                        if (numToAddToPlayerTurn > 0)
                        {
                            AbnormalPlugin.Log.LogDebug($"[SpeedLogic] Queuing opponent slot [{slot.Index}] for player's turn, amount [{numToAddToPlayerTurn}]");
                            SlotsQueuedForTurn[true].Add(new(slot.Index, numToAddToPlayerTurn));
                        }
                    }
                }
                foreach (Tuple<int, int> slotQueuedForTurn in SlotsQueuedForTurn[false])
                {
                    CardSlot slot = BoardManager.Instance.PlayerSlotsCopy.Find(x => x.Index == slotQueuedForTurn.Item1);
                    if (!__result.Contains(slot))
                    {
                        AbnormalPlugin.Log.LogDebug($"Adding queued player slot {slot.Index} to opponent's turn, amount {slotQueuedForTurn.Item2}");
                        for (int i = 0; i < slotQueuedForTurn.Item2; i++)
                            __result.Add(slot);
                    }
                }
                SlotsQueuedForTurn[false].Clear();
            }

            __result.Sort((CardSlot a, CardSlot b) => CardSpeed(b) - CardSpeed(a));
        }

        /// <summary>
        /// Used to determine a card's Speed. Player cards have a base of 3 while opponents have a base of 0.
        /// This is to simulate how Inscryption normally plays, with the player always going first.
        /// Bind decreases speed while Haste increases it.
        /// </summary>
        /// <param name="slot"></param>
        public static int CardSpeed(CardSlot slot)
        {
            if (slot.Card == null)
                return 0;

            int cardSpeed = slot.Card.OpponentCard ? 0 : 3;
            cardSpeed -= slot.Card.GetAbilityStacks(Bind.iconId);
            cardSpeed += slot.Card.GetAbilityStacks(Haste.iconId);

            return cardSpeed;
        }
    }
}
