using DiskCardGame;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public partial class Appearances {
        private static void AddGiantTowerAppearance() {
            GiantTowerAppearance.appearance = CardHelper.CreateAppearance<GiantTowerAppearance>(LobotomyPlugin.pluginGuid, "GiantTowerAppearance").Id;
        }
    }
    public class GiantTowerAppearance : GiantAnimatedPortrait {
        public static Appearance appearance;

        public override void ApplyAppearance() {
            base.ApplyAppearance();
            base.Card.StatsLayer.SetEmissionColor(GameColors.Instance.brightLimeGreen);
            base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("tower_cardBack.png");
        }
    }
}
