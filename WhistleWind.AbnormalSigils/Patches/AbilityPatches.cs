using DiskCardGame;
using GBC;
using HarmonyLib;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;

// Patches to make abilities function properly
namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch(typeof(PlayableCard))]
    internal class PlayableCardAbilityPatches
    {
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.TakeDamage))]
        private static IEnumerator PiercingIgnoresShields(IEnumerator enumerator, PlayableCard __instance, int damage, PlayableCard attacker)
        {
            bool shield = __instance.HasShield();
            yield return enumerator;
            // Deal damage after breaking a shield
            // This will only run if damage > 0 due to API patches so no need to check that
            if (shield && __instance != null && !__instance.Dead && attacker != null && attacker.HasAbility(Piercing.ability))
            {
                if (damage <= 0)
                    yield break;

                yield return PiercingDamagesThroughShields(__instance, damage, attacker);
            }
        }

        private static IEnumerator PiercingDamagesThroughShields(PlayableCard target, int damage, PlayableCard attacker)
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

        #region Neutered ability
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.Attack), MethodType.Getter)]
        private static void ModifyAttackStat(PlayableCard __instance, ref int __result)
        {
            if (__instance.LacksAbility(Neutered.ability))
                return;

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

    [HarmonyPatch]
    internal class OtherAbilityPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(ExplodeOnDeath), nameof(ExplodeOnDeath.BombCard))]
        private static IEnumerator Act1Detonator(IEnumerator result, PlayableCard target, PlayableCard attacker)
        {
            if (!SaveManager.SaveFile.IsPart1)
            {
                yield return new WaitForSeconds(0.25f);
                yield return target.TakeDamage(10, attacker);
            }
            else
            {
                yield return result;
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(Opponent), nameof(Opponent.QueuedCardIsBlocked))]
        private static void DontPlayLonelyIfHasFriend(ref bool __result, PlayableCard queuedCard)
        {
            if (queuedCard != null && queuedCard.HasAbility(Lonely.ability) && queuedCard.GetComponent<Lonely>().HasFriend)
                __result = true;
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

        #region SigilPower
        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), "OnCursorEnter")]
        private static void ShowStatsPlayableCards(PlayableCard __instance) => UpdatePlayableStatsSpellDisplay(__instance, true);

        [HarmonyPostfix, HarmonyPatch(typeof(PixelPlayableCard), "OnCursorEnter")]
        private static void ShowStatsPixelPlayableCards(PixelPlayableCard __instance) => UpdatePlayableStatsSpellDisplay(__instance, true);

        [HarmonyPostfix, HarmonyPatch(typeof(PixelPlayableCard), "OnCursorExit")]
        private static void HideStatsPixelPlayableCards(PixelPlayableCard __instance) => UpdatePlayableStatsSpellDisplay(__instance, false);

        [HarmonyPostfix, HarmonyPatch(typeof(MainInputInteractable), "OnCursorExit")]
        private static void ShowStatsSelectableCards(MainInputInteractable __instance)
        {
            if (__instance is PlayableCard)
            {
                PlayableCard playableCard = __instance as PlayableCard;
                UpdatePlayableStatsSpellDisplay(playableCard, false);
            }
        }

        internal static void UpdatePlayableStatsSpellDisplay(PlayableCard card, bool showStats)
        {
            if (!card.InHand || card.Info.SpecialStatIcon != SigilPower.Icon)
                return;

            card.RenderInfo.showSpecialStats = showStats;
            if (showStats)
            {
                card.RenderInfo.attack = card.Info.Attack;
                card.RenderInfo.health = card.Info.Health;
            }

            card.RenderInfo.attackTextColor = (card.GetPassiveAttackBuffs() + card.GetStatIconAttackBuffs() != 0) ? GameColors.Instance.darkBlue : Color.black;
            card.RenderInfo.healthTextColor = (card.GetPassiveHealthBuffs() + card.GetStatIconHealthBuffs() != 0) ? GameColors.Instance.darkBlue : Color.black;
            card.RenderCard();
        }
        #endregion
    }
}
