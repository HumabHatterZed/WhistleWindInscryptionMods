using DiskCardGame;
using System;
using System.Collections;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class HelperExtensions
    {
        /// <summary>
        /// Removes this card from the board.
        /// </summary>
        /// <param name="removeFromDeck">Whether to also remove from the player's deck. Does not affect opponent cards.</param>
        /// <param name="tweenLength">How long the ExitBoard animation should take to play.</param>
        public static void RemoveFromBoard(this PlayableCard item, bool removeFromDeck = false, float tweenLength = 0.3f)
        {
            // Remove from deck if this card is owned by the player
            if (removeFromDeck && !item.OpponentCard && !item.OriginatedFromQueue)
                RunState.Run.playerDeck.RemoveCard(item.Info);

            item.UnassignFromSlot();
            SpecialCardBehaviour[] components = item.GetComponents<SpecialCardBehaviour>();
            for (int i = 0; i < components.Length; i++)
            {
                components[i].OnCleanUp();
            }
            item.ExitBoard(tweenLength, Vector3.zero);
        }
        /// <summary>
        /// Kills this card without triggering OnDie or OnOtherCardDie. Triggers OnDie if it has the PackMule special ability.
        /// </summary>
        public static IEnumerator DieTriggerless(this PlayableCard card)
        {
            if (!card.Dead)
            {
                card.Dead = true;
                CardSlot slotBeforeDeath = card.Slot;
                if (card.Info != null && card.Info.name.ToLower().Contains("squirrel"))
                    AscensionStatsData.TryIncrementStat(AscensionStat.Type.SquirrelsKilled);

                card.Anim.SetShielded(shielded: false);
                yield return card.Anim.ClearLatchAbility();
                if (card.HasAbility(Ability.PermaDeath))
                {
                    card.Anim.PlayPermaDeathAnimation();
                    yield return new WaitForSeconds(1.25f);
                }
                else
                    card.Anim.PlayDeathAnimation();

                if (!card.HasAbility(Ability.QuadrupleBones) && slotBeforeDeath.IsPlayerSlot)
                    yield return Singleton<ResourcesManager>.Instance.AddBones(1, slotBeforeDeath);

                if (card.Info.SpecialAbilities.Contains(SpecialTriggeredAbility.PackMule) && card.TriggerHandler.RespondsToTrigger(Trigger.Die, false, null))
                    yield return card.TriggerHandler.OnTrigger(Trigger.Die, false, null);

                card.UnassignFromSlot();
                card.StartCoroutine(card.DestroyWhenStackIsClear());
            }
        }
        public static IEnumerator TakeDamageTriggerless(this PlayableCard card, int damage, PlayableCard attacker)
        {
            card.Status.damageTaken += damage;
            card.UpdateStatsText();
            if (card.Health > 0)
                card.Anim.PlayHitAnimation();
            if (card.Health <= 0)
                yield return card.Die(false, attacker);
        }

        public static bool HasFlags(this Enum enumeration, params Enum[] flags)
        {
            foreach (Enum flag in flags)
            {
                if (!enumeration.HasFlag(flag))
                    return false;
            }
            return true;
        }
        public static bool HasAnyOfFlags(this Enum enumeration, params Enum[] flags)
        {
            foreach (Enum flag in flags)
            {
                if (enumeration.HasFlag(flag))
                    return true;
            }
            return false;
        }
    }
}
