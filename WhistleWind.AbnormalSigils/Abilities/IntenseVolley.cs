using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_IntenseVolley() {
            const string rulebookName = "Volley Strike";
            const string rulebookDescription = "At the end of the owner's turn, this card will target 2-4 opposing spaces to attack on its next turn.";
            IntenseVolley.ability = AbnormalAbilityHelper.CreateAbility<IntenseVolley>(
                "sigilVolley",
                rulebookName, rulebookDescription, powerLevel: 5,
                modular: false, opponent: true, canStack: false)
                .Info.SetFlipYIfOpponent()
                .ability;
        }
    }
    /// <summary>
    /// At the end of the owner's turn, this card will target at 2-4 opposing spaces to attack on its next turn.
    /// </summary>
    public class IntenseVolley : AbilityBehaviour, IPlayerTurnEnd, IOpponentTurnEnd, ISetupAttackSequence, IOnPostSingularSlotAttackSlot {
        public static Ability ability;
        public override Ability Ability => ability;

        public readonly Dictionary<CardSlot, GameObject> currentTargets = new();

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            CleanupTargetIcons();
            yield break;
        }
        public virtual IEnumerator SelectTargets(int maxTargets, int maxNullTargets, int randomSeed) {
            GameObject targetIcon;
            List<CardSlot> validTargets = new();
            List<CardSlot> allTargets = BoardManager.Instance.GetSlotsCopy(base.Card.OpponentCard);
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
                    targetIcon = TargetIconHelper.CreateTargetIcon(slot, LifeManager.Instance.Scales3D.highlightedInteractable.highlightedColor);
                }
                else {
                    targetIcon = TargetIconHelper.CreateTargetIcon(slot);
                }
                targetIcon.transform.localScale *= 0.75f;
                currentTargets.Add(slot, targetIcon);
            }
        }

        public virtual IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) => OnPlayerTurnEnd();
        public virtual IEnumerator OnPlayerTurnEnd() {
            CleanupTargetIcons();
            int randomSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;
            int maxTargets = BoardManager.Instance.OpponentSlotsCopy.Count;
            if (SeededRandom.Value(randomSeed++) <= (base.Card.Health / (float)(base.Card.MaxHealth + 1))) {
                maxTargets--;
            }
            if (base.Card.Attack > 3 || SeededRandom.Value(randomSeed++) <= (maxTargets * 0.2f)) {
                maxTargets--;
            }
            yield return base.PreSuccessfulTriggerSequence();
            yield return SelectTargets(maxTargets, maxTargets--, randomSeed);
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot) {
            return currentTargets.Keys.ToList();
        }

        public IEnumerator OnPostSingularSlotAttackSlot(CardSlot attackingSlot, CardSlot targetSlot) {
            //AbnormalPlugin.Log.LogInfo($"[IntenseVolley] OnPostSingular {base.Card.Anim.DoingAttackAnimation} {targetSlot.Index}");
            if (currentTargets.TryGetValue(targetSlot, out GameObject obj)) {
                TargetIconHelper.CleanUpTargetIcon(obj);
                currentTargets.Remove(targetSlot);
            }
            yield break;
        }

        public void CleanupTargetIcons() {
            foreach (GameObject obj in currentTargets.Values) {
                TargetIconHelper.CleanUpTargetIcon(obj);
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

        public bool RespondsToPostSingularSlotAttackSlot(CardSlot attackingSlot, CardSlot targetSlot) => attackingSlot == base.Card.Slot;
    }
}
