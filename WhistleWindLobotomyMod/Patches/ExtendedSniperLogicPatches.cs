using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionCommunityPatch.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod;
using WhistleWindLobotomyMod.Core;
using static InscryptionCommunityPatch.Card.SniperFix;

// custom version of the Sniper fix that adds Marksman compatibility and the special behaviour of Blue Star and Judgement Bird
namespace WhistleWind.AbnormalSigils.Patches
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
            if (targetSlot.Card?.TemporaryMods.Exists(x => x.singletonId == executeId) ?? false)
                targetSlot.Card.Anim.SetMarkedForSacrifice(marked: true);

            return true;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.SniperAttack))]
        private static bool OverrideSniperSequence(CombatPhaseManager instance, CardSlot slot, ref IEnumerator __result)
        {
            if (IsJudgementBird(slot) || IsEnraged(slot) || IsRaging(slot))
            {
                __result = WstlSniperSequence(instance, slot);
                return false;
            }
            return true;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.WillDieFromSharp))]
        private static void AddExtraChecks(CardSlot slot, ref bool __result)
        {
            // Judgement Bird does not trigger OnDealDamage or OnTakeDamage
            if (IsJudgementBird(slot))
                __result = false;
        }
        private static IEnumerator WstlSniperSequence(CombatPhaseManager instance, CardSlot slot)
        {
            List<CardSlot> opposingSlots = new(); // the slots to attacks
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            int numAttacks = GetAttackCount(slot.Card);
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Part1SniperVisualizer visualizer = null;
            if (SaveManager.SaveFile.IsPart1)
                visualizer = instance.GetComponent<Part1SniperVisualizer>() ?? instance.gameObject.AddComponent<Part1SniperVisualizer>();

            if (IsRaging(slot))
            {
                opposingSlots = slot.Card.GetOpposingSlots();
                for (int i = 0; i < opposingSlots.Count; i++)
                {
                    instance.VisualizeConfirmSniperAbility(opposingSlots[i]);
                    visualizer?.VisualizeConfirmSniperAbility(opposingSlots[i]);
                }
            }
            else
            {
                CardSlot enragedTarget = null;
                if (IsEnraged(slot))
                {
                    string nameToFind;
                    if (slot.Card.Info.name == "wstl_redHoodedMercenary")
                        nameToFind = "wstl_willBeBadWolf";
                    else
                        nameToFind = "wstl_redHoodedMercenary";

                    enragedTarget = BoardManager.Instance.AllSlotsCopy.Find(x => x.Card?.Info.name == nameToFind);
                    if (enragedTarget != null)
                    {
                        for (int i = 0; i < numAttacks; i++)
                            opposingSlots.Add(enragedTarget);

                        instance.VisualizeConfirmSniperAbility(enragedTarget);
                        visualizer?.VisualizeConfirmSniperAbility(enragedTarget);
                    }
                }

                if (enragedTarget == null)
                {
                    if (slot.Card.OpponentCard)
                        yield return WstlOpponentLogic(instance, visualizer, opposingSlots, slot, numAttacks);
                    else
                        yield return WstlPlayerLogic(instance, visualizer, opposingSlots, slot, numAttacks);
                }
            }

            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            foreach (CardSlot opposingSlot in opposingSlots)
            {
                Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
                if (IsJudgementBird(slot) && opposingSlot.Card != null && !ImmuneToHanging(opposingSlot) && !opposingSlot.Card.AttackIsBlocked(slot))
                {
                    slot.Card.FlipFaceDown(false);
                    slot.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                    yield return Execution(opposingSlot.Card);
                }
                else
                    yield return instance.SlotAttackSlot(slot, opposingSlot, opposingSlots.Count > 1 ? 0.1f : 0f);
            }
            instance.VisualizeClearSniperAbility();
            visualizer?.VisualizeClearSniperAbility();
            yield break;
        }

        private static IEnumerator WstlPlayerLogic(CombatPhaseManager instance, Part1SniperVisualizer visualizer,
        List<CardSlot> opposingSlots, CardSlot slot, int numAttacks)
        {
            for (int i = 0; i < numAttacks; i++)
            {
                instance.VisualizeStartSniperAbility(slot);
                visualizer?.VisualizeStartSniperAbility(slot);
                CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;
                if (cardSlot != null && opposingSlots.Contains(cardSlot))
                {
                    instance.VisualizeAimSniperAbility(slot, cardSlot);
                    visualizer?.VisualizeAimSniperAbility(slot, cardSlot);
                }
                List<CardSlot> list = Singleton<BoardManager>.Instance.OpponentSlotsCopy;
                yield return Singleton<BoardManager>.Instance.ChooseTarget(list, list,
                    delegate (CardSlot s)
                    {
                        opposingSlots.Add(s);
                        if (IsJudgementBird(slot) && !ImmuneToHanging(s))
                            s.Card.AddTemporaryMod(new() { singletonId = executeId });

                        instance.VisualizeConfirmSniperAbility(s);
                        visualizer?.VisualizeConfirmSniperAbility(s);
                    }, null, delegate (CardSlot s)
                    {
                        InteractionCursor.Instance.ForceCursorType(ImmuneToHanging(s) ? CursorType.Target : CursorType.Sacrifice);
                        instance.VisualizeAimSniperAbility(slot, s);
                        visualizer?.VisualizeAimSniperAbility(slot, s);
                    }, () => false, CursorType.Target);
            }
        }
        private static IEnumerator WstlOpponentLogic(CombatPhaseManager instance, Part1SniperVisualizer visualizer,
        List<CardSlot> opposingSlots, CardSlot slot, int numAttacks)
        {
            List<CardSlot> playerSlots = Singleton<BoardManager>.Instance.PlayerSlotsCopy;
            List<PlayableCard> playerCards = playerSlots.FindAll(x => x.Card != null).ConvertAll((x) => x.Card);
            bool anyCards = playerCards.Count > 0;

            for (int i = 0; i < numAttacks; i++)
            {
                CardSlot attackSlot = slot.opposingSlot;
                if (anyCards)
                {
                    PlayableCard strongestKillable = GetStrongestKillableCard(anyCards, playerCards, opposingSlots, slot, numAttacks);
                    PlayableCard strongestAttackable = GetFirstStrongestAttackableCard(anyCards, playerCards, opposingSlots, slot, numAttacks);
                    PlayableCard strongestAttackableNoPreferences = GetFirstStrongestAttackableCardNoPreferences(anyCards, playerCards, opposingSlots, slot, numAttacks);

                    if (CanWin(opposingSlots, playerSlots, slot, numAttacks))
                        attackSlot = GetFirstAvailableOpenSlot(opposingSlots, playerSlots, slot, numAttacks);
                    else if (strongestKillable != null)
                        attackSlot = strongestKillable.Slot;
                    else if (strongestAttackable != null)
                        attackSlot = strongestAttackable.Slot;
                    else if (strongestAttackableNoPreferences != null)
                        attackSlot = strongestAttackableNoPreferences.Slot;
                }

                opposingSlots.Add(attackSlot);
                if (IsJudgementBird(slot) && !ImmuneToHanging(attackSlot))
                    attackSlot.Card.AddTemporaryMod(new() { singletonId = executeId });

                instance.VisualizeConfirmSniperAbility(attackSlot);
                visualizer?.VisualizeConfirmSniperAbility(attackSlot);
                yield return new WaitForSeconds(0.25f);
            }
        }
        private static bool ImmuneToHanging(CardSlot slot) => slot.Card?.HasAnyOfTraits(Trait.Terrain, Trait.Pelt, AbnormalPlugin.ImmuneToInstaDeath) ?? true;
        private static bool IsJudgementBird(CardSlot slot) => slot.Card.HasTrait(LobotomyCardManager.TraitExecutioner);
        private static bool IsEnraged(CardSlot slot) => slot.Card.GetComponent<CrimsonScar>()?.Enraged ?? false;
        private static bool IsRaging(CardSlot slot) => slot.Card.HasSpecialAbility(BlindRage.specialAbility);
        private static IEnumerator Execution(PlayableCard target)
        {
            target.Anim.PlaySacrificeSound();
            target.Anim.DeactivateSacrificeHoverMarker();
            yield return target.Die(wasSacrifice: false);
        }
    }
}
