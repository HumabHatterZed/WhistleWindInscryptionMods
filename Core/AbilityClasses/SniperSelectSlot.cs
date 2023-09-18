using DiskCardGame;
using InscryptionCommunityPatch.Card;
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
        public abstract bool IsPositiveEffect { get; }
        public abstract List<CardSlot> InitialTargets { get; }
        public virtual string NoTargetsDialogue => "There are no cards you can choose.";
        public virtual string InvalidTargetDialogue => "It's already latched...";
        public virtual string NullTargetDialogue => "You can't target the air.";
        public virtual string SelfTargetDialogue => "You must choose one of your other cards.";

        public virtual bool SlotIsNotValid(CardSlot slot) => slot.Card == null;
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
        private bool InvalidTargets(CardSlot slot) => slot.Card == base.Card || (slot.Card?.Dead ?? false) || SlotIsNotValid(slot);
        private bool CanTargetNull() => GetValidTargets().Exists((CardSlot s) => s.Card == null);
        private bool HasValidTarget() => GetValidTargets().Count > 0;

        private List<CardSlot> GetValidTargets()
        {
            List<CardSlot> validSlots = InitialTargets;
            validSlots.RemoveAll(x => InvalidTargets(x));
            return validSlots;
        }
        private IEnumerator PlayerSelectTarget(CombatPhaseManager instance, Part1SniperVisualizer visualiser)
        {
            instance.VisualizeStartSniperAbility(base.Card.Slot);
            visualiser?.VisualizeStartSniperAbility(base.Card.Slot);

            List<CardSlot> targetSlots = GetValidTargets();
            CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;

            if (cardSlot != null && targetSlots.Contains(cardSlot))
            {
                instance.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
                visualiser?.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
            }
            selectedSlot = null;

            yield return Singleton<BoardManager>.Instance.ChooseTarget(InitialTargets, targetSlots, delegate (CardSlot s)
            {
                selectedSlot = s;
                instance.VisualizeConfirmSniperAbility(s);
                visualiser?.VisualizeConfirmSniperAbility(s);
            }, OnInvalidTarget, delegate (CardSlot s)
            {
                if (!SlotIsNotValid(s))
                {
                    instance.VisualizeAimSniperAbility(base.Card.Slot, s);
                    visualiser?.VisualizeAimSniperAbility(base.Card.Slot, s);
                }
            }, () => false, CursorType.Target);
        }
        private IEnumerator OpponentSelectTarget(CombatPhaseManager instance, Part1SniperVisualizer visualiser)
        {
            List<CardSlot> validTargets = GetValidTargets();
            validTargets.RemoveAll(x => InvalidTargets(x));
            yield return new WaitForSeconds(0.3f);
            yield return AISelectTarget(validTargets, delegate (CardSlot s)
            {
                selectedSlot = s;
            });

            instance.VisualizeConfirmSniperAbility(selectedSlot);
            visualiser?.VisualizeConfirmSniperAbility(selectedSlot);
            yield return new WaitForSeconds(0.25f);
        }
        private void OnInvalidTarget(CardSlot slot)
        {
            if (!Singleton<TextDisplayer>.Instance.Displaying)
            {
                string dialogue = NullTargetDialogue;

                if (slot.Card == base.Card)
                    dialogue = SelfTargetDialogue;
                else if (slot.Card != null)
                    dialogue = InvalidTargetDialogue;

                base.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear(dialogue, 2.5f, 0f, Emotion.Anger));
            }
        }
        private IEnumerator AISelectTarget(List<CardSlot> validTargets, Action<CardSlot> chosenCallback)
        {
            if (validTargets.Count > 0)
            {
                validTargets.Sort((CardSlot a, CardSlot b) => AIEvaluateTarget(b.Card, IsPositiveEffect) - AIEvaluateTarget(a.Card, IsPositiveEffect));
                chosenCallback(validTargets[0]);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
            }
        }
        private int AIEvaluateTarget(PlayableCard card, bool positiveEffect)
        {
            if (card == null)
                return CanTargetNull() ? UnityEngine.Random.Range(0, 5) : -1000;

            int num = card.PowerLevel;
            if (card.Info.HasTrait(Trait.Terrain))
                num = 10 * (!positiveEffect ? 1 : -1);

            if (card.OpponentCard == positiveEffect)
                num += 1000;

            return num;
        }

        public IEnumerator SelectionSequence()
        {
            // Lock the view so players can't mess it up
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;

            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.2f);

            // If there are no valid targets, break
            if (!HasValidTarget())
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.2f);

                yield return DialogueHelper.PlayAlternateDialogue(dialogue: NoTargetsDialogue);
                yield return OnNoValidTargets();
                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
                yield break;
            }

            // set up the sniper visualiser
            CombatPhaseManager instance = Singleton<CombatPhaseManager>.Instance;
            Part1SniperVisualizer visualiser = instance.GetComponent<Part1SniperVisualizer>() ?? instance.gameObject.AddComponent<Part1SniperVisualizer>();

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
