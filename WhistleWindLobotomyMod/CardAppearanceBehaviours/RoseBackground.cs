using DiskCardGame;
using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;


namespace WhistleWindLobotomyMod {
    public partial class Appearances {
        private static void AddRoseBackground() {
            RoseBackground.appearance = CardHelper.CreateAppearance<RoseBackground>(LobotomyPlugin.pluginGuid, "RoseBackground").Id;
            Rose2Background.appearance = CardHelper.CreateAppearance<Rose2Background>(LobotomyPlugin.pluginGuid, "Rose2Background").Id;
            Rose3Background.appearance = CardHelper.CreateAppearance<Rose3Background>(LobotomyPlugin.pluginGuid, "Rose3Background").Id;
            Rose4Background.appearance = CardHelper.CreateAppearance<Rose4Background>(LobotomyPlugin.pluginGuid, "Rose4Background").Id;
        }
    }
    public class RoseBackground : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static readonly Texture2D stageBg = TextureLoader.LoadTextureFromFile("roseBackground.png");

        public override void OnPreRenderCard() => ApplyAppearance();
        public override void ApplyAppearance() {
            base.Card.RenderInfo.baseTextureOverride = stageBg;
        }
    }
    public class Rose2Background : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static readonly Texture2D stageBg = TextureLoader.LoadTextureFromFile("roseBackground2.png");

        public override void OnPreRenderCard() => ApplyAppearance();
        public override void ApplyAppearance() {
            base.Card.RenderInfo.baseTextureOverride = stageBg;
        }
    }
    public class Rose3Background : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static readonly Texture2D stageBg = TextureLoader.LoadTextureFromFile("roseBackground3.png");

        public override void OnPreRenderCard() => ApplyAppearance();
        public override void ApplyAppearance() {
            base.Card.RenderInfo.baseTextureOverride = stageBg;
        }
    }
    public class Rose4Background : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static readonly Texture2D stageBg = TextureLoader.LoadTextureFromFile("roseBackground4.png");

        public override void OnPreRenderCard() => ApplyAppearance();
        public override void ApplyAppearance() {
            base.Card.RenderInfo.baseTextureOverride = stageBg;
        }
    }
}
