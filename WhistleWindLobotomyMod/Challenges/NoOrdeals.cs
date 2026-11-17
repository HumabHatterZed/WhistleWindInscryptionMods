using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class NoOrdeals {
        internal const string TITLE = "No Ordeals";
        internal const string DESCRIPTION = "Ordeal battles will not appear.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                0,
                TextureLoader.LoadTextureFromFile("ascensionNoOrdeals.png"),
                TextureLoader.LoadTextureFromFile("ascensionNoOrdeals_activated.png")
                )
                .SetIncompatibleChallengeGetterStatic(AllOrdeals.ID, BossOrdeals.ID)
                .Challenge.challengeType;
        }
    }
}
