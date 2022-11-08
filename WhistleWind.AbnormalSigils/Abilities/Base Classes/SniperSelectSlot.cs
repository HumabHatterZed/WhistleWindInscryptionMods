using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Patches;

namespace WhistleWind.AbnormalSigils
{
    // Stolen from Zerg mod with love <3
    public abstract class SniperSelectSlot : AbilityBehaviour
    {
        public CardSlot selectedSlot = null;
        public virtual bool TargetAll => false;
        public virtual bool TargetAllies => true;
        public virtual string NoAlliesDialogue => "No allies.";
        public virtual string NullTargetDialogue => "You can't target the air.";
        public virtual string SelfTargetDialogue => "You must choose one of your other cards.";
        public IEnumerator SelectionSequence()
        {
            CombatPhaseManager instance = Singleton<CombatPhaseManager>.Instance;
            WstlPart1SniperVisualiser visualiser = null;
            if ((SaveManager.SaveFile?.IsPart1).GetValueOrDefault())
            {
                visualiser = instance.GetComponent<WstlPart1SniperVisualiser>() ?? instance.gameObject.AddComponent<WstlPart1SniperVisualiser>();
            }
            yield return base.PreSuccessfulTriggerSequence();

            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;

            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.2f);

            // if no targetable allies
            if (!ValidTargets())
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.2f);

                yield return OnNoValidAllies();
                yield break;
            }

            if (base.Card.OpponentCard)
            {
                yield return OpponentSelectTarget(instance, visualiser);
                yield break;
            }

            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            yield return PlayerSelectTarget(instance, visualiser);

            while (!ValidTarget(selectedSlot))
            {
                instance.VisualizeClearSniperAbility();
                visualiser?.VisualizeClearSniperAbility();

                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.2f);

                if (selectedSlot == base.Card.Slot)
                    yield return AbnormalCustomMethods.PlayAlternateDialogue(delay: 0f, dialogue: SelfTargetDialogue);
                else
                    yield return AbnormalCustomMethods.PlayAlternateDialogue(delay: 0f, dialogue: NullTargetDialogue);

                yield return PlayerSelectTarget(instance, visualiser);
            }

            // Put actual effect here
            yield return OnValidTargetSelected(selectedSlot.Card);

            instance.VisualizeClearSniperAbility();
            visualiser?.VisualizeClearSniperAbility();

            yield return new WaitForSeconds(0.2f);
            yield return base.LearnAbility();

            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);

            yield return OnPostValidTargetSelected();
        }

        public virtual IEnumerator OnNoValidAllies()
        {
            yield return AbnormalCustomMethods.PlayAlternateDialogue(dialogue: NoAlliesDialogue);
        }
        public virtual IEnumerator OnValidTargetSelected(PlayableCard card)
        {
            yield break;
        }
        public virtual IEnumerator OnPostValidTargetSelected()
        {
            yield break;
        }

        private IEnumerator OpponentSelectTarget(CombatPhaseManager instance, WstlPart1SniperVisualiser visualiser)
        {
            List<CardSlot> targetSlots = PossibleTargets();
            targetSlots.RemoveAll(s => s.Card != null && s.Card != base.Card);

            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;

            CardSlot randSlot = targetSlots[SeededRandom.Range(0, targetSlots.Count, randomSeed)];

            instance.VisualizeConfirmSniperAbility(randSlot);
            visualiser?.VisualizeConfirmSniperAbility(randSlot);

            yield return new WaitForSeconds(0.25f);

            yield return OnValidTargetSelected(randSlot.Card);

            instance.VisualizeClearSniperAbility();
            visualiser?.VisualizeClearSniperAbility();

            yield return new WaitForSeconds(0.2f);

            yield return OnPostValidTargetSelected();
        }

        private IEnumerator PlayerSelectTarget(CombatPhaseManager instance, WstlPart1SniperVisualiser visualiser)
        {
            instance.VisualizeStartSniperAbility(base.Card.Slot);
            visualiser?.VisualizeStartSniperAbility(base.Card.Slot);

            List<CardSlot> targetSlots = PossibleTargets();
            CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;

            if (cardSlot != null && targetSlots.Contains(cardSlot))
            {
                instance.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
                visualiser?.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
            }

            selectedSlot = null;

            yield return Singleton<BoardManager>.Instance.ChooseTarget(targetSlots, targetSlots, delegate (CardSlot s)
            {
                selectedSlot = s;
                instance.VisualizeConfirmSniperAbility(s);
                visualiser?.VisualizeConfirmSniperAbility(s);
            }, null, delegate (CardSlot s)
            {
                instance.VisualizeAimSniperAbility(base.Card.Slot, s);
                visualiser?.VisualizeAimSniperAbility(base.Card.Slot, s);

            }, () => false, CursorType.Target);
        }

        private bool ValidTargets()
        {
            List<CardSlot> validSlots = PossibleTargets();
            validSlots.RemoveAll((CardSlot x) => x.Card == null || x.Card.Dead || x.Card == base.Card);
            return validSlots.Count() > 0;
        }
        private bool ValidTarget(CardSlot targetedSlot)
        {
            return targetedSlot != null && targetedSlot.Card != null && targetedSlot.Card != base.Card && targetedSlot.Index != base.Card.Slot.Index;
        }
        private List<CardSlot> PossibleTargets()
        {
            if (TargetAll)
                return Singleton<BoardManager>.Instance.AllSlotsCopy;
            if (TargetAllies)
            {
                return base.Card.OpponentCard ? Singleton<BoardManager>.Instance.OpponentSlotsCopy : Singleton<BoardManager>.Instance.PlayerSlotsCopy;
            }
            return base.Card.OpponentCard ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;
        }
    }
}
