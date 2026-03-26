using DiskCardGame;
using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Appearances {
        private static void AddRoseBackground() {
            RoseBackground.appearance = CardHelper.CreateAppearance<RoseBackground>(LobotomyPlugin.pluginGuid, "RoseBackground").Id;
        }
    }
    public class RoseBackground : CardAppearanceBehaviour {
        public static Appearance appearance;
        private static Texture2D bg = null;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = (bg ??= TextureLoader.LoadTextureFromFile("roseBackground.png"));
    }
}
