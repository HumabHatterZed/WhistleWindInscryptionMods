using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string hundredsGoodDeeds = "wstl_hundredsGoodDeeds";
        private static void HundredsGoodDeeds_O0303() {
            string textureName = "oneSin";
            CardManager.New(LobotomyPlugin.pluginPrefix, hundredsGoodDeeds, oneSinName,
                attack: 0, health: 77)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName, "hundredsGoodDeeds_emission.png", "hundredsGoodDeeds_pixel.png")
                .AddAbilities(Confession.ID)
                .AddTraits(Trait.Uncuttable, Apostle)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetHideStats()
                .SetEventCard(false)
                .Build();
        }
    }
}