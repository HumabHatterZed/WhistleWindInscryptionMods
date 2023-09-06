using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_BindingStrike()
        {
            const string rulebookName = "Binding Strike";
            const string rulebookDescription = "When [creature] strikes an opposing creature, inflict 1 Bind.";
            const string dialogue = "Your beast falls behind the pack.";
            BindingStrike.ability = AbnormalAbilityHelper.CreateAbility<BindingStrike>(
                "sigilBindingStrike",
                rulebookName, rulebookDescription, dialogue, powerLevel: 1,
                modular: false, opponent: true, canStack: true).Id;
        }
    }
    public class BindingStrike : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        // only apply Bind if target isn't null/dead
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => target != null && !target.Dead;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return AddBindToCard(target);
            base.LearnAbility(0.4f);
        }
        private IEnumerator AddBindToCard(PlayableCard card)
        {
            card.Anim.LightNegationEffect();
            Bind component = card.GetComponent<Bind>();
            if (component == null)
            {
                // apply extra stacks if this ability has them
                int extraStacks = Mathf.Max(0, base.Card.GetAbilityStacks(ability) - 1);
                card.AddStatusEffectToCard<Bind>(extraStacks);
            }
            else
                component.UpdateStatusEffectCount(base.Card.GetAbilityStacks(ability), false);

            yield return new WaitForSeconds(0.1f);
        }
    }
}