using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public partial class Appearances {
        private static void AddSweeperAppearance() {
            SweeperAppearance.appearance = CardHelper.CreateAppearance<SweeperAppearance>(LobotomyPlugin.pluginGuid, "SweeperAppearance").Id;
        }
    }
    public class SweeperAppearance : PixelAppearanceBehaviour {
        public static Appearance appearance;
        public override void ApplyAppearance() {
            base.Card.RenderInfo.portraitOverride = TextureLoader.LoadSpriteFromFile(Random.RandomRangeInt(0, 2) switch {
                1 => "sweeper1.png",
                2 => "sweeper2.png",
                _ => "sweeper.png"
            }, asm: LobotomyPlugin.ModAssembly);
        }
    }
}
