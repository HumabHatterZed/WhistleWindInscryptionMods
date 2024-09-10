using DiskCardGame;
using EasyFeedback.APIs;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Unyielding()
        {
            const string rulebookName = "Unyielding";
            const string rulebookDescription = "[creature] cannot be moved from its space on the board by most effects.";
            const string dialogue = "This beast is stubborn. It refuses to move.";
            const string triggerText = "[creature] digs in its heels!";
            Unyielding.ability = AbnormalAbilityHelper.CreateAbility<Unyielding>(
                "sigilUnyielding",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 1,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    [HarmonyPatch]
    public class Unyielding : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public CardSlot homeSlot = null;
        public bool indicateStubbornness = true;
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            if (homeSlot == null)
            {
                yield return base.PreSuccessfulTriggerSequence();
                homeSlot = base.Card.Slot;
            }
            else
            {
                yield return BoardManager.Instance.AssignCardToSlot(base.Card, homeSlot, resolveTriggers: false);
            }
        }
        public override bool RespondsToUpkeep(bool playerUpkeep) => true;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            indicateStubbornness = true;
            yield break;
        }
        public static IEnumerator OnPreventMovement(AbilityBehaviour ability, CardSlot slotToReturnTo = null)
        {
            if (ability is Unyielding unyield && unyield.indicateStubbornness)
            {
                unyield.indicateStubbornness = false;
                ability.Card.Anim.StrongNegationEffect();
            }
            else
            {
                Unyielding behav = ability.Card.TriggerHandler.triggeredAbilities.Find(x => x.Item1 == Unyielding.ability)?.Item2 as Unyielding;
                if (behav != null && behav.indicateStubbornness)
                {
                    behav.indicateStubbornness = false;
                    ability.Card.Anim.StrongNegationEffect();
                }
            }
            
            if (slotToReturnTo != null)
            {
                ability.Card.SetIsOpponentCard(!slotToReturnTo.IsPlayerSlot);
                yield return BoardManager.Instance.AssignCardToSlot(ability.Card, slotToReturnTo, resolveTriggers: false);
            }

            yield return new WaitForSeconds(0.4f);
            yield return ability.LearnAbility();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Strafe), nameof(Strafe.OnTurnEnd))]
        private static IEnumerator PreventStrafeActivation(IEnumerator enumerator, Strafe __instance)
        {
            if (__instance.Card.HasAbility(Unyielding.ability))
            {
                yield return OnPreventMovement(__instance);
                yield break;
            }
            yield return enumerator;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(WhackAMole), nameof(WhackAMole.OnSlotTargetedForAttack))]
        private static IEnumerator PreventMoleWhacking(IEnumerator enumerator, WhackAMole __instance)
        {
            if (__instance.Card.HasAbility(Unyielding.ability))
            {
                yield return OnPreventMovement(__instance);
                yield break;
            }
            yield return enumerator;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(BoardManager), nameof(BoardManager.AssignCardToSlot))]
        private static IEnumerator PreventNewAssignments(IEnumerator enumerator, PlayableCard card, CardSlot slot)
        {
            if (card.HasAbility(Unyielding.ability))
            {
                Unyielding behav = card.TriggerHandler.triggeredAbilities.Find(x => x.Item1 == Unyielding.ability).Item2 as Unyielding;
                if (behav?.homeSlot != null && slot != behav.homeSlot) // if the card has already resolved and is being assigned to a different slot
                {
                    yield return OnPreventMovement(behav, behav.homeSlot);
                    yield break;
                }
            }
            yield return enumerator;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(FishHookItem), nameof(FishHookItem.GetValidTargets))]
        private static void PreventOpponentHooking(List<CardSlot> __result)
        {
            __result.RemoveAll(x => x.Card.HasAbility(Unyielding.ability));
        }
        [HarmonyPostfix, HarmonyPatch(typeof(FishHookGrab), nameof(FishHookGrab.PullHook))]
        private static IEnumerator PreventAnglerHooking(IEnumerator result, FishHookGrab __instance)
        {
            if (__instance.hookTargetSlot?.Card != null && __instance.hookTargetSlot.Card.HasAbility(Unyielding.ability))
                yield break;

            yield return result;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(Strafe), nameof(Strafe.MoveToSlot))]
        private static bool PreventMovingToSlot(CardSlot destination, ref bool destinationValid)
        {
            if (destinationValid && destination?.Card != null && destination.Card.HasAbility(Unyielding.ability))
                destinationValid = false;

            return true;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(StrafePush), nameof(StrafePush.SlotHasSpace))]
        private static void PreventShoving(CardSlot slot, ref bool __result)
        {
            if (__result && slot?.Card != null && slot.Card.HasAbility(Unyielding.ability))
                __result = false;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(GuardDog), nameof(GuardDog.RespondsToOtherCardResolve))]
        private static bool PreventGuardDogging(GuardDog __instance, ref bool __result)
        {
            if (__instance.Card.HasAbility(Unyielding.ability))
            {
                __result = false;
                return false;
            }
            return true;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(MoveBeside), nameof(MoveBeside.RespondsToOtherCardResolve))]
        private static bool PreventClingyBehaviour(MoveBeside __instance, ref bool __result)
        {
            if (__instance.Card.HasAbility(Unyielding.ability))
            {
                __result = false;
                return false;
            }
            return true;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(TailOnHit), nameof(TailOnHit.RespondsToCardGettingAttacked))]
        private static bool PreventGuardDogging(TailOnHit __instance, ref bool __result)
        {
            if (__instance.Card.HasAbility(Unyielding.ability))
            {
                __result = false;
                return false;
            }
            return true;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(StrafeSwap), nameof(StrafeSwap.DoStrafe))]
        private static bool PreventGrabnabbing(ref CardSlot toLeft, ref CardSlot toRight)
        {
            if (toLeft?.Card != null && toLeft.Card.HasAbility(Unyielding.ability))
            {
                toLeft.Card.Anim.StrongNegationEffect();
                toLeft = null;
            }

            if (toRight?.Card != null && toRight.Card.HasAbility(Unyielding.ability))
            {
                toRight.Card.Anim.StrongNegationEffect();
                toRight = null;
            }

            if (toLeft == toRight)
                return false;

            return true;
        }
    }
}
