using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void TodaysShyLookHappy_O0192()
        {
            WstlUtils.Add(
                "wstl_todaysShyLookHappy", "Today's Shy Look",
                "An indecisive creature. Its expression is different each time you draw it.",
                3, 1, 1, 0,
                Resources.todaysShyLookHappy,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>());
        }
    }
}