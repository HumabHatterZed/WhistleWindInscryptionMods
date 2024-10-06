using DiskCardGame;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Appearance_PurpleEmission()
        {
            ForcedPurpleEmission.appearance = CardHelper.CreateAppearance<ForcedPurpleEmission>(pluginGuid, "ForcedPurpleEmission").Id;
        }
    }
    public class ForcedPurpleEmission : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.forceEmissivePortrait = true;
            base.Card.StatsLayer.SetEmissionColor(GameColors.Instance.purple);
        }
    }
}
