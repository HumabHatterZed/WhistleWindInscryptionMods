using DiskCardGame;
using InscryptionAPI.Ascension;
using System.Linq;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class FinalJester
    {
        public static AscensionChallenge Id { get; private set; }
        internal static ChallengeManager.FullChallenge Info { get; private set; }

        internal static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Final Laugh",
                "Leshy is replaced as the final boss of the run with the Fool.",
                50,
                TextureLoader.LoadTextureFromFile("ascensionJesterStart.png"),
                TextureLoader.LoadTextureFromFile("ascensionJesterStart_activated.png"), 0)
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
