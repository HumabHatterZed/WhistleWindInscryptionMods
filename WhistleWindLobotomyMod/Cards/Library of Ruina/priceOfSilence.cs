using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string priceOfSilence = "wstl_priceOfSilence";
        private static void PriceOfSilence_O0565() {
            string name = "Price of Silence";
            string desc = "The unflinching hand of time cuts down man and beast alike.";
            string textureName = "priceOfSilence";
            CardManager.New(LobotomyPlugin.pluginPrefix, priceOfSilence, name,
                attack: 0, health: 3, desc)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetStatIcon(Time.Icon)
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.He);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 3, desc)
                .SetGemsCost(GemType.Green)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetStatIcon(Time.Icon)
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}