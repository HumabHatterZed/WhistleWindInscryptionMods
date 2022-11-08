using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
                atk: 2, hp: 5,
                blood: 1, bones: 0, energy: 0,
                Artwork.kingOfGreed, Artwork.kingOfGreed_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true);
        }
    }
}