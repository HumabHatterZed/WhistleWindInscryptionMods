using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void MHz176_T0727()
        {
            List<Ability> abilities = new()
            {
                Ability.BuffNeighbours,
                Ability.BuffEnemy
            };

            WstlUtils.Add(
                "wstl_mhz176", "1.76 MHz",
                "This is a record, a record of a day we must never forget.",
                0, 3, 1, 0,
                Resources.mhz176, Resources.mhz176_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}