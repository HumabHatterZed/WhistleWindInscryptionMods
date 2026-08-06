using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Slots;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_FlowerQueen() {
            const string rulebookName = "Flower Queen";
            const string rulebookDescription = "When [creature] is played, Bloom all spaces on its owner's side of the board. When moving to a new space, re-Bloom it.";
            const string dialogue = "From fertile flesh, a garden will soon bloom.";
            FlowerQueen.ID = AbnormalAbilityHelper.CreateAbility<FlowerQueen>(
                "sigilFlowerQueen",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: true, canStack: false)
                .SetSlotRedirect("Blooms", BloomingSlot.ID, Color.green)
                .Id;
        }
    }
    /// <summary>
    /// Whenever [creature] is assigned to a new space, Bloom it.
    /// </summary>
    public class FlowerQueen : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        private bool hasResolved = false;

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) {
            return otherCard == base.Card && hasResolved;
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard) {
            yield return base.Card.Slot.SetSlotModification(BloomingSlot.ID);
        }

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            hasResolved = true;
            foreach (CardSlot slot in BoardManager.Instance.GetSlots(!base.Card.OpponentCard)) {
                if (slot.GetSlotModification() == BloomingSlot.ID) {
                    slot.GetComponent<BloomingSlot>().Potency = 3;
                }
                else {
                    yield return slot.SetSlotModification(BloomingSlot.ID);
                }
            }
            yield return base.LearnAbility(0.5f);
        }
        //public override int Priority => 5; // trigger before strafe sigils
    }
}
