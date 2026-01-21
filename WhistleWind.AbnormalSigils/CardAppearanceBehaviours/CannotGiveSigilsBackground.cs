using DiskCardGame;
using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Appearance_CannotGiveSigilsBackground() {
            CannotGiveSigilsBackground.appearance = CardHelper.CreateAppearance<CannotGiveSigilsBackground>(pluginGuid, "CannotGiveSigilsBackground").Id;
            RareCannotGiveSigilsBackground.appearance = CardHelper.CreateAppearance<RareCannotGiveSigilsBackground>(pluginGuid, "CannotGiveSigilsBackgroundRare").Id;
        }
    }
    public class CannotGiveSigilsBackground : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("cannotGiveSigilsBackground.png");
    }
    public class RareCannotGiveSigilsBackground : CardAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("cannotGiveSigilsBackground_rare.png");
    }
}
