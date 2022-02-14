using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        List<SpecialAbilityIdentifier> specialAbilities = new()
        {
            TodaysShyLook.GetSpecialAbilityId
        };

        private void TodaysShyLookAngry_O0192()
        {
            WstlUtils.Add(
                "wstl_todaysShyLookAngry", "Today's Angry Look",
                "An indecisive creature. Its expression is different each time you draw it.",
                2, 1, 1, 0,
                Resources.todaysShyLookAngry,
                new List<Ability>(), specialAbilities: specialAbilities,
                new List<Tribe>());
        }
    }
}