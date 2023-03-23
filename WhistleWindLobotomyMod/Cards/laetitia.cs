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
        private void Card_Laetitia_O0167()
        {
            List<Ability> abilities = new() { GiftGiver.ability };
            List<Tribe> tribes = new() { TribeFae };

            CreateCard(
                "wstl_laetitia", "Laetitia",
                "A little witch carrying a heart-shaped gift.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.laetitia, Artwork.laetitia_emission, pixelTexture: Artwork.laetitia_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He);
        }
    }
}