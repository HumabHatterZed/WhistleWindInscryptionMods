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

            CardInfo mountainOfBodies3Card = NewCard(
                mountainOfBodies3, displayName: mountainName,
                attack: 5, health: 1, blood: 3)
                .SetPortraits(mountainOfBodies3)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetEvolveInfo("{0}");

            CardInfo mountainOfBodies2Card = NewCard(
                mountainOfBodies2, displayName: mountainName,
                attack: 3, health: 1, blood: 2)
                .SetPortraits(mountainOfBodies2)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetEvolveInfo("{0}");

            CardInfo mountainOfBodiesCard = NewCard(
                mountainOfBodies,
                mountainName,
                "A mass grave, melted and congealed into one eternally hungry beast.",
                attack: 2, health: 1, blood: 2, temple: CardTemple.Undead)
                .SetPortraits(mountainOfBodies)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetEvolveInfo("{0}");

            CreateCard(mountainOfBodies3Card, CardHelper.ChoiceType.Rare, nonChoice: true);
            CreateCard(mountainOfBodies2Card, CardHelper.ChoiceType.Rare, nonChoice: true);
            CreateCard(mountainOfBodiesCard, CardHelper.ChoiceType.Rare, RiskLevel.Aleph);
        }
    }
}