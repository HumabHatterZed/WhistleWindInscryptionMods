using WhistleWind.Core.Helpers;
using DiskCardGame;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Patches;

namespace WhistleWind.AbnormalSigils
{
    // Logic for abilities that have the player select a slot to be targeted
    // By default acts like Latch but can be overriden as needed
    public abstract class ActivatedSelectSlotBehaviour : BetterActivatedAbilityBehaviour
    {
        // store claw prefab here
        private static GameObject _clawPrefab;

        // claw prefab we'll be referencing
        private static GameObject ClawPrefab
        {
            get
            {
                if (_clawPrefab == null)
                    _clawPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/LatchClaw");

                return _clawPrefab;
            }
        }
        private static void AimWeaponAnim(GameObject tweenObj, Vector3 target) => Tween.LookAt(tweenObj.transform, target, Vector3.up, 0.075f, 0.0f, Tween.EaseInOut);

        public CardSlot selectedSlot = null;

        // Latches nothing by default
        public virtual Ability LatchAbility => Ability.None;
        public virtual bool TargetAll => true;
        public virtual bool TargetAllies => false;
        // see if we can target empty card slots (does GetValidTargets allow empty slots?)
        private bool CanTargetNull => GetValidTargets().Exists((CardSlot s) => s.Card == null);
        public virtual string NoTargetsDialogue => "There are no cards you can choose.";
        public virtual string InvalidTargetDialogue => "It's already latched...";

