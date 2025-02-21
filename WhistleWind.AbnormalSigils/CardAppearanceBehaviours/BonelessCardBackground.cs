using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Appearance_BonelessCardBackground()
        {
            BonelessCardBackground.appearance = CardHelper.CreateAppearance<BonelessCardBackground>(pluginGuid, "BonelessCardBackground").Id;
            RareBonelessCardBackground.appearance = CardHelper.CreateAppearance<RareBonelessCardBackground>(pluginGuid, "BonelessCardBackgroundRare").Id;
        }
    }
    public class BonelessCardBackground : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override Sprite OverrideBackground() => TextureLoader.LoadSpriteFromFile("bonelessBackground_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("bonelessBackground.png");
    }
    public class RareBonelessCardBackground : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override Sprite OverrideBackground() => TextureLoader.LoadSpriteFromFile("bonelessBackground_rare_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("bonelessBackground_rare.png");
    }
}
