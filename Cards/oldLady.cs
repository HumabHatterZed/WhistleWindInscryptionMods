using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_OldLady_O0112()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy
            };

            CardHelper.CreateCard(
                "wstl_oldLady", "Old Lady",
                "An aged storyteller. She can tell you any tale, even those that can't exist.",
                1, 2, 0, 2,
                Artwork.oldLady, Artwork.oldLady_emission, pixelTexture: Artwork.oldLady_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}