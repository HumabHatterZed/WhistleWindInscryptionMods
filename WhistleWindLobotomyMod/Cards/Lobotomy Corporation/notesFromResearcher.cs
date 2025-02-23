using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string notesFromResearcher = "wstl_notesFromResearcher";
        private static void NotesFromResearcher_T0978()
        {
            string name = "Notes from a Crazed Researcher";
            string name2 = "Frantic Notes from a Crazed Researcher";
            string desc = "An insane garble of guilty confessions and incoherent gibberish.";
            string textureName = "notesFromResearcher";
            CardManager.New(LobotomyPlugin.pluginPrefix, notesFromResearcher, name,
                attack: 2, health: 0, desc)
                .SetEnergyCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Brittle, GiveStatsSigils.AbilityID)
                .SetSpellType(SpellType.TargetedStats)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.He);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 2, health: 0, desc)
                .SetEnergyCost(3)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Brittle, GiveStatsSigils.AbilityID)
                .SetSpellType(SpellType.TargetedStats)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}