using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Laetitia_O0167()
        {
            List<Ability> abilities = new()
            {
                GiftGiver.ability
            };

            CardHelper.CreateCard(
                "wstl_laetitia", "Laetitia",
                "A little witch carrying a heart-shaped gift.",
                1, 2, 1, 0,
                Artwork.laetitia, Artwork.laetitia_emission, pixelTexture: Artwork.laetitia_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}