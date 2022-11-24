using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using InscryptionAPI.Regions;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Opponents.AbnormalEncounterData;

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
            if (__instance != null && (AscensionSaveData.Data.ChallengeIsActive(Id) || !SaveFile.IsAscension && ConfigManager.Instance.AbnormalBattles))
            {
                ChallengeActivationUI.TryShowActivation(Id);
                RegionProgression.Instance.regions[0].encounters.Clear();
                RegionProgression.Instance.regions[1].encounters.Clear();
                RegionProgression.Instance.regions[2].encounters.Clear();
                RegionProgression.Instance.regions[0].AddEncounters(StrangePack, BitterPack, StrangeFlock, HelperJuggernaut);
                RegionProgression.Instance.regions[1].AddEncounters(StrangeBees, StrangeCreatures1, WormsNest, StrangeCreatures2, StrangeFish);
                RegionProgression.Instance.regions[2].AddEncounters(StrangeHerd, AlriuneJuggernaut, SpidersNest, SwanJuggernaut);
            }
        }
    }
}
