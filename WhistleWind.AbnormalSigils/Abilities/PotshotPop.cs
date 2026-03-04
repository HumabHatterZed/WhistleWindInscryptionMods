using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_PotshotPop() {
            const string rulebookName = "Potshot Pop";
            const string rulebookDescription = "The selected card will gain Sentry for 2 turns.";
            PotshotPop.ID = AbnormalAbilityHelper.CreateAbility<PotshotPop>(
                "sigilPotshotPop",
                rulebookName, rulebookDescription, powerLevel: 2,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// The selected card will gain Sentry for 2 turns.
    /// </summary>
    public class PotshotPop : SodaAbilityBehaviour {
        public const Ability abilityToAdd = Ability.Sentry;
        public const string id = "PotshotPopped";

        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override string SingletonId => id;
        public override Ability AbilityToAdd => abilityToAdd;
        public override SpecialTriggeredAbility StatusEffectId => PotshotPopEffect.specialAbility;
        public override int BasePotency => 2;
    }
}
