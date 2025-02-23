using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string piscineMermaid = "wstl_piscineMermaid";
        private static void PiscineMermaid()
        {
            return;
            string textureName = "piscineMermaid";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, piscineMermaid, "Piscine Mermaid",
                attack: 1, health: 1)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(GiftGiver.ability, Ability.Submerge)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}