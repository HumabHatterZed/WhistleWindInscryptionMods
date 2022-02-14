using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void TodaysShyLookNeutral_O0192()
        {
            List<Ability> abilities = new()
            {
                Ability.DrawCopyOnDeath
            };
            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                TodaysShyLook.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_todaysShyLookNeutral", "Today's Neutral Look",
                "An indecisive creature. Its expression is different each time you draw it.",
                1, 2, 1, 0,
                Resources.todaysShyLook,
                abilities: abilities, specialAbilities: specialAbilities,
                new List<Tribe>());
        }
    }
}