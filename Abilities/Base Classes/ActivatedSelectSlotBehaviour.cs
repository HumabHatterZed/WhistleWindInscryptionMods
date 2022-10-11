using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    // Logic for abilities that have the player select a slot to be targeted
    // Defaults to the logic for Latch but can be overridden as needed
    public abstract class ActivatedSelectSlotBehaviour : ActivatedAbilityBehaviour
    {
        public CardSlot selectedSlot = null;
        // Required override field - set to Ability.None if you don't want to latch anything
        // also override OnValidTarget to not do the latch logic
        public abstract Ability LatchAbility { get; }
        public virtual bool TargetAll => true;
        public virtual bool TargetAllies => false;
        public virtual string NoAlliesDialogue => "No allies.";

        public virtual string InvalidTargetDialogue => "It's already latched...";

        private bool ActivatedThisTurn;

        public virtual IEnumerator OnNoValidAllies()
        {
            // Play NoAlliesDialogue by default
            yield return CustomMethods.PlayAlternateDialogue(dialogue: NoAlliesDialogue);
        }
        public virtual IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            // Add LatchAbility by default
            if (slot != null && slot.Card != null)
            {
                CardModificationInfo cardModificationInfo = new(this.LatchAbility) { fromTotem = true, singletonId = "wstl:ActivatedLatch" };

                slot.Card.Anim.LightNegationEffect();
                slot.Card.AddTemporaryMod(cardModificationInfo);
                yield return new WaitForSeconds(0.75f);
                yield return base.LearnAbility();
            }
        }
        public virtual IEnumerator OnPostValidTargetSelected()
        {
            // Set ActivatedThisTurn to true by default
            ActivatedThisTurn = true;
            yield break;
        }
        public virtual bool CardIsNotValid(PlayableCard card)
        {
            // By default returns whether the card is latched
            return card.TemporaryMods.Exists((CardModificationInfo m) => m.fromLatch || m.singletonId == "wstl:ActivatedLatch");
        }

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.Card.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            // By default reset ActivatedThisTurn
            ActivatedThisTurn = false;
            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.2f);
        }

        public override bool CanActivate()
        {
            // Can only once per turn by default
            if (!ActivatedThisTurn)
                return ValidTargets();

            return false;
        }
        public override IEnumerator Activate()
        {
            yield return base.PreSuccessfulTriggerSequence();

            // Lock the view so players can't mess it up
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;

            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.2f);

            // If there are no valid targets, break
            if (!ValidTargets())
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.2f);
                // Call ienumerator OnNoValidAllies()
                yield return OnNoValidAllies();
                yield break;
            }

            // Run opponent logic then break
            if (base.Card.OpponentCard)
            {
                yield return OpponentSelectTarget();
                yield break;
            }

            // Run player logic
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            yield return PlayerSelectTarget();

            yield return OnValidTargetSelected(selectedSlot);

            CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();

            yield return base.LearnAbility(0.4f);

            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);

            yield return OnPostValidTargetSelected();
        }
        private IEnumerator PlayerSelectTarget()
        {
            CombatPhaseManagerPatch.Instance.VisualizeStartSniperAbility(base.Card.Slot);

            List<CardSlot> allSlotsCopy = Singleton<BoardManager>.Instance.AllSlotsCopy;
            allSlotsCopy.Remove(base.Card.Slot);

            List<CardSlot> targetSlots = GetInitialTargets();
            targetSlots.RemoveAll((CardSlot x) => x.Card == null || x.Card.Dead || this.CardIsNotValid(x.Card) || x.Card == base.Card);

            CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;

            if (cardSlot != null && targetSlots.Contains(cardSlot))
            {
                CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
            }
            selectedSlot = null;

            yield return Singleton<BoardManager>.Instance.ChooseTarget(allSlotsCopy, targetSlots, delegate (CardSlot s)
            {
                selectedSlot = s;
                CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(s, false);
            }, OnInvalidTarget, delegate (CardSlot s)
            {
                if (s.Card != null)
                {
                    CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(base.Card.Slot, s);
                }
            }, () => false, CursorType.Target);
        }
        private IEnumerator OpponentSelectTarget()
        {
            List<CardSlot> validTargets = GetInitialTargets();
            validTargets.RemoveAll((CardSlot x) => x.Card == null || x.Card.Dead || this.CardIsNotValid(x.Card) || x.Card == base.Card);
            yield return new WaitForSeconds(0.3f);
            yield return this.AISelectTarget(validTargets, delegate (CardSlot s)
            {
                selectedSlot = s;
            });
            if (selectedSlot != null && selectedSlot.Card != null)
            {
                CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(base.Card.Slot, selectedSlot);
                yield return new WaitForSeconds(0.3f);
            }

            CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(selectedSlot, false);
            yield return new WaitForSeconds(0.25f);

            yield return OnValidTargetSelected(selectedSlot);

            CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();

            yield return new WaitForSeconds(0.2f);

            yield return OnPostValidTargetSelected();
        }
        private void OnInvalidTarget(CardSlot slot)
        {
            if (slot.Card != null && this.CardIsNotValid(slot.Card) && !Singleton<TextDisplayer>.Instance.Displaying)
            {
                base.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear(InvalidTargetDialogue, 2.5f, 0f, Emotion.Anger));
            }
        }
        private IEnumerator AISelectTarget(List<CardSlot> validTargets, Action<CardSlot> chosenCallback)
        {
            if (validTargets.Count > 0)
            {
                bool positiveAbility = AbilitiesUtil.GetInfo(this.LatchAbility).PositiveEffect;
                validTargets.Sort((CardSlot a, CardSlot b) => this.AIEvaluateTarget(b.Card, positiveAbility) - this.AIEvaluateTarget(a.Card, positiveAbility));
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
            int num = card.PowerLevel;
            if (card.Info.HasTrait(Trait.Terrain))
            {
                num = 10 * ((!positiveEffect) ? 1 : (-1));
            }
            if (card.OpponentCard == positiveEffect)
            {
                num += 1000;
            }
            return num;
        }
        private bool ValidTargets()
        {
            List<CardSlot> validSlots = GetInitialTargets();
            validSlots.RemoveAll((CardSlot x) => x.Card == null || x.Card.Dead || this.CardIsNotValid(x.Card) || x.Card == base.Card);
            return validSlots.Count() > 0;
        }
        private List<CardSlot> GetInitialTargets()
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
