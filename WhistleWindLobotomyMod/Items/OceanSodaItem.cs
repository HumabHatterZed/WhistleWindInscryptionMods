using DiskCardGame;
using WhistleWind.AbnormalSigils;

namespace WhistleWindLobotomyMod {
    public class OceanSodaItem : SodaItem {
        public override string ID => OceanSoda.id;
        public override Ability AbilityToAdd => Ability.Submerge;
        public override SpecialTriggeredAbility StatusEffect => OceanSodaEffect.specialAbility;
        public override int TurnsApplied => 2;
    }
}
