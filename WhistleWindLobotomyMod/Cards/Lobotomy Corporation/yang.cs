using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Yang_O07103()
        {
            const string yang = "yang";

            NewCard(yang, "Yang", "A white pendant that heals those nearby.",
                attack: 0, health: 3, blood: 1)
                .SetPortraits(yang, altPortraitName: "yangAlt")
                .SetPixelAlternatePortrait(TextureLoader.LoadTextureFromFile("yangAlt_pixel.png"))
                .AddAbilities(Regenerator.ability)
                .AddSpecialAbilities(Concord.specialAbility)
                .AddAppearances(AlternateBattlePortrait.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}