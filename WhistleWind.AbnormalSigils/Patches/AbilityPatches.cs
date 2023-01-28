using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
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
        private static void ModifyTakenDamage(ref PlayableCard __instance, ref int damage, PlayableCard attacker)
        {
            int skin = __instance.GetAbilityStacks(ThickSkin.ability);  // Count number of stacks of Thick Skin
            int protector = __instance.Slot.GetAdjacentCards().FindAll(x => x.HasAbility(Protector.ability)).Count; // Count num of adjacent cards with Protector

            int prudence = __instance.Info.GetExtendedPropertyAsInt("wstl:Prudence") ?? 0;  // Count amount of Prudence this card has
            int oneSided = 0;

            if (attacker != null)
            {
                if (attacker.HasAbility(OneSided.ability) && CheckValidOneSided(attacker, __instance))
                    oneSided = 2;

                if (attacker.HasAbility(Piercing.ability))
                {
                    // negate damage reduction
                    skin = 0;
                    protector = 0;

                    // Negate shield
                    if (__instance.HasShield())
                    {
                        __instance.Status.lostShield = true;
                        __instance.Anim.StrongNegationEffect();
                        if (__instance.Info.name == "MudTurtle")
                            __instance.SwitchToAlternatePortrait();

                        __instance.UpdateFaceUpOnBoardEffects();
                    }
                }
            }

            // Set damage equal to new value or to 0 if the new value is negative
            damage = Mathf.Max(damage + prudence + oneSided - (protector + skin), 0);
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
        // Adds 
        [HarmonyPostfix, HarmonyPatch(nameof(Deathtouch.RespondsToDealDamage))]
        private static void ImmunetoDeathTouch(ref bool __result, int amount, PlayableCard target)
        {
            if (amount > 0 && target != null && !target.Dead)
                __result = target.LacksAbility(Ability.MadeOfStone) && target.LacksTrait(AbnormalPlugin.ImmuneToInstaDeath);
        }
    }
}
