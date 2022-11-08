using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ForsakenMurderer_T0154()
        {
            CardHelper.CreateCard(
                "wstl_forsakenMurderer", "Forsaken Murderer",
                "Experimented on then forgotten. What was anger has become abhorrence.",
                atk: 4, hp: 1,
                blood: 0, bones: 8, energy: 0,
                Artwork.forsakenMurderer, Artwork.forsakenMurderer_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}