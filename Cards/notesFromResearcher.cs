using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                Resources.notesFromResearcher, Resources.notesFromResearcher_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isTerrain: true, isChoice: true);
        }
    }
}