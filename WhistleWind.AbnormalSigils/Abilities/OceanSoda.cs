using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_OceanSoda() {
            const string rulebookName = "Ocean Soda";
            const string rulebookDescription = "The selected card will become Waterborne for 3 turns.";
            OceanSoda.ID = AbnormalAbilityHelper.CreateAbility<OceanSoda>(
                "sigilOceanSoda",
                rulebookName, rulebookDescription, powerLevel: 0,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// The selected card will become Waterborne for 3 turns.
    /// </summary>
    public class OceanSoda : SodaAbilityBehaviour {
        public const Ability abilityToAdd = Ability.Submerge;
        public const string id = "OceanSoda";

        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override string SingletonId => id;
        public override Ability AbilityToAdd => abilityToAdd;
        public override SpecialTriggeredAbility StatusEffectId => OceanSodaEffect.specialAbility;
    }
}
