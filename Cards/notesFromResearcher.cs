using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void NotesFromResearcher_T0978()
        {
            List<Ability> abilities = new()
            {
                FlagBearer.ability,
                Volatile.ability
            };

            WstlUtils.Add(
                "wstl_notesFromResearcher", "Notes from a Crazed Researcher",
                "An insane garble of guilty confessions and incoherent gibberish.",
                0, 3, 1, 0,
                Resources.notesFromResearcher,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.notesFromResearcher_emission);
        }
    }
}