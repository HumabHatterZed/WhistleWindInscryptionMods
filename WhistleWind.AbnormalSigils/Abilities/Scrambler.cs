using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Scrambler() {
            string rulebookDescription = "This card gives its stats to the target card then randomises its new stats. For non-spells, activate when sacrificed.";

            const string rulebookName = "Scrambler";
            const string dialogue = "Do you love your city?";

            Scrambler.ID = AbnormalAbilityHelper.CreateAbility<Scrambler>(
                "sigilScrambler",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// This card gives its stats to the target card then randomises its new stats. For non-spells, activate when sacrificed.
    /// </summary>
    public class Scrambler : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool RespondsToSacrifice() => true;
        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            if (base.Card.Info.IsTargetedSpell() && slot.Card != null)
                return base.Card.OpponentCard == slot.Card.OpponentCard;

            return false;
        }

        public override IEnumerator OnSacrifice() {
            PlayableCard card = Singleton<BoardManager>.Instance.currentSacrificeDemandingCard;
            CardModificationInfo info = new(base.Card.Attack, base.Card.Health);

            yield return base.PreSuccessfulTriggerSequence();

            card.Anim.StrongNegationEffect();
            card.AddTemporaryMod(info);
            yield return new WaitForSeconds(0.4f);

            GetNewStats(card);
            card.Anim.PlayTransformAnimation();
            yield return new WaitForSeconds(0.5f);

            yield return base.LearnAbility();
        }
        public override IEnumerator OnResolveOnBoard() {
            CardModificationInfo info = new(base.Card.Attack, base.Card.Health);
            List<PlayableCard> cards = BoardManager.Instance.GetCards(!base.Card.OpponentCard);
            cards.Remove(base.Card);
            if (cards.Count == 0)
                yield break;

            foreach (PlayableCard card in cards) // make cards shake
            {
                card.Anim.StrongNegationEffect();
                card.AddTemporaryMod(info);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.2f);

            foreach (PlayableCard card in cards) // add stats
            {
                GetNewStats(card);
                card.Anim.PlayTransformAnimation();
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.4f);

            yield return base.LearnAbility();
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            CardModificationInfo info = new(base.Card.Attack, base.Card.Health);

            yield return base.PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(slot.Card.InHand ? View.Hand : View.Board, endDelay: 0.5f);

            slot.Card.Anim.StrongNegationEffect();
            slot.Card.AddTemporaryMod(info);
            yield return new WaitForSeconds(0.4f);

            GetNewStats(slot.Card);
            slot.Card.Anim.PlayTransformAnimation();
            yield return new WaitForSeconds(0.5f);

            yield return base.LearnAbility();
        }

        private void GetNewStats(PlayableCard card) {
            int[] stats = new[] { 0, 1 };
            int oldTotal = card.Attack + card.Health;
            int randomSeed = base.GetRandomSeed();

            while (oldTotal > 0) {
                // 33% of giving Power
                if (oldTotal > 1 && SeededRandom.Value(randomSeed *= 2) <= 0.4f) {
                    stats[0]++;
                    oldTotal -= 2;
                }
                else {
                    stats[1]++;
                    oldTotal--;
                }
            }

            card.AddTemporaryMod(new(stats[0] - card.Attack, stats[1] - card.Health) { nonCopyable = true });
        }
    }
}
