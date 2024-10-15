using DiskCardGame;
using GBC;
using InscryptionAPI.Card;
using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Appearance_MiniGiantPortrait()
        {
            MiniGiantPortrait.appearance = CardHelper.CreateAppearance<MiniGiantPortrait>(pluginGuid, "MiniGiantPortrait").Id;
        }
    }
    public class MiniGiantPortrait : PixelAppearanceBehaviour
    {
        public static Appearance appearance;

        public override void ApplyAppearance()
        {
            if (base.Card.Info.HasTrait(Trait.Giant))
                base.ApplyAppearance();
        }
    }
}
