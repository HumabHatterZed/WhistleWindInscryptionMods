using DiskCardGame;
using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Appearances {
        private static void AddOrdealBackgrounds() {
            OrdealBackgroundGreen.appearance = CardHelper.CreateAppearance<OrdealBackgroundGreen>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundGreen").Id;
            OrdealBackgroundAmber.appearance = CardHelper.CreateAppearance<OrdealBackgroundAmber>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundAmber").Id;
            OrdealBackgroundViolet.appearance = CardHelper.CreateAppearance<OrdealBackgroundViolet>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundViolet").Id;
            OrdealBackgroundCrimson.appearance = CardHelper.CreateAppearance<OrdealBackgroundCrimson>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundCrimson").Id;
            OrdealBackgroundIndigo.appearance = CardHelper.CreateAppearance<OrdealBackgroundIndigo>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundIndigo").Id;
            OrdealBackgroundWhite.appearance = CardHelper.CreateAppearance<OrdealBackgroundWhite>(LobotomyPlugin.pluginGuid, "OrdealCardBackgroundWhite").Id;
        }
    }
    public class OrdealBackgroundGreen : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("ordealCardBackground_g.png");
    }
    public class OrdealBackgroundAmber : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("ordealCardBackground_a.png");
    }
    public class OrdealBackgroundViolet : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("ordealCardBackground_v.png");
    }
    public class OrdealBackgroundCrimson : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("ordealCardBackground_c.png");
    }
    public class OrdealBackgroundIndigo : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("ordealCardBackground_i.png");
    }
    public class OrdealBackgroundWhite : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("ordealCardBackground_w.png");
    }
}
