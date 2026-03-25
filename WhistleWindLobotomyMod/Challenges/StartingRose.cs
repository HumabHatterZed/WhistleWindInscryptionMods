using DiskCardGame;
using InscryptionAPI.Ascension;
using System.Linq;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class StartingRose {
        internal const string TITLE = "Painted Red";
        internal const string DESCRIPTION = "You have the Curse of the Rose in all battles.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                20,
                TextureLoader.LoadTextureFromFile("ascensionRose.png"),
                TextureLoader.LoadTextureFromFile("ascensionRose_activated.png"), 0)
                .Challenge.challengeType;
        }
    }
}
