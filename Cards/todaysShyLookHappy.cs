using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void TodaysShyLookHappy_O0192()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                TodaysShyLook.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_todaysShyLookHappy", "Today's Happy Look",
                "An indecisive creature. Its expression is different each time you draw it.",
                1, 3, 1, 0,
                Resources.todaysShyLookHappy,
                new List<Ability>(), specialAbilities: specialAbilities,
                new List<Tribe>());
        }
    }
}