using DiskCardGame;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public partial class Appearances {
        private static void AddGiantBirdAppearance() {
            GiantBirdAppearance.appearance = CardHelper.CreateAppearance<GiantBirdAppearance>(LobotomyPlugin.pluginGuid, "GiantBirdAppearance").Id;
        }
    }
    public class GiantBirdAppearance : GiantAnimatedPortrait {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() {
            base.ApplyAppearance();
            base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("apocalypseBird_cardBack.png"));
        }
    }
}
