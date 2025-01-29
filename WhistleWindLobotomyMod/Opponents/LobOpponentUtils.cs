using DiskCardGame;
using InscryptionAPI.Encounters;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Opponents
{
    public static class LobOpponentUtils
    {
        public static bool FightingCustomOpponent(bool bossOnly)
        {
            if (TurnManager.Instance?.Opponent == null)
                return false;

            return TurnManager.Instance.Opponent is LobotomyBossOpponent || (!bossOnly && TurnManager.Instance.Opponent is OrdealOpponent);
        }

        public static bool IsCustomBoss<T>() where T : LobotomyBossOpponent
        {
            return TurnManager.Instance.Opponent is T;
        }

        public static int CardOffscreenLayer { get; internal set; }

        /// <summary>
        /// Recursively goes through each Transform in a given GameObjct and sets its layer to CardOffscreenLayer.
        /// </summary>
        /// <param name="obj"></param>
        private static void FixAnimatedPortraitLayers(GameObject obj)
        {
            obj.layer = CardOffscreenLayer;
            foreach (Transform child in obj.transform)
            {
                FixAnimatedPortraitLayers(child.gameObject);
            }
        }

        internal static void InitBossObjects()
        {
            CardOffscreenLayer = CardLoader.GetCardByName("!GIANTCARD_MOON").AnimatedPortrait.transform.GetChild(0).gameObject.layer;

            apocalypseBossPrefab = AssetManager.BossBundle.LoadAsset<GameObject>("ApocalypseBoss");
            helixBossPrefab = AssetManager.BossBundle.LoadAsset<GameObject>("LastHelixPortrait");
            FixAnimatedPortraitLayers(helixBossPrefab);

            bossSFX = // unity settings: compressed, preload data
            [
                AssetManager.BossBundle.LoadAsset<AudioClip>("bird_roar"),
                AssetManager.BossBundle.LoadAsset<AudioClip>("bird_mouth"),
                AssetManager.BossBundle.LoadAsset<AudioClip>("bird_down"),
                AssetManager.BossBundle.LoadAsset<AudioClip>("bird_laser_fire"),
                AssetManager.BossBundle.LoadAsset<AudioClip>("bird_dead"),
                AssetManager.BossBundle.LoadAsset<AudioClip>("helix_open"),
                AssetManager.BossBundle.LoadAsset<AudioClip>("helix_deactivate")
            ];
            bossLoop = // unity settings: streaming
            [
                AssetManager.BossBundle.LoadAsset<AudioClip>("second_trumpet_intro"),
                AssetManager.BossBundle.LoadAsset<AudioClip>("second_trumpet_intro_loop"),
                AssetManager.BossBundle.LoadAsset<AudioClip>("second_trumpet_main"),
                AssetManager.BossBundle.LoadAsset<AudioClip>("second_trumpet_main_loop")
            ];

            AssetManager.sfxClips.AddRange(bossSFX);
            AssetManager.musicLoops.AddRange(bossLoop);

            ApocalypseBossID = OpponentManager.Add(
                LobotomyPlugin.pluginGuid, "ApocalypseBossOpponent",
                ApocalypseBattleSequencer.ID, typeof(ApocalypseBossOpponent),
                NodeHelper.GetNodeTextureList("nodeApocalypseBoss1", "nodeApocalypseBoss2", "nodeApocalypseBoss3", "nodeApocalypseBoss4")
                ).Id;

            apocalypseRegion = ApocalypseBossUtils.CreateRegion();

            //whiteOrdealRegion = OrdealUtils.CreateWhiteOrdealRegion();
        }

        public static RegionData apocalypseRegion;
        //public static RegionData whiteOrdealRegion;
        //public static RegionData saviourRegion;
        //public static RegionData adultRegion;
        //public static RegionData jesterRegion;

        public static Opponent.Type ApocalypseBossID { get; private set; }
        //public static Opponent.Type SaviourBossID { get; private set; }


        public static GameObject apocalypseBossPrefab;
        //public static GameObject raptureBossPrefab;

        public static GameObject helixBossPrefab;

        public static AudioClip[] bossSFX;
        public static AudioClip[] bossLoop;

        public enum LobotomyBoss
        {
            Apocalypse,
            Saviour,
            Adult,
            Jester
        }
    }
}
