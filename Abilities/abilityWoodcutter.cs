using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Woodcutter()
        {
            const string rulebookName = "Woodcutter";
            const string rulebookDescription = "When a card moves into the space opposing this card, deal damage equal to this card's Power.";
            const string dialogue = "No matter how many trees fall, the forest remains dense.";

            return WstlUtils.CreateAbility<Woodcutter>(
                Resources.sigilWoodcutter,
                rulebookName, rulebookDescription, dialogue, 4, addModular: true);
        }
    }
    // ripped from Sentry code
    public class Woodcutter : AbilityBehaviour
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
                if (otherCard != null && !otherCard.Dead && base.Card.Attack > 0)
                {
                    yield return base.PreSuccessfulTriggerSequence();
                    base.Card.Anim.LightNegationEffect();
                    yield return new WaitForSeconds(0.5f);
                    bool impactFrameReached = false;
                    base.Card.Anim.PlayAttackAnimation(base.Card.IsFlyingAttackingReach(), otherCard.Slot, delegate
                    {
                        impactFrameReached = true;
                    });
                    yield return new WaitUntil(() => impactFrameReached);
                    yield return otherCard.TakeDamage(base.Card.Attack, base.Card);
                }
            }
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility(0.5f);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
    }
}
