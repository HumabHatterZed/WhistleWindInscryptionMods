using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_KingOfGreed_O0164()
        {
            List<Ability> abilities = new()
            {
                Ability.StrafeSwap
            };

            CardHelper.CreateCard(
                "wstl_kingOfGreed", "The King of Greed",
                "",
                2, 5, 1, 0,
                Resources.kingOfGreed, Resources.kingOfGreed_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}