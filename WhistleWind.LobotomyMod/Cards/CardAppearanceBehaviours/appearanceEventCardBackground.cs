using DiskCardGame;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_EventBackground()
        {
            EventBackground.appearance = CardHelper.CreateAppearance<EventBackground>(pluginGuid, "EventCardBackground").Id;
        }
    }
    public class EventBackground : CardAppearanceBehaviour
    {
        public static Appearance appearance;
        private static readonly Texture emptyBg = TextureLoader.LoadTextureFromBytes(Artwork.eventCardBackground);
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = emptyBg;
    }
}
