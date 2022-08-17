using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void DellaLuna_D01105()
        {
            List<Ability> abilities = new()
            {
                GroupHealer.ability
            };

            CardHelper.CreateCard(
                "wstl_dellaLuna", "Il Pianto della Luna",
                "In reality, man despairs at [c:bR]the moon[c:].",
                2, 6, 3, 0,
                Resources.dellaLuna, Resources.dellaLuna_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, isDonator: true, riskLevel: 4);
        }
    }
}