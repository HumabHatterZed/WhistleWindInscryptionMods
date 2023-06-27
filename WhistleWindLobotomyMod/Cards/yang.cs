using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.PixelCard;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Yang_O07103()
        {
            const string yang = "yang";

            CardInfo yangCard = NewCard(
                yang,
                "Yang",
                "A white pendant that heals those nearby.",
                attack: 0, health: 3, blood: 1)
                .SetPortraits(yang, altPortraitName: "yangAlt")
                .SetPixelAlternatePortrait(TextureLoader.LoadTextureFromFile("yangAlt_pixel.png"))
                .AddAbilities(Regenerator.ability)
                .AddSpecialAbilities(Concord.specialAbility)
                .AddAppearances(AlternateBattlePortrait.appearance)
                .SetOnePerDeck();

            CreateCard(yangCard, CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}