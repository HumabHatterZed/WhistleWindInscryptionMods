using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void BigBird_O0240()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };

            CardHelper.CreateCard(
                "wstl_bigBird", "Big Bird",
                "Its eyes light up the darkness like stars.",
                2, 4, 2, 0,
                Resources.bigBird,// Resources.bigBird_emission, gbcTexture: Resources.bigBird_pixel,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isChoice: true, onePerDeck: true, riskLevel: 4);
        }
    }
}