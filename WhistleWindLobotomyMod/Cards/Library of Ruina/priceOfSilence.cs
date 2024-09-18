using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_PriceOfSilence_O0565()
        {
            const string priceOfSilence = "priceOfSilence";

            CardManager.New(pluginPrefix, priceOfSilence, "Price of Silence",
                attack: 0, health: 3, "The unflinching hand of time cuts down man and beast alike.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, priceOfSilence)
                .SetStatIcon(Time.Icon)
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}