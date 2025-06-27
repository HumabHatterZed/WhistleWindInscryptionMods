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

        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            if (base.Card.Info.IsTargetedSpell() && slot.Card != null) {
                return base.Card.OriginatedFromQueue ? !slot.IsPlayerSlot : slot.IsPlayerSlot;
            }
            return false;
        }

        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            if (base.Card.OpponentCard) {
                ViewManager.Instance.SwitchToView(View.OpponentQueue);
                yield return TurnManager.Instance.Opponent.ReturnCardToQueue(slot.Card, 0.2f);
                slot.Card.UnassignFromSlot();
                slot.Card = null;
                yield return new WaitForSeconds(0.4f);
                yield break;
            }

            CardInfo copy = slot.Card.Info.Clone() as CardInfo;
            PlayableCardStatus status = new(slot.Card.Status);
            List<CardModificationInfo> tempMods = slot.Card.TemporaryMods;
            tempMods.Add(new() {
                bloodCostAdjustment = -slot.Card.BloodCost(),
                bonesCostAdjustment = GetBonesCost(slot.Card) - slot.Card.BonesCost(),
                energyCostAdjustment = -slot.Card.EnergyCost,
                nullifyGemsCost = true
            });

            List<SpecialCardBehaviour> behaviours = slot.Card.GetComponents<SpecialCardBehaviour>()?.ToList() ?? new();

            slot.Card.RemoveFromBoard(false);
            yield return HelperMethods.ChangeCurrentView(View.Default);
            yield return CardSpawner.Instance.SpawnCardToHand(copy, tempMods, 0.25f, (PlayableCard x) => {
                x.Status = status;
                for (int i = 0; i < behaviours.Count; i++) {
                    var copy = HelperMethods.CopySpecialCardBehaviour(behaviours[i], x.gameObject);
                    x.TriggerHandler.permanentlyAttachedBehaviours.Add(copy);
                }
            });

            yield return new WaitForSeconds(0.2f);
        }
        public override IEnumerator OnResolveOnBoard() {
            if (base.Card.OriginatedFromQueue) {
                ViewManager.Instance.SwitchToView(View.OpponentQueue);
                foreach (PlayableCard card in BoardManager.Instance.GetCards(false)) {
                    yield return TurnManager.Instance.Opponent.ReturnCardToQueue(card, 0.2f);
                    card.Slot.Card = null;
                    card.UnassignFromSlot();
                }
            }
            else {
                foreach (PlayableCard card in BoardManager.Instance.GetCards(true)) {
                    CardInfo copy = card.Info.Clone() as CardInfo;
                    PlayableCardStatus status = new(card.Status);
                    List<CardModificationInfo> tempMods = card.TemporaryMods;
                    tempMods.Add(new() {
                        bloodCostAdjustment = -card.BloodCost(),
                        bonesCostAdjustment = GetBonesCost(card) - card.BonesCost(),
                        energyCostAdjustment = -card.EnergyCost,
                        nullifyGemsCost = true
                    });

                    card.RemoveFromBoard(false);
                    yield return HelperMethods.ChangeCurrentView(View.Default, 0.1f, 0.1f);
                    yield return CardSpawner.Instance.SpawnCardToHand(copy, tempMods, 0.25f, (PlayableCard x) => { x.Status = status; });
                }
            }
            yield return new WaitForSeconds(0.2f);
        }

        private int GetBonesCost(PlayableCard card) => Mathf.Max(0, 2 - (TurnManager.Instance.TurnNumber - card.TurnPlayed));
    }

    public partial class AbnormalPlugin {
        private void Ability_ReturnCard() {
            const string rulebookName = "Creature Retrieval";
            const string rulebookDescription = "Return the selected card to your hand with its current status retained and its play cost changed to 0-2 Bones based on how recently it was played.";
            ReturnCard.ability = AbnormalAbilityHelper.CreateAbility<ReturnCard>(
                "sigilReturnCard", rulebookName, rulebookDescription,
                null, powerLevel: 0, canStack: false).Id;
        }
    }
}