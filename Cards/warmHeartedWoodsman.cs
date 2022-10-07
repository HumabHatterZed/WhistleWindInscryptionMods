using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_WarmHeartedWoodsman_F0532()
        {
            List<Ability> abilities = new()
            {
                Woodcutter.ability
            };
            CardHelper.CreateCard(
                "wstl_warmHeartedWoodsman", "Warm-Hearted Woodsman",
                "A tin woodsman on the search for a heart. Perhaps you can give him yours.",
                2, 3, 2, 0,
                Artwork.warmHeartedWoodsman, Artwork.warmHeartedWoodsman_emission, pixelTexture: Artwork.warmHeartedWoodsman_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}