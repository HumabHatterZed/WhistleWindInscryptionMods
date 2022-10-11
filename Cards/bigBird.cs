using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_BigBird_O0240()
        {
            List<Ability> abilities = new()
            {
                NeuteredLatch.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                ThreeBirds.specialAbility
            };

            CardHelper.CreateCard(
                "wstl_bigBird", "Big Bird",
                "Its eyes light up the darkness like stars.",
                2, 4, 2, 0,
                Artwork.bigBird, Artwork.bigBird_emission, pixelTexture: Artwork.bigBird_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}