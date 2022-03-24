using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SnowQueen_F0137()
        {
            List<Ability> abilities = new()
            {
                FrostRuler.ability
            };

            WstlUtils.Add(
                "wstl_snowQueen", "The Snow Queen",
                "A queen from far away. Those who enter her palace never leave.",
                1, 2, 0, 6,
                Resources.snowQueen, Resources.snowQueen_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}