using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core.Helpers;
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
                "Leshy will play Plague Doctor during regular battles. Beware the Clock.",
                20,
                LobotomyTextureLoader.LoadTextureFromBytes(Artwork.ascensionMiracleWorker),
                LobotomyTextureLoader.LoadTextureFromBytes(Artwork.ascensionMiracleWorker_activated)
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
            if (AscensionSaveData.Data.ChallengeIsActive(Id) || !SaveFile.IsAscension && ConfigManager.Instance.MiracleWorker)
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
            switch (ConfigManager.Instance.NumOfBlessings)
            {
                case 0:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor_emission);
                    break;
                case 1:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor1);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor1_emission);
                    break;
                case 2:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor2);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor2_emission);
                    break;
                case 3:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor3);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor3_emission);
                    break;
                case 4:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor4);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor4_emission);
                    break;
                case 5:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor5);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor5_emission);
                    break;
                case 6:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor6);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor6_emission);
                    break;
                case 7:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor7);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor7_emission);
                    break;
                case 8:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor8);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor8_emission);
                    break;
                case 9:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor9);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor9_emission);
                    break;
                case 10:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor10);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor10_emission);
                    break;
                default:
                    portrait = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor11);
                    emissive = LobotomyTextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor11_emission);
                    break;
            }
        }
    }
}
