using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_LaetitiaFriend_O0167()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            CardHelper.CreateCard(
                "wstl_laetitiaFriend", "Little Witch's Friend",
                "She brought her friends along.",
                2, 2, 0, 4,
                Artwork.laetitiaFriend, Artwork.laetitiaFriend_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}