using DiskCardGame;
using WhistleWind.Core.Helpers;


namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Appearance_CannotBoostStatsBackground() {
            CannotBoostStatsBackground.appearance = CardHelper.CreateAppearance<CannotBoostStatsBackground>(pluginGuid, "CannotBoostStatsBackground").Id;
            RareCannotBoostStatsBackground.appearance = CardHelper.CreateAppearance<RareCannotBoostStatsBackground>(pluginGuid, "CannotBoostStatsBackgroundRare").Id;
        }
    }
    public class CannotBoostStatsBackground : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("noCampfireBackground.png");
    }
    public class RareCannotBoostStatsBackground : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("noCampfireBackground_rare.png");
    }
}
