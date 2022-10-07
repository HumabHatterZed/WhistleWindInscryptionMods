using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_WisdomScarecrow_F0187()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability
            };
            CardHelper.CreateCard(
                "wstl_wisdomScarecrow", "Scarecrow Searching for Wisdom",
                "A hollow-headed scarecrow. Blood soaks its straw limbs.",
                1, 3, 0, 5,
                Artwork.wisdomScarecrow, Artwork.wisdomScarecrow_emission, pixelTexture: Artwork.wisdomScarecrow_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}