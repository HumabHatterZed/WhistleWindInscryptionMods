using DiskCardGame;
using InscryptionAPI;
using InscryptionAPI.Triggers;
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
                Resources.sigilQuickDraw, Resources.sigilQuickDraw_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3).Id;
        }
    }
    // ripped from Sentry code
    public class QuickDraw : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int lastShotTurn = -1;

        private PlayableCard lastShotCard;
        private PlayableCard queuedCard;
        private int NumShots => Mathf.Max(base.Card.Info.Abilities.FindAll((Ability x) => x == this.Ability).Count, 1);

        private bool antiLock = false;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            // don't respond to resolve for Pack Mule
            return !otherCard.Info.SpecialAbilities.Contains(SpecialTriggeredAbility.PackMule) && RespondsToTrigger(otherCard);
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
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            // Only respond if we're in an antiLock situation
            if (antiLock && queuedCard != null)
            {
                return base.Card.OpponentCard != playerTurnEnd && RespondsToTrigger(queuedCard);
            }
            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            WstlPlugin.Log.LogDebug("Killing queued card.");
            yield return FireAtOpposingSlot(queuedCard);
            antiLock = false;
            queuedCard = null;
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
            // If this is the same as the last shot card and is the same turn, don't fire again
            if (!(otherCard != this.lastShotCard) && Singleton<TurnManager>.Instance.TurnNumber == this.lastShotTurn)
            {
                yield break;
            }

            // If the Pack Mule just resolved on the board, queue it then break
            if (otherCard.Info.SpecialAbilities.Contains(SpecialTriggeredAbility.PackMule)
                && otherCard.TurnPlayed == 0)
            {
                WstlPlugin.Log.LogDebug("Enemy is a Pack Mule.");
                yield return QueueKill(otherCard);
                yield break;
            }
            if (otherCard.Info.HasAbility(Ability.TailOnHit)
                && otherCard.TurnPlayed != 0
                && !otherCard.Status.hiddenAbilities.Contains(Ability.TailOnHit))
            {
                WstlPlugin.Log.LogDebug("Enemy has Loose tail.");
                yield return QueueKill(otherCard);
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
        private IEnumerator QueueKill(PlayableCard otherCard)
        {
            WstlPlugin.Log.LogDebug("Enemy was played this turn.");
            antiLock = true;
            queuedCard = otherCard;
            base.Card.Anim.LightNegationEffect();
            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput($"Your [c:bR]{base.Card.Info.DisplayedNameLocalized}[c:] waits for an opportunity to strike.");
        }
    }
}
