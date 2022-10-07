using InscryptionAPI;
using InscryptionAPI.Ascension;
using DiskCardGame;
using HarmonyLib;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public static class BetterRareChances
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        public static void Register(Harmony harmony)
        {
            Id = ChallengeManager.Add(
                WstlPlugin.pluginGuid,
                "Better Rare Chances",
                "Raises the chance of getting a Rare card from the abnormal choice node.",
                -10,
                WstlTextureHelper.LoadTextureFromResource(Artwork.ascensionBetterRareChances),
                WstlTextureHelper.LoadTextureFromResource(Artwork.ascensionBetterRareChances_activated)
                ).Challenge.challengeType;
        }
    }
}
