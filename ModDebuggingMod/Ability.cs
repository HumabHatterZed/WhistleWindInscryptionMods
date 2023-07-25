using DiskCardGame;
using InscryptionAPI.Card;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void Ability_Test()
        {
            const string rulebookName = "Test";
            const string rulebookDescription = "When [creature] dies, the killer transforms into a copy of this card.";
            const string dialogue = "The curse continues unabated.";
            const string triggerText = "[creature] passes the curse on.";
            Test.ability = AbilityHelper.CreateAbility<Test>(
                pluginGuid, "sigilCursed",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 0,
                modular: true, opponent: false, canStack: false).Id;
        }
    }
    public class Test : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToOtherCardPreDeath(CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (deathSlot.Card == Card)
                return killer != null && killer.NotDead() && killer.OpponentCard != Card.OpponentCard;

            return false;
        }

        public override IEnumerator OnOtherCardPreDeath(CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            // copies straight from Daus special ability - attacks the killer after temporarily removing flying
            // feels kind of round-about, but its code is shorter than the other way
                    yield return new WaitForSeconds(0.25f);
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.25f);
                    CardModificationInfo removeFlyingMod = null;
                    if (base.Card.HasAbility(Ability.Flying))
                    {
                        removeFlyingMod = new CardModificationInfo
                        {
                            negateAbilities = { Ability.Flying }
                        };
                        base.Card.AddTemporaryMod(removeFlyingMod);
                    }
                    yield return Singleton<TurnManager>.Instance.CombatPhaseManager.SlotAttackSlot(base.Card.Slot, killer.Slot);
                    if (removeFlyingMod != null)
                    {
                        base.Card.RemoveTemporaryMod(removeFlyingMod);
                    }
                    yield return new WaitForSeconds(0.25f);

            // this is just the SlotAttackSequence code, minus some aspects
/*            yield return new WaitForSeconds(0.025f);
            if (base.Card.Anim.DoingAttackAnimation)
            {
                yield return new WaitUntil(() => !base.Card.Anim.DoingAttackAnimation);
                yield return new WaitForSeconds(0.25f);
            }

            // if attack is blocked (removed null check since that's already been done)
            if (base.Card.AttackIsBlocked(killer.Slot))
            {
                ProgressionData.SetAbilityLearned(Ability.PreventAttack);
                yield return Singleton<CombatPhaseManager>.Instance.ShowCardBlocked(base.Card);
            }
            else // regular attack
            {
                float heightOffset = (killer == null) ? 0f : killer.SlotHeightOffset;
                if (heightOffset > 0f)
                    Tween.Position(base.Card.transform, base.Card.transform.position + Vector3.up * heightOffset, 0.05f, 0f, Tween.EaseInOut);

                base.Card.Anim.PlayAttackAnimation(base.Card.IsFlyingAttackingReach(), killer.Slot, null);
                yield return new WaitForSeconds(0.07f);
                base.Card.Anim.SetAnimationPaused(true);
                PlayableCard attackingCard = base.Card;
                yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.CardGettingAttacked, false, killer);
                if (attackingCard?.Slot != null && killer?.Slot != null)
                {
                    base.Card.Slot = attackingCard.Slot;
                    if (base.Card.IsFlyingAttackingReach())
                    {
                        killer.Anim.PlayJumpAnimation();
                        yield return new WaitForSeconds(0.3f);
                        base.Card.Anim.PlayAttackInAirAnimation();
                    }
                    base.Card.Anim.SetAnimationPaused(false);
                    yield return new WaitForSeconds(0.05f);
                    int overkillDamage = base.Card.Attack - killer.Health;
                    yield return killer.TakeDamage(base.Card.Attack, base.Card);
                    yield return Singleton<CombatPhaseManager>.Instance.DealOverkillDamage(overkillDamage, base.Card.Slot, killer.Slot);
                    if (base.Card != null && heightOffset > 0f)
                        yield return Singleton<BoardManager>.Instance.AssignCardToSlot(base.Card, base.Card.Slot, 0.1f, null, resolveTriggers: false);
                }
            }*/

            yield return base.LearnAbility(0.5f);
        }
    }
}
