using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents {
    public abstract class LobotomyBossBattleSequencer : LobotomyBattleSequencer, IPlayerTurnEnd, IModifyDamageTaken/*, IItemCanBeUsed, IOnItemPreventedFromUse, IOnPostItemUsed*/ {
        public bool finalPhase = false;
        public bool changeToNextPhase = false;
        public int turnsToNextPhase = 3;
        public int timesHitThisTurn = 0;
        public int damageTakenThisTurn = 0;
        protected int reactiveDifficulty = 0;

        public int ReactiveDifficulty => reactiveDifficulty + RunState.Run.DifficultyModifier;
        public int PhaseDifficulty => TurnManager.Instance.Opponent.StartingLives - TurnManager.Instance.Opponent.NumLives;
        public PlayableCard BossCard { get; protected set; } = null;

        public virtual int BossHealthThreshold(int remainingLives) => -1;

        #region Triggers
        public bool RespondsToPlayerTurnEnd() => true;
        public int PlayerTurnEndPriority() => 0;
        public virtual IEnumerator OnPlayerTurnEnd() {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            AddNextTurnToPlan();
        }

        public virtual bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => true;
        public virtual int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => int.MinValue;
        /// <summary>
        /// Prevents damage taken by the boss from exceeding set health thresholds.
        /// </summary>
        public virtual int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            if (target == BossCard) {
                int threshold = BossHealthThreshold(TurnManager.Instance.Opponent.NumLives);
                if (target.Health - damage < threshold) {
                    return target.Health - threshold;
                }
            }
            return damage;
        }

        #endregion

        #region Reactive Difficulty
        public IEnumerator IncreaseReactiveDifficulty(int amount) {
            reactiveDifficulty += amount;
            LobotomyPlugin.Log.LogDebug($"[LobotomyBoss] Increase reactive: {reactiveDifficulty} (+{amount})");
            yield return OnReactiveDifficultyIncreased(amount);
        }

        public virtual IEnumerator OnReactiveDifficultyIncreased(int amount) {
            yield break;
        }

        #endregion

        #region Turn Plan
        public abstract List<CardInfo> CreateNextTurnPlan(int randomSeed, bool opponentWinning);
        public void AddNextTurnToPlan() {
            List<CardInfo> nextTurn = CreateNextTurnPlan(base.GetRandomSeed() + TurnManager.Instance.TurnNumber, LifeManager.Instance.Balance < 0);
            TurnManager.Instance.Opponent.TurnPlan.Add(nextTurn);
        }
        public override IEnumerator MoveOpponentCards() {
            int rand = base.GetRandomSeed() + TurnNumber;
            List<CardSlot> slots = CardScramble.GetOccupiedSlotsMovable(BoardManager.Instance.OpponentSlotsCopy);

            for (int i = 0; i < slots.Count; i++) {
                if (SeededRandom.Value(rand++) <= 0.75f) {
                    slots.Remove(slots[i]);
                }
            }

            // guarantee boss moves under certain conditions
            if (!slots.Contains(BossCard.Slot) && (damageTakenThisTurn > 3 || changeToNextPhase || SeededRandom.Bool(rand++))) {
                slots.Add(BossCard.Slot);
            }

            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);
            yield return CardScramble.RandomiseCardsInSlots(slots, rand, sortPredicate: delegate (CardSlot s) {
                if (s == BossCard.Slot)
                    return 1000;

                return s.Card.HasAbility(HighStrung.ability) ? 100 : 0;
            });
        }
        #endregion

        #region Turn-Based Variables
        public void IncrementStatsThisTurn(int timesHit, int damageTaken) {
            timesHitThisTurn += timesHit;
            damageTakenThisTurn += damageTaken;
            LobotomyPlugin.Log.LogDebug($"[IncrementStats] {timesHitThisTurn} (+{timesHit}) {damageTakenThisTurn} (+{damageTaken})");
        }
        public void ResetVariablesTurnEnd() {
            timesHitThisTurn = damageTakenThisTurn = currentExcessBones = 0;
        }

        /// <summary>
        /// Reset certain variables and make sure combat end logic is executed even when the turn is skipped.
        /// </summary>
        public override IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) {
            if (opponentTurnSkipped) {
                yield return OpponentCombatEnd();
            }
            else {
                ResetVariablesTurnEnd();
            }
        }
        #endregion
    }
}
