using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Prudence : StatusEffectBehaviour, IModifyDamageTaken
    {
        public static SpecialTriggeredAbility specialAbility;

        public override string CardModSingletonName => "prudence";

        public override List<string> EffectDecalIds() => new();

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return target == base.PlayableCard;
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            damage += EffectSeverity;
            return damage;
        }

        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Prudence()
        {
            const string rName = "Prudence";
            const string rDesc = "When this card is struck, take damage equal to this status effect.";

            Prudence.specialAbility = StatusEffectManager.NewStatusEffect<Prudence>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilPrudence.png", pixelIconTexture: "sigilPrudence_pixel.png",
                powerLevel: -3, iconColour: GameColors.Instance.lightPurple,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect }).BehaviourId;
        }
    }
}
