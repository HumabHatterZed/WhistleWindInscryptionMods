using DiskCardGame;
using InscryptionAPI.Card;
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

            CardManager.New(pluginPrefix, todaysShyLook, lookName,
                attack: 1, health: 2, description)
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, todaysShyLook)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName(evolveName)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true); ;

            CardManager.New(pluginPrefix, "todaysShyLookNeutral", lookName,
                attack: 1, health: 2, description)
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, todaysShyLook)
                .AddAbilities(Ability.DrawCopyOnDeath)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName(evolveName)
                .Build();

            CardManager.New(pluginPrefix, todaysShyLookHappy, "Today's Happy Look",
                attack: 1, health: 3, description)
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, todaysShyLookHappy)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName("Tomorrow's Happy Look")
                .Build();

            CardManager.New(pluginPrefix, todaysShyLookAngry, "Today's Angry Look",
                attack: 2, health: 1, description)
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, todaysShyLookAngry)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName("Tomorrow's Angry Look")
                .Build();
        }
    }
}