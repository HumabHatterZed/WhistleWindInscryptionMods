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
            const string rulebookDescription = "When [creature] is played, Bloom all opposing spaces.";
            const string dialogue = "From fertile flesh, a garden will soon bloom.";
            FlowerQueen.ID = AbnormalAbilityHelper.CreateAbility<FlowerQueen>(
                "sigilFlowerQueen",
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                modular: false, opponent: true, canStack: false)
                .SetSlotRedirect("Bloom", BloomingSlot.Id, Color.green)
                .Id;
        }
    }
    /// <summary>
    /// At the end of the owner's turn, [creature] Blooms its current space.
    /// </summary>
    public class FlowerQueen : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            foreach (CardSlot slot in BoardManager.Instance.GetSlotsCopy(base.Card.OpponentCard)) {
                if (slot.Card == null || slot.Card.LacksAbility(Scorching.ID) || slot.Card.HasAbility(Ability.Flying)) {
                    yield return slot.SetSlotModification(BloomingSlot.Id);
                }
            }
        }
        public override int Priority => 5; // trigger before strafe sigils
    }
}
