using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void VoidDream_T0299()
        {
            List<Ability> abilities = new()
            {
                Ability.Flying,
                Ability.Evolve
            };
            List<Tribe> tribes = new()
            {
                Tribe.Hooved
            };
            CardHelper.CreateCard(
                "wstl_voidDream", "Void Dream",
                "A sleeping goat. Or is it a sheep?",
                1, 1, 1, 0,
                Resources.voidDream, Resources.voidDream_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isChoice: true, evolveName: "wstl_voidDreamRooster", riskLevel: 2);
        }
    }
}