using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string skinProphecy = "wstl_skinProphecy";
        private static void SkinProphecy_T0990() {
            string name = "Skin Prophecy";
            string desc = "A holy book, wrapped in skin to preserve its sanctity.";
            string textureName = "skinProphecy";
            CardManager.New(LobotomyPlugin.pluginPrefix, skinProphecy, name,
                attack: 0, health: 3, desc)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Witness.ability)
                .AddTribes(TribeDivine)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 3, desc)
                .SetGemsCost(GemType.Orange)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Witness.ability)
                .AddTribes(TribeDivine)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}