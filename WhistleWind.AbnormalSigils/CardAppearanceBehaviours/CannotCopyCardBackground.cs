using DiskCardGame;
using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Appearance_CannotCopyCardBackground() {
            CannotCopyCardBackground.appearance = CardHelper.CreateAppearance<CannotCopyCardBackground>(pluginGuid, "CannotCopyCardBackground").Id;
            RareCannotCopyCardBackground.appearance = CardHelper.CreateAppearance<RareCannotCopyCardBackground>(pluginGuid, "CannotCopyCardBackgroundRare").Id;
        }
    }
    public class CannotCopyCardBackground : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("cannotCopyBackground.png");
    }
    public class RareCannotCopyCardBackground : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("cannotCopyBackground_rare.png");
    }
}
