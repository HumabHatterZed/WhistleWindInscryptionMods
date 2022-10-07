using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

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
                Resources.weCanChangeAnything, Resources.weCanChangeAnything_emission, gbcTexture: Resources.weCanChangeAnything_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 1);
        }
    }
}