using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Appears in R0
    /// Difficulty range: (1 - 3) +[0,2] // 4 difficulty is for boss node w/o challlenges
    /// 
    /// D | Turn 1 | Turn 2 | Turn 3 | Turn 4 | ## | HP | Atk
    /// 1 | P      | P P    | P      | P P    | 6  | 6  | 6
    /// 2 | P P    | P P    | P P    | P P    | 8  | 8  | 8
    /// 3 | P P    | P P P  | P P    | P P P  | 10 | 10 | 10
    /// 4 | P P P  | P P P  | P P P  | P P P  | 12 | 12 | 12
    /// 5 | P P P  | P P P P| P P P  | P P P P| 14 | 14 | 14
    /// </summary>
    public class OrdealAmberMidnight : OrdealBattleSequencer {
        private CardSlot[] resolveSlots;
        private PlayableCard bossCard;
        //private int turnsOnBoard;
        private int turnsOffBoard;

        private bool selectedSlotForBoss = false;

        public override IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) {
            yield return base.OnOpponentTurnEnd(opponentTurnSkipped);
            LobotomyPlugin.Log.LogDebug($"[AmberMidnight] OpponentTurnEnd check for boss resolve");
            if (!opponentTurnSkipped && !defeated) {
                // if an Eternal Meal hasn't been created yet or if it is burrowing
                // check for board resolve
                if (bossCard == null || !bossCard.OnBoard) {
                    LobotomyPlugin.Log.LogDebug($"[AmberMidnight] off board countdown");
                    turnsOffBoard--;
                    if (turnsOffBoard == 1) {
                        LobotomyPlugin.Log.LogDebug($"[AmberMidnight] select slots for resolve");
                        yield return SelectSlotForBossResolve();
                    }
                    else if (turnsOffBoard < 1) {
                        if (!selectedSlotForBoss) {
                            LobotomyPlugin.Log.LogDebug($"[AmberMidnight] force select resolve slots");
                            yield return SelectSlotForBossResolve();
                            yield return new WaitForSeconds(0.5f);
                        }
                        
                        yield return ResolveBoss();
                    }
                }
            }
        }


        private IEnumerator SelectSlotForBossResolve() {
            selectedSlotForBoss = true;
            yield return HelperMethods.ChangeCurrentView(View.Board);
            resolveSlots[0] = BoardManager.Instance.OpponentSlotsCopy[Random.Range(0, BoardManager.Instance.OpponentSlotsCopy.Count - 2)];
            resolveSlots[1] = BoardManager.Instance.OpponentSlotsCopy[resolveSlots[0].Index + 1];
            resolveSlots[2] = BoardManager.Instance.PlayerSlotsCopy[resolveSlots[0].Index];
            resolveSlots[3] = BoardManager.Instance.PlayerSlotsCopy[resolveSlots[1].Index];

            if (bossCard == null) {
                LobotomyPlugin.Log.LogDebug("[AmberMidnight] Create boss card");
                bossCard = CreateEternalMealCard();
            }

            // move the boss card to be directly below the slot it will initially emerge from
            bossCard.transform.parent = resolveSlots[2].transform;
            bossCard.transform.localPosition = new Vector3(0.7f, -0.025f, 1.05f);
            bossCard.transform.rotation = resolveSlots[2].transform.GetChild(0).rotation;
            bossCard.Anim.SetCardRendererFlipped(false);
            bossCard.RenderCard();

            for (int i = 0; i < resolveSlots.Length; i++) {
                CreateTargetIcon(resolveSlots[i], GameColors.Instance.glowRed);
            }
            
            yield return new WaitForSeconds(0.5f);
        }

        private IEnumerator ResolveBoss() {
            LobotomyPlugin.Log.LogDebug("[AmberMidnight] Resolve boss");
            CleanupTargetIcons();
            
            // play death animations first so they appear to die at the same-ish time
            for (int i = 0; i < resolveSlots.Length; i++) {
                if (resolveSlots[i].Card != null) {
                    resolveSlots[i].Card.Anim.PlayDeathAnimation();
                    yield return new WaitForSeconds(0.01f);
                }
            }
            LobotomyPlugin.Log.LogDebug("[AmberMidnight] triggerless deaths");
            // then resolve actual (non-card) triggers
            for (int i = 0; i < resolveSlots.Length; i++) {
                if (resolveSlots[i].Card != null) {
                    if (ValidCards.Contains(resolveSlots[i].Card.Info.name)) {
                        amountKilledThisTurn++;
                    }
                    yield return resolveSlots[i].Card.DieTriggerless(false);
                }
            }

            yield return HelperMethods.ChangeCurrentView(View.Board);
            LobotomyPlugin.Log.LogDebug("[AmberMidnight] Move above board");
            yield return MoveEternalMealAboveBoard();
            LobotomyPlugin.Log.LogDebug($"[AmberMidnight] Assign to slot {resolveSlots[0].Index}");
            yield return BoardManager.Instance.AssignCardToSlot(bossCard, resolveSlots[0]);

            turnsOffBoard = Mathf.Min(4, 1 + RunState.CurrentRegionTier + RunState.Run.DifficultyModifier);
            selectedSlotForBoss = false;
            LobotomyPlugin.Log.LogDebug($"[AmberMidnight] turnsOffBoard {turnsOffBoard}");
        }

        private PlayableCard CreateEternalMealCard() {
            CardInfo info = CardLoader.GetCardByName(Cards.eternalMeal);
            return CardSpawner.SpawnPlayableCard(info);
        }

        private IEnumerator MoveEternalMealAboveBoard() {
            AudioController.Instance.PlaySound3D("giant_stones_falling", MixerGroup.CardPaperSFX, bossCard.transform.position);
            yield return OrdealUtils.ShakeBoard();
            bossCard.Anim.PlayLandOnBoardEffects();
            Tween.LocalPosition(bossCard.transform, new Vector3(0.7f, 0.025f, 1.05f), 0.3f, 0.05f, Tween.EaseOut, Tween.LoopType.None, null, bossCard.Anim.PlayRiffleSound);

            yield return new WaitForSeconds(1f);
            bossCard.transform.parent = resolveSlots[0].transform;
            bossCard.transform.rotation = resolveSlots[0].transform.GetChild(0).rotation;
            bossCard.Anim.SetCardRendererFlipped(true);
            bossCard.RenderCard();
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            if (card.Info.name == Cards.eternalMeal) {
                turnsOffBoard = BoardManager.Instance.GetOpponentCards().Count > 0 ? 2 : 1;
            }
            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
        }

        public override void TryAddOrdealRandomBuff(PlayableCard card) {
            if (card.Info.name != Cards.eternalMeal) {
                base.TryAddOrdealRandomBuff(card);
                amountKilledThisTurn--;
            }
            else if (bossCard == null) {
                bossCard = card;
            }
        }

        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            int tierDifficulty = RunState.CurrentRegionTier + RunState.Run.DifficultyModifier;
            resolveSlots = new CardSlot[4];
            targetIconPrefab = AssetManager.warningTargetPrefab;
            turnsOffBoard = Mathf.Max(1, 4 - tierDifficulty);

            if (tierDifficulty > 2) {
                List<CardInfo> infos = new() { null, null, CardLoader.GetCardByName(Cards.eternalMeal) };
                infos = new(infos.Randomize()) {
                    null
                };
                EncounterData.StartCondition cond = new() { cardsInOpponentSlots = infos.ToArray()};
                encounterData.startConditions.Add(cond);
                turnsOffBoard++;
            }

            ValidCards.Add(Cards.perfectFood);
            ValidCards.Add(Cards.foodChain);
            ValidCards.Add(Cards.eternalMeal);
            LobotomyPlugin.Log.LogDebug($"[AmberMidnight] construct turns off board {turnsOffBoard}");
            return Mathf.Min(3, tierDifficulty);
        }
    }
}