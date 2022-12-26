using DiskCardGame;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Appearance_RedEmission()
        {
            ForcedRed.appearance = CardHelper.CreateAppearance<ForcedRed>("ForcedRed").Id;
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
