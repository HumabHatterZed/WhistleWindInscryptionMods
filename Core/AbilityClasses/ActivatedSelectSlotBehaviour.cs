using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionCommunityPatch.Card;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWind.Core.AbilityClasses
{
    // Logic for abilities that have the player select a slot to be targeted
    // By default acts like Latch but can be overriden as needed
    public abstract class ActivatedSelectSlotBehaviour : ExtendedActivatedAbilityBehaviour
    {
        public CardSlot selectedSlot = null;
        public List<CardSlot> ValidTargets
        {
            get
            {
                List<CardSlot> allTargets = Singleton<BoardManager>.Instance.AllSlotsCopy;
                allTargets.RemoveAll(t => !IsValidTarget(t));
                return allTargets;
            }
        }

        private bool CanTargetNull => ValidTargets.Any(x => x.Card == null);
        public virtual bool IsValidTarget(CardSlot slot)
        {
            if (slot.Card != null && slot.Card != base.Card && !slot.Card.Dead)
            {
                return !SaveManager.SaveFile.IsPart3 || !slot.Card.TemporaryMods.Exists(m => m.fromLatch);
            }
            return false;
        }
        public virtual string NoTargetsDialogue => "There are no cards you can choose.";
        public virtual string NullTargetDialogue => "You can't target the air.";
        public virtual string SelfTargetDialogue => "You must choose one of your other cards.";
        public virtual string InvalidTargetDialogue(CardSlot slot) => "It's already latched...";

        public virtual Ability LatchAbility => Ability.None; // Latches nothing by default
        private bool ShowLatch => LatchAbility != Ability.None;

        private int turnDelay = 0;
        public virtual int TurnDelay => -1;// by default, can always activate

        public virtual IEnumerator OnNoValidTargets() { yield break; }

        public virtual IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            // Perform latch logic by default
            // Though since LatchABility is None by default, nothing will actually happen
            if (LatchAbility != Ability.None && slot != null && slot.Card != null)
            {
                CardModificationInfo cardModificationInfo = new(LatchAbility) { fromLatch = SaveManager.SaveFile.IsPart3 };
                slot.Card.Anim.LightNegationEffect();
                slot.Card.AddTemporaryMod(cardModificationInfo);
                yield return new WaitForSeconds(0.75f);
                yield return base.LearnAbility();
            }
        }
        public virtual IEnumerator OnPostValidTargetSelected(CardSlot slot = null) { yield break; }

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (turnDelay > 0) // if turnDelay is above 0, reduce it by 1
            {
                turnDelay--;
                if (turnDelay == 0)
                {
                    yield return HelperMethods.ChangeCurrentView(View.Board);
                    base.Card.Anim.LightNegationEffect();
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
        public override bool CanActivate() => turnDelay <= 0;
        public override IEnumerator Activate()
        {
            yield return base.PreSuccessfulTriggerSequence();

            // Lock the view so players can't mess it up
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;

            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.2f);

            if (ValidTargets.Count == 0) // If there are no valid targets, break
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.45f);

                yield return DialogueHelper.PlayAlternateDialogue(dialogue: NoTargetsDialogue);
                yield return OnNoValidTargets();
                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
                yield break;
            }

            yield return new WaitForSeconds(0.1f);

            // get the animation controller
            Transform latchParent = null;
            GameObject claw = null;
            if (ShowLatch)
            {
                CardAnimationController anim = this.Card.Anim;

                // create a game object to connect the prefab to this card
                GameObject latchParentGameObject = new()
                {
                    name = "LatchParent",
                    transform = { position = anim.transform.position }
                };
                latchParentGameObject.transform.SetParent(anim.transform);

                // create claw object using claw prefab and latchParent
                latchParent = latchParentGameObject.transform;
                claw = Instantiate(ClawPrefab, latchParent);

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
                        rend.material = cannonMat;
                }
            }

            // set up the sniper visualiser
            CombatPhaseManager instance = Singleton<CombatPhaseManager>.Instance;
            Part1SniperVisualizer visualiser = null;
            if ((SaveManager.SaveFile?.IsPart1).GetValueOrDefault())
                visualiser = instance.GetComponent<Part1SniperVisualizer>() ?? instance.gameObject.AddComponent<Part1SniperVisualizer>();

            // Run opponent logic then break
            if (base.Card.OpponentCard)
            {
                yield return OpponentSelectTarget(instance, visualiser);
                if (selectedSlot != null && selectedSlot.Card != null)
                {
                    if (ShowLatch)
                        AimWeaponAnim(latchParent.gameObject, selectedSlot.transform.position);
                    yield return new WaitForSeconds(0.3f);
                }
            }
            else
            {
                // Run player logic
                Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode);
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

                yield return PlayerSelectTarget(instance, visualiser, latchParent);
            }

            // clear the sniper icons here so they disappear before the claw does its thing
            instance.VisualizeClearSniperAbility();
            visualiser?.VisualizeClearSniperAbility();

            // once a target is selected, run logic
            yield return OnValidTargetSelected(selectedSlot);

            // claw thing
            if (ShowLatch)
            {
                claw.SetActive(true);

                CustomCoroutine.FlickerSequence(
                    () => claw.SetActive(true),
                    () => claw.SetActive(false),
                    true,
                    false,
                    0.05f,
                    2
                );
            }

            yield return base.LearnAbility(0.4f);
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            yield return new WaitForSeconds(0.2f);

            // reset the turn delay
            if (turnDelay == 0)
                turnDelay = TurnDelay;

            yield return OnPostValidTargetSelected(selectedSlot);

            if (!base.Card.OpponentCard)
                yield return HelperMethods.ChangeCurrentView(View.Default);
        }
        private IEnumerator PlayerSelectTarget(CombatPhaseManager instance, Part1SniperVisualizer visualiser, Transform latchParent = null)
        {
            // call both together, because that's how it's done in the API
            // and I'm not going to mess with what ain't broke
            instance.VisualizeStartSniperAbility(base.Card.Slot);
            visualiser?.VisualizeStartSniperAbility(base.Card.Slot);

            CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;

            if (cardSlot != null && ValidTargets.Contains(cardSlot))
            {
                instance.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
                visualiser?.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
            }
            selectedSlot = null;

            yield return Singleton<BoardManager>.Instance.ChooseTarget(BoardManager.Instance.AllSlotsCopy, ValidTargets, delegate (CardSlot s)
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
                    if (ShowLatch)
                        AimWeaponAnim(latchParent.gameObject, s.transform.position);
                }
            }, () => false, CursorType.Target);
        }
        private IEnumerator OpponentSelectTarget(CombatPhaseManager instance, Part1SniperVisualizer visualiser)
        {
            List<CardSlot> validTargets = ValidTargets;

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
            if (!IsValidTarget(slot) && !Singleton<TextDisplayer>.Instance.Displaying)
            {
                string dialogue;
                if (slot.Card != null)
                {
                    if (slot.Card == base.Card)
                        dialogue = SelfTargetDialogue;
                    else
                        dialogue = InvalidTargetDialogue(slot); // slot.Card is never null
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
                // if latch isn't None return default bool, other return whether we're targeting allies or not
                bool positiveAbility = this.LatchAbility != Ability.None ? AbilitiesUtil.GetInfo(this.LatchAbility).PositiveEffect : ValidTargets.Any(x => x.IsPlayerSlot == base.Card.Slot.IsPlayerSlot);
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
            if (card == null)
            {
                if (CanTargetNull)
                    return UnityEngine.Random.Range(0, 5);
                return -1000;
            }
            int num = card.PowerLevel;
            if (card.Info.HasAnyOfTraits(Trait.Terrain, Trait.Pelt))
                num = 10 * (!positiveEffect ? 1 : -1);
            if (card.OpponentCard == positiveEffect)
                num += 1000;
            return num;
        }

        private static GameObject _clawPrefab; // store claw prefab here
        private static GameObject ClawPrefab // claw prefab we'll be referencing
        {
            get
            {
                if (Act1LatchAbilityFix._clawPrefab != null) // use the Latch Fix's claw prefab if it's not null
                    return Act1LatchAbilityFix._clawPrefab;

                if (_clawPrefab == null) // otherwise use the default
                    _clawPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/LatchClaw");

                return _clawPrefab;
            }
        }
        private static void AimWeaponAnim(GameObject tweenObj, Vector3 target) => Tween.LookAt(tweenObj.transform, target, Vector3.up, 0.075f, 0.0f, Tween.EaseInOut);

    }
}