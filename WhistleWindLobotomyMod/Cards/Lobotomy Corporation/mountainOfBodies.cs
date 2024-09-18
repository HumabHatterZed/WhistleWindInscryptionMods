using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MountainOfBodies_T0175()
        {
            const string mountainName = "The Mountain of Smiling Bodies";
            const string mountainOfBodies = "mountainOfBodies";
            const string mountainOfBodies2 = "mountainOfBodies2";
            const string mountainOfBodies3 = "mountainOfBodies3";
            Ability[] abilities = new[] { Assimilator.ability };
            SpecialTriggeredAbility[] specialAbilities = new[] { Smile.specialAbility };

            CardManager.New(pluginPrefix, mountainOfBodies3, displayName: mountainName,
                attack: 3, health: 1)
                .SetBloodCost(3)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, mountainOfBodies3)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetDefaultEvolutionName(mountainName)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(pluginPrefix, mountainOfBodies2, displayName: mountainName,
                attack: 2, health: 1)
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, mountainOfBodies2)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetDefaultEvolutionName(mountainName)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(pluginPrefix, mountainOfBodies, mountainName,
                attack: 2, health: 1, "A mass grave, melted and congealed into one eternally hungry beast.")
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, mountainOfBodies)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetDefaultEvolutionName(mountainName)
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);
        }
    }
}