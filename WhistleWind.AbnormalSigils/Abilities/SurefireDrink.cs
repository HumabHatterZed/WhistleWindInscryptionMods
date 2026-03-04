using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_SurefireDrink() {
            const string rulebookName = "Surefire Soda";
            const string rulebookDescription = "The selected card will gain Sniper for this turn.";
            SurefireDrink.ID = AbnormalAbilityHelper.CreateAbility<SurefireDrink>(
                "sigilSurefireDrink",
                rulebookName, rulebookDescription, powerLevel: 2,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// The selected card will have Sniper for this turn.
    /// </summary>
    public class SurefireDrink : SodaAbilityBehaviour {
        public const Ability abilityToAdd = Ability.Sniper;
        public const string id = "SurefireDrunk";

        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override string SingletonId => id;
        public override Ability AbilityToAdd => abilityToAdd;
        public override SpecialTriggeredAbility StatusEffectId => SurefireDrinkEffect.specialAbility;
        public override int BasePotency => 1;
    }
}
