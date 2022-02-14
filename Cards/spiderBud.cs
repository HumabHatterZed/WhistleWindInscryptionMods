using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SpiderBud_O0243()
        {
            List<Ability> abilities = new()
            {
                BroodMother.ability
            };

            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_spiderBud", "Spider Bud",
                "Grotesque mother of spiders.",
                0, 2, 0, 4,
                Resources.spiderBud,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.spiderBud_emission);
        }
    }
}