using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void VoidDream_T0299()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Evolve,
                Ability.Flying
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Hooved
            };

            WstlUtils.Add(
                "wstl_voidDream", "Void Dream",
                "A sleeping goat. Or is it a sheep?",
                1, 0, 1, 0,
                Resources.voidDream,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                evolveId: new EvolveIdentifier("wstl_voidDreamRooster", 1));
        }
    }
}