using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class SoulboundCards {
        private const string TITLE = "Animal Safety";
        private const string DESCRIPTION = "All cards in your deck have the Death Penalty sigil.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register() {
            ID = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                TITLE,
                DESCRIPTION,
                50,
                TextureLoader.LoadTextureFromFile("ascensionSoulbound.png"),
                TextureLoader.LoadTextureFromFile("ascensionSoulbound_activated.png")
                ).Challenge.challengeType;
        }
    }
}
