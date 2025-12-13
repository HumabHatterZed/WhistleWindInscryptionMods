using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.Core.AbilityClasses {
    public abstract class NoFirstPersonTargetSlotItem : TargetSlotItem {
        public override string FirstPersonPrefabId => null;
        public override Vector3 FirstPersonItemPos => Vector3.zero;
        public override Vector3 FirstPersonItemEulers => Vector3.zero;
        public override CursorType SelectionCursorType => CursorType.Target;

        public override IEnumerator ActivateSequence() {
            base.PlayExitAnimation();
            yield return new WaitForSeconds(0.1f);
            Singleton<UIManager>.Instance.Effects.GetEffect<EyelidMaskEffect>()?.SetIntensity(0.6f, 0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(this.SelectionView);
            yield return new WaitForSeconds(0.25f);

            Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
            CardSlot target = null;
            List<CardSlot> validTargets = this.GetValidTargets();
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            yield return Singleton<BoardManager>.Instance.ChooseTarget(this.GetAllTargets(), validTargets, delegate (CardSlot slot) {
                target = slot;
            }, OnInvalidTargetSelected, delegate (CardSlot slot) {
            }, () => Singleton<ViewManager>.Instance.CurrentView != this.SelectionView || !Singleton<TurnManager>.Instance.IsPlayerMainPhase, this.SelectionCursorType);
            if (target != null) {
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
                Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
                yield return this.OnValidTargetSelected(target, null);
            }
            else {
                base.ActivationCancelled = true;
            }
            Singleton<UIManager>.Instance.Effects.GetEffect<EyelidMaskEffect>()?.SetIntensity(0f, 0.2f);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
    }
}