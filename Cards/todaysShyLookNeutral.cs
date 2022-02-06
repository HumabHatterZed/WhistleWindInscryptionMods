using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void TodaysShyLookNeutral_O0192()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.DrawCopyOnDeath
            };

            WstlUtils.Add(
                "wstl_todaysShyLookNeutral", "Today's Shy Look",
                "An indecisive creature. Its expression is different each time you draw it.",
                2, 1, 0, 0,
                Resources.todaysShyLook,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>());
        }
    }
}