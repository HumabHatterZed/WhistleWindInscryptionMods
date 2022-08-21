using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Appearance_WhiteEmission()
        {
            ForcedWhite.appearance = CardHelper.CreateAppearance<ForcedWhite>("ForcedWhite").Id;
        }
    }
    public class ForcedWhite : CardAppearanceBehaviour
    {
        public static CardAppearanceBehaviour.Appearance appearance;
        public override void ApplyAppearance()
        {
            if (base.Card.Info.name != "wstl_plagueDoctor" || (base.Card.Info.name == "wstl_plagueDoctor" && ConfigUtils.Instance.NumOfBlessings == 11))
            {
                base.Card.RenderInfo.forceEmissivePortrait = true;
                base.Card.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
            }
        }
    }
}
