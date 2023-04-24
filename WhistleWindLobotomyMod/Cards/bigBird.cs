using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BigBird_O0240()
        {
            List<Ability> abilities = new() { Cycler.ability };
            List<Tribe> tribes = new() { Tribe.Bird };
            List<Trait> traits = new() { TraitEmeraldCity };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                ThreeBirds.specialAbility
            };
            CreateCard(
                "wstl_bigBird", "Big Bird",
                "Its eyes light up the darkness like stars.",
                atk: 2, hp: 4,
                blood: 2, bones: 0, energy: 0,
                Artwork.bigBird, Artwork.bigBird_emission, pixelTexture: Artwork.bigBird_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits, onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw,
                evolveName: "[name]Bigger Bird");
        }
    }
}