using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using System.Linq;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class FinalOrdeal // taken from infiniscryption
    {
        public static AscensionChallenge Id { get; private set; }

        internal static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Final Ordeals",
                "Leshy is replaced as the final boss of the run with the White Ordeals.",
                60,
                TextureLoader.LoadTextureFromFile("ascensionFinalOrdeal.png"),
                TextureLoader.LoadTextureFromFile("ascensionFinalOrdeal_activated.png")
                )
                .SetBoss(true)
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
