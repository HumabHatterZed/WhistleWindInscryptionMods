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
        private bool startNextWaveUpkeep = false;
        public override IEnumerator PlayerUpkeep() {
            yield return UpdateConsoleDisplay(newWave, !newWave);
            if (numTurnsLeft == 0) {
                Opponent.NumLives--;
                startNextWaveUpkeep = true;
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
                if (SeededRandom.Value(rand++) <= 0.1f * (8 + RunState.Run.DifficultyModifier - Opponent.NumLives)) {
                    mod.healthAdjustment++;
                }
                if (SeededRandom.Value(rand++) <= 0.1f * (5 + RunState.Run.DifficultyModifier - Opponent.NumLives)) {
                    mod.healthAdjustment++;
                }
                if (SeededRandom.Value(rand++) <= 0.1f * (5 + RunState.Run.DifficultyModifier - Opponent.NumLives)) {
                    mod.attackAdjustment++;
                }
                //if (RunState.Run.DifficultyModifier > 2 || Opponent.NumLives == 1) {
                //    mod.abilities.Add(Ability.Sniper);
                //    mod.fromCardMerge = true;
                //}
                card.AddTemporaryMod(mod);
            }
        }

        public override IEnumerator OpponentLifeLost() {
            LobotomyPlugin.Log.LogDebug($"[IndigoMidnight] OpponentLifeLost: numLives: {Opponent.NumLives}");

            if (Opponent.NumLives == 0) {
                defeated = false; // prevent end ordeal logic from running
                yield return Opponent.OutroSequence(true);
                yield break;
            }

            //bool wonByKill = OrdealDisplayConsole.Instance.amountLeft == 0 && TurnManager.Instance.IsPlayerTurn;
            int reactiveDifficulty = numTurnsLeft + RunState.Run.DifficultyModifier + Mathf.Max(0, 5 - LifeManager.Instance.DamageUntilPlayerWin) + BoardManager.Instance.GetOpponentOpenSlots().Count;
            if (Opponent.NumLives == 1) {
                reactiveDifficulty += 5;
            }
            numWavesLeft++;
            defeated = false;
            amountKilledThisTurn = 0;
            numTurnsLeft = NUM_TURNS;

            if (!startNextWaveUpkeep) {
                numTurnsLeft++; // buffer to account to how turns work
            }
            
            OrdealDisplayConsole.Instance.amountLeft = MinNumCardsRequired = ConstructWaveBlueprint(reactiveDifficulty);
            ViewManager.Instance.SwitchToView(View.Default);
            base.StartCoroutine(ShowResetSequence(false));
            SweeperOpponent.EmitCentre();
            yield return new WaitForSeconds(0.5f);
            //base.StartCoroutine(TurnManager.Instance.Opponent.ClearBoard());
            yield return TurnManager.Instance.Opponent.ClearQueue();
            yield return TurnManager.Instance.Opponent.QueueNewCards(changeView: false);

            if (startNextWaveUpkeep) {
                yield return UpdateConsoleDisplay(true, false);
            }
            startNextWaveUpkeep = false;
        }

        private IEnumerator UpdateConsoleDisplay(bool updateNumWaves, bool reduceTurn) {
            yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter, endDelay: 0.5f);
            OrdealDisplayConsole.Instance.SetCounterTextColour(Color.black);
            if (updateNumWaves) {
                newWave = false;
                yield return OrdealDisplayConsole.Instance.UpdateConsoleDisplay(numWavesLeft.ToString(), "wave", false, 0.5f, 0.8f);
            }

            if (reduceTurn) {
                numTurnsLeft--;
            }

            yield return OrdealDisplayConsole.Instance.UpdateConsoleDisplay(numTurnsLeft.ToString(), "turns left", false, 0.5f, 0.8f);

            base.StartCoroutine(OrdealDisplayConsole.Instance.ResetConsoleDisplay(0.5f, 0f));
            yield return new WaitForSeconds(0.5f);
        }

        private int ConstructWaveBlueprint(int reactiveDifficulty) {
            int numTurns = NUM_TURNS + RunState.Run.DifficultyModifier; // [6, 8]
            int numCards = 6 + RunState.Run.DifficultyModifier; // [7, 9]
            int seed = base.GetRandomSeed() + TurnNumber;
            List<List<CardInfo>> newPlan = new();

            Debug.Log($"[IndigoMidnight.ConstructWaveBlueprint] Reactive Difficulty: {reactiveDifficulty}");

            for (int i = 0; i < numTurns; i++) {
                int numPasses = 1 + reactiveDifficulty / 4;
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