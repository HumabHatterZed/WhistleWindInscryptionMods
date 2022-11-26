using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod.Core.Challenges
{
    public static class BetterRareChances
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        public static void Register(Harmony harmony)
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Better Rare Chances",
                "Raises the chance of getting a Rare card from the abnormal choice node.",
                -10,
                LobotomyTextureLoader.LoadTextureFromBytes(Artwork.ascensionBetterRareChances),
                LobotomyTextureLoader.LoadTextureFromBytes(Artwork.ascensionBetterRareChances_activated)
                ).Challenge.challengeType;
        }
    }
}
