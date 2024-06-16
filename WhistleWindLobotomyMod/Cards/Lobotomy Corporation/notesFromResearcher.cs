using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NotesFromResearcher_T0978()
        {
            const string notesFromResearcher = "notesFromResearcher";

            NewCard(notesFromResearcher, "Notes from a Crazed Researcher", "An insane garble of guilty confessions and incoherent gibberish.",
                attack: 2, health: 0, bones: 3, temple: CardTemple.Undead)
                .SetPortraits(notesFromResearcher)
                .AddAbilities(Ability.Brittle, GiveStatsSigils.AbilityID)
                .SetSpellType(SpellType.TargetedStats)
                .SetDefaultEvolutionName("Frantic Notes from a Crazed Researcher")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}