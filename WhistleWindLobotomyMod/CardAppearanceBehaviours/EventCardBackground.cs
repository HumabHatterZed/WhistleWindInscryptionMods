using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Appearances {
        private static void AddEventBackgrounds() {
            EventBackground.appearance = CardHelper.CreateAppearance<EventBackground>(LobotomyPlugin.pluginGuid, "EventCardBackground").Id;
            RareEventBackground.appearance = CardHelper.CreateAppearance<RareEventBackground>(LobotomyPlugin.pluginGuid, "EventCardBackgroundRare").Id;
        }
    }
    public class EventBackground : PixelAppearanceBehaviour {
        public static Appearance appearance;
        private static Sprite pixelBg = null;
        private static Texture2D bg = null;
        public override Sprite OverrideBackground() => pixelBg ??= TextureLoader.LoadSpriteFromFile("eventCardBackground_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("eventCardBackground.png"));
    }
    public class RareEventBackground : PixelAppearanceBehaviour {
        public static Appearance appearance;
        private static Sprite pixelBg = null;
        private static Texture2D bg = null;
        public override Sprite OverrideBackground() => pixelBg ??= TextureLoader.LoadSpriteFromFile("eventCardBackgroundRare_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("eventCardBackgroundRare.png"));
    }
}
