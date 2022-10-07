using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_VoidDreamRooster_T0299()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy
            };
            List<Tribe> tribes = new()
            {
                Tribe.Hooved,
                Tribe.Bird
            };
            CardHelper.CreateCard(
                "wstl_voidDreamRooster", "Void Dream",
                "Quite the chimera.",
                2, 3, 2, 0,
                Artwork.voidDreamRooster, Artwork.voidDreamRooster_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}