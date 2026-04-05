using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;

namespace WhistleWind.AbnormalSigils {
    /// <summary>
    /// Version of vanilla CreateCardsAdjacent class where the left and right card IDs are defined separately.
    /// </summary>
    public abstract class CreateTwoCardsAdjacent : AbilityBehaviour {
        protected abstract string LeftSpawnedCardId { get; }
        protected abstract string RightSpawnedCardId { get; }
        protected abstract string CannotSpawnDialogue { get; }

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            CardSlot toLeft = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: true);
            CardSlot toRight = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: false);
            bool toLeftValid = toLeft != null && toLeft.Card == null;
            bool toRightValid = toRight != null && toRight.Card == null;
            yield return base.PreSuccessfulTriggerSequence();
            if (toLeftValid) {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toLeft, LeftSpawnedCardId);
            }
            if (toRightValid) {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toRight, RightSpawnedCardId);
            }
            if (toLeftValid || toRightValid) {
                yield return base.LearnAbility();
            }
            else if (!base.HasLearned) {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(this.CannotSpawnDialogue, -0.65f, 0.4f);
            }
        }

        private IEnumerator SpawnCardOnSlot(CardSlot slot, string alternateSpawnCardId) {
            CardInfo cardByName = CardLoader.GetCardByName(alternateSpawnCardId);
            this.ModifySpawnedCard(cardByName);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, slot, 0.15f);
        }

        private void ModifySpawnedCard(CardInfo card) {
            StatusEffectPatches.ModifySpawnedCardStatusEffects(this.Card, card, this.Ability);
        }
    }
}
