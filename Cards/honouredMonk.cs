using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void HonouredMonk_D01110()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };

            WstlUtils.Add(
                "wstl_honouredMonk", "Honoured Monk",
                "A monk seeking enlightenment through good deeds. But surely there's a quicker way to nirvana...",
                2, 1, 2, 0,
                Resources.honouredMonk,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                evolveId: new EvolveIdentifier("wstl_cloudedMonk", 1));
        }
    }
}