using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string mountainOfBodies = "wstl_mountainOfBodies";
        public const string mountainOfBodiesPixel = "wstlGBC_mountainOfBodiesPixel";
        public const string mountainOfBodies2 = "wstl_mountainOfBodies2";
        public const string mountainOfBodies3 = "wstl_mountainOfBodies3";
        private static void MountainOfBodies_T0175()
        {
            string mountainName = "The Mountain of Smiling Bodies";
            string desc = "A mass grave, melted and congealed into one eternally hungry beast.";
            string textureName = "mountainOfBodies3";
            string textureName2 = "mountainOfBodies2";
            string textureName3 = "mountainOfBodies";
            Ability[] abilities = new[] { Assimilator.ability };
            SpecialTriggeredAbility[] specialAbilities = new[] { Smile.specialAbility };

            CardManager.New(LobotomyPlugin.pluginPrefix, mountainOfBodies3, displayName: mountainName,
                attack: 3, health: 1)
                .SetBloodCost(3)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetDefaultEvolutionName(mountainName)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(LobotomyPlugin.pluginPrefix, mountainOfBodies2, displayName: mountainName,
                attack: 2, health: 1)
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetDefaultEvolutionName(mountainName)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(LobotomyPlugin.pluginPrefix, mountainOfBodies, mountainName,
                attack: 2, health: 1, desc)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetDefaultEvolutionName(mountainName)
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph);

            CardManager.New(LobotomyPlugin.pixelPrefix, mountainOfBodiesPixel, mountainName,
                attack: 2, health: 1, desc)
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetDefaultEvolutionName(mountainName)
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);
        }
    }
}