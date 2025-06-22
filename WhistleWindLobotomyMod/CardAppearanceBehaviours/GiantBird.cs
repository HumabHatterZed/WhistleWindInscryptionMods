using DiskCardGame;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public partial class Appearances {
        private static void AddGiantBirdAppearance() {
            GiantBirdAppearance.appearance = CardHelper.CreateAppearance<GiantBirdAppearance>(LobotomyPlugin.pluginGuid, "GiantBirdAppearance").Id;
        }
    }
    public class GiantBirdAppearance : GiantAnimatedPortrait {
        public static Appearance appearance;

        public override void ApplyAppearance() {
            base.ApplyAppearance();
            base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("apocalypseBird_cardBack.png");
        }
    }
}
