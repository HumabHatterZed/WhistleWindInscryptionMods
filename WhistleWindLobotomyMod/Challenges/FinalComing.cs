using DiskCardGame;
using InscryptionAPI.Ascension;
using System.Linq;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class FinalComing
    {
        internal const string title = "Final Coming";
        internal const string description = "The Saviour boss will be stronger and guaranteed to appear.";

        public static AscensionChallenge Id { get; private set; }

        internal static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                title,
                description,
                70,
                TextureLoader.LoadTextureFromFile("ascensionRaptureStart.png"),
                TextureLoader.LoadTextureFromFile("ascensionRaptureStart_activated.png"), 0)
                .SetFlags("ModdedFinalBoss")
                .SetIncompatibleChallengeGetter(x =>
                (from x2 in x
                 where x2?.Flags != null && x2.Flags.Exists(x3 => x3?.ToString() == "ModdedFinalBoss")
                 select x2 into x4
                 select x4.Challenge.challengeType).Concat(new AscensionChallenge[1] { AscensionChallenge.FinalBoss })
                )
                .Challenge.challengeType;
        }
    }
}
