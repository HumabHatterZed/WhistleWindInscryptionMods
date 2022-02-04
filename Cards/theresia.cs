using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void Theresia_T0909()
        {
            List<Ability> abilities = new List<Ability>
            {
                Healer.ability
            };

            WstlUtils.Add(
                "wstl_theresia", "Theresia",
                "An old music box. It plays a familiar melody.",
                2, 0, 2, 0,
                Resources.theresia,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}