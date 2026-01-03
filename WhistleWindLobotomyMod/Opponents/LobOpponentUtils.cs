using DiskCardGame;
using InscryptionAPI.Encounters;
using InscryptionAPI.Resource;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Opponents {
    public static class LobOpponentUtils {
        public static bool FightingCustomBoss() {
            return TurnManager.Instance?.Opponent is LobotomyBossOpponent;
        }
        public static bool FightingCustomOpponent() {
            return TurnManager.Instance.Opponent is LobotomyOpponent;
        }

        public static bool IsCustomBoss<T>(out T opponent) where T : LobotomyBossOpponent {
            if (TurnManager.Instance?.Opponent != null && TurnManager.Instance.Opponent is T opp) {
                opponent = opp;
                return true;
            }
            opponent = null;
            return false;
        }

        public static int CardOffscreenLayer { get; internal set; }

        /// <summary>
        /// Recursively goes through each Transform in a given GameObjct and sets its layer to CardOffscreenLayer.
        /// </summary>
        /// <param name="obj"></param>
        private static void FixAnimatedPortraitLayers(GameObject obj) {
            obj.layer = CardOffscreenLayer;
            foreach (Transform child in obj.transform) {
                FixAnimatedPortraitLayers(child.gameObject);
            }
        }

        internal static void InitBossObjects(AssetBundle bundle) {
            CardOffscreenLayer = CardLoader.GetCardByName("!GIANTCARD_MOON").AnimatedPortrait.transform.GetChild(0).gameObject.layer;

            ApocalypseBossPrefab = bundle.LoadAsset<GameObject>("ApocalypseBoss");
            ApocalypseBossMouthPrefab = bundle.LoadAsset<GameObject>("ApocalypseMouth");
            HelixBossPrefab = bundle.LoadAsset<GameObject>("LastHelixPortrait");
            HelixBossLaserPrefab = bundle.LoadAsset<GameObject>("LastHelixLaser");
            GrantUsLovePrefab = bundle.LoadAsset<GameObject>("GrantUsLovePortrait");
            FixAnimatedPortraitLayers(HelixBossPrefab);
            FixAnimatedPortraitLayers(GrantUsLovePrefab);

            ResourceBankManager.Add(LobotomyPlugin.pluginGuid, "Prefabs/Environment/TableEffects/" + "CityTableEffects", bundle.LoadAsset<GameObject>("CityTableEffects"));

            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("bird_roar"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("bird_mouth"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("bird_down"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("bird_laser_fire"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("bird_dead"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("helix_open"));
            AssetManager.sfxClips.Add(bundle.LoadAsset<AudioClip>("helix_deactivate"));

            ApocalypseBossID = OpponentManager.Add(
                LobotomyPlugin.pluginGuid, "ApocalypseBossOpponent",
                ApocalypseBattleSequencer.ID, typeof(ApocalypseBossOpponent),
                NodeHelper.GetNodeTextureList("nodeApocalypseBoss1", "nodeApocalypseBoss2", "nodeApocalypseBoss3", "nodeApocalypseBoss4")
                ).Id;

            apocalypseRegion = ApocalypseBossUtils.CreateRegion();
            whiteOrdealRegion = OrdealUtils.CreateWhiteOrdealRegion();
        }

        public static RegionData apocalypseRegion;
        public static RegionData whiteOrdealRegion;
        //public static RegionData saviourRegion;
        //public static RegionData adultRegion;
        //public static RegionData jesterRegion;

        public static Opponent.Type ApocalypseBossID { get; private set; }
        //public static Opponent.Type SaviourBossID { get; private set; }


        public static GameObject ApocalypseBossPrefab { get; private set; }
        public static GameObject ApocalypseBossMouthPrefab { get; private set; }
        //public static GameObject raptureBossPrefab;

        public static GameObject HelixBossPrefab { get; private set; }
        public static GameObject HelixBossLaserPrefab { get; private set; }
        public static GameObject GrantUsLovePrefab { get; private set; }

        public enum LobotomyBoss {
            Apocalypse,
            Saviour,
            Adult,
            Jester
        }
    }
}
