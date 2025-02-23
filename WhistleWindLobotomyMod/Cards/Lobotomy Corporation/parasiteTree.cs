using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string parasiteTree = "wstl_parasiteTree";
        private static void ParasiteTree_D04108()
        {
            string textureName = "parasiteTree";
            CardManager.New(LobotomyPlugin.pluginPrefix, parasiteTree, "Parasite Tree",
                attack: 0, health: 3, "A beautiful tree. It wants only to help you and your beasts.")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Gardener.ability)
                .AddTribes(TribeBotanic)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}