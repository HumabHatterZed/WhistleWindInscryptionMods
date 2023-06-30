﻿using DiskCardGame;
using GBC;
using InscryptionAPI.Card;
using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_MiracleWorker()
        {
            MiracleWorkerAppearance.appearance = CardHelper.CreateAppearance<MiracleWorkerAppearance>(pluginGuid, "MiracleWorkerAppearance").Id;
        }
    }
    public class MiracleWorkerAppearance : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override Sprite OverridePixelPortrait()
        {
            return LobotomyPlugin.UpdateDoctorPixelPortrait(LobotomyConfigManager.Instance.NumOfBlessings);
        }
        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.portraitOverride = LobotomyPlugin.PlagueDoctorPortraits[Mathf.Min(11, LobotomyConfigManager.Instance.NumOfBlessings)];
            base.Card.RenderInfo.forceEmissivePortrait |= LobotomyConfigManager.Instance.NumOfBlessings >= 11;
        }
    }
}
