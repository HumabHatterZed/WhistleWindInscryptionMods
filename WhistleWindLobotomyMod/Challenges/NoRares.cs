using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod.Challenges
{
    public static class NoRares
    {
        public static AscensionChallenge Id { get; private set; }

        public static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "No Abnormal Rares",
                "Abnormal card choices cannot offer rare cards.",
                5,
                TextureLoader.LoadTextureFromFile("ascensionNoRares.png"),
                TextureLoader.LoadTextureFromFile("ascensionNoRares_activated.png")
                ).Challenge.challengeType;
        }
    }
}
