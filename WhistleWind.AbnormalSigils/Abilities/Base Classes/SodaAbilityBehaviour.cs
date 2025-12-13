using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils {
    public abstract class SodaAbilityBehaviour : AbilityBehaviour {
        public abstract Ability AbilityToAdd { get; }
        public abstract SpecialTriggeredAbility StatusEffectId { get; }
        public abstract string SingletonId { get; }
        public virtual int BasePotency { get; } = 3;

        public override bool RespondsToSacrifice() => true;
        public override IEnumerator OnSacrifice() {
            yield return base.PreSuccessfulTriggerSequence();
            yield return Sequence(BoardManager.Instance.CurrentSacrificeDemandingCard, StatusEffectId, AbilityToAdd, BasePotency, SingletonId);
            yield return base.LearnAbility(0.5f);
        }

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
            => attacker == base.Card && base.Card.Info.IsTargetedSpell() && slot.Card != null;
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            yield return base.PreSuccessfulTriggerSequence();
            yield return Sequence(slot.Card, StatusEffectId, AbilityToAdd, BasePotency, SingletonId);
            yield return base.LearnAbility(0.5f);
        }

        public static IEnumerator Sequence(PlayableCard target, SpecialTriggeredAbility statusId, Ability abilityToAdd, int potency, string singletonId) {
            CardModificationInfo mod = new CardModificationInfo(abilityToAdd) { fromCardMerge = true, singletonId = singletonId };
            target.Anim.PlayTransformAnimation();
            target.AddTemporaryMod(mod);
            target.Status.hiddenAbilities.Add(abilityToAdd);
            yield return ApplyCounterBehaviour(target, statusId, potency);
        }

        private static IEnumerator ApplyCounterBehaviour(PlayableCard target, SpecialTriggeredAbility statusId, int basePotency) {
            int potency = basePotency;
            bool sodaLover = target.HasTrait(AbnormalPlugin.SodaLover);
            if (sodaLover)
                potency++;

            yield return target.AddStatusEffect(statusId, potency);
            //target.GetStatusEffect(statusId).SetPotency(potency, false);

            if (sodaLover && !DialogueEventsData.EventIsPlayed("SodaLover")) {
                yield return new WaitForSeconds(0.4f);
                yield return DialogueManager.PlayDialogueEventSafe("SodaLover");
            }
        }
    }
}
