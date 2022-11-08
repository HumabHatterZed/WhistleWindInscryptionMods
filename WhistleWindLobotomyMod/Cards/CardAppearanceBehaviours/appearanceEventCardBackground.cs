using DiskCardGame;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_EventBackground()
        {
            EventBackground.appearance = CardHelper.CreateAppearance<EventBackground>("EventCardBackground").Id;
        }
    }
    public class EventBackground : CardAppearanceBehaviour
    {
        public static CardAppearanceBehaviour.Appearance appearance;
        private static Texture emptyBg = TextureLoader.LoadTextureFromBytes(Artwork.eventCardBackground);
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = emptyBg;
    }
}
