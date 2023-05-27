using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionCommunityPatch.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core;
using static InscryptionCommunityPatch.Card.SniperFix;

// custom version of the Sniper fix that adds Marksman compatibility and the special behaviour of Blue Star and Judgement Bird
namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch]
    internal class AddSniperPiperPatch
    {
        [HarmonyPrefix, HarmonyPatch(typeof(CombatPhaseManager), nameof(CombatPhaseManager.SlotAttackSequence))]
        private static bool OverrideWithMarksman(CombatPhaseManager __instance, ref IEnumerator __result, CardSlot slot)
        {
            if (slot?.Card != null && slot.Card.HasAbility(Ability.Sniper))
            {
                if (IsJudgementBird(slot))
                    __result = WstlSniperSequence(__instance, slot);
                else
                    __result = SniperAttack(__instance, slot);

                return false;
            }
            return true;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.WillDieFromSharp))]
        private static void ImmuneToSharp(CardSlot slot, ref bool __result)
        {
            if (IsJudgementBird(slot))
                __result = false;
        }
        private static IEnumerator WstlSniperSequence(CombatPhaseManager instance, CardSlot slot)
        {
            List<CardSlot> opposingSlots = slot.Card.GetOpposingSlots();
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            int numAttacks = GetAttackCount(slot.Card);

            opposingSlots.Clear();
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            WstlPart1SniperVisualiser visualizer = null;
            if ((SaveManager.SaveFile?.IsPart1).GetValueOrDefault())
                visualizer = instance.GetComponent<WstlPart1SniperVisualiser>() ?? instance.gameObject.AddComponent<WstlPart1SniperVisualiser>();

            if (slot.Card.OpponentCard)
                yield return WstlOpponentLogic(instance, visualizer, opposingSlots, slot, numAttacks);
            else
                yield return WstlPlayerLogic(instance, visualizer, opposingSlots, slot, numAttacks);

            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            foreach (CardSlot opposingSlot in opposingSlots)
            {
                Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
                if (opposingSlot.Card != null && opposingSlot.Card.LacksAllTraits(Trait.Terrain, Trait.Pelt, AbnormalPlugin.ImmuneToInstaDeath) && !opposingSlot.Card.AttackIsBlocked(slot))
                {
                    if (slot.Card.FaceDown)
                    {
                        slot.Card.SetFaceDown(false);
                        slot.Card.UpdateFaceUpOnBoardEffects();
                        yield return new WaitForSeconds(0.2f);
                    }
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

        private static IEnumerator WstlPlayerLogic(CombatPhaseManager instance, WstlPart1SniperVisualiser visualizer,
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
                yield return Singleton<BoardManager>.Instance.ChooseTarget(Singleton<BoardManager>.Instance.OpponentSlotsCopy, Singleton<BoardManager>.Instance.OpponentSlotsCopy,
                    delegate (CardSlot s)
                    {
                        opposingSlots.Add(s);
                        bool neckless = s.Card.HasAnyOfTraits(Trait.Terrain, Trait.Pelt, AbnormalPlugin.ImmuneToInstaDeath);
                        instance.VisualizeConfirmSniperAbility(s);
                        visualizer?.VisualizeConfirmSniperAbility(s, true, neckless);
                    }, null, delegate (CardSlot s)
                    {
                        instance.VisualizeAimSniperAbility(slot, s);
                        visualizer?.VisualizeAimSniperAbility(slot, s);
                    }, () => false, CursorType.Sacrifice);
            }
        }
        private static IEnumerator WstlOpponentLogic(CombatPhaseManager instance, WstlPart1SniperVisualiser visualizer,
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

                bool neckless = attackSlot.Card.HasAnyOfTraits(Trait.Terrain, Trait.Pelt, AbnormalPlugin.ImmuneToInstaDeath);
                opposingSlots.Add(attackSlot);
                instance.VisualizeConfirmSniperAbility(attackSlot);
                visualizer?.VisualizeConfirmSniperAbility(attackSlot, true, neckless);
                yield return new WaitForSeconds(0.25f);
            }
        }
        private static bool IsJudgementBird(CardSlot slot) => slot.Card.HasTrait(AbnormalPlugin.Executioner);
        private static IEnumerator Execution(PlayableCard target)
        {
            target.Anim.PlaySacrificeSound();
            target.Anim.DeactivateSacrificeHoverMarker();
            yield return target.Die(wasSacrifice: false);
        }
    }
}
