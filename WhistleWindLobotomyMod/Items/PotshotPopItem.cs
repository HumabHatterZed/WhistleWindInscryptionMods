using DiskCardGame;
using WhistleWind.AbnormalSigils;

namespace WhistleWindLobotomyMod {
    public class PotshotPopItem : SodaItem {
        public override string ID => PotshotPop.id;
        public override Ability AbilityToAdd => Ability.Sentry;
        public override SpecialTriggeredAbility StatusEffect => PotshotPopEffect.specialAbility;
        public override int TurnsApplied => 2;
    }
}
