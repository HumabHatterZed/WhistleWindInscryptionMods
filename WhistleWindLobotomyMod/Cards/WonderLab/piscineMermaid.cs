using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string piscineMermaid = "wstlWonder_piscineMermaid";
        private static void PiscineMermaid() {
            string textureName = "piscineMermaid";
            
            CardManager.New(LobotomyPlugin.wonderlabPrefix, piscineMermaid, "Piscine Mermaid",
                attack: 2, health: 1, "Its love always ends poorly, yet it cannot help itself all the same.")
                .SetBloodCost(1).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.MoveBeside, Ability.Submerge)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}