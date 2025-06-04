using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod.Challenges
{
    public static class ApostleGrizzlies
    {
        public static AscensionChallenge Id { get; private set; }

        public static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Apostle Grizzlies",
                "Guardian Apostles appear during the first 3 boss battles.",
                30,
                TextureLoader.LoadTextureFromFile("ascensionApostleBear.png"),
                TextureLoader.LoadTextureFromFile("ascensionApostleBear_activated.png")
                )
                .SetDependantChallengeGetterStatic(AscensionChallenge.GrizzlyMode)
                .Challenge.challengeType;
        }
    }
}
