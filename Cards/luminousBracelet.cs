using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void LuminousBracelet_O0995()
        {
            List<Ability> abilities = new()
            {
                Regenerator.ability
            };

            CardHelper.CreateCard(
                "wstl_luminousBracelet", "Luminous Bracelet",
                "A bracelet that will heal those nearby. It does not forgive the greedy.",
                0, 2, 0, 3,
                Resources.luminousBracelet, Resources.luminousBracelet_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 2);
        }
    }
}