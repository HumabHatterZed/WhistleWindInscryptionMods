﻿using DiskCardGame;
using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_GiantBird()
        {
            GiantBirdAppearance.appearance = CardHelper.CreateAppearance<GiantBirdAppearance>(pluginGuid, "GiantBirdAppearance").Id;
        }
    }
    public class GiantBirdAppearance : GiantAnimatedPortrait
    {
        public static Appearance appearance;

        public override void ApplyAppearance()
        {
            base.ApplyAppearance();
            base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("apocalypseBird_cardBack.png");
        }
    }
}
