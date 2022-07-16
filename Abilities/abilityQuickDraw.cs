using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_QuickDraw()
        {
            const string rulebookName = "Quick Draw";
            const string rulebookDescription = "When a card moves into the space opposing this card, deal 1 damage.";
            const string dialogue = "The early bird gets the worm.";

            QuickDraw.ability = AbilityHelper.CreateAbility<QuickDraw>(
                Resources.sigilQuickDraw,
                rulebookName, rulebookDescription, dialogue, 3).Id;
        }
    }
    // ripped from Sentry code
    public class QuickDraw : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int lastShotTurn = -1;

        private PlayableCard lastShotCard;

        private int NumShots => Mathf.Max(base.Card.Info.Abilities.FindAll((Ability x) => x == this.Ability).Count, 1);

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return RespondsToTrigger(otherCard);
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return FireAtOpposingSlot(otherCard);
        }

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            return RespondsToTrigger(otherCard);
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            yield return FireAtOpposingSlot(otherCard);
        }

        private bool RespondsToTrigger(PlayableCard otherCard)
        {
            if (!base.Card.Dead && !otherCard.Dead)
            {
                return otherCard.Slot == base.Card.Slot.opposingSlot;
            }
            return false;
        }

        private IEnumerator FireAtOpposingSlot(PlayableCard otherCard)
        {
            if (!(otherCard != this.lastShotCard) && Singleton<TurnManager>.Instance.TurnNumber == this.lastShotTurn)
            {
                yield break;
            }
            this.lastShotCard = otherCard;
            this.lastShotTurn = Singleton<TurnManager>.Instance.TurnNumber;
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < this.NumShots; i++)
            {
                if (otherCard != null && !otherCard.Dead)
                {
                    yield return base.PreSuccessfulTriggerSequence();
                    base.Card.Anim.LightNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                    bool impactFrameReached = false;
                    base.Card.Anim.PlayAttackAnimation(base.Card.IsFlyingAttackingReach(), otherCard.Slot, delegate
                    {
                        impactFrameReached = true;
                    });
                    yield return new WaitUntil(() => impactFrameReached);
                    yield return otherCard.TakeDamage(1, base.Card);
                }
            }
            yield return new WaitForSeconds(0.25f);
            yield return base.LearnAbility();
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
    }
}
