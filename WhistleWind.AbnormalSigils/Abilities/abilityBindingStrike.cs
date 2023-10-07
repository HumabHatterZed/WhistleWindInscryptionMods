﻿using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_BindingStrike()
        {
            const string rulebookName = "Binding Strike";
            const string rulebookDescription = "When [creature] strikes an opposing creature, inflict Bind equal to twice this card's Power.";
            const string dialogue = "Your beast falls behind the pack.";
            BindingStrike.ability = AbnormalAbilityHelper.CreateAbility<BindingStrike>(
                "sigilBindingStrike",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class BindingStrike : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        // only apply Bind if target isn't null/dead
        public override bool RespondsToDealDamage(int amount, PlayableCard target)
            => target != null && !target.Dead && target.LacksTrait(AbnormalPlugin.ImmuneToAilments);

        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return AddBindToCard(target);
            base.LearnAbility(0.4f);
        }
        private IEnumerator AddBindToCard(PlayableCard card)
        {
            int stacks = base.Card.Attack * 2;
            card.Anim.LightNegationEffect();
            if (card.HasStatusEffect<Bind>())
                card.UpdateStatusEffectCount<Bind>(stacks, false);

            else
                card.AddStatusEffectToCard<Bind>(stacks, false);

            yield return new WaitForSeconds(0.1f);
        }
    }
}