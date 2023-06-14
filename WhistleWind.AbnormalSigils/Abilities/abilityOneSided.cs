using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_OneSided()
        {
            const string rulebookName = "Opportunistic";
            const string rulebookDescription = "[creature] deals 1 additional damage when striking a card that cannot attack it.";
            const string dialogue = "A cheap hit.";
            OneSided.ability = AbnormalAbilityHelper.CreateAbility<OneSided>(
                "sigilOneSided",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: true).Id;
        }
    }
    public class OneSided : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            if (amount > 0 && target != null && !target.Dead)
                return CheckValid(target);

            return false;
        }

        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.LearnAbility(0.4f);
        }
        private bool CheckValid(PlayableCard target)
        {
            // if target has no Power, if this card can submerge or is facedown (cannot be hit), return true by default
            if (target.Attack == 0 || base.Card.HasAnyOfAbilities(Ability.Submerge, Ability.SubmergeSquid) || base.Card.FaceDown)
                return true;

            // if this card doesn't have Sniper or Marksman (will attack opposing)
            if (base.Card.LacksAbility(Ability.Sniper))
            {
                // if this card has Bi or Tri Strike, check whether the opponent has it too
                if (base.Card.HasAbility(Ability.SplitStrike) || base.Card.HasTriStrike())
                    return !(target.HasAbility(Ability.SplitStrike) || target.HasTriStrike());

                // otherwise, return whether the opponent can attack this card (won't attack directly or is blocked)
                return target.CanAttackDirectly(base.Card.Slot) || target.AttackIsBlocked(base.Card.Slot);
            }
            // if the target is opposing this card
            if (target.Slot == base.Card.Slot.opposingSlot)
                return target.CanAttackDirectly(base.Card.Slot) || target.AttackIsBlocked(base.Card.Slot);

            // if the target is in an opposing adjacent slot
            if (Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot.opposingSlot).Contains(target.Slot))
                return target.LacksAbility(Ability.SplitStrike) || !target.HasTriStrike();

            // otherwise return true
            return true;
        }
    }
}
