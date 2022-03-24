using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void YouMustBeHappy_T0994()
        {
            List<Ability> abilities = new()
            {
                Scrambler.ability
            };

            WstlUtils.Add(
                "wstl_youMustBeHappy", "You Must be Happy",
                "Those that undergo the procedure find themselves rested and healthy again.",
                0, 1, 0, 2,
                Resources.youMustBeHappy, Resources.youMustBeHappy_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}