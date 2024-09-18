using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ParasiteTree_D04108()
        {
            const string parasiteTree = "parasiteTree";

            CardManager.New(pluginPrefix, parasiteTree, "Parasite Tree",
                attack: 0, health: 3, "A beautiful tree. It wants only to help you and your beasts.")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, parasiteTree)
                .AddAbilities(Gardener.ability)
                .AddTribes(TribeBotanic)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}