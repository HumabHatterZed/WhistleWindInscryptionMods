using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Spells.Patchers;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Patches to make abilities function properly
namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch(typeof(PlayableCard))]
    internal class PlayableCardAbilityPatches
    {
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.TakeDamage))]
        private static IEnumerator IgnoreShield(IEnumerator enumerator, PlayableCard __instance, int damage, PlayableCard attacker)
        {
            bool shield = __instance.HasShield();
            yield return enumerator;
            // Deal damage after breaking a shield
            // This will only run if damage > 0 due to API patches so no need to check that
            if (shield && __instance != null && !__instance.Dead && attacker != null && attacker.HasAbility(Piercing.ability))
            {
                if (damage <= 0)
                    yield break;

                yield return PiercingDamage(__instance, damage, attacker);
            }
        }

        private static IEnumerator PiercingDamage(PlayableCard target, int damage, PlayableCard attacker)
        {
            // recreate the damage logic since BreakShield breaks out of the method at the end
            target.Status.damageTaken += damage;
            target.UpdateStatsText();
            if (target.Health > 0)
                target.Anim.PlayHitAnimation();

            if (target.TriggerHandler.RespondsToTrigger(Trigger.TakeDamage, attacker))
                yield return target.TriggerHandler.OnTrigger(Trigger.TakeDamage, attacker);

            if (target.Health <= 0)
                yield return target.Die(wasSacrifice: false, attacker);

            if (attacker.TriggerHandler.RespondsToTrigger(Trigger.DealDamage, damage, target))
                yield return attacker.TriggerHandler.OnTrigger(Trigger.DealDamage, damage, target);

            yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(
                Trigger.OtherCardDealtDamage, false, attacker, attacker.Attack, target);

            yield return CustomTriggerFinder.TriggerInHand<IOnOtherCardDealtDamageInHand>(
            x => x.RespondsToOtherCardDealtDamageInHand(attacker, attacker.Attack, target),
                x => x.OnOtherCardDealtDamageInHand(attacker, attacker.Attack, target));
        }

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
    }

    [HarmonyPatch]
    internal class OtherAbilityPatches
    {
        [HarmonyPatch(typeof(VariableStatBehaviour), nameof(VariableStatBehaviour.UpdateStats))]
        [HarmonyPrefix]
        public static bool ShowStatsWhenInHand(ref VariableStatBehaviour __instance)
        {
            // if a spell that displays stats, show when in hand or on board
            if (__instance is SigilPower && __instance.PlayableCard != null && __instance.PlayableCard.InHand)
            {
                int[] array = __instance.GetStatValues();
                __instance.statsMod.attackAdjustment = array[0];
                __instance.statsMod.healthAdjustment = array[1];
                __instance.PlayableCard.RenderInfo.showSpecialStats = true;
                if (!__instance.StatValuesEqual(__instance.prevStatValues, array) || __instance.prevOnBoard != __instance.PlayableCard.InHand)
                {
                    UpdateVariableStatsText(__instance.PlayableCard);
                }
                __instance.prevStatValues = array;
                __instance.prevOnBoard = __instance.PlayableCard.InHand;
                return false;
            }
            return true;
        }

        private static void UpdateVariableStatsText(PlayableCard card)
        {
            card.RenderInfo.attack = card.Attack;
            card.RenderInfo.health = card.Health;
            card.RenderInfo.attackTextColor = (card.GetPassiveAttackBuffs() + card.GetStatIconAttackBuffs() != 0) ? GameColors.Instance.darkBlue : Color.black;
            card.RenderCard();
        }
        [HarmonyPostfix, HarmonyPatch(typeof(Deathtouch), nameof(Deathtouch.RespondsToDealDamage))]
        private static void ImmunetoDeathTouch(ref bool __result, int amount, PlayableCard target)
        {
            if (amount > 0 && target != null && !target.Dead && target.HasTrait(AbnormalPlugin.ImmuneToInstaDeath))
                __result = false;
        }

        // Triggers card with Fungal Infector before other cards
        [HarmonyPostfix, HarmonyPatch(typeof(GlobalTriggerHandler), nameof(GlobalTriggerHandler.TriggerCardsOnBoard))]
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
}
