using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;

using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Sin : StatusEffectBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        public override string CardModSingletonName => "sin";

        public override List<string> EffectDecalIds() => new();
    }
    public partial class LobotomyPlugin
    {
        private void StatusEffect_Sin()
        {
            const string rName = "Sin";
            const string rDesc = "This card will be killed at 5+ Sin.";

            Sin.specialAbility = StatusEffectManager.NewStatusEffect<Sin>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilUnjustScale.png",
                powerLevel: -3, iconColour: Color.black,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect }).BehaviourId;
        }
    }
}
