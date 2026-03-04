using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string apostleHeretic = "wstl_apostleHeretic";
        private static void ApostleHeretic_T0346() {
            string textureName = "apostleHeretic";
            CardManager.New(LobotomyPlugin.pluginPrefix, apostleHeretic, "Heretic",
                attack: 0, health: 7)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Confession.ID)
                .AddTraits(Trait.Uncuttable, Apostle)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetEventCard(false)
                .Build();
        }
    }
}