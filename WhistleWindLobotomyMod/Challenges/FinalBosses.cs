using DiskCardGame;
using InscryptionAPI.Ascension;
using System.Linq;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class FinalApocalypse
    {
        public static AscensionChallenge Id { get; private set; }

        internal static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Final Apocalypse",
                "Leshy is replaced as the final boss of the run with the Beast.",
                70,
                TextureLoader.LoadTextureFromFile("ascensionFinalApocalypse.png"),
                TextureLoader.LoadTextureFromFile("ascensionFinalApocalypse_activated.png"), 0)
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
    public static class FinalJester
    {
        public static AscensionChallenge Id { get; private set; }

        internal static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Final Laugh",
                "Leshy is replaced as the final boss of the run with the Fool.",
                50,
                TextureLoader.LoadTextureFromFile("ascensionJesterStart"),
                TextureLoader.LoadTextureFromFile("ascensionJesterStart_activated"), 0)
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
    public static class FinalTrick
    {
        public static AscensionChallenge Id { get; private set; }

        internal static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Final Trick",
                "The Adult boss will be stronger and guaranteed to appear.",
                50,
                TextureLoader.LoadTextureFromFile("ascensionLiarStart"),
                TextureLoader.LoadTextureFromFile("ascensionLiarStart_activated"), 0)
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
    public static class FinalComing
    {
        public static AscensionChallenge Id { get; private set; }

        internal static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Final Coming",
                "The Saviour boss will be stronger and guaranteed to appear.",
                70,
                TextureLoader.LoadTextureFromFile("ascensionRaptureStart"),
                TextureLoader.LoadTextureFromFile("ascensionRaptureStart_activated"), 0)
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
