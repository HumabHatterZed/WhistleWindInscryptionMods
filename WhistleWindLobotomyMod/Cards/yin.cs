using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Yin_O05102()
        {
            const string yin = "yin";

            NewCard(yin, "Yin", "A black pendant in search of its missing half.",
                attack: 2, health: 3, blood: 2)
                .SetPortraits(yin, altPortraitName: "yinAlt")
                .SetPixelAlternatePortrait(TextureLoader.LoadTextureFromFile("yinAlt_pixel.png"))
                .AddAbilities(Ability.Strafe, Ability.Submerge)
                .AddAppearances(AlternateBattlePortrait.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}