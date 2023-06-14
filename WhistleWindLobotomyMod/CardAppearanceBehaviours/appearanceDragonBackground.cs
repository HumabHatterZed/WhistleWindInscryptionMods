using DiskCardGame;
using WhistleWind.Core.Helpers;


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
            base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("dragonHeadBackground");
    }
    public class DragonHornsBackground : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance() =>
            base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("dragonHornsBackground");
    }
    public class DragonBodyBackground : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance() =>
            base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("dragonBodyBackground");
    }
}
