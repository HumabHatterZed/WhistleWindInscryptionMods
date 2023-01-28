using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_CENSORED_O0389()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CensoredSpecial.specialAbility
            };
            LobotomyCardHelper.CreateCard(
                "wstl_censored", "CENSORED",
                "It's best you never learn what it looks like.",
                atk: 7, hp: 4,
                blood: 4, bones: 0, energy: 0,
                Artwork.censored, Artwork.censored_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: LobotomyCardHelper.RiskLevel.Aleph);
        }
    }
}