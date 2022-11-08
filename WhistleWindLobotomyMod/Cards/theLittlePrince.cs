using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheLittlePrince_O0466()
        {
            List<Ability> abilities = new()
            {
                Spores.ability
            };
            CardHelper.CreateCard(
                "wstl_theLittlePrince", "The Little Prince",
                "A giant mushroom chunk. A mist of spores surrounds it.",
                atk: 1, hp: 4,
                blood: 2, bones: 0, energy: 0,
                Artwork.theLittlePrince, Artwork.theLittlePrince_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}