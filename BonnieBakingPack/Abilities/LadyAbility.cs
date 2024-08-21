using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BonniesBakingPack
{
    public class LadyAbility : CardAppearanceBehaviour
    {
        public static CardAppearanceBehaviour.Appearance CardAppearance;

        internal static void Add()
        {
            CardAppearance = CardAppearanceBehaviourManager.Add(BakingPlugin.pluginGuid, "LadyAbility", typeof(LadyAbility)).Id;
        }

        public override void ApplyAppearance()
        {
            base.Card.StatsLayer.SetEmissionColor(GameColors.Instance.nearWhite);
        }
    }
}
