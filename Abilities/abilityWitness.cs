using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Witness()
        {
            const string rulebookName = "Witness";
            const string rulebookDescription = "Activate: Pay 2 bones to increase a selected card's Health by 2 and increase their taken damage by 1. This effect stacks up to 3 times per card.";
            const string dialogue = "The truth will set you free.";

            Witness.ability = AbilityHelper.CreateActivatedAbility<Witness>(
                Resources.sigilWitness, Resources.sigilWitness_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2).Id;
        }
    }
    public class Witness : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int BonesCost => 2;

        private CardSlot targetedSlot = null;
        private string invalidDialogue;
        public override bool CanActivate()
        {
            int count = 0;
            if (!base.Card.OpponentCard)
            {
                // Are there other cards to affect, are any of them valid targets, and is this card on the player's side
                foreach (var slot in Singleton<BoardManager>.Instance.PlayerSlotsCopy.Where((CardSlot slot) => slot.Card != base.Card))
                {
                    if (slot.Card != null)
                    {
                        int prudence = !(slot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)slot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence");
                        count += prudence < 3 ? 1 : 0;
                    }
                }
            }
            return count > 0;
        }

        // Discard all hands in card and draw an equal number
        public override IEnumerator Activate()
        {
            yield return base.PreSuccessfulTriggerSequence();
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode);

            yield return PlayerChooseTarget();

            bool valid = targetedSlot != null && targetedSlot.Card != null && targetedSlot.Card != base.Card && targetedSlot.Index != base.Card.Slot.Index;
            if (valid)
            {
                int prudence1 = !(targetedSlot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)targetedSlot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence");
                valid = prudence1 < 3;
            }
            if (!valid)
            {
                while (!valid)
                {
                    invalidDialogue = targetedSlot == base.Card.Slot ? "You must choose one of your other cards to proselytise." : "You can't preach to the air.";
                    if (targetedSlot.Card != null)
                    {
                        int prudence2 = !(targetedSlot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)targetedSlot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence");
                        if (prudence2 >= 3)
                        {
                            invalidDialogue = "That card's [c:bR]prudence[c:] is too high. Choose another.";
                        }
                    }
                    base.Card.Anim.StrongNegationEffect();
                    yield return CustomMethods.PlayAlternateDialogue(dialogue: invalidDialogue);

                    CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();
                    yield return PlayerChooseTarget();

                    valid = targetedSlot != null && (targetedSlot.Card != null && targetedSlot.Card != base.Card && targetedSlot.Index != base.Card.Slot.Index);
                    if (valid)
                    {
                        int prudence3 = !(targetedSlot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)targetedSlot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence");
                        valid = prudence3 < 3;
                    }
                }
            }
            int prudence = !(targetedSlot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)targetedSlot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence");
            yield return targetedSlot.Card.Info.SetExtendedProperty("wstl:Prudence", prudence + 1);
            targetedSlot.Card.HealDamage(2);
            targetedSlot.Card.Anim.StrongNegationEffect();
            CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();
            yield return new WaitForSeconds(0.25f);
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);

            yield return new WaitForSeconds(0.2f);
            yield return base.LearnAbility();
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.DefaultView);
        }
        private IEnumerator PlayerChooseTarget()
        {
            CombatPhaseManagerPatch.Instance.VisualizeStartSniperAbility(base.Card.Slot);

            List<CardSlot> targetSlots = base.Card.OpponentCard ? Singleton<BoardManager>.Instance.OpponentSlotsCopy : Singleton<BoardManager>.Instance.PlayerSlotsCopy;
            CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;

            if (cardSlot != null && targetSlots.Contains(cardSlot))
            {
                CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
            }

            targetedSlot = null;

            yield return Singleton<BoardManager>.Instance.ChooseTarget(targetSlots, targetSlots, delegate (CardSlot s)
            {
                targetedSlot = s;
                CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(s, false);
            }, null, delegate (CardSlot s)
            {
                CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(base.Card.Slot, s);

            }, () => false, CursorType.Target);
        }
    }
}
