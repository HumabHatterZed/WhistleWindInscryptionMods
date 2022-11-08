using DiskCardGame;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_WhiteEmission()
        {
            ForcedWhite.appearance = CardHelper.CreateAppearance<ForcedWhite>("ForcedWhite").Id;
        }
    }
    public class ForcedWhite : CardAppearanceBehaviour
    {
        public static CardAppearanceBehaviour.Appearance appearance;
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.forceEmissivePortrait = true;
            base.Card.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
        }
    }
}