        // by default, can always activate
        public virtual int TurnDelay => -1;
        private int turnDelay = 0;
        public virtual IEnumerator OnNoValidTargets()
        {
            yield break;
        }
        public virtual IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            // Perform latch logic by default
            // Though since LatchABility is None by default, nothing will actually happen
            if (LatchAbility != Ability.None && slot != null && slot.Card != null)
            {
                CardModificationInfo cardModificationInfo = new(this.LatchAbility) { fromTotem = true, fromLatch = true };

                slot.Card.Anim.LightNegationEffect();
                slot.Card.AddTemporaryMod(cardModificationInfo);
                yield return new WaitForSeconds(0.75f);
                yield return base.LearnAbility();
            }
        }
        public virtual IEnumerator OnPostValidTargetSelected()
        {
            yield break;
        }
        public virtual bool CardIsNotValid(PlayableCard card)
        {
            // By default returns whether the card is latched
            return card.TemporaryMods.Exists((CardModificationInfo m) => m.fromLatch);
        }
        private bool CardSlotIsNotValid(CardSlot slot)
        {
            if (slot.Card != null)
                return CardIsNotValid(slot.Card);
            return CanTargetNull;
        }
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.Card.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            // if turnDelay is above 0, reduce it by 1
            if (turnDelay > 0)
            {
                turnDelay--;
                base.Card.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.2f);
            }
        }

        public override bool CanActivate()
        {
            // If turnDelay is negative, can always activate
            if (turnDelay < 0)
                return true;

            return ValidTargetsExist();
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

            yield return new WaitForSeconds(0.1f);

            // get the animation controller
            CardAnimationController anim = this.Card.Anim;

            // create a game object to connect the prefab to this card
            GameObject latchParentGameObject = new GameObject
            {
                name = "LatchParent",
                transform =
                {
                    position = anim.transform.position
                }
            };
            latchParentGameObject.transform.SetParent(anim.transform);

            // create claw object using claw prefab and latchParent
            Transform latchParent = latchParentGameObject.transform;
            GameObject claw = UnityEngine.Object.Instantiate(ClawPrefab, latchParent);

            // get the cannon material if possible, and set the render materials to it
            Material cannonMat = null;
            try
            {
                cannonMat = new Material(ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CannonTargetIcon").GetComponentInChildren<Renderer>().material);
            }
            catch { }
            if (cannonMat != null)
            {
                Renderer[] renderers = claw.GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in renderers.Where(rend => rend))
                {
                    rend.material = cannonMat;
                }
            }

            // set up the sniper visualiser
            CombatPhaseManager instance = Singleton<CombatPhaseManager>.Instance;
            WstlPart1SniperVisualiser visualiser = null;
            if ((SaveManager.SaveFile?.IsPart1).GetValueOrDefault())
                visualiser = instance.GetComponent<WstlPart1SniperVisualiser>() ?? instance.gameObject.AddComponent<WstlPart1SniperVisualiser>();

            // Run opponent logic then break
            if (base.Card.OpponentCard)
            {
                yield return OpponentSelectTarget(instance, visualiser);
                if (selectedSlot != null && selectedSlot.Card != null)
                {
                    AimWeaponAnim(latchParent.gameObject, selectedSlot.transform.position);
                    yield return new WaitForSeconds(0.3f);
                }
            }
            else
            {
                // Run player logic
                Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode);
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

                yield return PlayerSelectTarget(latchParent, instance, visualiser);
            }

            // clear the sniper icons here so they disappear before the claw does its thing
            instance.VisualizeClearSniperAbility();
            visualiser?.VisualizeClearSniperAbility();

            // once a target is selected, run logic
            yield return OnValidTargetSelected(selectedSlot);

            // claw thing
            claw.SetActive(true);

            CustomCoroutine.FlickerSequence(
                () => claw.SetActive(true),
                () => claw.SetActive(false),
                true,
                false,
                0.05f,
                2
            );

            yield return base.LearnAbility(0.4f);
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            yield return new WaitForSeconds(0.2f);

            // reset the turn delay
            if (turnDelay == 0)
                turnDelay = TurnDelay;

            yield return OnPostValidTargetSelected();

            if (!base.Card.OpponentCard)
                yield return HelperMethods.ChangeCurrentView(View.Default);
        }
        private IEnumerator PlayerSelectTarget(Transform latchParent, CombatPhaseManager instance, WstlPart1SniperVisualiser visualiser)
        {
            // call both together, because that's how it's done in the API
            // and I'm not going to mess with what ain't broke
            instance.VisualizeStartSniperAbility(base.Card.Slot);
            visualiser?.VisualizeStartSniperAbility(base.Card.Slot);

            List<CardSlot> possibleTargets = GetInitialTargets();
            possibleTargets.Remove(base.Card.Slot);

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
                visualiser?.VisualizeConfirmSniperAbility(s);
            }, OnInvalidTarget, delegate (CardSlot s)
            {
                if (CanTargetNull || s.Card != null)
                {
                    instance.VisualizeAimSniperAbility(base.Card.Slot, s);
                    visualiser?.VisualizeAimSniperAbility(base.Card.Slot, s);
                    AimWeaponAnim(latchParent.gameObject, s.transform.position);
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
            if (selectedSlot != null && selectedSlot.Card != null)
            {
                instance.VisualizeAimSniperAbility(base.Card.Slot, selectedSlot);
                visualiser?.VisualizeAimSniperAbility(base.Card.Slot, selectedSlot);
                yield return new WaitForSeconds(0.3f);
            }

            instance.VisualizeConfirmSniperAbility(selectedSlot);
            visualiser?.VisualizeConfirmSniperAbility(selectedSlot);
            yield return new WaitForSeconds(0.25f);
        }
        private void OnInvalidTarget(CardSlot slot)
        {
            if (CardSlotIsNotValid(slot) && !Singleton<TextDisplayer>.Instance.Displaying)
            {
                base.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear(InvalidTargetDialogue, 2.5f, 0f, Emotion.Anger));
            }
        }
        private IEnumerator AISelectTarget(List<CardSlot> validTargets, Action<CardSlot> chosenCallback)
        {
            if (validTargets.Count > 0)
            {
                // if latch isn't None return default bool, other return whether we're targeting allies or not
                bool positiveAbility = this.LatchAbility != Ability.None ? AbilitiesUtil.GetInfo(this.LatchAbility).PositiveEffect : TargetAllies;
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
        public bool ValidTargetsExist()
        {
            return GetValidTargets().Count > 0;
        }
        public virtual Predicate<CardSlot> InvalidTargets()
        {
            // by default remove empty slots, dead cards, invalid cards, or this card
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

            return base.Card.OpponentCard ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;
        }
    }
}
