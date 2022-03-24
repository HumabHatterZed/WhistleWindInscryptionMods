using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void CENSORED_O0389()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability
            };

            WstlUtils.Add(
                "wstl_censored", "CENSORED",
                "It's best you never learn what it looks like.",
                6, 2, 4, 0,
                Resources.censored, Resources.censored_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isRare: true);
        }
    }
}