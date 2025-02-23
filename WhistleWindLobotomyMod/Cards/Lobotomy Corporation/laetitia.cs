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
        public const string laetitia = "wstl_laetitia";
        private static void Laetitia_O0167()
        {
            string name = "Laetitita";
            string desc = "A little witch carrying a heart-shaped gift.";
            string textureName = "laetitia";
            CardManager.New(LobotomyPlugin.pluginPrefix, laetitia, name,
                attack: 1, health: 2, desc)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(GiftGiver.ability)
                .AddTribes(TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.He);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 1, health: 1, desc)
                .SetGemsCost(GemType.Orange)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(GiftGiver.ability)
                .AddTribes(TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}