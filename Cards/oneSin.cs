using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void OneSin_O0303()
        {
            List<Ability> abilities = new List<Ability>
            {
                Martyr.ability
            };

            WstlUtils.Add(
                "wstl_oneSin", "One Sin",
                "Its hollow sockets see through you.",
                1, 0, 2, 0,
                Resources.oneSin,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}