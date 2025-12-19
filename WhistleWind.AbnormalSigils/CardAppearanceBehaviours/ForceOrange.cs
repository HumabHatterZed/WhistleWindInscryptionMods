using DiskCardGame;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Appearance_OrangeEmission() {
            ForcedOrangeEmission.appearance = CardHelper.CreateAppearance<ForcedOrangeEmission>(pluginGuid, "ForcedOrangeEmission").Id;
        }
    }
    public class ForcedOrangeEmission : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() {
            base.Card.RenderInfo.forceEmissivePortrait = true;
            base.Card.StatsLayer.SetEmissionColor(GameColors.Instance.orange);
        }
    }
}
