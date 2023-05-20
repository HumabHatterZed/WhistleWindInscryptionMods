using DiskCardGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWind.Core.AbilityClasses
{
    // Stolen from Zerg mod with love <3
    public abstract class SniperSelectSlot : AbilityBehaviour
    {
        public CardSlot selectedSlot = null;
        public virtual bool TargetAll => false;
        public virtual bool TargetAllies => true;
        public virtual string NoTargetsDialogue => "There are no cards you can choose.";
        public virtual string InvalidTargetDialogue => "It's already latched...";
        public virtual string NullTargetDialogue => "You can't target the air.";
        public virtual string SelfTargetDialogue => "You must choose one of your other cards.";
        private bool CanTargetNull => GetValidTargets().Exists((CardSlot s) => s.Card == null);
        public virtual IEnumerator OnNoValidTargets()
        {
            yield break;
        }
        public virtual IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            yield break;
        }
        public virtual IEnumerator OnPostValidTargetSelected()
        {
            yield break;
        }
        public virtual bool CardIsNotValid(PlayableCard card) => card == base.Card;
        private bool CardSlotIsNotValid(CardSlot slot)
        {
            if (slot.Card != null)
                return CardIsNotValid(slot.Card);
            return !CanTargetNull;
        }
        private IEnumerator PlayerSelectTarget(CombatPhaseManager instance, WstlPart1SniperVisualiser visualiser)
        {
            instance.VisualizeStartSniperAbility(base.Card.Slot);
            visualiser?.VisualizeStartSniperAbility(base.Card.Slot);

            List<CardSlot> possibleTargets = GetInitialTargets();

            List<CardSlot> targetSlots = GetValidTargets();

            CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;

            if (cardSlot != null && targetSlots.Contains(cardSlot))
            {
                instance.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
                visualiser?.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
            }
            selectedSlot = null;

            yield return Singleton<BoardManager>.Instance.ChooseTarget(possibleTargets, targetSlots, delegate (CardSlot s)
            {
                selectedSlot = s;
                instance.VisualizeConfirmSniperAbility(s);
                visualiser?.VisualizeConfirmSniperAbility(s, false, false);
            }, OnInvalidTarget, delegate (CardSlot s)
            {
                if (CanTargetNull || s.Card != null)
                {
                    instance.VisualizeAimSniperAbility(base.Card.Slot, s);
                    visualiser?.VisualizeAimSniperAbility(base.Card.Slot, s);
                }
            }, () => false, CursorType.Target);
        }
        private IEnumerator OpponentSelectTarget(CombatPhaseManager instance, WstlPart1SniperVisualiser visualiser)
        {
            List<CardSlot> validTargets = GetValidTargets();
            validTargets.RemoveAll((CardSlot x) => x.Card == null || x.Card.Dead || this.CardIsNotValid(x.Card) || x.Card == base.Card);
            yield return new WaitForSeconds(0.3f);
            yield return this.AISelectTarget(validTargets, delegate (CardSlot s)
            {
                selectedSlot = s;
            });

            instance.VisualizeConfirmSniperAbility(selectedSlot);
            visualiser?.VisualizeConfirmSniperAbility(selectedSlot, false, false);
            yield return new WaitForSeconds(0.25f);
        }
        private void OnInvalidTarget(CardSlot slot)
        {
            if (CardSlotIsNotValid(slot) && !Singleton<TextDisplayer>.Instance.Displaying)
            {
                string dialogue = InvalidTargetDialogue;
                if (slot.Card != null)
                {
                    if (slot.Card == base.Card)
                        dialogue = SelfTargetDialogue;
                }
                else
                    dialogue = NullTargetDialogue;

                base.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear(dialogue, 2.5f, 0f, Emotion.Anger));
            }
        }
        private IEnumerator AISelectTarget(List<CardSlot> validTargets, Action<CardSlot> chosenCallback)
        {
            if (validTargets.Count > 0)
            {
                validTargets.Sort((CardSlot a, CardSlot b) => this.AIEvaluateTarget(b.Card, TargetAllies) - this.AIEvaluateTarget(a.Card, TargetAllies));
                chosenCallback(validTargets[0]);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                base.Card.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.2f);
            }
        }
        private int AIEvaluateTarget(PlayableCard card, bool positiveEffect)
        {
            if (card == null)
            {
                if (CanTargetNull)
                    return UnityEngine.Random.Range(0, 5);
                return -1000;
            }
            int num = card.PowerLevel;
            if (card.Info.HasTrait(Trait.Terrain))
                num = 10 * (!positiveEffect ? 1 : -1);
            if (card.OpponentCard == positiveEffect)
                num += 1000;
            return num;
        }
        public bool ValidTargetsExist() => GetValidTargets().Count > 0;
        public virtual Predicate<CardSlot> InvalidTargets()
        {
            return (CardSlot x) => x.Card == null || x.Card.Dead || this.CardIsNotValid(x.Card) || x.Card == base.Card;
        }
        private List<CardSlot> GetValidTargets()
        {
            List<CardSlot> validSlots = GetInitialTargets();
            validSlots.RemoveAll(InvalidTargets());
            return validSlots;
        }
        public List<CardSlot> GetInitialTargets()
        {
            if (TargetAll)
                return Singleton<BoardManager>.Instance.AllSlotsCopy;

            if (TargetAllies)
                return base.Card.OpponentCard ? Singleton<BoardManager>.Instance.OpponentSlotsCopy : Singleton<BoardManager>.Instance.PlayerSlotsCopy;

            // default to the opposing card slots
            return base.Card.OpponentCard ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;
        }
        public IEnumerator SelectionSequence()
        {
            yield return base.PreSuccessfulTriggerSequence();

            // Lock the view so players can't mess it up
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;

            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.2f);

            // If there are no valid targets, break
            if (!ValidTargetsExist())
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.2f);

                yield return HelperMethods.PlayAlternateDialogue(dialogue: NoTargetsDialogue);
                yield return OnNoValidTargets();
                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
                yield break;
            }

            // set up the sniper visualiser
            CombatPhaseManager instance = Singleton<CombatPhaseManager>.Instance;
            WstlPart1SniperVisualiser visualiser = null;
            if ((SaveManager.SaveFile?.IsPart1).GetValueOrDefault())
                visualiser = instance.GetComponent<WstlPart1SniperVisualiser>() ?? instance.gameObject.AddComponent<WstlPart1SniperVisualiser>();

            if (base.Card.OpponentCard)
            {
                yield return OpponentSelectTarget(instance, visualiser);
                if (selectedSlot != null && selectedSlot.Card != null)
                    yield return new WaitForSeconds(0.3f);
            }
            else
            {
                // Run player logic
                Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode);
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

                yield return PlayerSelectTarget(instance, visualiser);
            }

            // clear the sniper icons here so they disappear before the claw does its thing
            instance.VisualizeClearSniperAbility();
            visualiser?.VisualizeClearSniperAbility();

            // once a target is selected, run logic
            yield return OnValidTargetSelected(selectedSlot);

            yield return base.LearnAbility(0.4f);
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            yield return new WaitForSeconds(0.2f);

            yield return OnPostValidTargetSelected();
        }
    }
}
