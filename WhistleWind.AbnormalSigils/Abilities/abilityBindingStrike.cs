using DiskCardGame;
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
            const string rulebookDescription = "When [creature] strikes an opposing creature, inflict Bind equal to this card's Power.";
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
                int extraStacks = Mathf.Max(0, base.Card.Attack - 1);
                card.AddStatusEffectToCard<Bind>(extraStacks);
            }
            else
                component.UpdateStatusEffectCount(base.Card.Attack, false);

            yield return new WaitForSeconds(0.1f);
        }
    }
}