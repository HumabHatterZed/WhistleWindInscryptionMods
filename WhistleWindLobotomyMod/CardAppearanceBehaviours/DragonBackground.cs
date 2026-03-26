using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Appearances {
        private static void AddDragonBackgrounds() {
            DragonHeadBackground.appearance = CardHelper.CreateAppearance<DragonHeadBackground>(LobotomyPlugin.pluginGuid, "DragonHeadBackground").Id;
            DragonHornsBackground.appearance = CardHelper.CreateAppearance<DragonHornsBackground>(LobotomyPlugin.pluginGuid, "DragonHornsBackground").Id;
            DragonBodyBackground.appearance = CardHelper.CreateAppearance<DragonBodyBackground>(LobotomyPlugin.pluginGuid, "DragonBodyBackground").Id;
        }
    }
    public class DragonHeadBackground : PixelAppearanceBehaviour {
        public static Appearance appearance;
        private static Sprite pixelBg = null;
        private static Texture2D bg = null;
        public override Sprite OverrideBackground() => pixelBg ??= TextureLoader.LoadSpriteFromFile("dragonHeadBackground_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("dragonHeadBackground.png"));
    }
    public class DragonHornsBackground : PixelAppearanceBehaviour {
        public static Appearance appearance;
        private static Sprite pixelBg = null;
        private static Texture2D bg = null;
        public override Sprite OverrideBackground() => pixelBg ??= TextureLoader.LoadSpriteFromFile("dragonHornsBackground_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("dragonHornsBackground.png"));
    }
    public class DragonBodyBackground : PixelAppearanceBehaviour {
        public static Appearance appearance;
        private static Sprite pixelBg = null;
        private static Texture2D bg = null;
        public override Sprite OverrideBackground() => pixelBg ??= TextureLoader.LoadSpriteFromFile("dragonBodyBackground_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("dragonBodyBackground.png"));
    }
}
