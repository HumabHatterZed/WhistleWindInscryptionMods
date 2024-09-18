using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionCommunityPatch.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents.Apocalypse;
using static InscryptionCommunityPatch.Card.SniperFix;

// custom version of the Sniper fix that adds Marksman compatibility and the special behaviour of Blue Star and Judgement Bird
namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal class ExtendedSniperLogicPatches
    {
        private const string executeId = "wstl:JudgementExecution";

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CombatPhaseManager), nameof(CombatPhaseManager.VisualizeConfirmSniperAbility))]
        [HarmonyPatch(typeof(Part1SniperVisualizer), nameof(Part1SniperVisualizer.VisualizeConfirmSniperAbility))]
        private static bool PerformHanging(CardSlot targetSlot)
        {
            if ((targetSlot.Card?.TemporaryMods.Exists(x => x.singletonId == executeId) ?? false) && !ImmuneToHanging(targetSlot))
                targetSlot.Card.Anim.SetMarkedForSacrifice(marked: true);

            return true;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.WillDieFromSharp))]
        private static void AddExtraChecks(CardSlot slot, ref bool __result)
        {
            // Judgement Bird does not trigger OnDealDamage or OnTakeDamage
            if (IsExecutioner(slot))
                __result = false;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.DoSniperLogic))]
        private static IEnumerator CustomSniperLogic(
            IEnumerator enumerator, CombatPhaseManager instance, Part1SniperVisualizer visualizer,
            List<CardSlot> opposingSlots, CardSlot attackingSlot, int numAttacks)
        {
            if (attackingSlot.Card == null)
            {
                yield return enumerator;
                yield break;
            }
            if (IsRaging(attackingSlot)) // if raging, target random slots
            {
                opposingSlots.AddRange(attackingSlot.Card.GetOpposingSlots());
                for (int i = 0; i < opposingSlots.Count; i++)
                {
                    instance.VisualizeConfirmSniperAbility(opposingSlots[i]);
                    visualizer?.VisualizeConfirmSniperAbility(opposingSlots[i]);
                }
                yield break;
            }
            if (IsEnraged(attackingSlot)) // if enraged, target the rival if they exist
            {
                string nameToFind = "wstl_redHoodedMercenary";
                if (attackingSlot.Card.Info.name == nameToFind)
                    nameToFind = "wstl_willBeBadWolf";

                CardSlot enragedTarget = BoardManager.Instance.AllSlotsCopy.Find(x => x.Card?.Info.name == nameToFind);
                if (enragedTarget != null)
                {
                    for (int i = 0; i < numAttacks; i++)
                        opposingSlots.Add(enragedTarget);

                    instance.VisualizeConfirmSniperAbility(enragedTarget);
                    visualizer?.VisualizeConfirmSniperAbility(enragedTarget);
                    yield break;
                }
            }

            yield return enumerator;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.DoAttackTargetSlotsLogic))]
        private static IEnumerator CustomOpposingSlotsLogic(IEnumerator enumerator, CardSlot attackingSlot, CardSlot opposingSlot)
        {
            if (!IsExecutioner(attackingSlot) || !CanTargetFaceDown(opposingSlot, attackingSlot.Card) ||
                ImmuneToHanging(opposingSlot) || attackingSlot.Card.AttackIsBlocked(opposingSlot))
            {
                yield return enumerator;
                yield break;
            }

            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            yield return opposingSlot.Card.FlipFaceDown(false);
            opposingSlot.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return Execution(opposingSlot.Card);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.PlayerTargetSelectedCallback))]
        private static void CustomTargetCallback(CardSlot targetSlot, CardSlot attackingSlot)
        {
            if (IsExecutioner(attackingSlot) && CanTargetFaceDown(targetSlot, attackingSlot.Card) && !ImmuneToHanging(targetSlot))
                targetSlot.Card.AddTemporaryMod(new() { singletonId = executeId });
        }

        [HarmonyPostfix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.PlayerSlotCursorEnterCallback))]
        private static void CustomCursorEnterCallback(CardSlot targetSlot, CardSlot attackingSlot)
        {
            if (IsExecutioner(attackingSlot))
                InteractionCursor.Instance.ForceCursorType(
                    (CanTargetFaceDown(targetSlot, attackingSlot.Card) && ImmuneToHanging(targetSlot)) ? CursorType.Target : CursorType.Sacrifice);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.GetValidTargets))]
        private static void CustomSniperTargets(ref List<CardSlot> __result, bool playerIsAttacker, CardSlot attackingSlot)
        {
            if (attackingSlot.Card.HasStatusEffect<Enchanted>(true)) // ensure Sniper cards are affected by Enchanted
            {
                List<CardSlot> slots = BoardManager.Instance.AllSlotsCopy.FindAll(x => x.Card != null && x.Card.HasAbility(Dazzling.ability));
                // if there's a target card that this card can hit
                if (slots.Count > 0 && slots.Exists(x => !attackingSlot.Card.CanAttackDirectly(x)))
                {
                    __result = slots.FindAll(x => !attackingSlot.Card.CanAttackDirectly(x));
                    return;
                }
            }
            if (!playerIsAttacker && (attackingSlot.Card?.HasStatusEffect<Sin>() ?? false)) // Snipers with Sin can target any card on the board
            {
                __result = BoardManager.Instance.AllSlotsCopy;
                __result.Remove(attackingSlot);
                __result.RemoveAll(x => x.Card == (TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer).BossCard);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.OpponentSelectTargetSlot))]
        private static void CustomSelectTargetSlot(ref CardSlot __result, List<CardSlot> opposingSlots, List<PlayableCard> playerCards, CardSlot attackingSlot, int numAttacks)
        {
            bool anyCards = playerCards.Count > 0;
            if (attackingSlot.Card == null || !anyCards)
                return;

            int sinCount = attackingSlot.Card.GetStatusEffectPotency<Sin>();
            if (sinCount > 0)
            {
                List<CardSlot> filteredSlots = new()
                        {
                            GetStrongestKillableCard(anyCards, playerCards, opposingSlots, attackingSlot, numAttacks)?.Slot,
                            GetFirstStrongestAttackableCard(anyCards, playerCards, opposingSlots, attackingSlot, numAttacks)?.Slot,
                            GetFirstStrongestAttackableCardNoPreferences(anyCards, playerCards, opposingSlots, attackingSlot, numAttacks)?.Slot,
                            __result
                        };
                // remove null slots and sort by lowest sin count
                filteredSlots.RemoveAll(x => x == null);
                filteredSlots.Sort((a, b) => (a.Card?.GetStatusEffectPotency<Sin>() ?? 0) - (b.Card?.GetStatusEffectPotency<Sin>() ?? 0));

                if (filteredSlots.Exists(x => x.Card != null))
                {
                    if (sinCount >= 5) // if we have 5+ Sin, target the card with the lowest sin count
                        __result = filteredSlots[0];
                    else // otherwise target a card whose sin count will go over 5, otherwise default logic
                        __result = filteredSlots.Find(x => x.Card.GetStatusEffectPotency<Sin>() + sinCount >= 5) ?? __result;
                }
            }
        }

        private static bool ImmuneToHanging(CardSlot slot)
        {
            if (slot.Card != null)
                return slot.Card.HasAbility(Ability.MadeOfStone) || slot.Card.HasAnyOfTraits(Trait.Terrain, Trait.Pelt, AbnormalPlugin.ImmuneToInstaDeath);

            return true;
        }
        private static bool CanTargetFaceDown(CardSlot target, PlayableCard attackingCard)
        {
            if (target.Card != null && target.Card.FaceDown)
                return attackingCard.HasAbility(Persistent.ability);

            return true;
        }
        private static bool IsExecutioner(CardSlot slot) => slot.Card && slot.Card.HasTrait(LobotomyCardManager.Executioner);
        private static bool IsEnraged(CardSlot slot) => slot.Card.GetComponent<CrimsonScar>()?.Enraged ?? false;
        private static bool IsRaging(CardSlot slot) => slot.Card.HasSpecialAbility(BlindRage.specialAbility);
        private static IEnumerator Execution(PlayableCard target)
        {
            target.Anim.PlaySacrificeSound();
            target.Anim.DeactivateSacrificeHoverMarker();
            yield return target.Die(false);
        }
    }
}
