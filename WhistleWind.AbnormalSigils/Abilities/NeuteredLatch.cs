using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_NeuteredLatch() {
            const string rulebookName = "Neutered Latch";
            const string rulebookDescription = "Pay 3 Energy to choose a creature to gain the Neutered sigil.";
            const string dialogue = "The will to fight has been lost.";
            const string triggerText = "[creature] prevents the chosen creature from attacking.";
            NeuteredLatch.ID = AbnormalAbilityHelper.CreateActivatedAbility<NeuteredLatch>(
                "sigilNeuteredLatch",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3)
                .SetAbilityRedirect("Neutered", Neutered.ID, GameColors.Instance.gray)
                .Id;
        }
    }
    /// <summary>
    /// Pay 3 Energy to choose a creature to gain the Neutered sigil.
    /// </summary>
    public class NeuteredLatch : ActivatedSelectSlotBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override Ability LatchAbility => Neutered.ID;
        public override int StartingEnergyCost => 3;

        public override bool IsValidTarget(CardSlot slot) {
            if (!base.IsValidTarget(slot))
                return false;

            return slot.Card.LacksAbility(Neutered.ID) && slot.Card.Attack > 0;
        }
    }
}
