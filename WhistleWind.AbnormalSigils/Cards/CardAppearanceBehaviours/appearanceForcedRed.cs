using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Appearance_RedEmission()
        {
            ForcedRed.appearance = AbnormalCardHelper.CreateAppearance<ForcedRed>("ForcedRed").Id;
        }
    }
    public class ForcedRed : CardAppearanceBehaviour
    {
        public static CardAppearanceBehaviour.Appearance appearance;
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.forceEmissivePortrait = true;
            base.Card.StatsLayer.SetEmissionColor(GameColors.Instance.glowRed);
        }
    }
}
