﻿using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod.Core.Challenges
{
    public static class BetterRareChances
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        public static void Register()
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Better Rare Chances",
                "Rare cards are more likely to appear at abnormal choice nodes.",
                -10,
                TextureLoader.LoadTextureFromBytes(Artwork.ascensionBetterRareChances),
                TextureLoader.LoadTextureFromBytes(Artwork.ascensionBetterRareChances_activated)
                ).Challenge.challengeType;
        }
    }
}
