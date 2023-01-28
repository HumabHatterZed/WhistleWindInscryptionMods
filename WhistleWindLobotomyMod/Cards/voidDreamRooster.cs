using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void VoidDreamRooster_T0299()
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
                Resources.voidDreamRooster, Resources.voidDreamRooster_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}