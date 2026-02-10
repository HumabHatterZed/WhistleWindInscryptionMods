using DiskCardGame;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Based on the Sweepers battle in Limbus Company.
    /// Indigo Midnight is a constant onslaught of cards, where the player must survive three waves.
    /// Cards appear in a steady stream.
    /// Cards required: 5
    /// Valid regions: 0, 1, 2
    /// </summary>
    public class OrdealIndigoMidnight : OrdealIndigoNoon {
        public override Opponent.Type BossType => OrdealUtils.SweeperOpponentID;
        public override int MaxBonesOwned => 10;
        public SweeperOpponent SweeperOpponent => Opponent as SweeperOpponent;
        private const int NUM_TURNS = 5;
        private int numWavesLeft = 1;
        private int numTurnsLeft = NUM_TURNS;
        private bool newWave = true;

        public override IEnumerator PlayerUpkeep() {
            yield return UpdateCounterIcon(newWave, !newWave);
            if (numTurnsLeft == 0) {
                Opponent.NumLives--;
                yield return OpponentLifeLost();
            }
        }

        public override void DefeatOrdealAndDisplayOutroBanner() {
            defeated = true; // don't play the outro banner until the end
            if (Opponent.NumLives == 0) {
                SweeperOpponent.StopEmissions();
                base.DefeatOrdealAndDisplayOutroBanner();
            }
        }

        public override void TryAddOrdealRandomBuff(PlayableCard card) {
            if (card.Info.displayedName == "Sweeper") {
                CardModificationInfo mod;
                int rand = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;
                mod = new(Shadowed.ability) { fromCardMerge = true, singletonId = "OrdealRandomBuff" };
                if (SeededRandom.Value(rand++) <= 0.075f * (7 + RunState.Run.DifficultyModifier)) {
                    mod.healthAdjustment++;
                }
                if (SeededRandom.Value(rand++) <= (4 + RunState.Run.DifficultyModifier) * 0.1f) {
                    mod.healthAdjustment++;
                }
                if (SeededRandom.Value(rand++) <= (TurnManager.Instance.TurnNumber + RunState.Run.DifficultyModifier - 2) * 0.1f) {
                    mod.attackAdjustment++;
                }
                card.AddTemporaryMod(mod);
            }
        }

        public override IEnumerator OpponentLifeLost() {
            LobotomyPlugin.Log.LogDebug($"[IndigoMidnight] OpponentLifeLost: numLives: {Opponent.NumLives}");

            if (Opponent.NumLives == 0) {
                defeated = false;
                yield return Opponent.OutroSequence(true);
                yield break;
            }

            bool wonByKill = OrdealCounterManager.Instance.amountLeft == 0 && TurnManager.Instance.IsPlayerTurn;
            int reactiveDifficulty = Mathf.Max(0, 5 - LifeManager.Instance.DamageUntilPlayerWin) + numTurnsLeft;
            reactiveDifficulty += BoardManager.Instance.GetOpponentOpenSlots().Count;
            numWavesLeft++;
            defeated = false;
            amountKilledThisTurn = 0;
            numTurnsLeft = NUM_TURNS;

            if (wonByKill) {
                numTurnsLeft++;
            }
            
            OrdealCounterManager.Instance.amountLeft = MinNumCardsRequired = ConstructWaveBlueprint(reactiveDifficulty);
            if (RunState.Run.DifficultyModifier > 2) {
                yield return ShowResetSequence();
            }
            else if (RunState.Run.DifficultyModifier > 1 && LifeManager.Instance.DamageUntilPlayerWin < 4) {
                yield return LifeManager.Instance.ShowDamageSequence(2, 1, true);
            }

                yield return HelperMethods.ChangeCurrentView(View.Default);
            
            SweeperOpponent.EmitCentre();
            yield return new WaitForSeconds(0.5f);
            //base.StartCoroutine(TurnManager.Instance.Opponent.ClearBoard());
            yield return TurnManager.Instance.Opponent.ClearQueue();
            yield return TurnManager.Instance.Opponent.QueueNewCards(changeView: false);

            
            if (!wonByKill) {
                yield return UpdateCounterIcon(true, false);
            }
        }

        private IEnumerator UpdateCounterIcon(bool updateNumWaves, bool reduceTurn) {
            yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter, endDelay: 0.5f);
            OrdealCounterManager.Instance.SetTextColour(Color.black);
            if (updateNumWaves) {
                newWave = false;
                yield return OrdealCounterManager.Instance.FlickerConsole(3, numWavesLeft, "wave", 0.5f);
            }

            if (reduceTurn) {
                numTurnsLeft--;
            }

            yield return OrdealCounterManager.Instance.FlickerConsole(3, numTurnsLeft, "turns left", 0.5f);
            base.StartCoroutine(OrdealCounterManager.Instance.FlickerConsole(3, OrdealCounterManager.Instance.amountLeft, preWait: 0.5f));
            yield return new WaitForSeconds(0.5f);
        }

        private int ConstructWaveBlueprint(int reactiveDifficulty) {
            int numTurns = NUM_TURNS + RunState.Run.DifficultyModifier; // [6, 8]
            int numCards = 6 + RunState.Run.DifficultyModifier; // [7, 9]
            int seed = base.GetRandomSeed() + TurnNumber;
            List<List<CardInfo>> newPlan = new();

            Debug.Log($"[IndigoMidnight.ConstructWaveBlueprint] Reactive Difficulty: {reactiveDifficulty}");

            for (int i = 0; i < numTurns; i++) {
                int numPasses = 1 + reactiveDifficulty / 7;
                List<CardInfo> turn = new() {
                    CardLoader.GetCardByName(GetRandomSweeper(seed++, 7))
                };

                for (int j = 0; j < numPasses; j++) {
                    if (SeededRandom.Range(0, 4, seed++) <= (RunState.Run.DifficultyModifier)) {
                        turn.Add(CardLoader.GetCardByName(GetRandomSweeper(seed++, 7)));
                    }
                    if (SeededRandom.Range(0, 6, seed++) <= (RunState.Run.DifficultyModifier)) {
                        turn.Add(CardLoader.GetCardByName(GetRandomSweeper(seed++, 7)));
                    }
                    if (SeededRandom.Range(0, 10, seed++) <= (RunState.Run.DifficultyModifier)) {
                        turn.Add(CardLoader.GetCardByName(GetRandomSweeper(seed++, 7)));
                    }
                }

                newPlan.Add(turn);
            }

            EncounterBluePrint = newPlan;
            Opponent.ReplaceAndAppendTurnPlan(newPlan);
            Opponent.TurnPlan[TurnManager.Instance.TurnNumber] = new(newPlan[newPlan.Count - 1]);
            return numCards;
        }

        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            int numTurns = NUM_TURNS + RunState.Run.DifficultyModifier; // [6, 8]
            int numCards = 6 + RunState.Run.DifficultyModifier; // [7, 9]
            int seed = base.GetRandomSeed() + RunState.Run.DifficultyModifier;

            for (int i = 0; i < numTurns; i++) {
                List<EncounterBlueprintData.CardBlueprint> turn = new() {
                    EncounterManager.NewCardBlueprint(GetRandomSweeper(seed++, 7))
                };
                
                if (SeededRandom.Range(0, 4, seed++) <= (RunState.Run.DifficultyModifier - 1)) {
                    turn.Add(EncounterManager.NewCardBlueprint(GetRandomSweeper(seed++, 7)));
                }
                if (i > 0) {
                    if (SeededRandom.Range(0, 7, seed++) <= (RunState.Run.DifficultyModifier - 1)) {
                        turn.Add(EncounterManager.NewCardBlueprint(GetRandomSweeper(seed++, 7)));
                    }
                    if (SeededRandom.Range(0, 12, seed++) <= (RunState.Run.DifficultyModifier - 1)) {
                        turn.Add(EncounterManager.NewCardBlueprint(GetRandomSweeper(seed++, 7)));
                    }
                }
                encounterData.Blueprint.AddTurn(turn);
            }

            // override the opponent type or else it'll be the default generic ordeal opponent
            encounterData.opponentType = OrdealUtils.SweeperOpponentID;
            return numCards;
        }
    }
}