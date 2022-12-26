using DiskCardGame;
using WhistleWind.Core.Helpers;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_ForcedEmission()
        {
            Forced.appearance = CardHelper.CreateAppearance<Forced>(pluginGuid, "Forced").Id;
        }
    }
    public class Forced : CardAppearanceBehaviour
    {
        public static CardAppearanceBehaviour.Appearance appearance;
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.forceEmissivePortrait = true;
        }
    }
}
