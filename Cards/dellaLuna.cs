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

            WstlUtils.Add(
                "wstl_dellaLuna", "Il Pianto della Luna",
                "Tales say that [c:bR]the moon[c:] bewitches man. In reality man despairs at it.",
                1, 7, 3, 0,
                Resources.dellaLuna, Resources.dellaLuna_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}