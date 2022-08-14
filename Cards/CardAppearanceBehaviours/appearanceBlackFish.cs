using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Appearance_BlackFish()
        {
            BlackFish.appearance = CardHelper.CreateAppearance<BlackFish>("BlackFish").Id;
        }
    }
    public class BlackFish : CardAppearanceBehaviour
    {
        public static CardAppearanceBehaviour.Appearance appearance;
        public override void ApplyAppearance()
        {
            if (base.Card is PlayableCard)
            {
                base.Card.RenderInfo.portraitOverride = base.Card.Info.alternatePortrait;
            }
            else
            {
                base.Card.RenderInfo.portraitOverride = null;
            }
        }
    }
}
