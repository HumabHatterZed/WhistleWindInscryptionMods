using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_FizzyLifter() {
            const string rulebookName = "Fizzy Lifter";
            const string rulebookDescription = "The selected card will become Airborne for 3 turns.";
            FizzyLifter.ID = AbnormalAbilityHelper.CreateAbility<FizzyLifter>(
                "sigilFizzyLifter",
                rulebookName, rulebookDescription, powerLevel: 0,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// The selected card will become Airborne for 3 turns.
    /// </summary>
    public class FizzyLifter : SodaAbilityBehaviour {
        public const Ability abilityToAdd = Ability.Flying;
        public const string id = "FizzyLifted";

        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override string SingletonId => id;
        public override Ability AbilityToAdd => abilityToAdd;
        public override SpecialTriggeredAbility StatusEffectId => FizzyLifterEffect.specialAbility;
    }
}
