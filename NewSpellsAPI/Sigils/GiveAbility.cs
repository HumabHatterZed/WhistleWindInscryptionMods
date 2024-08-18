using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public abstract class GiveAbility : AbilityBehaviour
    {
        public virtual TargetBehaviour TargetMode { get; set; } = TargetBehaviour.TargetFriendlies;
        private bool ValidTarget(PlayableCard card)
        {
            return TargetMode switch
            {
                TargetBehaviour.TargetFriendlies => card.OpponentCard == base.Card.OpponentCard,
                TargetBehaviour.TargetOpponents => card.OpponentCard != base.Card.OpponentCard,
                _ => true
            };
        }

        public override bool RespondsToSacrifice() => true;
        public override IEnumerator OnSacrifice()
        {
            PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;
            if (card != null)
            {
                yield return OnValidTarget(card);
                yield return new WaitForSeconds(0.5f);
                yield return base.LearnAbility();
            }
        }

        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override IEnumerator OnResolveOnBoard()
        {
            foreach (CardSlot slot in BoardManager.Instance.AllSlotsCopy.Where(x => x.Card != null && ValidTarget(x.Card)))
            {
                yield return OnValidTarget(slot.Card);
            }
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
        }

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            if (base.Card.Info.IsSpell() && slot.Card != null)
            {
                return ValidTarget(slot.Card);
            }
            return false;
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            yield return OnValidTarget(slot.Card);
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
        }

        public abstract IEnumerator OnValidTarget(PlayableCard card);

        public enum TargetBehaviour
        {
            TargetFriendlies,
            TargetOpponents,
            TargetAny
        }
    }
}
