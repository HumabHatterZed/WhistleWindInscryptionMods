using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WhistleWindLobotomyMod
{
    [HarmonyPatch(typeof(Deathtouch))]
    public static class DeathtouchPatch
    {
        // Makes WhiteNight, its Apostles, and Hundreds of Good Deeds immune to Touch of Death
        // Effectively gives them Made of Stone but without the whole 'they're not made of stone' thing
        [HarmonyPrefix, HarmonyPatch(nameof(Deathtouch.RespondsToDealDamage))]
        public static bool ImmunetoDeathTouch(ref int amount, ref PlayableCard target)
        {
            bool whiteNightEvent = target.HasAbility(TrueSaviour.ability) || target.HasAbility(Confession.ability);
            if (amount > 0 && target != null && !target.Dead)
            {
                return !whiteNightEvent && !target.HasAbility(Ability.MadeOfStone);
            }
            return false;
        }
    }
    [HarmonyPatch(typeof(PlayableCard))]
    public static class PlayableCardPatches
    {
        // Increases damage taken by amount of Prudence a card has
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.TakeDamage))]
        public static void TakePrudenceDamage(PlayableCard __instance, ref int damage)
        {
            int prudence = !(__instance.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)__instance.Info.GetExtendedPropertyAsInt("wstl:Prudence");
            if (prudence > 0)
            {
                damage += prudence;
            }
        }
        // Fixes All Strike to attack the opposing space instead of just slot[0] for non-giant player cards
        // Also re-adds Moon Strike functionality for Blue Star cards
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.GetOpposingSlots))]
        private static List<CardSlot> BlueStarAbility(List<CardSlot> list, PlayableCard __instance)
        {
            // Blue Star gets old Omni Strike functionality
            // Make sure we have AllStrike still (haven't lost it)
            if (__instance.Info.name.StartsWith("wstl_blueStar") && __instance.HasAbility(Ability.AllStrike))
            {
                ProgressionData.SetAbilityLearned(Ability.AllStrike);
                List<CardSlot> list2 = __instance.OpponentCard ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;

                // if there's an attackable card, return original list
                if (list2.Exists((x) => x.Card != null && !__instance.CanAttackDirectly(x)))
                    return list;

                // otherwise return the entire opposing side
                list2.Sort((a, b) => a.Index - b.Index);
                return list2;
            }
            return list;
        }
    }

    // Triggers card with Fungal Infector before other cards
    [HarmonyPatch(typeof(GlobalTriggerHandler))]
    public static class GlobalTriggerHandlerPatches
    {
        [HarmonyPostfix, HarmonyPatch(nameof(GlobalTriggerHandler.TriggerCardsOnBoard))]
        public static IEnumerator ChangeSporePriority(IEnumerator enumerator, GlobalTriggerHandler __instance, Trigger trigger, bool triggerFacedown, params object[] otherArgs)
        {
            if (trigger == Trigger.TurnEnd)
            {
                List<PlayableCard> list = new List<PlayableCard>(Singleton<BoardManager>.Instance.CardsOnBoard);
                if (list.Where(item => item.HasAbility(Spores.ability)).Count() > 0)
                {
                    WstlPlugin.Log.LogDebug("Triggering Fungal Infector before other cards.");
                    yield return __instance.TriggerNonCardReceivers(beforeCards: true, trigger, otherArgs);

                    // Trigger remaining Spore cards
                    foreach (PlayableCard item in list)
                    {
                        if (item != null && item.HasAbility(Spores.ability) && (!item.FaceDown || triggerFacedown) && item.TriggerHandler.RespondsToTrigger(trigger, otherArgs))
                        {
                            yield return item.TriggerHandler.OnTrigger(trigger, otherArgs);
                        }
                    }

                    // Trigger remaining cards
                    foreach (PlayableCard item in list)
                    {
                        if (item != null && item.LacksAbility(Spores.ability) && (!item.FaceDown || triggerFacedown) && item.TriggerHandler.RespondsToTrigger(trigger, otherArgs))
                        {
                            yield return item.TriggerHandler.OnTrigger(trigger, otherArgs);
                        }
                    }

                    yield return __instance.TriggerNonCardReceivers(beforeCards: false, trigger, otherArgs);
                    yield break;
                }
            }
            yield return enumerator;
        }
    }
}
