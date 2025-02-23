using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string snowQueen = "wstl_snowQueen";
        private static void SnowQueen_F0137()
        {
            string name = "The Snow Queen";
            string name2 = "The Snow Empress";
            string desc = "A queen from far away. Those who enter her palace never leave.";
            string textureName = "snowQueen";
            CardManager.New(LobotomyPlugin.pluginPrefix, snowQueen, name,
                attack: 2, health: 2, desc)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(FrostRuler.ability)
                .AddTribes(TribeFae)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.He);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 2, health: 2, desc)
                .SetGemsCost(GemType.Blue, GemType.Green)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(FrostRuler.ability)
                .AddTribes(TribeFae)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}