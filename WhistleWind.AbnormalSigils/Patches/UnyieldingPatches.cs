using DiskCardGame;
using GBC;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.Patches {
    /// <summary>
    /// Patches related to Unyielding sigil.
    /// </summary>
    [HarmonyPatch]
    internal class UnyieldingPatches {
        [HarmonyPrefix, HarmonyPatch(typeof(Strafe), nameof(Strafe.MoveToSlot))]
        private static bool PreventMovingToSlot(CardSlot destination, ref bool destinationValid) {
            if (destinationValid && destination?.Card != null && destination.Card.HasAbility(Unyielding.ability))
                destinationValid = false;

            return true;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(Strafe), nameof(Strafe.OnTurnEnd))]
        private static IEnumerator PreventStrafeActivation(IEnumerator enumerator, Strafe __instance) {
            if (__instance.Card.HasAbility(Unyielding.ability)) {
                yield return Unyielding.OnPreventMovement(__instance);
                yield break;
            }
            yield return enumerator;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(StrafePush), nameof(StrafePush.SlotHasSpace))]
        private static void PreventShoving(CardSlot slot, ref bool __result) {
            if (__result && slot?.Card != null && slot.Card.HasAbility(Unyielding.ability))
                __result = false;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(StrafeSwap), nameof(StrafeSwap.DoStrafe))]
        private static bool PreventGrabnabbing(ref CardSlot toLeft, ref CardSlot toRight) {
            if (toLeft?.Card != null && toLeft.Card.HasAbility(Unyielding.ability)) {
                toLeft.Card.Anim.StrongNegationEffect();
                toLeft = null;
            }

            if (toRight?.Card != null && toRight.Card.HasAbility(Unyielding.ability)) {
                toRight.Card.Anim.StrongNegationEffect();
                toRight = null;
            }

            if (toLeft == toRight)
                return false;

            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(WhackAMole), nameof(WhackAMole.OnSlotTargetedForAttack))]
        private static IEnumerator PreventMoleWhacking(IEnumerator enumerator, WhackAMole __instance) {
            if (__instance.Card.HasAbility(Unyielding.ability)) {
                yield return Unyielding.OnPreventMovement(__instance);
                yield break;
            }
            yield return enumerator;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(BoardManager), nameof(BoardManager.AssignCardToSlot))]
        private static IEnumerator PreventNewAssignments(IEnumerator enumerator, PlayableCard card, CardSlot slot) {
            if (card.HasAbility(Unyielding.ability)) {
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
            if (card.HasAbility(Unyielding.ability)) {
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
            __result.RemoveAll(x => x.Card.HasAbility(Unyielding.ability));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(FishHookGrab), nameof(FishHookGrab.PullHook))]
        private static IEnumerator PreventAnglerPullHook(IEnumerator result, FishHookGrab __instance) {
            if (__instance.hookTargetSlot?.Card != null && __instance.hookTargetSlot.Card.HasAbility(Unyielding.ability))
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
            List<CardSlot> slotsWithCards = Singleton<BoardManager>.Instance.PlayerSlotsCopy.FindAll(x => x.Card != null && !x.Card.HasAbility(Unyielding.ability));
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

        [HarmonyPrefix, HarmonyPatch(typeof(BoardManager), nameof(BoardManager.MoveAllCardsClockwise))]
        private static bool AccountForUnyieldingCards(ref IEnumerator __result, BoardManager __instance) {
            __result = RotateAllCardsOnBoard(true);
            return false;
        }
        public static IEnumerator RotateAllCardsOnBoard(bool clockwise, CardSlot leader = null) {
            if (!BoardManager.Instance.AllSlotsCopy.Exists(x => x.Card != null && !x.Card.HasAbility(Unyielding.ability))) {
                yield break;
            }
            AbnormalPlugin.Log.LogInfo($"[RotateAllCardsOnBoard] Clockwise: {clockwise}");
            List<CardSlot> playerSlots = BoardManager.Instance.PlayerSlotsCopy;
            List<CardSlot> opponentSlots = BoardManager.Instance.OpponentSlotsCopy;
            Dictionary<PlayableCard, CardSlot> originalAssignments = new();
            Dictionary<PlayableCard, CardSlot> destinations = new();
            int highestIndex = playerSlots.Count - 1;

            foreach (CardSlot slot in BoardManager.Instance.AllSlotsCopy) {
                AbnormalPlugin.Log.LogInfo($"Cache slot {slot.Index} [{slot.IsPlayerSlot}]");
                if (slot.Card == null || slot.Card.HasAbility(Unyielding.ability)) {
                    continue;
                }

                CardSlot destination = null;
                int i = slot.Index;
                if (slot.IsPlayerSlot) {
                    if (clockwise) {
                        destination = i == 0 ? opponentSlots[0] : playerSlots[i - 1];
                    }
                    else {
                        destination = i == highestIndex ? opponentSlots[highestIndex] : playerSlots[i + 1];
                    }
                }
                else {
                    if (clockwise) {
                        destination = i == highestIndex ? playerSlots[highestIndex] : opponentSlots[i + 1];
                    }
                    else {
                        destination = i == 0 ? playerSlots[0] : opponentSlots[i - 1];
                    }
                }
                originalAssignments.Add(slot.Card, slot);
                destinations.Add(slot.Card, destination);
                AbnormalPlugin.Log.LogInfo($"Cache destination: {destination.Index} [{destination.IsPlayerSlot}]");
            }

            foreach (CardSlot slot in BoardManager.Instance.AllSlotsCopy) {
                if (slot.Card == null || slot.Card.HasAbility(Unyielding.ability)) {
                    continue;
                }
                slot.Card.Slot = null;
                slot.Card = null;
            }

            foreach (KeyValuePair<PlayableCard, CardSlot> keyPairs in originalAssignments) {
                PlayableCard card = keyPairs.Key;
                AbnormalPlugin.Log.LogInfo($"Assigning card [{card?.Info.name}] in slot [{keyPairs.Value?.Index}]");
                if (card != null && destinations.TryGetValue(card, out CardSlot slot) && slot.Card == null) {
                    bool isOpponent = card.OpponentCard;
                    card.SetIsOpponentCard(!slot.IsPlayerSlot);
                    yield return AssignCardToSlotCoro(card, slot, isOpponent);
                    //CustomCoroutine.Instance.StartCoroutine(AssignCardToSlotCoro(card, slot, isOpponent));
                    AbnormalPlugin.Log.LogInfo($"Assign to Slot[{slot.Index}]");
                }
            }
            ResourcesManager.Instance.ForceGemsUpdate();
        }
        private static IEnumerator AssignCardToSlotCoro(PlayableCard card, CardSlot slot, bool wasOpponent) {
            yield return BoardManager.Instance.AssignCardToSlot(card, slot);
            if (card.FaceDown) {
                if (wasOpponent != card.OpponentCard) {
                    card.SetFaceDown(false);
                    card.UpdateFaceUpOnBoardEffects();
                }
            }
        }
    }
}
