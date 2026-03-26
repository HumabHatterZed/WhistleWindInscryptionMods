using DiskCardGame;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Appearances {
        private static void AddOrdealBackgrounds() {
            OrdealBackgroundGreen.appearance = CardHelper.CreateAppearance<OrdealBackgroundGreen>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundGreen").Id;
            OrdealBackgroundGreenTerrain.appearance = CardHelper.CreateAppearance<OrdealBackgroundGreenTerrain>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundGreenTerrain").Id;
            OrdealBackgroundAmber.appearance = CardHelper.CreateAppearance<OrdealBackgroundAmber>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundAmber").Id;
            OrdealBackgroundViolet.appearance = CardHelper.CreateAppearance<OrdealBackgroundViolet>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundViolet").Id;
            OrdealBackgroundVioletTerrain.appearance = CardHelper.CreateAppearance<OrdealBackgroundVioletTerrain>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundVioletTerrain").Id;
            OrdealBackgroundVioletRed.appearance = CardHelper.CreateAppearance<OrdealBackgroundVioletRed>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundVioletRed").Id;
            OrdealBackgroundVioletWhite.appearance = CardHelper.CreateAppearance<OrdealBackgroundVioletWhite>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundVioletWhite").Id;
            OrdealBackgroundVioletBlack.appearance = CardHelper.CreateAppearance<OrdealBackgroundVioletBlack>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundVioletBlack").Id;
            OrdealBackgroundVioletPale.appearance = CardHelper.CreateAppearance<OrdealBackgroundVioletPale>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundVioletPale").Id;
            OrdealBackgroundCrimson.appearance = CardHelper.CreateAppearance<OrdealBackgroundCrimson>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundCrimson").Id;
            OrdealBackgroundCrimsonClimax.appearance = CardHelper.CreateAppearance<OrdealBackgroundCrimsonClimax>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundCrimsonClimax").Id;
            OrdealBackgroundIndigo.appearance = CardHelper.CreateAppearance<OrdealBackgroundIndigo>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundIndigo").Id;
            OrdealBackgroundIndigoTerrain.appearance = CardHelper.CreateAppearance<OrdealBackgroundIndigoTerrain>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundIndigoTerrain").Id;
            OrdealBackgroundWhite.appearance = CardHelper.CreateAppearance<OrdealBackgroundWhite>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundWhite").Id;
        }
    }
    public class OrdealBackgroundGreen : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_g.png"));
    }
    public class OrdealBackgroundGreenTerrain : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_g_terrain.png"));
    }
    public class OrdealBackgroundAmber : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_a.png"));
    }
    public class OrdealBackgroundViolet : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_v.png"));
    }
    public class OrdealBackgroundVioletTerrain : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_v_terrain.png"));
    }
    #region Midnights of Violet
    public class OrdealBackgroundVioletRed : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() {
            base.Card.RenderInfo.hiddenAttack = true;
            base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_v_r.png"));
        }
        public override void OnPreRenderCard() {
            base.Card.RenderInfo.hiddenAttack = true;
        }
    }
    public class OrdealBackgroundVioletWhite : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() {
            base.Card.RenderInfo.hiddenAttack = true;
            base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_v_w.png"));
        }
        public override void OnPreRenderCard() {
            base.Card.RenderInfo.hiddenAttack = true;
        }
    }
    public class OrdealBackgroundVioletBlack : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() {
            base.Card.RenderInfo.hiddenAttack = true;
            base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_v_b.png"));
        }
        public override void OnPreRenderCard() {
            base.Card.RenderInfo.hiddenAttack = true;
        }
    }
    public class OrdealBackgroundVioletPale : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() {
            base.Card.RenderInfo.hiddenAttack = true;
            base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_v_p.png"));
        }
        public override void OnPreRenderCard() {
            base.Card.RenderInfo.hiddenAttack = true;
        }
    }
    #endregion
    public class OrdealBackgroundCrimson : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_c.png"));
    }
    public class OrdealBackgroundCrimsonClimax : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_climax.png"));
    }
    public class OrdealBackgroundIndigo : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_i.png"));
    }
    public class OrdealBackgroundIndigoTerrain : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_i_terrain.png"));
    }
    public class OrdealBackgroundWhite : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("ordealCardBackground_w.png"));
    }
}
