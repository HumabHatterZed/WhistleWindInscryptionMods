using DiskCardGame;
using System.Collections;
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

        public override IEnumerator OnSacrifice()
        {
            yield break;
        }
        public override IEnumerator OnPlayFromHand()
        {
            yield break;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield break;
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            yield break;
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
    }
}
