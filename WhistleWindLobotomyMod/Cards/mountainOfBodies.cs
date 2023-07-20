using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
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

            NewCard(mountainOfBodies3, displayName: mountainName,
                attack: 5, health: 1, blood: 3)
                .SetPortraits(mountainOfBodies3)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetDefaultEvolutionName(mountainName)
                .Build(CardHelper.ChoiceType.Rare, nonChoice: true);

            NewCard(mountainOfBodies2, displayName: mountainName,
                attack: 3, health: 1, blood: 2)
                .SetPortraits(mountainOfBodies2)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetDefaultEvolutionName(mountainName)
                .Build(CardHelper.ChoiceType.Rare, nonChoice: true);

            NewCard(mountainOfBodies, mountainName, "A mass grave, melted and congealed into one eternally hungry beast.",
                attack: 2, health: 1, blood: 2, temple: CardTemple.Undead)
                .SetPortraits(mountainOfBodies)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetDefaultEvolutionName(mountainName)
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Aleph);
        }
    }
}