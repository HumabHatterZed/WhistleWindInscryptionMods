using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    // for triggering special card behaviour in Healer
    public abstract class TransformOnAdjacentDeath : SpecialCardBehaviour {
        public abstract string CardToTransformInto { get; }
        public abstract string PostEvolveDialogueId { get; }
        public virtual int NumDeathsTillEvolve { get; } = 1;

        protected int numAlliesKilled;
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            // return true if the target was in an adjacent slot
            if (base.PlayableCard.OnBoard && fromCombat)
                return Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot).Contains(deathSlot);

            return false;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            numAlliesKilled++;
            if (numAlliesKilled < NumDeathsTillEvolve) {
                yield break;
            }

            yield return new WaitForSeconds(0.15f);
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            CardInfo cardByName = HelperMethods.GetInfoWithMods(base.PlayableCard, CardToTransformInto);
            yield return base.PlayableCard.TransformIntoCard(cardByName);
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent(PostEvolveDialogueId);
        }
    }
}
