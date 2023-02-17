﻿using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using InscryptionAPI.Regions;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.LobotomyEncounterManager;

namespace WhistleWindLobotomyMod.Core.Challenges
{
    public static class AbnormalEncounters // taken from infiniscryption
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        public static void Register(Harmony harmony)
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Abnormal Encounters",
                "All regular battles will only use abnormality cards.",
                20,
                TextureLoader.LoadTextureFromBytes(Artwork.ascensionAbnormalEncounters),
                TextureLoader.LoadTextureFromBytes(Artwork.ascensionAbnormalEncounters_activated)
                ).Challenge.challengeType;

            // Do later?
            /*CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                if (AscensionSaveData.Data.ChallengeIsActive(Id))
                {
                    cards.CardByName("Starvation").portraitTex = null;
                }

                return cards;
            };*/

            harmony.PatchAll(typeof(AbnormalEncounters));
        }

        [HarmonyPatch(typeof(GameFlowManager), nameof(GameFlowManager.Start))]
        [HarmonyPostfix]
        public static void ClearVanillaEncounters(ref GameFlowManager __instance)
        {
            if (__instance != null)
            {
                if (SaveFile.IsAscension ? AscensionSaveData.Data.ChallengeIsActive(Id)
                    : LobotomyConfigManager.Instance.AbnormalBattles)
                {
                    ChallengeActivationUI.TryShowActivation(Id);
                    for (int i = 0; i < 3; i++)
                        RegionProgression.Instance.regions[i].encounters.RemoveAll(e => !ModEncounters[i].Contains(e));
                }
            }
        }
    }
}
