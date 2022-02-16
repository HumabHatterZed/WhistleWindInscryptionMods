using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
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

            WstlUtils.Add(
                "wstl_voidDream", "Void Dream",
                "A sleeping goat. Or is it a sheep?",
                0, 1, 1, 0,
                Resources.voidDream,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.voidDream_emission,
                evolveId: new EvolveIdentifier("wstl_voidDreamRooster", 1));
        }
    }
}