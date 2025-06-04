using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod.Challenges
{
    public static class BetterRareChances
    {
        public static AscensionChallenge Id { get; private set; }
        internal static ChallengeManager.FullChallenge Info { get; private set; }

        // Creates the challenge then calls the relevant patches
        public static void Register()
        {
            Info = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Better Rare Chances",
                "Abnormal card choices offer rare cards more often.",
                -5,
                TextureLoader.LoadTextureFromFile("ascensionBetterRareChances.png"),
                TextureLoader.LoadTextureFromFile("ascensionBetterRareChances_activated.png")
                );

            Id = Info.Challenge.challengeType;
        }
    }
}
