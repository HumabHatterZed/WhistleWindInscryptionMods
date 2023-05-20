using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;

// Patches to make abilities function properly
namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch(typeof(PlayableCard))]
    internal class PlayableCardPatches
    {
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.TakeDamage))]
        private static void ModifyTakenDamage(ref PlayableCard __instance, ref int damage, PlayableCard attacker)
        {
            bool attackerHasPiercing = attacker != null && attacker.HasAbility(Piercing.ability);

            damage += __instance.Info.GetExtendedPropertyAsInt("wstl:Prudence") ?? 0;

            if (!attackerHasPiercing)
            {
                damage -= __instance.GetAbilityStacks(ThickSkin.ability);
                damage -= __instance.Slot.GetAdjacentCards().FindAll(x => x.HasAbility(Protector.ability)).Count;
            }
            else if (__instance.HasShield())
            {
                __instance.Status.lostShield = true;
                __instance.Anim.StrongNegationEffect();
                if (__instance.Info.name == "MudTurtle")
                    __instance.SwitchToAlternatePortrait();

                __instance.UpdateFaceUpOnBoardEffects();
            }

            if (attacker != null)
            {
                if (attacker.HasAbility(OneSided.ability) && CheckValidOneSided(attacker, __instance))
                    damage += attacker.GetAbilityStacks(OneSided.ability);
            }

            if (damage < 0)
                damage = 0;
        }

        private static bool CheckValidOneSided(PlayableCard attacker, PlayableCard target)
        {
            // if target has no Power, if this card can submerge or is facedown (cannot be hit), return true by default
            if (target.Attack == 0 || attacker.HasAnyOfAbilities(Ability.Submerge, Ability.SubmergeSquid) || attacker.FaceDown)
                return true;

            // if this card doesn't have Sniper or Marksman (will attack opposing)
            if (attacker.LacksAllAbilities(Ability.Sniper, Marksman.ability))
            {
                // if this card has Bi or Tri Strike, check whether the opponent has it too
                if (attacker.HasAbility(Ability.SplitStrike) || attacker.HasTriStrike())
                    return !(target.HasAbility(Ability.SplitStrike) || target.HasTriStrike());

                // otherwise, return whether the opponent can attack this card (won't attack directly or is blocked)
                return target.CanAttackDirectly(attacker.Slot) || target.AttackIsBlocked(attacker.Slot);
            }
            // if the target is opposing this card
            if (target.Slot == attacker.Slot.opposingSlot)
                return target.CanAttackDirectly(attacker.Slot) || target.AttackIsBlocked(attacker.Slot);

            // if the target is in an opposing adjacent slot
            if (Singleton<BoardManager>.Instance.GetAdjacentSlots(attacker.Slot.opposingSlot).Contains(target.Slot))
                return target.LacksAbility(Ability.SplitStrike) || !target.HasTriStrike();

            // otherwise return true
            return true;
        }

        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.AttackIsBlocked))]
        private static void PiercingNegatesRepulsive(PlayableCard __instance, CardSlot opposingSlot, ref bool __result)
        {
            if (!__result)
                return;

            if (__instance.HasAbility(Piercing.ability))
            {
                if (opposingSlot.Card != null && opposingSlot.Card.HasAbility(Ability.PreventAttack))
                {
                    if (__instance.LacksAbility(Ability.Flying) || opposingSlot.Card.HasAbility(Ability.Reach))
                        __result = false;
                }
            }
        }

        #region Neutered patches
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.Attack), MethodType.Getter)]
        private static void ModifyAttackStat(PlayableCard __instance, ref int __result)
        {
            if (!__instance.OnBoard || __instance.LacksAbility(Neutered.ability))
                return;

            if (__instance.Info.Attack + __instance.GetPassiveAttackBuffs() > 0)
                __instance.RenderInfo.attackTextColor = GameColors.Instance.darkBlue;

            __result = 0;
        }

        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.OnStatsChanged))]
        private static void NeuteredColourChange(PlayableCard __instance)
        {
            if (__instance.HasAbility(Neutered.ability))
                __instance.RenderInfo.attackTextColor = GameColors.Instance.darkBlue;
        }
        #endregion
    }

    #region Sporogenic patch
    [HarmonyPatch(typeof(GlobalTriggerHandler))]
    internal class GlobalTriggerHandlerPatches
    {
        // Triggers card with Fungal Infector before other cards
        [HarmonyPostfix, HarmonyPatch(nameof(GlobalTriggerHandler.TriggerCardsOnBoard))]
        private static IEnumerator TriggerSporogenicFirst(IEnumerator enumerator, GlobalTriggerHandler __instance, Trigger trigger, bool triggerFacedown, params object[] otherArgs)
        {
            if (trigger == Trigger.TurnEnd)
            {
                List<PlayableCard> list = Singleton<BoardManager>.Instance.CardsOnBoard;

                if (list.Exists(x => x.HasAbility(Sporogenic.ability)))
                {
                    yield return __instance.TriggerNonCardReceivers(beforeCards: true, trigger, otherArgs);

                    // Trigger Sporogenic cards
                    foreach (PlayableCard item in list.Where(x => x.HasAbility(Sporogenic.ability)))
                    {
                        if ((!item.FaceDown || triggerFacedown) && item.TriggerHandler.RespondsToTrigger(trigger, otherArgs))
                            yield return item.TriggerHandler.OnTrigger(trigger, otherArgs);
                    }

                    // Trigger remaining cards
                    foreach (PlayableCard item in list.Where(x => x.LacksAbility(Sporogenic.ability)))
                    {
                        if ((!item.FaceDown || triggerFacedown) && item.TriggerHandler.RespondsToTrigger(trigger, otherArgs))
                            yield return item.TriggerHandler.OnTrigger(trigger, otherArgs);
                    }

                    yield return __instance.TriggerNonCardReceivers(beforeCards: false, trigger, otherArgs);
                    yield break;
                }
            }
            yield return enumerator;
        }
    }
    #endregion

    #region ImmuneToInstaDeath patch
    [HarmonyPatch(typeof(Deathtouch))]
    internal class DeathtouchPatch
    {
        // Adds 
        [HarmonyPostfix, HarmonyPatch(nameof(Deathtouch.RespondsToDealDamage))]
        private static void ImmunetoDeathTouch(ref bool __result, int amount, PlayableCard target)
        {
            if (amount > 0 && target != null && !target.Dead)
                __result &= target.LacksTrait(AbnormalPlugin.ImmuneToInstaDeath);
        }
    }
    #endregion
}
