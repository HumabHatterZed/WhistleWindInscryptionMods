using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_PriceOfSilence_O0565()
        {
            LobotomyCardHelper.CreateCard(
                "wstl_priceOfSilence", "Price of Silence",
                "The unflinching hand of time cuts down man and beast alike.",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.priceOfSilence, Artwork.priceOfSilence_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), appearances: new(),
                statIcon: Time.icon, choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.He,
                modTypes: LobotomyCardHelper.ModCardType.Ruina);
        }
    }
}