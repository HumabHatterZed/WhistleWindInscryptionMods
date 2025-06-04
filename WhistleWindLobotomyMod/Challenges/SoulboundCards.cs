using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class SoulboundCards
    {
        internal const string title = "Animal Safety";
        internal const string description = "All cards in your main deck have the Death Penalty sigil.";

        public static AscensionChallenge Id { get; private set; }

        public static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                title,
                description,
                50,
                TextureLoader.LoadTextureFromFile("ascensionSoulbound.png"),
                TextureLoader.LoadTextureFromFile("ascensionSoulbound_activated.png")
                ).Challenge.challengeType;
        }
    }
}
