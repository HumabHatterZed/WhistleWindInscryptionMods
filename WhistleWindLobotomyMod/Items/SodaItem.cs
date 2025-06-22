using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWindLobotomyMod {
    public abstract class SodaItem : NoFirstPersonTargetSlotItem {
        public abstract Ability AbilityToAdd { get; }
        public abstract SpecialTriggeredAbility StatusEffect { get; }
        public abstract string ID { get; }
        public abstract int TurnsApplied { get; }
        public override View SelectionView => View.Board;
        public override bool ExtraActivationPrerequisitesMet() => GetValidTargets().Count > 0;
        public override IEnumerator OnValidTargetSelected(CardSlot target, GameObject firstPersonItem) {
            PlayableCard targetCard = target.Card;
            AudioController.Instance.PlaySound3D("soda_open", MixerGroup.TableObjectsSFX, target.transform.position, 1f, 0.1f);
            yield return SodaAbilityBehaviour.Sequence(targetCard, StatusEffect, AbilityToAdd, TurnsApplied, ID);
            yield return new WaitForSeconds(0.5f);
        }

        public override void OnInvalidTargetSelected(CardSlot targetSlot) {
            if (targetSlot.Card != null) {
                if (targetSlot.Card.Info.HasTrait(Trait.Uncuttable)) {
                    CustomCoroutine.Instance.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear("This one isn't thirsty.", 2.5f, 0f, Emotion.Anger));
                }
                else if (targetSlot.Card == null) {
                    CustomCoroutine.Instance.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear("Don't pour soda onto the board.", 3f));
                }
            }
        }

        public override List<CardSlot> GetAllTargets() {
            return Singleton<BoardManager>.Instance.AllSlotsCopy;
        }

        public override List<CardSlot> GetValidTargets() {
            List<CardSlot> allSlotsCopy = Singleton<BoardManager>.Instance.AllSlotsCopy;
            allSlotsCopy.RemoveAll(x => x.Card == null || x.Card.Info.HasTrait(Trait.Uncuttable));
            return allSlotsCopy;
        }
    }
}
