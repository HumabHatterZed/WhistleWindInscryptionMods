using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Patches;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_MindStrike()
        {
            const string rulebookName = "Mind Strike";
            const string rulebookDescription = "When [creature] strikes another creature, deal no damage and instead inflict Sinking equal to half this card's Health, rounded up.";
            const string dialogue = "Why destroy the flesh when you can destroy the mind?";
            const string triggerText = "[creature] deals emotional damage!";
            MindStrike.ability = AbnormalAbilityHelper.CreateAbility<MindStrike>(
                "sigilMindFlayer",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: false, opponent: true, canStack: false).SetAbilityRedirect("Sinking", Sinking.iconId, GameColors.Instance.seafoam).Info
                .AddMetaCategories(AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook, AbilityMetaCategory.Part3Rulebook).ability;
        }
    }
    public class MindStrike : AbilityBehaviour, IModifyDamageTaken
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => target != null;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return target.AddStatusEffect<Sinking>((base.Card.Health + 1) / 2);
            yield return base.LearnAbility(0.3f);
        }
        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => attacker == base.Card;
        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => 0;
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => -1000;
    }
}
