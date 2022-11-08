using DiskCardGame;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_BlackFish()
        {
            BlackFish.appearance = CardHelper.CreateAppearance<BlackFish>("BlackFish").Id;
        }
    }
    public class BlackFish : CardAppearanceBehaviour
    {
        public static CardAppearanceBehaviour.Appearance appearance;
        public override void ApplyAppearance()
        {
            if (base.Card is PlayableCard)
            {
                base.Card.RenderInfo.portraitOverride = base.Card.Info.alternatePortrait;
            }
            else
            {
                base.Card.RenderInfo.portraitOverride = null;
            }
        }
    }
}
