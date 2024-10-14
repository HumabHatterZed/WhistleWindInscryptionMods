using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges
{
    public static class BossOrdeals // taken from infiniscryption
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        internal static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Ordeal Bosses",
                "All bosses will be replaced with Midnight Ordeals.",
                10,
                TextureLoader.LoadTextureFromFile("ascensionBossOrdeals.png"),
                TextureLoader.LoadTextureFromFile("ascensionBossOrdeals_activated.png")
                )
                .SetIncompatibleChallengeGetterStatic(AbnormalBosses.Id)
                .Challenge.challengeType;
        }
    }
}
