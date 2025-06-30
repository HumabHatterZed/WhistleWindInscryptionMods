using Core.AbilityClasses;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_MindStrike() {
            const string rulebookName = "Mind Strike";
            const string rulebookDescription = "[creature] has its base Power capped at 1. When striking another creature, Sinking equal to half this card's Health, rounded up.";
            const string dialogue = "Why destroy the flesh when you can destroy the mind?";
            const string triggerText = "[creature] deals emotional damage!";
            MindStrike.ability = AbnormalAbilityHelper.CreateAbility<MindStrike>(
                "sigilMindFlayer",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: false, opponent: true, canStack: false).SetAbilityRedirect("Sinking", Sinking.iconId, GameColors.Instance.seafoam).Info
                .AddMetaCategories(AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook, AbilityMetaCategory.Part3Rulebook).ability;
        }
    }
    /// <summary>
    /// When [creature] strikes another creature, deal no damage and instead inflict Sinking equal to half this card's Health, rounded up.
    /// </summary>
    public class MindStrike : ModifyDamageDealtAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => target != null && target.LacksTrait(AbnormalPlugin.ImmuneToAilments);
        public override IEnumerator OnDealDamage(int amount, PlayableCard target) {
            yield return target.AddStatusEffect<Sinking>((base.Card.Health + 1) / 2);
            yield return base.LearnAbility(0.3f);
        }

        public override bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => attacker == base.Card;
        public override int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => !target.HasStatusEffect<Sinking>() ? 0 : Mathf.Min(0, damage - base.Card.Attack);
        public override int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => -1000;
    }
}
