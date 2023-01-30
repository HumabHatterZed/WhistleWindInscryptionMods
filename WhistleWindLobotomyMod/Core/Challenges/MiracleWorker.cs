using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod.Core.Challenges
{
    public static class MiracleWorker
    {
        public static AscensionChallenge Id { get; private set; }

        private static Texture2D portrait = null;
        private static Texture2D emissive = null;

        // Creates the challenge then calls the relevant patches
        public static void Register(Harmony harmony)
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "Miracle Worker",
                "Leshy will play Plague Doctor against you. Beware the Clock.",
                20,
                TextureLoader.LoadTextureFromBytes(Artwork.ascensionMiracleWorker),
                TextureLoader.LoadTextureFromBytes(Artwork.ascensionMiracleWorker_activated)
                ).Challenge.challengeType;

            harmony.PatchAll(typeof(MiracleWorker));
        }

        private static readonly Opponent.Type[] SUPPORTED_OPPONENTS = new Opponent.Type[] {
            Opponent.Type.ProspectorBoss,
            Opponent.Type.AnglerBoss,
            Opponent.Type.TrapperTraderBoss,
            Opponent.Type.LeshyBoss,
            Opponent.Type.PirateSkullBoss
        };

        [HarmonyPatch(typeof(Opponent), nameof(Opponent.SpawnOpponent))]
        [HarmonyPostfix]
        public static void AddPlagueDoctor(EncounterData encounterData, ref Opponent __result)
        {
            if (AscensionSaveData.Data.ChallengeIsActive(Id) || !SaveFile.IsAscension && LobotomyConfigManager.Instance.MiracleWorker)
            {
                if (!SUPPORTED_OPPONENTS.Contains(encounterData.opponentType))
                    ChallengeActivationUI.TryShowActivation(Id);
                List<List<CardInfo>> turnPlan = __result.TurnPlan;
                CardInfo info = CardLoader.GetCardByName("wstl_plagueDoctor");
                UpdatePortrait();
                info.SetPortrait(portrait, emissive);
                for (int i = 0; i < turnPlan.Count(); i++)
                {
                    if (turnPlan[i].Count() > 0 && turnPlan[i].Count() < 4)
                    {
                        turnPlan[i].Add(info);
                        break;
                    }
                }
                LobotomyPlugin.Log.LogDebug($"start5");
                __result.TurnPlan = turnPlan;
            }
        }
        private static void UpdatePortrait()
        {
            switch (LobotomyConfigManager.Instance.NumOfBlessings)
            {
                case 0:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor_emission);
                    break;
                case 1:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor1);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor1_emission);
                    break;
                case 2:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor2);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor2_emission);
                    break;
                case 3:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor3);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor3_emission);
                    break;
                case 4:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor4);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor4_emission);
                    break;
                case 5:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor5);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor5_emission);
                    break;
                case 6:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor6);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor6_emission);
                    break;
                case 7:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor7);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor7_emission);
                    break;
                case 8:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor8);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor8_emission);
                    break;
                case 9:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor9);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor9_emission);
                    break;
                case 10:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor10);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor10_emission);
                    break;
                default:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor11);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor11_emission);
                    break;
            }
        }
    }
}
