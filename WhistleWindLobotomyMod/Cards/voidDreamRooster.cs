using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
                atk: 2, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.voidDreamRooster, Artwork.voidDreamRooster_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}