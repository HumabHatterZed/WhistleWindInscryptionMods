using DiskCardGame;
using System.Collections;
using UnityEngine;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;

namespace WhistleWind.AbnormalSigils
{
    public abstract class TargetedSpell : AbilityBehaviour
    {
        public abstract bool TargetAlly { get; }

        // use these overrides for controlling whether to respond to triggers
        public virtual bool ConditionForOnPlayFromHand() => true;
        public virtual bool ConditionForOnDie(bool wasSacrifice, PlayableCard killer) => true;
        public virtual bool ConditionForOnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => true;

        // use these overrides for controlling onTrigger logic/effects
        public virtual IEnumerator EffectOnSacrifice()
        {
            yield break;
        }
        public virtual IEnumerator EffectOnPlayFromHand()
        {
            yield break;
        }
        public virtual IEnumerator EffectOnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield break;
        }
        public virtual IEnumerator EffectOnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            yield break;
        }

        public override IEnumerator OnSacrifice()
        {
            yield return EffectOnSacrifice();
        }
        public override IEnumerator OnPlayFromHand()
        {
            yield return EffectOnPlayFromHand();
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return EffectOnDie(wasSacrifice, killer);
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            yield return EffectOnSlotTargetedForAttack(slot, attacker);
        }

        public override bool RespondsToSacrifice() => true;
        public override bool RespondsToPlayFromHand() => SpellAPI.Enabled && ConditionForOnPlayFromHand();
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => SpellAPI.Enabled && ConditionForOnDie(wasSacrifice, killer);
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            if (SpellAPI.Enabled && ConditionForOnSlotTargetedForAttack(slot, attacker) && slot.Card != null)
                return TargetAlly ? !slot.Card.OpponentCard : slot.Card.OpponentCard;

            return false;
        }

        public IEnumerator SwitchCardView(View view, float start = 0.2f, float end = 0.2f)
        {
            if (Singleton<ViewManager>.Instance.CurrentView != view)
            {
                yield return new WaitForSeconds(start);
                Singleton<ViewManager>.Instance.SwitchToView(view);
                yield return new WaitForSeconds(end);
            }
        }
    }
}
