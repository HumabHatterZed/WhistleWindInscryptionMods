using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_PriceOfSilence_O0565()
        {
            CardHelper.CreateCard(
                "wstl_priceOfSilence", "Price of Silence",
                "The unflinching hand of time cuts down man and beast alike.",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.priceOfSilence, Artwork.priceOfSilence_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), appearances: new(),
                statIcon: Time.icon, cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.He,
                metaTypes: CardHelper.MetaType.Ruina);
        }
    }
}