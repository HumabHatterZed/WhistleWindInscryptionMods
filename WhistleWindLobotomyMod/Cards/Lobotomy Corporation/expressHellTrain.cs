using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string expressHellTrain = "wstl_expressHellTrain";
        private static void ExpressHellTrain_T0986()
        {
            string name = "Express Train to Hell";
            string name2 = "Express Train to Turbo Hell";
            string desc = "When the time comes, the train will sound its mighty horn.";
            string textureName = "expressHellTrain";
            CardManager.New(LobotomyPlugin.pluginPrefix, expressHellTrain, name,
                attack: 0, health: 1, desc)
                .SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(TheTrain.ability)
                .AddTribes(TribeMechanical)
                .SetTerrain()
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Rare, RiskLevel.Waw);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 1, desc)
                .SetEnergyCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(TheTrain.ability)
                .AddTribes(TribeMechanical)
                .SetTerrain()
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Rare, RiskLevel.Waw, true);
        }
    }
}