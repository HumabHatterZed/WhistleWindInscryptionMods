using DiskCardGame;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_DragonBackground()
        {
            DragonHeadBackground.appearance = CardHelper.CreateAppearance<DragonHeadBackground>(pluginGuid, "DragonHeadBackground").Id;
            DragonHornsBackground.appearance = CardHelper.CreateAppearance<DragonHornsBackground>(pluginGuid, "DragonHornsBackground").Id;
            DragonBodyBackground.appearance = CardHelper.CreateAppearance<DragonBodyBackground>(pluginGuid, "DragonBodyBackground").Id;
        }
    }
    public class DragonHeadBackground : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance() =>
            base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromBytes(Artwork.dragonHeadBackground);
    }
    public class DragonHornsBackground : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance() =>
            base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromBytes(Artwork.dragonHornsBackground);
    }
    public class DragonBodyBackground : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance() =>
            base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromBytes(Artwork.dragonBodyBackground);
    }
}
