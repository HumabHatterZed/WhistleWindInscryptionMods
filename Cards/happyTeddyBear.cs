using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void HappyTeddyBear_T0406()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy
            };

            WstlUtils.Add(
                "wstl_happyTeddyBear", "Happy Teddy Bear",
                "Its memories began with a warm hug.",
                3, 2, 0, 8,
                Resources.happyTeddyBear, Resources.happyTeddyBear_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}