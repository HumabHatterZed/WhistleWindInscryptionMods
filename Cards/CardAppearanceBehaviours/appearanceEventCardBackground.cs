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
        private void Appearance_EventBackground()
        {
            EventBackground.appearance = CardHelper.CreateAppearance<RareEventBackground>("EventCardBackground").Id;
        }
    }
    public class EventBackground : CardAppearanceBehaviour
    {
        public static CardAppearanceBehaviour.Appearance appearance;
        private static Texture emptyBg = WstlTextureHelper.LoadTextureFromResource(Artwork.eventCardBackground);
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.baseTextureOverride = emptyBg;
        }
    }
}
