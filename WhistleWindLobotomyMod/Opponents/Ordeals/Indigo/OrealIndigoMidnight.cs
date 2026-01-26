using DiskCardGame;
using InscryptionAPI.Encounters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Based on the Sweepers battle in Limbus Company.
    /// Indigo Midnight is a constant onslaught of cards, where the player must survive three waves.
    /// Cards appear in a steady stream.
    /// Cards required: 5
    /// Valid regions: 0, 1, 2
    /// </summary>
    public class OrdealIndigoMidnight : OrdealIndigoNoon {
        private int numWavesLeft = 3;
        private int numTurnsLeft = 5;
        private bool newWave = true;

        public override IEnumerator PlayerUpkeep() {
            numTurnsLeft--;
            if (numTurnsLeft == 0) {
                Opponent.NumLives--;
                yield return OpponentLifeLost();
            }
            yield return UpdateCounterIcon(newWave);
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            if (!CardIsValidOrdeal(card)) {
                if (card.OpponentCard) {
                    yield return DialogueHelper.PlayDialogueEvent("OrdealNonOrdealKilled");
                }
                yield break;
            }
            amountKilledThisTurn++;
            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);

            LobotomyPlugin.Log.LogDebug($"[IndigoMidnight] OnOtherCardDie: dead card:[{card.Info.displayedName}] total killed:[{amountKilledThisTurn}]");
            LobotomyPlugin.Log.LogDebug($"[IndigoMidnight] Cards left: {OrdealCounterManager.Instance.amountLeft - amountKilledThisTurn}");
        }

        public override IEnumerator OpponentLifeLost() {
            LobotomyPlugin.Log.LogDebug($"[IndigoMidnight] OpponentLifeLost: numLives: {Opponent.NumLives}");

            if (Opponent.NumLives == 0) {
                yield return HelperMethods.ChangeCurrentView(View.Default);
                OrdealCounterManager.Instance.EnableConsole(false);
                yield return new WaitForSeconds(0.25f);
                OrdealCounterManager.Instance.SetShown(false);
                yield return new WaitForSeconds(1.5f);
                yield return Opponent.DefeatedFinalBossSequence();
                yield break;
            }

            yield return HelperMethods.ChangeCurrentView(View.OpponentQueue);
            yield return Opponent.ClearBoard();

            defeated = false;
            numTurnsLeft = 5;
            numWavesLeft--;

            MinNumCardsRequired = ConstructWaveBlueprint();
            yield return Opponent.QueueNewCards();

            yield return new WaitForSeconds(0.75f);
            yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter);
            OrdealCounterManager.Instance.EnableConsole(false);
            yield return new WaitForSeconds(0.3f);
            OrdealCounterManager.Instance.SetTextColour(Color.black);
            OrdealCounterManager.Instance.UpdateConsole(ordealTier, MinNumCardsRequired);
            OrdealCounterManager.Instance.EnableConsole(true);
            yield return new WaitForSeconds(0.3f);
        }

        private IEnumerator UpdateCounterIcon(bool updateNumWaves) {
            yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter, endDelay: 0.5f);
            if (updateNumWaves) {
                newWave = false;
                OrdealCounterManager.Instance.EnableConsole(false);
                yield return new WaitForSeconds(0.8f);
                OrdealCounterManager.Instance.UpdateConsole(ordealTier, numWavesLeft, "waves left");
                OrdealCounterManager.Instance.EnableConsole(true);
                yield return new WaitForSeconds(2f);
            }

            OrdealCounterManager.Instance.EnableConsole(false);
            yield return new WaitForSeconds(0.8f);
            OrdealCounterManager.Instance.UpdateConsole(ordealTier, numTurnsLeft, "turns left");
            OrdealCounterManager.Instance.EnableConsole(true);
            yield return new WaitForSeconds(1.5f);

            OrdealCounterManager.Instance.EnableConsole(false);
            yield return new WaitForSeconds(0.8f);
            OrdealCounterManager.Instance.UpdateConsole(ordealTier, OrdealCounterManager.Instance.amountLeft);
            OrdealCounterManager.Instance.EnableConsole(true);

            yield return new WaitForSeconds(0.75f);
        }

        private int ConstructWaveBlueprint() {
            int numTurns = 5 + Opponent.Difficulty / 6;
            int numCards = 6 + RunState.Run.DifficultyModifier;
            int seed = base.GetRandomSeed();
            List<List<CardInfo>> newPlan = new();

            for (int i = 0; i < numTurns; i++) {
                List<CardInfo> turn = new() {
                    CardLoader.GetCardByName(GetRandomSweeper(seed++, 7))
                };

                // extra sweeper every third turn
                if (i % 3 == 0) {
                    // UPDATE RANGE ONCE ALL SWEEPERS ARE MADE
                    turn.Add(CardLoader.GetCardByName(GetRandomSweeper(seed++, 7)));
                    if (i % 6 == 0) {
                        // UPDATE RANGE ONCE ALL SWEEPERS ARE MADE
                        turn.Add(CardLoader.GetCardByName(GetRandomSweeper(seed++, 7)));
                    }
                }

                if (i > 0) {
                    if (i % 7 == 0) {
                        // UPDATE RANGE ONCE ALL SWEEPERS ARE MADE
                        turn.Add(CardLoader.GetCardByName(GetRandomSweeper(seed++, 7)));
                    }

                    if (i % 4 == 0) {
                        newPlan.Add(new());
                    }
                }

                newPlan.Add(turn);
            }

            EncounterBluePrint = newPlan;
            Opponent.ReplaceAndAppendTurnPlan(newPlan);
            return numCards;
        }

        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            int numTurns = 5 + baseDifficulty / 6;
            int numCards = 6 + RunState.Run.DifficultyModifier;
            int seed = base.GetRandomSeed();

            for (int i = 0; i < numTurns; i++) {
                List<EncounterBlueprintData.CardBlueprint> turn = new() {
                    EncounterManager.NewCardBlueprint(GetRandomSweeper(seed++, 7))
                };

                if (i > 0) {
                    // extra sweeper every third turn
                    if (i % 3 == 0) {
                        turn.Add(EncounterManager.NewCardBlueprint(GetRandomSweeper(seed++, 7)));
                    }

                    if (i % 7 == 0) {
                        turn.Add(EncounterManager.NewCardBlueprint(GetRandomSweeper(seed++, 7)));
                    }

                    if (i % 2 == 0) {
                        encounterData.Blueprint.AddTurn();
                    }
                }

                encounterData.Blueprint.AddTurn(turn);
            }
            return numCards;
        }
    }
}