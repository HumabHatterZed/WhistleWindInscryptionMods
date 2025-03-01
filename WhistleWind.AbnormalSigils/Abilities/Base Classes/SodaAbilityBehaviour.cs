using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public abstract class SodaAbilityBehaviour : AbilityBehaviour
    {
        public abstract Ability AbilityToAdd { get; }
        public abstract SpecialTriggeredAbility StatusEffectId { get; }
        public abstract string SingletonId { get; }

        public virtual int BasePotency { get; } = 3;

        public override bool RespondsToSacrifice() => true;
        public override IEnumerator OnSacrifice()
        {
            yield return Sequence(BoardManager.Instance.CurrentSacrificeDemandingCard);
            yield return base.LearnAbility(0.5f);
        }

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
            => attacker == base.Card && base.Card.Info.IsTargetedSpell() && slot.Card != null;
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => Sequence(slot.Card);
        
        private IEnumerator Sequence(PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            CardModificationInfo mod = new CardModificationInfo(AbilityToAdd) { fromCardMerge = true, singletonId = SingletonId };
            target.Anim.PlayTransformAnimation();
            target.AddTemporaryMod(mod);
            yield return ApplyCounterBehaviour(target);
        }

        private IEnumerator ApplyCounterBehaviour(PlayableCard target)
        {
            int potency = BasePotency;
            bool sodaLover = target.HasTrait(AbnormalPlugin.SodaLover);
            if (sodaLover)
                potency++;

            yield return target.AddStatusEffect(StatusEffectId, potency);
            target.GetStatusEffect(StatusEffectId).SetPotency(potency, false);

            if (sodaLover && !DialogueEventsData.EventIsPlayed("SodaLover"))
            {
                yield return new WaitForSeconds(0.4f);
                yield return DialogueManager.PlayDialogueEventSafe("SodaLover");
            }
        }
    }
}
