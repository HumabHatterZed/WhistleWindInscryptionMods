using DiskCardGame;
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

            CardInfo priceOfSilenceCard = NewCard(
                priceOfSilence,
                "Price of Silence",
                "The unflinching hand of time cuts down man and beast alike.",
                attack: 0, health: 3, blood: 2)
                .SetPortraits(priceOfSilence)
                .SetStatIcon(Time.Icon);

            CreateCard(priceOfSilenceCard, CardHelper.ChoiceType.Common, RiskLevel.He, ModCardType.Ruina);
        }
    }
}