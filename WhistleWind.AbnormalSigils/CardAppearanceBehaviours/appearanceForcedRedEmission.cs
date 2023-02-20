using DiskCardGame;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Appearance_RedEmission()
        {
            ForcedRedEmission.appearance = CardHelper.CreateAppearance<ForcedRedEmission>(pluginGuid, "ForcedRedEmission").Id;
        }
    }
    public class ForcedRedEmission : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.forceEmissivePortrait = true;
            base.Card.StatsLayer.SetEmissionColor(GameColors.Instance.glowRed);
        }
    }
}
