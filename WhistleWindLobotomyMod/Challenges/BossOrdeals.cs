using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class BossOrdeals // taken from infiniscryption
    {
        internal const string title = "Ordeal Bosses";
        internal const string description = "All bosses are replaced with Ordeals of Midnight.";

        public static AscensionChallenge Id { get; private set; }
        internal static ChallengeManager.FullChallenge Info { get; private set; }

        internal static void Register()
        {
            Info = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                title,
                description,
                20,
                TextureLoader.LoadTextureFromFile("ascensionBossOrdeals.png"),
                TextureLoader.LoadTextureFromFile("ascensionBossOrdeals_activated.png")
                );

            Id = Info.Challenge.challengeType;
        }
    }
}
