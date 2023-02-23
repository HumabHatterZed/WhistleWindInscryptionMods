using DiskCardGame;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Appearance_BlackFish()
        {
            AlternateBattlePortrait.appearance = CardHelper.CreateAppearance<AlternateBattlePortrait>(pluginGuid, "AlternateBattlePortrait").Id;
        }
    }
    public class AlternateBattlePortrait : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance()
        {
            if (base.Card is PlayableCard)
                base.Card.RenderInfo.portraitOverride = base.Card.Info.alternatePortrait;
            else
                base.Card.RenderInfo.portraitOverride = null;

        }
    }
}
