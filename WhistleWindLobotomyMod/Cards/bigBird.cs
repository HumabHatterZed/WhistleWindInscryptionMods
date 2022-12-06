using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
            LobotomyCardHelper.CreateCard(
                "wstl_bigBird", "Big Bird",
                "Its eyes light up the darkness like stars.",
                atk: 2, hp: 4,
                blood: 3, bones: 0, energy: 0,
                Artwork.bigBird, Artwork.bigBird_emission, pixelTexture: Artwork.bigBird_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw);
        }
    }
}