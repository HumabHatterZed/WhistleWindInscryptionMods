using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ScorchedGirl_F0102()
        {
            List<Ability> abilities = new()
            {
                Volatile.ability
            };

            CardHelper.CreateCard(
                "wstl_scorchedGirl", "Scorched Girl",
                "Though there's nothing left to burn, the fire won't go out.",
                1, 1, 0, 3,
                Artwork.scorchedGirl, Artwork.scorchedGirl_emission, pixelTexture: Artwork.scorchedGirl_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}