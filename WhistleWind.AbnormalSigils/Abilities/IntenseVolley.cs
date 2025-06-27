using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_IntenseVolley() {
            const string rulebookName = "Intense Volley";
            const string rulebookDescription = "At the end of the owner's turn, this card will target at least 2 opposing spaces to attack on its next turn.";
            IntenseVolley.ability = AbnormalAbilityHelper.CreateAbility<IntenseVolley>(
                "sigilVolley",
                rulebookName, rulebookDescription, powerLevel: 5,
                modular: false, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class IntenseVolley : AbilityBehaviour, IPlayerTurnEnd, IOpponentTurnEnd, ISetupAttackSequence {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly List<Tuple<CardSlot, GameObject>> currentTargets = new();
        //private readonly List<GameObject> targetIcons = new();

        public IEnumerator SelectTargets(int maxTargets, int maxNullTargets, int randomSeed) {
            List<CardSlot> allTargets = BoardManager.Instance.GetSlotsCopy(base.Card.OpponentCard);
            List<CardSlot> validTargets = new();
            for (int i = 0; i < maxTargets; i++) {
                if (allTargets.Count == 0) {
                    break;
                }
                CardSlot slot = allTargets.GetSeededRandom(randomSeed++);
                validTargets.Add(slot);
                allTargets.Remove(slot);
            }

            foreach (CardSlot slot in validTargets) {
                if (slot.Card == null) {
                    if (maxNullTargets == 0) {
                        continue;
                    }
                    maxNullTargets--;
                }
                yield return new WaitForSeconds(0.05f);
                if (LifeManager.Instance.Scales3D?.highlightedInteractable != null) {
                    currentTargets.Add(new(slot, TargetIconHelper.CreateTargetIcon(slot, LifeManager.Instance.Scales3D.highlightedInteractable.highlightedColor)));
                }
                else {
                    currentTargets.Add(new(slot, TargetIconHelper.CreateTargetIcon(slot)));
                }
                currentTargets[currentTargets.Count - 1].Item2.transform.localScale *= 0.75f;
            }
        }

        public IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) => OnPlayerTurnEnd();
        public IEnumerator OnPlayerTurnEnd() {
            CleanupTargetIcons();
            int randomSeed = base.GetRandomSeed();
            int maxTargets = BoardManager.Instance.OpponentSlotsCopy.Count;
            if (base.Card.LacksAbility(Challenging.ability) || SeededRandom.Value(randomSeed++) <= (base.Card.Health / (float)(base.Card.MaxHealth + 1))) {
                maxTargets--;
            }
            if (SeededRandom.Value(randomSeed++) <= 0.5f) {
                maxTargets--;
            }
            yield return SelectTargets(maxTargets, maxTargets--, randomSeed);
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot) {
            List<CardSlot> retval = new();
            foreach (Tuple<CardSlot, GameObject> tuple in currentTargets) {
                retval.Add(tuple.Item1);
                TargetIconHelper.CleanUpTargetIcon(tuple.Item2);
            }
            return retval;
        }

        private void CleanupTargetIcons() {
            foreach (Tuple<CardSlot, GameObject> tuple in currentTargets) {
                TargetIconHelper.CleanUpTargetIcon(tuple.Item2);
            }
            currentTargets.Clear();
        }

        public bool RespondsToOpponentTurnEnd(bool opponentTurnSkipped) => base.Card.OpponentCard && !opponentTurnSkipped;
        public bool RespondsToPlayerTurnEnd() => !base.Card.OpponentCard;
        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
            => card == base.Card && modType == OpposingSlotTriggerPriority.PostAdditionModification;
        
        public int OpponentTurnEndPriority(bool opponentTurnSkipped) => 0;
        public int PlayerTurnEndPriority() => 0;
        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
            => 0;
    }
}
