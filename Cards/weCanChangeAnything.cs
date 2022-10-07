using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_WeCanChangeAnything_T0985()
        {
            List<Ability> abilities = new()
            {
                Grinder.ability
            };

            CardHelper.CreateCard(
                "wstl_weCanChangeAnything", "We Can Change Anything",
                "Whatever you're dissatisfied with, this machine will fix it. You just have to step inside.",
                1, 2, 1, 0,
                Artwork.weCanChangeAnything, Artwork.weCanChangeAnything_emission, pixelTexture: Artwork.weCanChangeAnything_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Zayin);
        }
    }
}