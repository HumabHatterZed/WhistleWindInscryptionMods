using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void TodaysShyLookAngry_O0192()
        {
            WstlUtils.Add(
                "wstl_todaysShyLookAngry", "Today's Shy Look",
                "An indecisive creature. Its expression is different each time you draw it.",
                1, 2, 1, 0,
                Resources.todaysShyLookAngry,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>());
        }
    }
}