using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheFirebird_O02101()
        {
            List<Ability> abilities = new()
            {
                Burning.ability,
                Ability.Flying
            };
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };

            CreateCard(
                "wstl_theFirebird", "The Firebird",
                "A bird that longs for the thrill of being hunted.",
                atk: 2, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.theFirebird, Artwork.theFirebird_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw);
        }
    }
}