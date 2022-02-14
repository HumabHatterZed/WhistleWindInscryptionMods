using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void OneSin_O0303()
        {
            List<Ability> abilities = new()
            {
                Martyr.ability
            };

            WstlUtils.Add(
                "wstl_oneSin", "One Sin",
                "Its hollow sockets see through you.",
                0, 1, 0, 4,
                Resources.oneSin,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}