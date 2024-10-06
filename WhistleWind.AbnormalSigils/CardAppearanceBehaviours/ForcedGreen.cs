using DiskCardGame;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Appearance_GreenEmission()
        {
            ForcedGreenEmission.appearance = CardHelper.CreateAppearance<ForcedGreenEmission>(pluginGuid, "ForcedGreenEmission").Id;
        }
    }
    public class ForcedGreenEmission : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.forceEmissivePortrait = true;
            base.Card.StatsLayer.SetEmissionColor(GameColors.Instance.brightLimeGreen);
        }
    }
}
