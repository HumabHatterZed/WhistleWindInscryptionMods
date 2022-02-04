using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SpiderBud_O0243()
        {
            List<Ability> abilities = new List<Ability>
            {
                BroodMother.ability
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_spiderBud", "Spider Bud",
                "Grotesque mother of spiders.",
                2, 0, 0, 0,
                Resources.spiderBud,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}