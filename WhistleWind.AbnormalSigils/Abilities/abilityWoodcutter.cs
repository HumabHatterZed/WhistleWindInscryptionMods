using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Woodcutter()
        {
            const string rulebookName = "Woodcutter";
            const string rulebookDescription = "When a card moves into the space opposing this card, deal damage equal to this card's Power.";
            const string dialogue = "No matter how many trees fall, the forest remains dense.";

            Woodcutter.ability = AbnormalAbilityHelper.CreateAbility<Woodcutter>(
                Artwork.sigilWoodcutter, Artwork.sigilWoodcutter_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                modular: false, opponent: true, canStack: false).Id;
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
                return base.Card.Attack > 0 && otherCard.Slot == base.Card.Slot.opposingSlot;
            }
            return false;
        }

        private IEnumerator FireAtOpposingSlot(PlayableCard otherCard)
        {
            // Copy otherCard so we can change it later
            PlayableCard opposingCard = otherCard;

            // || base.Card.Anim.Anim.speed == 0f
            // The above line of code prevents Sentry from activating multiple times during normal combat

            if (!(opposingCard != this.lastShotCard) && Singleton<TurnManager>.Instance.TurnNumber == this.lastShotTurn)
                yield break;

            bool midCombat = false;
            this.lastShotCard = opposingCard;
            this.lastShotTurn = Singleton<TurnManager>.Instance.TurnNumber;

            Singleton<ViewManager>.Instance.SwitchToView(View.Board, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < this.NumShots; i++)
            {
                if (opposingCard != null && !opposingCard.Dead)
                {
                    yield return base.PreSuccessfulTriggerSequence();

                    // Check if the animation is paused then unpause it
                    if (base.Card.Anim.Anim.speed == 0f)
                    {
                        // indicates that we need to restart the attack animation at the end of the sequence
                        midCombat = true;

                        ShowPart3Turret(base.Card, opposingCard);

                        // Unpause the animation then wait for it to stop
                        base.Card.Anim.Anim.speed = 1f;
                        yield return new WaitUntil(() => !base.Card.Anim.DoingAttackAnimation);
                    }
                    else
                    {
                        // vanilla sentry code
                        base.Card.Anim.LightNegationEffect();
                        yield return new WaitForSeconds(0.5f);

                        ShowPart3Turret(base.Card, opposingCard);

                        // Expand the attack animation to include a part for triggering CardGettingAttacked

                        base.Card.Anim.PlayAttackAnimation(base.Card.IsFlyingAttackingReach(), opposingCard.Slot, null);
                        yield return new WaitForSeconds(0.07f);
                        base.Card.Anim.SetAnimationPaused(paused: true);

                        PlayableCard attackingCard = base.Card;
                        yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.CardGettingAttacked, false, opposingCard);

                        opposingCard = UpdateOpposingCard(base.Card, opposingCard);

                        if (attackingCard != null && attackingCard.Slot != null)
                        {
                            CardSlot attackingSlot = attackingCard.Slot;

                            if (opposingCard != null)
                            {
                                if (attackingSlot.Card.IsFlyingAttackingReach())
                                {
                                    opposingCard.Anim.PlayJumpAnimation();
                                    yield return new WaitForSeconds(0.3f);
                                    attackingSlot.Card.Anim.PlayAttackInAirAnimation();
                                }
                                attackingSlot.Card.Anim.SetAnimationPaused(paused: false);
                                yield return new WaitForSeconds(0.05f);
                            }
                        }
                    }

                    if (opposingCard != null)
                        yield return opposingCard.TakeDamage(base.Card.Attack, base.Card);
                }
            }
            yield return this.LearnAbility(0.5f);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            // If otherCard isn't dead, restart the attack animation for when regular combat resumes
            if (midCombat && !otherCard.Dead)
            {
                base.Card.Anim.PlayAttackAnimation(base.Card.IsFlyingAttackingReach(), otherCard.Slot, null);
                yield return new WaitForSeconds(0.07f);
                base.Card.Anim.SetAnimationPaused(paused: true);
            }
        }
        private static void ShowPart3Turret(PlayableCard card, PlayableCard otherCard)
        {
            // putting this code here because the main code's overwhelming enough to look at as is
            if (card.Anim is DiskCardAnimationController)
            {
                (card.Anim as DiskCardAnimationController).SetWeaponMesh(DiskCardWeapon.Turret);
                (card.Anim as DiskCardAnimationController).AimWeaponAnim(otherCard.Slot.transform.position);
                (card.Anim as DiskCardAnimationController).ShowWeaponAnim();
            }
        }
        private static PlayableCard UpdateOpposingCard(PlayableCard source, PlayableCard target)
        {
            // putting this code here because the main code's overwhelming enough to look at as is
            CardSlot opposingSlot = source.Slot.opposingSlot;
            if (opposingSlot.Card != null)
            {
                if (opposingSlot.Card != target)
                    return opposingSlot.Card;

                return target;
            }
            return null;
        }
    }
}
