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

            CardManager.New(pluginPrefix, notesFromResearcher, "Notes from a Crazed Researcher",
                attack: 2, health: 0, "An insane garble of guilty confessions and incoherent gibberish.")
                .SetBonesCost(3)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, notesFromResearcher)
                .AddAbilities(Ability.Brittle, GiveStatsSigils.AbilityID)
                .SetSpellType(SpellType.TargetedStats)
                .SetDefaultEvolutionName("Frantic Notes from a Crazed Researcher")
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}