using DiskCardGame;
using WhistleWind.AbnormalSigils;

namespace WhistleWindLobotomyMod {
    public class SurefireDrinkItem : SodaItem {
        public override string ID => SurefireDrink.id;
        public override Ability AbilityToAdd => Ability.Sniper;
        public override SpecialTriggeredAbility StatusEffect => SurefireDrinkEffect.specialAbility;
        public override int TurnsApplied => 1;
    }
}
