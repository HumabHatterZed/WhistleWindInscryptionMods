using DiskCardGame;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Appearance_ForcedEmission()
        {
            ForcedEmission.appearance = CardHelper.CreateAppearance<ForcedEmission>(pluginGuid, "ForcedEmission").Id;
        }
    }
    public class ForcedEmission : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.forceEmissivePortrait = true;
        }
    }
}
