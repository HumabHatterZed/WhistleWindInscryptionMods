using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Nettles() {
            const string rulebookName = "Nettle Clothes";
            const string rulebookDescription = "When this card is played, Brothers are created on adjacent empty spaces. This card gains the first sigil of each adjacent Brother while they are on the board.";
            const string dialogue = "These clothes will restore our happy days.";
            const string triggerText = "[creature] brings out its family!";
            Nettles.ID = AbnormalAbilityHelper.CreateAbility<Nettles>(
                "sigilNettles",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// When this card is played, Brothers are created on adjacent empty spaces. This card gains the first sigil of each adjacent Brother while they are on the board.
    /// </summary>
    public class Nettles : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override int Priority => 1000;

        PlayableCard leftCard = null;
        PlayableCard rightCard = null;

        private CardModificationInfo LEFT_MOD;
        private CardModificationInfo RIGHT_MOD;

        public const string LEFT_MOD_ID = "Nettles_L";
        public const string RIGHT_MOD_ID = "Nettles_R";

        private bool beginChecks = false;
        public override void ManagedUpdate() {
            base.ManagedUpdate();

            if (beginChecks && base.Card.OnBoard && !base.Card.Dead && GlobalTriggerHandler.Instance.StackSize == 0) {
                bool updateMods = false;
                PlayableCard currentLeft = base.Card.Slot.GetAdjacent(true)?.Card;
                PlayableCard currentRight = base.Card.Slot.GetAdjacent(false)?.Card;

                if (currentLeft != leftCard) {
                    updateMods = true;
                    if (currentLeft != null && currentLeft.HasTrait(AbnormalPlugin.SwanBrother)) {
                        leftCard = currentLeft;
                    }
                    else {
                        leftCard = null;
                    }
                    base.Card.RemoveTemporaryMod(LEFT_MOD, false);
                    LEFT_MOD.abilities.Clear();

                    if (leftCard != null) {
                        LEFT_MOD.abilities.Add(leftCard.Info.Abilities.Count > 0 ? leftCard.Info.Abilities[0] : Ability.Sharp);
                        base.Card.AddTemporaryMod(LEFT_MOD);
                        updateMods = false;
                    }
                }

                if (currentRight != rightCard) {
                    updateMods = true;
                    if (currentRight != null && currentRight.HasTrait(AbnormalPlugin.SwanBrother)) {
                        rightCard = currentRight;
                    }
                    else {
                        rightCard = null;
                    }
                    base.Card.RemoveTemporaryMod(RIGHT_MOD, false);
                    RIGHT_MOD.abilities.Clear();
                    if (rightCard != null) {
                        RIGHT_MOD.abilities.Add(rightCard.Info.Abilities.Count > 0 ? rightCard.Info.Abilities[0] : Ability.Sharp);
                        base.Card.AddTemporaryMod(RIGHT_MOD);
                        updateMods = false;
                    }

                }
                if (updateMods) {
                    base.Card.OnStatsChanged();
                }
            }
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            return card == leftCard || card == rightCard;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            base.Card.Anim.LightNegationEffect();
            yield return DialogueHelper.PlayDialogueEvent("NettlesDie");
        }

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            ViewManager.Instance.SwitchToView(View.Board);
            yield return base.PreSuccessfulTriggerSequence();

            int rand = base.GetRandomSeed();
            CardSlot toLeft = BoardManager.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: true);
            CardSlot toRight = BoardManager.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: false);
            bool toLeftValid = toLeft != null && toLeft.Card == null;
            bool toRightValid = toRight != null && toRight.Card == null;

            base.Card.RemoveTemporaryMod(base.Card.TemporaryMods.Find(x => x.singletonId == LEFT_MOD_ID));
            base.Card.RemoveTemporaryMod(base.Card.TemporaryMods.Find(x => x.singletonId == RIGHT_MOD_ID));

            LEFT_MOD = new() { singletonId = LEFT_MOD_ID, nonCopyable = true };
            RIGHT_MOD = new() { singletonId = RIGHT_MOD_ID, nonCopyable = true };
            beginChecks = true; // make sure everything has fully loaded before we start doing per-frame checks

            if (toLeftValid) {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toLeft, rand++);
            }
            if (toRightValid) {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toRight, rand++);
            }

            if (toLeftValid || toRightValid) {
                yield return base.LearnAbility(0.4f);
            }
            else if (!base.HasLearned) {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Once again, she is left alone.", -0.65f, 0.4f);
            }
        }

        private string GetRandomBrotherCardName(int randomSeed) {
            List<string> validCards = CardManager.AllCardsCopy.Where(x => x.HasTrait(AbnormalPlugin.SwanBrother)).Select(x => x.name).ToList();
            validCards.RemoveAll(x => BoardManager.Instance.GetCards(!base.Card.OpponentCard).Exists(pc => pc.name == x));
            return validCards[SeededRandom.Range(0, validCards.Count, randomSeed)];
        }

        private IEnumerator SpawnCardOnSlot(CardSlot slot, int randomSeed) {
            CardInfo cardByName = CardLoader.GetCardByName(GetRandomBrotherCardName(randomSeed));
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, slot, 0.15f);
        }
    }
}
