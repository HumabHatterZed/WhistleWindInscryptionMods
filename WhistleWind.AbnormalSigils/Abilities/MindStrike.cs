using Core.AbilityClasses;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_MindStrike() {
            const string rulebookName = "Mind Strike";
            const string rulebookDescription = "When [creature] strikes another creature, cap the damage dealt to 1 then inflict Sinking equal to half this card's Health, rounded up.";
            const string dialogue = "Why destroy the flesh when you can destroy the mind?";
            const string triggerText = "[creature] deals emotional damage!";
            MindStrike.ability = AbnormalAbilityHelper.CreateAbility<MindStrike>(
                "sigilMindFlayer",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: false, opponent: true, canStack: false)
                .SetAbilityRedirect("Sinking", Sinking.iconId, GameColors.Instance.seafoam).Info
                .SetFlipYIfOpponent(true)
                .ability;
        }
    }
    /// <summary>
    /// [creature] may only deal 1 damage to creatures. When striking another creature, inflict Sinking equal to half this card's Health, rounded up.
    /// </summary>
    public class MindStrike : ModifyDamageDealtAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => target != null && target.LacksAbility(Ability.MadeOfStone) && target.LacksTrait(AbnormalPlugin.ImmuneToAilments);
        public override IEnumerator OnDealDamage(int amount, PlayableCard target) {
            yield return target.AddStatusEffect<Sinking>((base.Card.Health + 1) / 2);
            yield return base.LearnAbility(0.3f);
        }

        public override bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            return attacker == base.Card && damage > 1;
        }

        public override int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            return 1;
        }

        public override int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) {
            return -9001;
        }
    }
}
