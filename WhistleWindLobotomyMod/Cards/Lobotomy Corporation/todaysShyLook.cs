using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string todaysShyLook = "wstl_todaysShyLook";
        public const string todaysShyLookNeutral = "wstl_todaysShyLookNeutral";
        public const string todaysShyLookHappy = "wstl_todaysShyLookHappy";
        public const string todaysShyLookAngry = "wstl_todaysShyLookAngry";
        private static void TodaysShyLook_O0192()
        {
            string lookName = "Today's Shy Look";
            string description = "An indecisive creature. Her expression is different whenever you draw her.";
            string evolveName = "Tomorrow's Shy Look";
            string textureName = "todaysShyLook";
            string textureName2 = "todaysShyLookAngry";
            string textureName3 = "todaysShyLookHappy";
            SpecialTriggeredAbility[] specialAbilities = new[] { TodaysExpression.specialAbility };
            Tribe[] tribes = new[] { TribeAnthropoid };
            Trait[] traits = new[] { Trait.DeathcardCreationNonOption };

            CardManager.New(LobotomyPlugin.pluginPrefix, todaysShyLook, lookName,
                attack: 1, health: 2, description)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName(evolveName)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true); ;

            CardManager.New(LobotomyPlugin.pluginPrefix, todaysShyLookNeutral, lookName,
                attack: 1, health: 2, description)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.DrawCopyOnDeath)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName(evolveName)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, todaysShyLookHappy, "Today's Happy Look",
                attack: 1, health: 3, description)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName("Tomorrow's Happy Look")
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, todaysShyLookAngry, "Today's Angry Look",
                attack: 2, health: 1, description)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetDefaultEvolutionName("Tomorrow's Angry Look")
                .Build();
        }
    }
}