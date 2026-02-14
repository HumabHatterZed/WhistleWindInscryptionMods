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

        internal static void InitBossObjects(AssetBundle bundle) {
            ApocalypseBossPrefab = bundle.LoadAsset<GameObject>("ApocalypseBoss");
            ApocalypseBossMouthPrefab = bundle.LoadAsset<GameObject>("ApocalypseMouth");
            HelixBossLaserPrefab = bundle.LoadAsset<GameObject>("LastHelixLaser");

            ShrineBossRedPrefab = bundle.LoadAsset<GameObject>("ShrineClaw");
            //ShrineBossWhitePrefab = bundle.LoadAsset<GameObject>("ShrineTentacle");
            ShrineBossBlackPrefab = bundle.LoadAsset<GameObject>("ShrineSpike");
            ShrineBossPalePrefab = bundle.LoadAsset<GameObject>("ShrineEye");

            ResourceBankManager.AddTableEffect(LobotomyPlugin.pluginGuid, "CityTableEffects", bundle.LoadAsset<GameObject>("CityTableEffects"));
            ResourceBankManager.AddTableEffect(LobotomyPlugin.pluginGuid, "SweeperTableEffects", bundle.LoadAsset<GameObject>("SweeperTableEffects"));

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
            indigoOrdealRegion = OrdealUtils.CreateSweeperRegion(whiteOrdealRegion);
        }

        public static RegionData apocalypseRegion;
        //public static RegionData saviourRegion;
        //public static RegionData adultRegion;
        //public static RegionData jesterRegion;
        public static RegionData whiteOrdealRegion;
        public static RegionData indigoOrdealRegion;

        public static Opponent.Type ApocalypseBossID { get; private set; }
        //public static Opponent.Type SaviourBossID { get; private set; }

        public static GameObject ApocalypseBossPrefab { get; private set; }
        public static GameObject ApocalypseBossMouthPrefab { get; private set; }
        //public static GameObject raptureBossPrefab;

        public static GameObject HelixBossLaserPrefab { get; private set; }

        public static GameObject ShrineBossRedPrefab { get; private set; }
        public static GameObject ShrineBossWhitePrefab { get; private set; }
        public static GameObject ShrineBossBlackPrefab { get; private set; }
        public static GameObject ShrineBossPalePrefab { get; private set; }
    }
}
