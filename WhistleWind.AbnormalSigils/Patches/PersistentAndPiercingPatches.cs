using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionCommunityPatch.Card;
using Pixelplacement;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils.Patches {
    /// <summary>
    /// Patches relating to the behaviour of the Piercing and Persistent sigils.
    /// </summary>
    [HarmonyPatch]
    internal class PersistentAndPiercingPatches {
        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.CanAttackDirectly))]
        private static void CanAttackDirectlyPatch(PlayableCard __instance, CardSlot opposingSlot, ref bool __result) {
            // Ethereal cards always hit directly
            if (__instance.HasAbility(Ethereal.ability)) {
                __result = true;
                return;
            }

            if (opposingSlot.Card != null) {
                // Ethereal cards cannot be hit
                if (opposingSlot.Card.HasAbility(Ethereal.ability)) {
                    __result = true;
                    return;
                }

                // if we can make direct contact with the opposing card
                if (__instance.LacksAbility(Ability.Flying) || opposingSlot.Card.HasAbility(Ability.Reach)) {
                    // piercing can always hit face down cards
                    if (opposingSlot.Card.FaceDown) {
                        if (__instance.HasAbility(Piercing.ability)) {
                            __result = false;
                        }
                    }
                    else if (__instance.HasAbility(Persistent.ability)) {
                        __result = false;
                    }
                }
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.AttackIsBlocked))]
        private static void PersistentIgnoresRepulsive(PlayableCard __instance, CardSlot opposingSlot, ref bool __result) {
            if (__instance.HasAbility(Persistent.ability))
                __result = false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(CombatPhaseManager), nameof(CombatPhaseManager.SlotAttackSlot))]
        private static bool PerformPersistenceAttack(CombatPhaseManager __instance, CardSlot attackingSlot, CardSlot opposingSlot, float waitAfter, ref IEnumerator __result) {
            // both opposing and attacker cards must exist
            if (attackingSlot.Card != null && attackingSlot.Card.HasAbility(Persistent.ability)
                && AbnormalAbilityHelper.SimulatePersistentAttack(attackingSlot.Card, opposingSlot.Card)) {
                __result = PersistentSlotAttackSlot(__instance, attackingSlot, opposingSlot, waitAfter);
                return false;
            }
            return true;
        }

        private static IEnumerator PersistentSlotAttackSlot(CombatPhaseManager instance, CardSlot attacker, CardSlot opposing, float waitAfter = 0f) {
            CardSlot opposingSlot = opposing;
            CardSlot attackingSlot = attacker;
            PlayableCard persistentTarget = opposingSlot.Card;

            bool targetHasMoved = false;

            yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.SlotTargetedForAttack, false, opposingSlot, attackingSlot.Card);
            yield return new WaitForSeconds(0.025f);

            if (attackingSlot.Card == null)
                yield break;

            if (opposingSlot.Card != persistentTarget) {
                targetHasMoved = true;

                yield return attacker.Card.GetComponent<Persistent>().PreSuccessfulTriggerSequence();
                yield return UpdateSniperIcons(instance, attacker, opposingSlot, persistentTarget.Slot);
                opposingSlot = persistentTarget.Slot; // update after updating the sniper icons
            }

            if (attackingSlot.Card.Anim.DoingAttackAnimation) {
                yield return new WaitUntil(() => !attackingSlot.Card.Anim.DoingAttackAnimation);
                yield return new WaitForSeconds(0.25f);
            }
            if (opposingSlot.Card != null && attackingSlot.Card.AttackIsBlocked(opposingSlot)) {
                ProgressionData.SetAbilityLearned(Ability.PreventAttack);
                yield return instance.ShowCardBlocked(attackingSlot.Card);
            }
            else if (attackingSlot.Card.CanAttackDirectly(opposingSlot)) {
                instance.DamageDealtThisPhase += attackingSlot.Card.Attack;
                yield return instance.VisualizeCardAttackingDirectly(attackingSlot, opposingSlot, attackingSlot.Card.Attack);
                if (attackingSlot.Card.TriggerHandler.RespondsToTrigger(Trigger.DealDamageDirectly, attackingSlot.Card.Attack))
                    yield return attackingSlot.Card.TriggerHandler.OnTrigger(Trigger.DealDamageDirectly, attackingSlot.Card.Attack);

            }
            else {
                float heightOffset = (opposingSlot.Card == null) ? 0f : opposingSlot.Card.SlotHeightOffset;
                if (heightOffset > 0f) {
                    Tween.Position(attackingSlot.Card.transform, attackingSlot.Card.transform.position + Vector3.up * heightOffset, 0.05f, 0f, Tween.EaseInOut);
                }
                attackingSlot.Card.Anim.PlayAttackAnimation(attackingSlot.Card.IsFlyingAttackingReach(), opposingSlot, null);
                yield return new WaitForSeconds(0.07f);
                attackingSlot.Card.Anim.SetAnimationPaused(paused: true);
                PlayableCard attackingCard = attackingSlot.Card;
                yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.CardGettingAttacked, false, opposingSlot.Card);

                // if the card has moved after triggering CardGettingAttacked
                if (opposingSlot.Card != persistentTarget) {
                    targetHasMoved = true;

                    yield return attacker.Card.GetComponent<Persistent>().PreSuccessfulTriggerSequence();
                    yield return UpdateSniperIcons(instance, attacker, opposingSlot, persistentTarget.Slot);
                    opposingSlot = persistentTarget.Slot;

                    // update the animation to target the correct slot
                    attackingSlot.Card.Anim.SetAnimationPaused(paused: false);
                    attackingSlot.Card.Anim.PlayAttackAnimation(attackingSlot.Card.IsFlyingAttackingReach(), opposingSlot, null);
                    yield return new WaitForSeconds(0.07f);
                    attackingSlot.Card.Anim.SetAnimationPaused(paused: true);
                }

                if (attackingCard != null && attackingCard.Slot != null) {
                    if (opposingSlot.Card == null) {
                        attackingSlot.Card.Anim.SetAnimationPaused(paused: false);
                        yield return new WaitForSeconds(0.25f);
                        yield break;
                    }

                    attackingSlot = attackingCard.Slot;

                    if (attackingSlot.Card.IsFlyingAttackingReach()) {
                        opposingSlot.Card.Anim.PlayJumpAnimation();
                        yield return new WaitForSeconds(0.3f);
                        attackingSlot.Card.Anim.PlayAttackInAirAnimation();
                    }
                    attackingSlot.Card.Anim.SetAnimationPaused(paused: false);

                    yield return new WaitForSeconds(0.05f);
                    int overkillDamage = attackingSlot.Card.Attack - opposingSlot.Card.Health;
                    int damage = attackingSlot.Card.Attack;

                    yield return opposingSlot.Card.TakeDamage(damage, attackingSlot.Card);
                    yield return instance.DealOverkillDamage(overkillDamage, attackingSlot, opposingSlot);

                    if (attackingSlot.Card != null && heightOffset > 0f)
                        yield return Singleton<BoardManager>.Instance.AssignCardToSlot(attackingSlot.Card, attackingSlot.Card.Slot, 0.1f, null, resolveTriggers: false);

                }
            }
            yield return new WaitForSeconds(waitAfter);
            if (targetHasMoved)
                yield return attacker.Card.GetComponent<Persistent>().LearnAbility(waitAfter);
        }

        private static IEnumerator UpdateSniperIcons(CombatPhaseManager instance, CardSlot attacker, CardSlot previousSlot, CardSlot newSlot) {
            Part1SniperVisualizer visualizer = null;
            if (SaveManager.SaveFile.IsPart1)
                visualizer = instance.GetComponent<Part1SniperVisualizer>() ?? instance.gameObject.AddComponent<Part1SniperVisualizer>();

            GameObject previous = visualizer?.sniperIcons.Find(x => x.gameObject.transform.parent == previousSlot.transform);
            if (previous != null) {
                Tween.LocalScale(previous.transform, Vector3.zero, 0.1f, 0f, Tween.EaseIn, Tween.LoopType.None, null, delegate () {
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
    }
}
