using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.Patches {
    /// <summary>
    /// Patches related to Unyielding sigil.
    /// Contains logic for clockwise rotation.
    /// </summary>
    [HarmonyPatch]
    internal class UnyieldingPatches {
        [HarmonyPrefix, HarmonyPatch(typeof(Strafe), nameof(Strafe.MoveToSlot))]
        private static bool PreventMovingToSlot(CardSlot destination, ref bool destinationValid) {
            if (destinationValid && destination?.Card != null && !Unyielding.CardCanBeMoved(destination.Card))
                destinationValid = false;

            return true;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(Strafe), nameof(Strafe.OnTurnEnd))]
        private static IEnumerator PreventStrafeActivation(IEnumerator enumerator, Strafe __instance) {
            if (!Unyielding.CardCanBeMoved(__instance.Card)) {
                yield return Unyielding.OnPreventMovement(__instance);
                yield break;
            }
            yield return enumerator;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(StrafePush), nameof(StrafePush.SlotHasSpace))]
        private static void PreventShoving(CardSlot slot, ref bool __result) {
            if (__result && slot?.Card != null && !Unyielding.CardCanBeMoved(slot.Card))
                __result = false;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(StrafeSwap), nameof(StrafeSwap.DoStrafe))]
        private static bool PreventGrabnabbing(ref CardSlot toLeft, ref CardSlot toRight) {
            if (toLeft?.Card != null && !Unyielding.CardCanBeMoved(toLeft.Card)) {
                toLeft.Card.Anim.StrongNegationEffect();
                toLeft = null;
            }

            if (toRight?.Card != null && !Unyielding.CardCanBeMoved(toRight.Card)) {
                toRight.Card.Anim.StrongNegationEffect();
                toRight = null;
            }

            if (toLeft == toRight)
                return false;

            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(WhackAMole), nameof(WhackAMole.OnSlotTargetedForAttack))]
        private static IEnumerator PreventMoleWhacking(IEnumerator enumerator, WhackAMole __instance) {
            if (!Unyielding.CardCanBeMoved(__instance.Card)) {
                yield return Unyielding.OnPreventMovement(__instance);
                yield break;
            }
            yield return enumerator;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(BoardManager), nameof(BoardManager.AssignCardToSlot))]
        private static IEnumerator PreventNewAssignments(IEnumerator enumerator, PlayableCard card, CardSlot slot) {
            if (!Unyielding.CardCanBeMoved(card)) {
                Unyielding behav = card.TriggerHandler.triggeredAbilities.Find(x => x.Item1 == Unyielding.ability)?.Item2 as Unyielding;
                if (behav?.homeSlot != null && slot != behav.homeSlot) // if the card has already resolved and is being assigned to a different slot
                {
                    yield return Unyielding.OnPreventMovement(behav, behav.homeSlot);
                    yield break;
                }
            }
            yield return enumerator;
        }

        private static bool UnyieldingPatchCheck(PlayableCard card, ref bool result) {
            if (!Unyielding.CardCanBeMoved(card)) {
                result = false;
                return false;
            }
            return true;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(GuardDog), nameof(GuardDog.RespondsToOtherCardResolve))]
        private static bool PreventGuardDogging(GuardDog __instance, ref bool __result) => UnyieldingPatchCheck(__instance.Card, ref __result);

        [HarmonyPrefix, HarmonyPatch(typeof(MoveBeside), nameof(MoveBeside.RespondsToOtherCardResolve))]
        private static bool PreventClingyBehaviour(MoveBeside __instance, ref bool __result) => UnyieldingPatchCheck(__instance.Card, ref __result);

        [HarmonyPrefix, HarmonyPatch(typeof(TailOnHit), nameof(TailOnHit.RespondsToCardGettingAttacked))]
        private static bool PreventTailLoss(TailOnHit __instance, ref bool __result) => UnyieldingPatchCheck(__instance.Card, ref __result);

        [HarmonyPostfix, HarmonyPatch(typeof(FishHookItem), nameof(FishHookItem.GetValidTargets))]
        private static void PreventOpponentHooking(List<CardSlot> __result) {
            __result.RemoveAll(x => !Unyielding.CardCanBeMoved(x.Card));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(FishHookGrab), nameof(FishHookGrab.PullHook))]
        private static IEnumerator PreventAnglerPullHook(IEnumerator result, FishHookGrab __instance) {
            if (__instance.hookTargetSlot?.Card != null && !Unyielding.CardCanBeMoved(__instance.hookTargetSlot.Card))
                yield break;

            yield return result;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(FishHookGrab), nameof(FishHookGrab.AimHookAtRandomSlot))]
        private static bool PreventAnglerAimHook(ref IEnumerator __result, FishHookGrab __instance) {
            __result = NewAimHookAtRandomSlot(__instance);
            return false;
        }

        public static IEnumerator NewAimHookAtRandomSlot(FishHookGrab instance) {
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;
            List<CardSlot> slotsWithCards = Singleton<BoardManager>.Instance.PlayerSlotsCopy.FindAll(x => x.Card != null && Unyielding.CardCanBeMoved(x.Card));
            CardSlot slot;
            if (slotsWithCards.Count > 0) {
                yield return instance.TeachMechanicSequence("TeachFishHookAimRandom");
                slot = slotsWithCards[SeededRandom.Range(0, slotsWithCards.Count, randomSeed)];
            }
            else {
                slot = Singleton<BoardManager>.Instance.PlayerSlotsCopy[SeededRandom.Range(0, 4, randomSeed)];
            }
            yield return instance.AimHook(slot);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PocketWatchItem), nameof(PocketWatchItem.SomeCardsOnBoard))]
        private static void PreventRotationWhenUnyieldingFull(ref bool __result) {
            // if the board is full and there is at least 1 Unyielding card, prevent rotation
            if (__result && !BoardManager.Instance.AllSlotsCopy.Exists(x => x.Card == null) && BoardManager.Instance.CardsOnBoard.Exists(x => !Unyielding.CardCanBeMoved(x))) {
                __result = false;
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(BoardManager), nameof(BoardManager.MoveAllCardsClockwise))]
        private static bool AccountForUnyieldingCards(ref IEnumerator __result, BoardManager __instance) {
            __result = RotateAllCardsOnBoard(true);
            return false;
        }
        /// <summary>
        /// Version of BoardManager.MoveAllCardsClockwise that accounts for Unyielding cards and supports counterclockwise rotation.
        /// </summary>
        /// <param name="clockwise">Whether cards on the board should be rotated clockwise or counterclockwise.</param>
        /// <returns></returns>
        public static IEnumerator RotateAllCardsOnBoard(bool clockwise) {
            if (!BoardManager.Instance.AllSlotsCopy.Exists(x => x.Card != null && Unyielding.CardCanBeMoved(x.Card))) {
                yield break;
            }
            //AbnormalPlugin.Log.LogInfo($"[RotateAllCardsOnBoard] Clockwise: {clockwise}");
            List<CardSlot> playerSlots = BoardManager.Instance.PlayerSlotsCopy;
            List<CardSlot> opponentSlots = BoardManager.Instance.OpponentSlotsCopy;

            Dictionary<PlayableCard, CardSlot> originalAssignments = new();
            Dictionary<PlayableCard, CardSlot> destinations = new();
            int highestIndex = playerSlots.Count - 1;

            for (int i = 0; i <= highestIndex; i++) {
                CardSlot slot = clockwise ? playerSlots[i] : opponentSlots[i];
                HandleSlotRotation(slot, clockwise, 0, playerSlots, opponentSlots, originalAssignments, destinations);
            }
            for (int i = highestIndex; i >= 0; i--) {
                CardSlot slot = clockwise ? opponentSlots[i] : playerSlots[i];
                HandleSlotRotation(slot, clockwise, highestIndex, playerSlots, opponentSlots, originalAssignments, destinations);
            }

            foreach (KeyValuePair<PlayableCard, CardSlot> keyPairs in originalAssignments) {
                PlayableCard card = keyPairs.Key;
                //AbnormalPlugin.Log.LogInfo($"Assigning card [{card?.Info.name}] in slot [{keyPairs.Value?.Index}]");
                if (card != null && destinations.TryGetValue(card, out CardSlot slot) && slot.Card == null) {
                    bool movingToOtherSide = card.OpponentCard == slot.IsPlayerSlot;
                    card.SetIsOpponentCard(!slot.IsPlayerSlot);
                    if (movingToOtherSide && SaveManager.SaveFile.IsPart1) {
                        card.transform.eulerAngles += new Vector3(0f, 0f, -180f);
                    }
                    yield return BoardManager.Instance.AssignCardToSlot(card, slot, resolveTriggers: false);
                    if (movingToOtherSide && card.FaceDown) {
                        card.SetFaceDown(false);
                        card.UpdateFaceUpOnBoardEffects();
                    }
                    //AbnormalPlugin.Log.LogInfo($"Assign to Slot[{slot.Index}]");
                }
            }
            ResourcesManager.Instance.ForceGemsUpdate();

            // trigger OtherCardAssignedToSlot after rotating all cards, rather than after each one
            foreach (PlayableCard card in destinations.Keys) {
                yield return GlobalTriggerHandler.Instance.TriggerCardsOnBoard(Trigger.OtherCardAssignedToSlot, false, card);
            }
        }
        private static void HandleSlotRotation(
            CardSlot slot, bool clockwise, int edgeIndex,
            List<CardSlot> playerSlots, List<CardSlot> opponentSlots,
            Dictionary<PlayableCard, CardSlot> originalSlots, Dictionary<PlayableCard, CardSlot> destinations) {
            AbnormalPlugin.Log.LogInfo($"Cache player slot {slot.Index}");
            int nextIndex;
            CardSlot destination;
            if (slot.IsPlayerSlot) {
                nextIndex = slot.Index + (clockwise ? -1 : 1);
                destination = DetermineDestination(slot, edgeIndex, nextIndex, playerSlots, opponentSlots);
            }
            else {
                nextIndex = slot.Index + (clockwise ? 1 : -1);
                destination = DetermineDestination(slot, edgeIndex, nextIndex, opponentSlots, playerSlots);
            }
            if (destination != null) {
                originalSlots.Add(slot.Card, slot);
                destinations.Add(slot.Card, destination);
                AbnormalPlugin.Log.LogInfo($"Cache destination: {destination.Index} [{destination.IsPlayerSlot}]");
            }
        }
        private static CardSlot DetermineDestination(
            CardSlot slot, int edgeIndex, int nextIndex,
            List<CardSlot> sameSideSlots, List<CardSlot> opposingSlots) {
            if (slot.Card == null || !Unyielding.CardCanBeMoved(slot.Card)) {
                return null;
            }
            CardSlot destination = slot.Index == edgeIndex ? opposingSlots[edgeIndex] : sameSideSlots[nextIndex];
            if (destination.Card != null && !Unyielding.CardCanBeMoved(destination.Card)) {
                return null;
            }
            return destination;
        }
    }
}
