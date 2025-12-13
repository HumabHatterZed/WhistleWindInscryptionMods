using DiskCardGame;
using WhistleWind.AbnormalSigils;

namespace WhistleWindLobotomyMod {
    public class FizzyLifterItem : SodaItem {
        public override string ID => FizzyLifter.id;
        public override Ability AbilityToAdd => Ability.Flying;
        public override SpecialTriggeredAbility StatusEffect => FizzyLifterEffect.specialAbility;
        public override int TurnsApplied => 2;
    }
}
