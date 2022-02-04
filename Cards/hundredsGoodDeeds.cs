using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void HundredsGoodDeeds_O0303()
        {
            List<Ability> abilities = new List<Ability>
            {
                Martyr.ability
            };

            WstlUtils.Add(
                "wstl_hundredsGoodDeeds", "Hundreds of Good Deeds",
                "Its hollow sockets see through you.",
                33, 0, 0, 0,
                Resources.hundredsGoodDeeds,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>());
        }
    }
}