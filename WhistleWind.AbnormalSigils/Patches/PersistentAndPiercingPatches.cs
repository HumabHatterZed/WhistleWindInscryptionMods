﻿using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionAPI.Helpers.Extensions;
using InscryptionCommunityPatch.Card;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

// Patches to make abilities function properly
namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch]
    internal class PersistentAndPiercingPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.CanAttackDirectly))]
        private static void AllowAttackingSubmerged(PlayableCard __instance, CardSlot opposingSlot, ref bool __result)
        {
            if (!__result || __instance.LacksAbility(Persistent.ability))
                return;

            if (opposingSlot.Card != null && opposingSlot.Card.FaceDown)
                __result = false;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.AttackIsBlocked))]
        private static void NotAffectedByRepulsive(PlayableCard __instance, CardSlot opposingSlot, ref bool __result)
        {
            if (!__result || __instance.LacksAbility(Persistent.ability))
                return;

            if (opposingSlot.Card != null && opposingSlot.Card.HasAbility(Ability.PreventAttack))
            {
                if (__instance.LacksAbility(Ability.Flying) || opposingSlot.Card.HasAbility(Ability.Reach))
                    __result = false;
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(CombatPhaseManager), nameof(CombatPhaseManager.SlotAttackSlot))]
        private static bool GetPersistentTarget(CombatPhaseManager __instance, CardSlot attackingSlot, CardSlot opposingSlot, float waitAfter, ref IEnumerator __result)
        {
            // both opposing and attacker cards must exist
            if (attackingSlot.Card != null && attackingSlot.Card.HasAnyOfAbilities(Piercing.ability, Persistent.ability))
            {
                __result = PersistentSlotAttackSlot(__instance, attackingSlot, opposingSlot, waitAfter);
                return false;
            }
            return true;
        }

        private static IEnumerator ForceTargetFaceUp(CardSlot opposingSlot, CardSlot attacker)
        {
            yield return opposingSlot.Card.FlipFaceDown(false, 0.45f);
            yield return attacker.Card.GetComponent<Persistent>().PreSuccessfulTriggerSequence();
        }

        private static IEnumerator UpdateSniperIcons(CombatPhaseManager instance, CardSlot attacker, CardSlot previousSlot, CardSlot newSlot)
        {
            Part1SniperVisualizer visualizer = null;
            if (SaveManager.SaveFile.IsPart1)
                visualizer = instance.GetComponent<Part1SniperVisualizer>() ?? instance.gameObject.AddComponent<Part1SniperVisualizer>();

            GameObject previous = visualizer?.sniperIcons.Find(x => x.gameObject.transform.parent == previousSlot.transform);
            if (previous != null)
            {
                Tween.LocalScale(previous.transform, Vector3.zero, 0.1f, 0f, Tween.EaseIn, Tween.LoopType.None, null, delegate ()
                {
                    UnityEngine.Object.Destroy(previous);
                }, true);

                yield return new WaitForSeconds(0.05f);

                instance.VisualizeAimSniperAbility(attacker, newSlot);
                visualizer?.VisualizeAimSniperAbility(attacker, newSlot);
                instance.VisualizeConfirmSniperAbility(newSlot);
                visualizer?.VisualizeConfirmSniperAbility(newSlot);

                yield return new WaitForSeconds(0.15f);
            }
        }
        private static IEnumerator PersistentSlotAttackSlot(CombatPhaseManager instance, CardSlot attacker, CardSlot opposing, float waitAfter = 0f)
        {
            CardSlot opposingSlot = opposing;
            CardSlot attackingSlot = attacker;
            PlayableCard persistentTarget = opposingSlot.Card;
            bool piercing = attackingSlot.Card.HasAbility(Piercing.ability);
            bool persistent = attackingSlot.Card.HasAbility(Persistent.ability);

            bool forcedFaceUp = persistentTarget?.FaceDown ?? false;
            bool targetSwitched = false;

            yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.SlotTargetedForAttack, false, opposingSlot, attackingSlot.Card);
            yield return new WaitForSeconds(0.025f);

            if (attackingSlot.Card == null)
                yield break;

            if (persistent && persistentTarget != null && opposingSlot.Card != persistentTarget)
            {
                yield return attacker.Card.GetComponent<Persistent>().PreSuccessfulTriggerSequence();
                yield return UpdateSniperIcons(instance, attacker, opposingSlot, persistentTarget.Slot);
                opposingSlot = persistentTarget.Slot;
                targetSwitched = true;
            }

            if (attackingSlot.Card.Anim.DoingAttackAnimation)
            {
                yield return new WaitUntil(() => !attackingSlot.Card.Anim.DoingAttackAnimation);
                yield return new WaitForSeconds(0.25f);
            }
            if (opposingSlot.Card != null && attackingSlot.Card.AttackIsBlocked(opposingSlot))
            {
                ProgressionData.SetAbilityLearned(Ability.PreventAttack);
                yield return instance.ShowCardBlocked(attackingSlot.Card);
            }
            else if (attackingSlot.Card.CanAttackDirectly(opposingSlot))
            {
                instance.DamageDealtThisPhase += attackingSlot.Card.Attack;
                yield return instance.VisualizeCardAttackingDirectly(attackingSlot, opposingSlot, attackingSlot.Card.Attack);
                if (attackingSlot.Card.TriggerHandler.RespondsToTrigger(Trigger.DealDamageDirectly, attackingSlot.Card.Attack))
                    yield return attackingSlot.Card.TriggerHandler.OnTrigger(Trigger.DealDamageDirectly, attackingSlot.Card.Attack);

            }
            else
            {
                float heightOffset = (opposingSlot.Card == null) ? 0f : opposingSlot.Card.SlotHeightOffset;
                if (heightOffset > 0f)
                {
                    Tween.Position(attackingSlot.Card.transform, attackingSlot.Card.transform.position + Vector3.up * heightOffset, 0.05f, 0f, Tween.EaseInOut);
                }
                attackingSlot.Card.Anim.PlayAttackAnimation(attackingSlot.Card.IsFlyingAttackingReach(), opposingSlot, null);
                yield return new WaitForSeconds(0.07f);
                attackingSlot.Card.Anim.SetAnimationPaused(paused: true);
                PlayableCard attackingCard = attackingSlot.Card;
                yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.CardGettingAttacked, false, opposingSlot.Card);

                if (persistent && persistentTarget != null && opposingSlot.Card != persistentTarget)
                {
                    yield return attacker.Card.GetComponent<Persistent>().PreSuccessfulTriggerSequence();
                    yield return UpdateSniperIcons(instance, attacker, opposingSlot, persistentTarget.Slot);
                    opposingSlot = persistentTarget.Slot;
                    targetSwitched = true;

                    // update the animation to target the correct slot
                    attackingSlot.Card.Anim.SetAnimationPaused(paused: false);
                    attackingSlot.Card.Anim.PlayAttackAnimation(attackingSlot.Card.IsFlyingAttackingReach(), opposingSlot, null);
                    yield return new WaitForSeconds(0.07f);
                    attackingSlot.Card.Anim.SetAnimationPaused(paused: true);
                }

                if (attackingCard != null && attackingCard.Slot != null)
                {
                    if (opposingSlot.Card == null)
                    {
                        attackingSlot.Card.Anim.SetAnimationPaused(paused: false);
                        yield return new WaitForSeconds(0.25f);
                        yield break;
                    }

                    attackingSlot = attackingCard.Slot;

                    if (persistent && forcedFaceUp)
                        yield return ForceTargetFaceUp(opposingSlot, attackingSlot);
                    
                    if (attackingSlot.Card.IsFlyingAttackingReach())
                    {
                        opposingSlot.Card.Anim.PlayJumpAnimation();
                        yield return new WaitForSeconds(0.3f);
                        attackingSlot.Card.Anim.PlayAttackInAirAnimation();
                    }
                    attackingSlot.Card.Anim.SetAnimationPaused(paused: false);
                    
                    yield return new WaitForSeconds(0.05f);
                    int overkillDamage = attackingSlot.Card.Attack - opposingSlot.Card.Health;
                    if (piercing)
                        overkillDamage = Mathf.Max(1, overkillDamage + 1);

                    int damage = attackingSlot.Card.Attack;
                    if (targetSwitched || opposingSlot.Card.HasAbility(Ability.PreventAttack))
                        damage++;

                    yield return opposingSlot.Card.TakeDamage(damage, attackingSlot.Card);
                    yield return instance.DealOverkillDamage(overkillDamage, attackingSlot, opposingSlot);
                    
                    if (attackingSlot.Card != null && heightOffset > 0f)
                        yield return Singleton<BoardManager>.Instance.AssignCardToSlot(attackingSlot.Card, attackingSlot.Card.Slot, 0.1f, null, resolveTriggers: false);

                }
            }
            yield return new WaitForSeconds(waitAfter);
            if (persistent)
            {
                if (targetSwitched || forcedFaceUp)
                    yield return attacker.Card.GetComponent<Persistent>().LearnAbility(waitAfter);

                yield return opposingSlot.Card?.FlipFaceDown(forcedFaceUp);
            }
        }
    }
}
