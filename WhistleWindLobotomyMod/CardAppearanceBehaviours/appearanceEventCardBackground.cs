using DiskCardGame;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

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
    public class EventBackground : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromBytes(Artwork.eventCardBackground);
    }
    public class RareEventBackground : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromBytes(Artwork.eventCardBackgroundRare);
    }
}
