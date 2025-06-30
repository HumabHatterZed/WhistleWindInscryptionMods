using DiskCardGame;
using GBC;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;

// Patches to make abilities function properly
namespace WhistleWind.AbnormalSigils.Patches {
    [HarmonyPatch]
    internal class AbilityPatches {
        [HarmonyPostfix, HarmonyPatch(typeof(Opponent), nameof(Opponent.QueuedCardIsBlocked))]
        private static void DontPlayLonelyIfHasFriend(ref bool __result, PlayableCard queuedCard) {
            if (queuedCard != null && queuedCard.HasAbility(Lonely.ability) && queuedCard.GetComponent<Lonely>().HasFriend)
                __result = true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Deathtouch), nameof(Deathtouch.RespondsToDealDamage))]
        private static void DeathTouchImmunetoInstaDeath(ref bool __result, int amount, PlayableCard target) {
            if (__result && target.HasTrait(AbnormalPlugin.ImmuneToInstaDeath))
                __result = false;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(AbilityBehaviour), nameof(AbilityBehaviour.GetNonDefaultModsFromSelf))]
        private static void DeathPenaltyNonInheritable(ref List<CardModificationInfo> __result) {
            if (__result.Count == 0 || __result[0].abilities.Count == 0) {
                return;
            }

            __result[0].abilities.Remove(DeathPenalty.ability);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ExplodeOnDeath), nameof(ExplodeOnDeath.BombCard))]
        private static IEnumerator Act1Detonator(IEnumerator result, PlayableCard target, PlayableCard attacker) {
            if (SaveManager.SaveFile.IsPart1) {
                yield return new WaitForSeconds(0.25f);
                yield return target.TakeDamage(10, attacker);
            }
            else {
                yield return result;
            }
        }

        [HarmonyPriority(Priority.Last)]
        [HarmonyPostfix, HarmonyPatch(typeof(CardInfo), nameof(CardInfo.Attack), MethodType.Getter)]
        private static void MindStrikeModifyAttackStat(CardInfo __instance, ref int __result) {
            if (__instance.HasAbility(MindStrike.ability) && __result > 0)
                __result = 1;
        }

        //private static Type type = AccessTools.TypeByName("DiskCardGame.GlobalTriggerHandler+<TriggerCardsOnBoard>d__16");
        //// Triggers card with Fungal Infector before other cards
        //[HarmonyPostfix, HarmonyPatch(typeof(GlobalTriggerHandler), nameof(GlobalTriggerHandler.TriggerCardsOnBoard))]
        //private static IEnumerator TriggerSporogenicFirst(IEnumerator enumerator, GlobalTriggerHandler __instance, Trigger trigger, bool triggerFacedown, params object[] otherArgs) {
        //    if (trigger != Trigger.TurnEnd) {
        //        yield return enumerator;
        //        yield break;
        //    }
        //    List<PlayableCard> list = Singleton<BoardManager>.Instance.CardsOnBoard;
        //    List<PlayableCard> spore = list.FindAll(x => x.HasAbility(Sporogenic.ability));

        //    yield return __instance.TriggerNonCardReceivers(beforeCards: true, trigger, otherArgs);

        //    // Trigger Sporogenic cards
        //    foreach (PlayableCard item in spore) {
        //        if ((!item.FaceDown || triggerFacedown) && item.TriggerHandler.RespondsToTrigger(trigger, otherArgs))
        //            yield return item.TriggerHandler.OnTrigger(trigger, otherArgs);
        //    }

        //    // Trigger remaining cards
        //    foreach (PlayableCard item in list.Where(x => x.LacksAbility(Sporogenic.ability))) {
        //        if ((!item.FaceDown || triggerFacedown) && item.TriggerHandler.RespondsToTrigger(trigger, otherArgs))
        //            yield return item.TriggerHandler.OnTrigger(trigger, otherArgs);
        //    }

        //    yield return __instance.TriggerNonCardReceivers(beforeCards: false, trigger, otherArgs);
        //}

        //[HarmonyPatch(typeof(PlayableCard))]
        //internal class PlayableCardPatches {
        //    [HarmonyPriority(Priority.Last)]
        //    [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.Attack), MethodType.Getter)]
        //    private static void NeuteredModifyAttackStat(PlayableCard __instance, ref int __result) {
        //        if (__instance.HasAbility(Neutered.ability))
        //            __result = 0;
        //    }

        //    [HarmonyPriority(Priority.Last)]
        //    [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.OnStatsChanged))]
        //    private static void NeuteredColourChange(PlayableCard __instance) {
        //        if (__instance.HasAbility(Neutered.ability))
        //            __instance.RenderInfo.attackTextColor = GameColors.Instance.darkBlue;
        //    }
        //}

        [HarmonyPatch]
        internal class SigilPowerPatches {
            [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), "OnCursorEnter")]
            private static void ShowStatsPlayableCards(PlayableCard __instance) => UpdatePlayableStatsSpellDisplay(__instance, true);

            [HarmonyPostfix, HarmonyPatch(typeof(PixelPlayableCard), "OnCursorEnter")]
            private static void ShowStatsPixelPlayableCards(PixelPlayableCard __instance) => UpdatePlayableStatsSpellDisplay(__instance, true);

            [HarmonyPostfix, HarmonyPatch(typeof(PixelPlayableCard), "OnCursorExit")]
            private static void HideStatsPixelPlayableCards(PixelPlayableCard __instance) => UpdatePlayableStatsSpellDisplay(__instance, false);

            [HarmonyPostfix, HarmonyPatch(typeof(MainInputInteractable), "OnCursorExit")]
            private static void ShowStatsSelectableCards(MainInputInteractable __instance) {
                if (__instance is PlayableCard) {
                    PlayableCard playableCard = __instance as PlayableCard;
                    UpdatePlayableStatsSpellDisplay(playableCard, false);
                }
            }

            internal static void UpdatePlayableStatsSpellDisplay(PlayableCard card, bool showStats) {
                if (!card.InHand || card.Info.SpecialStatIcon != SigilPower.Icon)
                    return;

                card.RenderInfo.showSpecialStats = showStats;
                if (showStats) {
                    card.RenderInfo.attack = card.Info.Attack;
                    card.RenderInfo.health = card.Info.Health;
                }

                card.RenderInfo.attackTextColor = (card.GetPassiveAttackBuffs() + card.GetStatIconAttackBuffs() != 0) ? GameColors.Instance.darkBlue : Color.black;
                card.RenderInfo.healthTextColor = (card.GetPassiveHealthBuffs() + card.GetStatIconHealthBuffs() != 0) ? GameColors.Instance.darkBlue : Color.black;
                card.RenderCard();
            }
        }
    }
}
