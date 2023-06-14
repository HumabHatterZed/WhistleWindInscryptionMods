using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NotesFromResearcher_T0978()
        {
            const string notesFromResearcher = "notesFromResearcher";

            CardInfo notesFromResearcherCard = NewCard(
                notesFromResearcher,
                "Notes from a Crazed Researcher",
                "An insane garble of guilty confessions and incoherent gibberish.",
                attack: 2, health: 0, blood: 4)
                .SetPortraits(notesFromResearcher)
                .AddAbilities(Ability.Brittle, GiveStatsSigils.AbilityID)
                .SetSpellType(SpellType.TargetedStatsSigils)
                .SetEvolveInfo("Frantic {0}");

            CreateCard(notesFromResearcherCard, CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}