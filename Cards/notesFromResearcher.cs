using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NotesFromResearcher_T0978()
        {
            List<Ability> abilities = new List<Ability>
            {
                FlagBearer.ability,
                Ability.ExplodeOnDeath
            };

            WstlUtils.Add(
                "wstl_notesFromResearcher", "Notes from a Crazed Researcher",
                "An insane garble of guilty confessions and incoherent gibberish.",
                2, 0, 1, 0,
                Resources.notesFromResearcher,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}