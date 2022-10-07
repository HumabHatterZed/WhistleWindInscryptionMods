using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public abstract class ActivatedLatchBehaviour : ActivatedAbilityBehaviour
    {
        public CardSlot selectedSlot = null;
        public abstract Ability LatchAbility { get; }
        public virtual bool TargetAll => true;
        public virtual bool TargetAllies => false;
        public virtual string NoAlliesDialogue => "No allies.";
        public virtual string NullTargetDialogue => "You can't target the air.";
        public virtual string SelfTargetDialogue => "You must choose one of your other cards.";

        private bool ActivatedThisTurn;

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.Card.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            ActivatedThisTurn = false;
            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.2f);
        }

        public override bool CanActivate()
        {
            if (!ActivatedThisTurn)
            {
                List<CardSlot> validTargets = Singleton<BoardManager>.Instance.AllSlotsCopy;
                validTargets.RemoveAll((CardSlot x) => x.Card == null || x.Card.Dead || this.CardHasLatchMod(x.Card) || x.Card == base.Card);
                return validTargets.Count > 0;
            }
            return false;
        }
        public override IEnumerator Activate()
        {
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
                yield return OpponentSelectTarget();
                yield break;
            }

            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            yield return PlayerSelectTarget();

            yield return OnValidTargetSelected(selectedSlot);

            CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();

            yield return new WaitForSeconds(0.2f);
            yield return base.LearnAbility();

            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, false);

            yield return OnPostValidTargetSelected();
        }

        public virtual IEnumerator OnNoValidAllies()
        {
            yield return CustomMethods.PlayAlternateDialogue(dialogue: NoAlliesDialogue);
        }
        public virtual IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            if (slot != null && slot.Card != null)
            {
                CardModificationInfo cardModificationInfo = new(this.LatchAbility) { fromTotem = true, singletonId = "wstl:ActivatedLatch"};

                slot.Card.Anim.LightNegationEffect();
                slot.Card.AddTemporaryMod(cardModificationInfo);
                yield return new WaitForSeconds(0.75f);
                yield return base.LearnAbility();
            }
        }
        public virtual IEnumerator OnPostValidTargetSelected()
        {
            ActivatedThisTurn = true;
            yield break;
        }

        public IEnumerator OpponentSelectTarget()
        {
            List<CardSlot> validTargets = PossibleTargets();
            validTargets.RemoveAll((CardSlot x) => x.Card == null || x.Card.Dead || this.CardHasLatchMod(x.Card) || x.Card == base.Card);
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

        public IEnumerator PlayerSelectTarget()
        {
            CombatPhaseManagerPatch.Instance.VisualizeStartSniperAbility(base.Card.Slot);

            List<CardSlot> allSlotsCopy = Singleton<BoardManager>.Instance.AllSlotsCopy;
            allSlotsCopy.Remove(base.Card.Slot);

            List<CardSlot> targetSlots = PossibleTargets();
            targetSlots.RemoveAll((CardSlot x) => x.Card == null || x.Card.Dead || this.CardHasLatchMod(x.Card) || x.Card == base.Card);

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
            List<CardSlot> validSlots = PossibleTargets();
            validSlots.RemoveAll((CardSlot x) => x.Card == null || x.Card.Dead || this.CardHasLatchMod(x.Card) || x.Card == base.Card);
            return validSlots.Count() > 0;
        }

        private void OnInvalidTarget(CardSlot slot)
        {
            if (slot.Card != null && this.CardHasLatchMod(slot.Card) && !Singleton<TextDisplayer>.Instance.Displaying)
            {
                base.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear("It's already latched...", 2.5f, 0f, Emotion.Anger));
            }
        }

        private bool CardHasLatchMod(PlayableCard card)
        {
            return card.TemporaryMods.Exists((CardModificationInfo m) => m.fromLatch || m.singletonId == "wstl:ActivatedLatch");
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
