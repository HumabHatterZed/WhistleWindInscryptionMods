using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void HonouredMonk_D01110()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Evolve
            };

            WstlUtils.Add(
                "wstl_honouredMonk", "Honoured Monk",
                "A monk seeking enlightenment through good deeds. But surely there's a quicker way to nirvana...",
                1, 2, 2, 0,
                Resources.honouredMonk,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                evolveId: new EvolveIdentifier("wstl_cloudedMonk", 1));
        }
    }
}