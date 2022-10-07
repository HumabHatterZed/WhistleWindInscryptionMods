using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ChildOfTheGalaxy_O0155()
        {
            List<Ability> abilities = new()
            {
                FlagBearer.ability,
                Ability.BoneDigger
            };

            CardHelper.CreateCard(
                "wstl_childOfTheGalaxy", "Child of the Galaxy",
                "A small child longing for a friend. Will you be his?",
                1, 4, 2, 0,
                Artwork.childOfTheGalaxy, Artwork.childOfTheGalaxy_emission, pixelTexture: Artwork.childOfTheGalaxy_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}