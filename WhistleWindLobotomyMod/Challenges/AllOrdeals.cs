using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Angler;
using WhistleWindLobotomyMod.Opponents.Leshy;
using WhistleWindLobotomyMod.Opponents.PirateSkull;
using WhistleWindLobotomyMod.Opponents.Prospector;
using WhistleWindLobotomyMod.Opponents.TrapperTrader;


namespace WhistleWindLobotomyMod.Challenges
{
    public static class AllOrdeals // taken from infiniscryption
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        internal static void Register(Harmony harmony)
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "All Ordeals",
                "All non-boss battles are replaced with Ordeals.",
                20,
                TextureLoader.LoadTextureFromFile("ascensionAbnormalBosses"),
                TextureLoader.LoadTextureFromFile("ascensionAbnormalBosses_activated")
                )
                .SetIncompatibleChallengeGetterStatic(AbnormalEncounters.Id)
                .Challenge.challengeType;

            harmony.PatchAll(typeof(AllOrdeals));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RunState), nameof(RunState.NextRegion))]
        private static void ShowAllOrdealsActivation()
        {
            if (RunState.CurrentRegionTier < 3)
            {
                ChallengeActivationUI.TryShowActivation(AllOrdeals.Id);
            }
        }
    }
}
