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
    /// [creature] has its base Power capped at 1. When striking another creature, Sinking equal to half this card's Health, rounded up.
    /// </summary>
    public class MindStrike : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => target != null && target.LacksAbility(Ability.MadeOfStone) && target.LacksTrait(AbnormalPlugin.ImmuneToAilments);
        public override IEnumerator OnDealDamage(int amount, PlayableCard target) {
            yield return target.AddStatusEffect<Sinking>((base.Card.Health + 1) / 2);
            yield return base.LearnAbility(0.3f);
        }
    }
}
