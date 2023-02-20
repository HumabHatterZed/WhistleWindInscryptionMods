using DiskCardGame;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Appearance_WhiteEmission()
        {
            ForcedWhiteEmission.appearance = CardHelper.CreateAppearance<ForcedWhiteEmission>(pluginGuid, "ForcedWhiteEmission").Id;
        }
    }
    public class ForcedWhiteEmission : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.forceEmissivePortrait = true;
            base.Card.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
        }
    }
}
