using DiskCardGame;
using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Appearance_CannotGainSigilsBackground() {
            CannotGainSigilsBackground.appearance = CardHelper.CreateAppearance<CannotGainSigilsBackground>(pluginGuid, "CannotGainSigilsBackground").Id;
            RareCannotGainSigilsBackground.appearance = CardHelper.CreateAppearance<RareCannotGainSigilsBackground>(pluginGuid, "CannotGainSigilsBackgroundRare").Id;
        }
    }
    public class CannotGainSigilsBackground : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("cannotGainSigilsBackground.png");
    }
    public class RareCannotGainSigilsBackground : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("cannotGainSigilsBackground_rare.png");
    }
}
