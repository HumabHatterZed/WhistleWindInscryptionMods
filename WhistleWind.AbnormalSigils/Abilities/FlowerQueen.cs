using DiskCardGame;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Slots;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_FlowerQueen() {
            const string rulebookName = "Flower Queen";
            const string rulebookDescription = "At the end of the owner's turn, [creature] Blooms its current space.";
            const string dialogue = "From fertile flesh, a garden will soon bloom.";
            FlowerQueen.ability = AbnormalAbilityHelper.CreateAbility<FlowerQueen>(
                "sigilFlowerQueen",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: true, canStack: false)
                .SetSlotRedirect("Blooms", BloomingSlot.Id, Color.green)
                .Id;
        }
    }
    /// <summary>
    /// At the end of the owner's turn, [creature] Blooms its current space.
    /// </summary>
    public class FlowerQueen : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd && base.Card.Slot.GetSlotModification() != BloomingSlot.Id;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.LightNegationEffect();
            yield return base.Card.Slot.SetSlotModification(BloomingSlot.Id);
            yield return new WaitForSeconds(0.2f);
            yield return base.LearnAbility(0.3f);
        }

        public override int Priority => 5; // trigger before strafe sigils
    }
}
