using DiskCardGame;
using GBC;
using InscryptionAPI.Card;
using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Appearance_AlternateBattlePortrait()
        {
            AlternateBattlePortrait.appearance = CardHelper.CreateAppearance<AlternateBattlePortrait>(pluginGuid, "AlternateBattlePortrait").Id;
        }
    }
    public class AlternateBattlePortrait : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override Sprite OverridePixelPortrait()
        {
            if (base.Card is PixelPlayableCard)
                return base.Card.Info.PixelAlternatePortrait();

            return null;
        }
        public override void ApplyAppearance()
        {
            if (base.Card is PlayableCard)
                base.Card.RenderInfo.portraitOverride = base.Card.Info.alternatePortrait;
            else
                base.Card.RenderInfo.portraitOverride = null;
        }
    }
}
