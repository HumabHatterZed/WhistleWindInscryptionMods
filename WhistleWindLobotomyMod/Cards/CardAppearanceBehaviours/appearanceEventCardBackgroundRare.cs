using DiskCardGame;
using UnityEngine;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_RareEventBackground()
        {
            RareEventBackground.appearance = CardHelper.CreateAppearance<RareEventBackground>("EventCardBackgroundRare").Id;
        }
    }
    public class RareEventBackground : CardAppearanceBehaviour
    {
        public static CardAppearanceBehaviour.Appearance appearance;
        private static Texture emptyBg = TextureLoader.LoadTextureFromBytes(Artwork.eventCardBackgroundRare);
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = emptyBg;
    }
}
