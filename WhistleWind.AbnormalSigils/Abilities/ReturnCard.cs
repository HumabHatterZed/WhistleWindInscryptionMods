using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;


namespace WhistleWind.AbnormalSigils {
    /// <summary>
    /// Return the selected card to your hand with its current status retained and its play cost changed to 0-2 Bones based on how recently it was played.
    /// </summary>
    public class ReturnCard : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() =>
            base.Card.Info.IsGlobalSpell()
                && BoardManager.Instance.GetCards(!base.Card.OpponentCard, x => x.LacksAbility(Unyielding.ability)).Count > 0;

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            if (base.Card.Info.IsTargetedSpell() && slot.Card != null) {
                if (!Unyielding.CardCanBeMoved(slot.Card)) {
                    base.StartCoroutine(UnyieldingDialogue(slot.Card));
                }
                else {
                    return slot.IsPlayerSlot || base.Card.OriginatedFromQueue;
                }
            }
            return false;
        }

        private IEnumerator UnyieldingDialogue(PlayableCard card) {
            if (!base.Card.OpponentCard && !base.HasLearned) {
                card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.3f);
                yield return DialogueHelper.ShowUntilInput("It refuses to move.");
            }
        }

        private IEnumerator RecallCard(CardSlot slot, float opponentWaitAfter) {
            if (base.Card.OpponentCard) {
                ViewManager.Instance.SwitchToView(View.OpponentQueue);
                slot.Card.AddTemporaryMod(new() { negateAbilities = new() { Ability.DrawCopy } });
                yield return TurnManager.Instance.Opponent.ReturnCardToQueue(slot.Card, 0.2f);
                slot.Card.UnassignFromSlot();
                slot.Card = null;
                yield return new WaitForSeconds(opponentWaitAfter);
                yield break;
            }

            bool hasFecundity = slot.Card.HasAbility(Ability.DrawCopy);
            CardInfo copy = slot.Card.Info.Clone() as CardInfo;
            PlayableCardStatus status = new(slot.Card.Status);
            List<CardModificationInfo> tempMods = slot.Card.TemporaryMods;
            CardModificationInfo recallMod = new() {
                bloodCostAdjustment = -999,
                bonesCostAdjustment = GetBonesCost(slot.Card) - slot.Card.BonesCost(),
                energyCostAdjustment = -999,
                nullifyGemsCost = true,
                singletonId = "wstl:Recalled"
            };
            if (hasFecundity && SaveFile.IsAscension) {
                recallMod.AddNegateAbilities(Ability.DrawCopy);
            }
            tempMods.Add(recallMod);

            List<SpecialCardBehaviour> behaviours = slot.Card.GetComponents<SpecialCardBehaviour>()?.ToList() ?? new();

            slot.Card.RemoveFromBoard(false);
            yield return HelperMethods.ChangeCurrentView(View.Default);
            yield return CardSpawner.Instance.SpawnCardToHand(copy, tempMods, 0.25f, (PlayableCard x) => {
                x.Status = status;
                for (int i = 0; i < behaviours.Count; i++) {
                    if (!x.TriggerHandler.permanentlyAttachedBehaviours.Contains(behaviours[i])) {
                        var copy = HelperMethods.CopySpecialCardBehaviour(behaviours[i], x.gameObject);
                        x.TriggerHandler.permanentlyAttachedBehaviours.Add(copy);
                    }
                }
            });

            yield return new WaitForSeconds(0.2f);

            if (hasFecundity && SaveFile.IsAscension && !DialogueEventsData.EventIsPlayed("AscensionFecundityNerfRecall")) {
                Singleton<ChallengeActivationUI>.Instance.ShowTextLines(new string[3] {
                        Localization.Translate("DEPLOY SIGIL NERF: FECUNDITY"),
                        Localization.Translate("RemoveSigilFromCopy()"),
                        Localization.Translate("// It had to be done.")
                    });
                yield return new WaitForSeconds(0.5f);
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("AscensionFecundityNerf", TextDisplayer.MessageAdvanceMode.Input);
                DialogueEventsData.MarkEventPlayed("AscensionFecundityNerfRecall");
            }
        }

        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            yield return RecallCard(slot, 0.4f);
        }
        public override IEnumerator OnResolveOnBoard() {
            foreach (PlayableCard card in BoardManager.Instance.GetCards(!base.Card.OpponentCard)) {
                yield return RecallCard(card.Slot, 0.1f);
            }
        }

        private int GetBonesCost(PlayableCard card) => Mathf.Max(0, 2 - (TurnManager.Instance.TurnNumber - card.TurnPlayed));
    }

    public partial class AbnormalPlugin {
        private void Ability_ReturnCard() {
            const string rulebookName = "Recall Creature";
            const string rulebookDescription = "Return the selected card to your hand with its current status retained and its play cost changed to 0-2 Bones based on how recently it was played.";
            ReturnCard.ability = AbnormalAbilityHelper.CreateAbility<ReturnCard>(
                "sigilReturnCard", rulebookName, rulebookDescription,
                null, powerLevel: 0, canStack: false).Id;
        }
    }
}