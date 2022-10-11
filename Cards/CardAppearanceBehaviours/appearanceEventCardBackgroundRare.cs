using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Appearance_RareEventBackground()
        {
            RareEventBackground.appearance = CardHelper.CreateAppearance<RareEventBackground>("EventCardBackgroundRare").Id;
        }
    }
    public class RareEventBackground : CardAppearanceBehaviour
    {
        public static CardAppearanceBehaviour.Appearance appearance;
        private static Texture emptyBg = WstlTextureHelper.LoadTextureFromResource(Artwork.eventCardBackgroundRare);
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.baseTextureOverride = emptyBg;
        }
    }
}
