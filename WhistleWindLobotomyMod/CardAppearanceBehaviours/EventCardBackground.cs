using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_EventBackground()
        {
            EventBackground.appearance = CardHelper.CreateAppearance<EventBackground>(pluginGuid, "EventCardBackground").Id;
            RareEventBackground.appearance = CardHelper.CreateAppearance<RareEventBackground>(pluginGuid, "EventCardBackgroundRare").Id;
        }
    }
    public class EventBackground : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override Sprite OverrideBackground() => TextureLoader.LoadSpriteFromFile("eventCardBackground_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("eventCardBackground.png");
    }
    public class RareEventBackground : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override Sprite OverrideBackground() => TextureLoader.LoadSpriteFromFile("eventCardBackgroundRare_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("eventCardBackgroundRare.png");
    }
}
