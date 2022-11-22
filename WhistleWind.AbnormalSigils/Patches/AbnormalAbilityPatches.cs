using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

// Patches to make abilities function properly
namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch(typeof(PlayableCard))]
    internal class PlayableCardPatches
    {
        // Adds in logic for Piercing, Prudence, Protector, Thick Skin
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.TakeDamage))]
        private static void ModifyTakenDamage(PlayableCard __instance, ref int damage, PlayableCard attacker)
        {
            // if attacker doesn't have Piercing
            if (attacker != null && attacker.HasAbility(Piercing.ability))
            {
                if (__instance.HasShield())
                {
                    __instance.Status.lostShield = true;
                    __instance.Anim.StrongNegationEffect();
                    if (__instance.Info.name == "MudTurtle")
                        __instance.SwitchToAlternatePortrait();

                    __instance.UpdateFaceUpOnBoardEffects();
                }
            }
            else
            {
                // Count number of adjacent cards with Protector
                int protector = __instance.Slot.GetAdjacentCards().FindAll(x => x.HasAbility(Protector.ability)).Count;

                // Count number of stacks of Thick Skin
                int skin = __instance.GetAbilityStacks(ThickSkin.ability);

                // Count amount of Prudence this card has
                int prudence = __instance.Info.GetExtendedPropertyAsInt("wstl:Prudence") ?? 0;

                // Set damage equal to new value or to 0 if the new value is negative
                damage = Mathf.Max(damage + prudence - (protector + skin), 0);
            }
        }

        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.GetOpposingSlots))]
        private static List<CardSlot> BlueStarAbility(List<CardSlot> list, PlayableCard __instance)
        {
            // Blue Star gets old Omni Strike functionality
            // Make sure we have AllStrike still (haven't lost it)
            if (__instance.Info.name.StartsWith("wstl_blueStar") && __instance.HasAbility(Ability.AllStrike))
            {
                ProgressionData.SetAbilityLearned(Ability.AllStrike);
                List<CardSlot> list2 = AbnormalMethods.GetSlotsCopy(!__instance.OpponentCard);

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

    [HarmonyPatch(typeof(GlobalTriggerHandler))]
    internal class GlobalTriggerHandlerPatches
    {
        // Triggers card with Fungal Infector before other cards
        [HarmonyPostfix, HarmonyPatch(nameof(GlobalTriggerHandler.TriggerCardsOnBoard))]
        private static IEnumerator TriggerFungalInfectorFirst(IEnumerator enumerator, GlobalTriggerHandler __instance, Trigger trigger, bool triggerFacedown, params object[] otherArgs)
        {
            if (trigger == Trigger.TurnEnd)
            {
                List<PlayableCard> list = new List<PlayableCard>(Singleton<BoardManager>.Instance.CardsOnBoard);
                if (list.Exists(item => item.HasAbility(Spores.ability)))
                {
                    AbnormalPlugin.Log.LogDebug("Triggering Fungal Infector before other cards.");
                    yield return __instance.TriggerNonCardReceivers(beforeCards: true, trigger, otherArgs);

                    // Trigger remaining Spore cards
                    foreach (PlayableCard item in list)
                    {
                        if (item != null && item.HasAbility(Spores.ability) && (!item.FaceDown || triggerFacedown) && item.TriggerHandler.RespondsToTrigger(trigger, otherArgs))
                            yield return item.TriggerHandler.OnTrigger(trigger, otherArgs);
                    }

                    // Trigger remaining cards
                    foreach (PlayableCard item in list)
                    {
                        if (item != null && item.LacksAbility(Spores.ability) && (!item.FaceDown || triggerFacedown) && item.TriggerHandler.RespondsToTrigger(trigger, otherArgs))
                            yield return item.TriggerHandler.OnTrigger(trigger, otherArgs);
                    }

                    yield return __instance.TriggerNonCardReceivers(beforeCards: false, trigger, otherArgs);
                    yield break;
                }
            }
            yield return enumerator;
        }
    }
    [HarmonyPatch(typeof(Deathtouch))]
    internal class DeathtouchPatch
    {
        // Makes WhiteNight and Hundred Deeds immune to Deathtouch
        [HarmonyPrefix, HarmonyPatch(nameof(Deathtouch.RespondsToDealDamage))]
        private static bool ImmunetoDeathTouch(ref int amount, ref PlayableCard target)
        {
            if (amount > 0 && target != null && !target.Dead)
                return !target.HasAbility(Ability.MadeOfStone) && !target.HasSpecialAbility(ImmuneToInstaDeath.specialAbility);

            return false;
        }
    }
}
