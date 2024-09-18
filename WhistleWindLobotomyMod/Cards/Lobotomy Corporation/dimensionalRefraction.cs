using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DimensionalRefraction_O0388()
        {
            const string dimensionalRefraction = "dimensionalRefraction";

            CardManager.New(pluginPrefix, dimensionalRefraction, "Dimensional Refraction Variant",
                attack: 0, health: 1, "A strange phenomenon. Or rather, the creature is the phenomena in and of itself.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, dimensionalRefraction)
                .AddAbilities(Ability.RandomAbility)
                .SetStatIcon(SigilPower.Icon)
                .SetDefaultEvolutionName("4th Dimensional Refraction Variant")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}