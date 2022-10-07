using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

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
                Artwork.kingOfGreed, Artwork.kingOfGreed_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true);
        }
    }
}