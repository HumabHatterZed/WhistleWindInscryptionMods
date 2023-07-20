using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TodaysShyLook_O0192()
        {
            const string lookName = "Today's Shy Look";
            const string description = "An indecisive creature. Her expression is different whenever you draw her.";
            const string evolveName = "Tomorrow's Shy Look";
            const string todaysShyLook = "todaysShyLook";
            const string todaysShyLookHappy = "todaysShyLookHappy";
            const string todaysShyLookAngry = "todaysShyLookAngry";
            SpecialTriggeredAbility[] specialAbilities = new[] { TodaysExpression.specialAbility };
            Tribe[] tribes = new[] { TribeAnthropoid };
            Trait[] traits = new[] { Trait.DeathcardCreationNonOption };

            NewCard(todaysShyLook, lookName, description,
                attack: 1, health: 2, blood: 1)
                .SetPortraits(todaysShyLook)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName(evolveName)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw); ;

            NewCard("todaysShyLookNeutral", lookName, description,
                attack: 1, health: 2, blood: 1)
                .SetPortraits(todaysShyLook)
                .AddAbilities(Ability.DrawCopyOnDeath)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName(evolveName)
                .Build();

            NewCard(todaysShyLookHappy, "Today's Happy Look", description,
                attack: 1, health: 3, blood: 1)
                .SetPortraits(todaysShyLookHappy)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName("Tomorrow's Happy Look")
                .Build();

            NewCard(todaysShyLookAngry, "Today's Angry Look", description,
                attack: 2, health: 1, blood: 1)
                .SetPortraits(todaysShyLookAngry)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName("Tomorrow's Angry Look")
                .Build();
        }
    }
}