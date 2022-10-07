using InscryptionAPI;
using InscryptionAPI.Ascension;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using DiskCardGame;
using HarmonyLib;
using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

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
                WstlTextureHelper.LoadTextureFromResource(Resources.ascensionBetterRareChances),
                WstlTextureHelper.LoadTextureFromResource(Resources.ascensionBetterRareChances_activated)
                ).Challenge.challengeType;
        }
    }
}
