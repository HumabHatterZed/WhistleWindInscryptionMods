using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string dimensionalRefraction = "wstl_dimensionalRefraction";
        private static void DimensionalRefraction_O0388()
        {
            string textureName = "dimensionalRefraction";
            CardManager.New(LobotomyPlugin.pluginPrefix, dimensionalRefraction, "Dimensional Refraction Variant",
                attack: 0, health: 1, "A strange phenomenon. Or rather, the creature is the phenomena in and of itself.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.RandomAbility)
                .SetStatIcon(SigilPower.Icon)
                .SetDefaultEvolutionName("4th Dimensional Refraction Variant")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}