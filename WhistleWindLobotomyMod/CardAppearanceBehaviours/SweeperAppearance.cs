using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_SweeperAppearance()
        {
            SweeperAppearance.appearance = CardHelper.CreateAppearance<SweeperAppearance>(pluginGuid, "SweeperAppearance").Id;
        }
    }
    public class SweeperAppearance : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.portraitOverride = TextureLoader.LoadSpriteFromFile(Random.RandomRangeInt(0, 2) switch
            {
                1 => "sweeper1.png",
                2 => "sweeper2.png",
                _ => "sweeper.png"
            }, asm: LobotomyPlugin.ModAssembly);
        }
    }
}
