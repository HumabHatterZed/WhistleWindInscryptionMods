﻿using InscryptionAPI.PixelCard;
using UnityEngine;
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
    public class DragonHeadBackground : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override Sprite OverrideBackground() => TextureLoader.LoadSpriteFromFile("dragonHeadBackground_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("dragonHeadBackground.png");
    }
    public class DragonHornsBackground : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override Sprite OverrideBackground() => TextureLoader.LoadSpriteFromFile("dragonHornsBackground_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("dragonHornsBackground.png");
    }
    public class DragonBodyBackground : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override Sprite OverrideBackground() => TextureLoader.LoadSpriteFromFile("dragonBodyBackground_pixel.png");
        public override void ApplyAppearance() => base.Card.RenderInfo.baseTextureOverride = TextureLoader.LoadTextureFromFile("dragonBodyBackground.png");
    }
}
