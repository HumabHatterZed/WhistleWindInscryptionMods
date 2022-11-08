using DiskCardGame;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_ForcedEmission()
        {
            Forced.appearance = CardHelper.CreateAppearance<Forced>("Forced").Id;
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
