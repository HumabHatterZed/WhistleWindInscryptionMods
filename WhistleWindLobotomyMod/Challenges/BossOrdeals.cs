using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class BossOrdeals // taken from infiniscryption
    {
        internal const string TITLE = "Ordeal Bosses";
        internal const string DESCRIPTION = "All bosses are replaced with Ordeals of Midnight.";

        public static AscensionChallenge ID { get; private set; }
        
        internal static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                40,
                TextureLoader.LoadTextureFromFile("ascensionBossOrdeals.png"),
                TextureLoader.LoadTextureFromFile("ascensionBossOrdeals_activated.png")
                )
                .Challenge.challengeType;
        }
    }
}
