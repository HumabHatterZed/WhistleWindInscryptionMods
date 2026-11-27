using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents {
    public class OrdealAmberMidnight : OrdealBattleSequencer {
        private PlayableCard Boss { get; set; }
        private Animator BossAnimator;

        private CardSlot[] resolveSlots;
        
        private int turnsOnBoard;
        private int turnsOffBoard;

        private bool selectedSlotForBoss = false;

        public override IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) {
            yield return base.OnOpponentTurnEnd(opponentTurnSkipped);

            LobotomyPlugin.Log.LogDebug($"[AmberMidnight] OpponentTurnEnd check for boss resolve");
            if (opponentTurnSkipped || defeated) {
                yield break;
            }

            if (Boss == null || !Boss.OnBoard) {
                // if an Eternal Meal hasn't been created yet or if it is burrowing
                // check for board resolve

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
            else if (Boss.OnBoard) {
                turnsOnBoard--;
                if (turnsOnBoard == 1) {
                    yield return HelperMethods.ChangeCurrentView(View.OpponentQueue);
                    BossAnimator.SetInteger("IdleState", 2);
                    yield return new WaitForSeconds(2f);
                }
                else if (turnsOnBoard < 1) {
                    LobotomyPlugin.Log.LogDebug("[AmberMidnight] End Turns On Board");
                    CardSlot anchorSlot = Boss.Slot;
                    // unassign boss from all slots
                    foreach (CardSlot slot in BoardManager.Instance.OpponentSlotsCopy) {
                        if (slot.Card == Boss) {
                            slot.Card = null;
                        }
                    }
                    Boss.Slot = null;

                    Boss.transform.localPosition += new Vector3(0, -0.005f, 0f);
                    yield return HelperMethods.ChangeCurrentView(View.OpponentQueue);
                    BossAnimator.SetInteger("IdleState", 0);
                    yield return new WaitForSeconds(0.1f);
                    LobotomyPlugin.Log.LogDebug("[AmberMidnight] Summon worms");
                    yield return SummonCards();

                    yield return new WaitForSeconds(1.75f);
                    
                    LobotomyPlugin.Log.LogDebug("[AmberMidnight] Move below board");
                    yield return MoveEternalMealBelowBoard(Boss, anchorSlot);
                    
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

            if (Boss == null) {
                LobotomyPlugin.Log.LogDebug("[AmberMidnight] Create boss card");
                Boss = CreateEternalMealCard();
                Boss.SetIsOpponentCard(true);
                Camera liveRenderCam = Singleton<CardRenderCamera>.Instance.GetLiveRenderCamera(Boss.StatsLayer);
                // get the animator from the actual rendered portrait offscreen, not the animator on the card object on the board
                Transform t = liveRenderCam.transform.GetChild(1).GetChild(0).GetChild(0);
                BossAnimator = t.GetChild(t.childCount - 1).GetComponentInChildren<Animator>();
            }

            // move the boss card to be directly below the slot it will initially emerge from
            Boss.transform.parent = resolveSlots[2].transform;
            Boss.transform.localPosition = new Vector3(0.7f, -0.025f, 1.05f);
            Boss.transform.rotation = resolveSlots[2].transform.GetChild(0).rotation;
            // since we're parented to a player slot
            // unflip the card renderer so it doesn't appear upside down
            // when it emerges from below the board
            Boss.Anim.SetCardRendererFlipped(false);
            Boss.RenderCard();

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
            ViewManager.Instance.SwitchToView(View.OpponentQueue);
            yield return BoardManager.Instance.AssignCardToSlot(Boss, resolveSlots[0]);
            yield return new WaitForSeconds(0.5f);

            selectedSlotForBoss = false;
            turnsOffBoard = Mathf.Min(4, 1 + RunState.CurrentRegionTier + RunState.Run.DifficultyModifier);
            LobotomyPlugin.Log.LogDebug($"[AmberMidnight] turnsOffBoard {turnsOffBoard}");
        }

        private IEnumerator MoveEternalMealAboveBoard() {
            AudioController.Instance.PlaySound3D("giant_stones_falling", MixerGroup.CardPaperSFX, Boss.transform.position);
            yield return OrdealUtils.ShakeBoard();
            Boss.Anim.PlayLandOnBoardEffects();
            BossAnimator.Play("entry");
            Tween.LocalPosition(Boss.transform, new Vector3(0.7f, 0.025f, 1.05f), 0.3f, 0.05f, Tween.EaseOut, Tween.LoopType.None, null, Boss.Anim.PlayRiffleSound);

            yield return new WaitForSeconds(2f);
            BossAnimator.SetInteger("IdleState", 1);
            // once we're above the board, pre-emptively parent it to its actual slot
            // this will prevent it from doing a spin once we properly assign it
            Boss.transform.parent = resolveSlots[0].transform;
            Boss.transform.rotation = resolveSlots[0].transform.GetChild(0).rotation;
            Boss.Anim.SetCardRendererFlipped(true);
            Boss.RenderCard();
        }
        private IEnumerator MoveEternalMealBelowBoard(PlayableCard card, CardSlot anchorSlot) {
            AudioController.Instance.PlaySound3D("giant_stones_falling", MixerGroup.CardPaperSFX, card.transform.position);
            card.transform.parent = anchorSlot.transform;
            card.transform.rotation = anchorSlot.transform.GetChild(0).rotation;
            Tween.LocalPosition(card.transform, new Vector3(0.7f, -0.025f, 1.05f), 0.3f, 0.05f, Tween.EaseOut, Tween.LoopType.None, null, card.Anim.PlayRiffleSound);
            yield return OrdealUtils.ShakeBoard();
        }
        private IEnumerator SummonCards() {
            int rand = base.GetRandomSeed();
            AudioController.Instance.PlaySound3D("goovoice_calm#2", MixerGroup.CardPaperSFX, base.transform.position);
            yield return CombatHelpers.CreateCardInRandomSlot(CardLoader.GetCardByName(Cards.foodChain), BoardManager.Instance.GetOpponentOpenSlots());
            yield return CombatHelpers.CreateCardInRandomSlot(CardLoader.GetCardByName(Cards.foodChain), BoardManager.Instance.GetOpponentOpenSlots());
        }


        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            if (card.Info.name == Cards.eternalMeal) {
                // set time until the next eternal meal is spawned
                turnsOffBoard = BoardManager.Instance.GetOpponentCards().Count > 0 ? 2 : 1;
                Boss = null;
            }
            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
        }

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) {
            return otherCard != null && otherCard == Boss;
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard) {
            turnsOnBoard = Mathf.Max(1, 4 - RunState.CurrentRegionTier - RunState.Run.DifficultyModifier);
            LobotomyPlugin.Log.LogDebug($"[AmberMidnight] Assigned to slot, turnsOnBoard = {turnsOnBoard}");
            yield break;
        }

        public override void TryAddOrdealRandomBuff(PlayableCard card) {
            if (card.Info.name != Cards.eternalMeal) {
                base.TryAddOrdealRandomBuff(card);
                amountKilledThisTurn--; // increase the total num of cards that need to be killed
            }
            //else if (Boss == null) {
            //    Boss = card;
            //}
        }

        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            int tierDifficulty = RunState.CurrentRegionTier + RunState.Run.DifficultyModifier;
            resolveSlots = new CardSlot[4];
            targetIconPrefab = AssetManager.warningTargetPrefab;
            turnsOffBoard = Mathf.Max(1, 4 - tierDifficulty);

            if (tierDifficulty > 2) {
                // on the final region (default diff) have one worm already present on the board
                List<CardInfo> infos = new() { null, null, CardLoader.GetCardByName(Cards.eternalMeal) };
                infos = new(infos.Randomize()) {
                    null
                };
                EncounterData.StartCondition cond = new() { cardsInOpponentSlots = infos.ToArray() };
                encounterData.startConditions.Add(cond);
                turnsOffBoard++;
            }

            ValidCards.Add(Cards.perfectFood);
            ValidCards.Add(Cards.foodChain);
            ValidCards.Add(Cards.eternalMeal);
            LobotomyPlugin.Log.LogDebug($"[AmberMidnight] construct turns off board {turnsOffBoard}");
            return Mathf.Min(3, tierDifficulty);
        }

        private PlayableCard CreateEternalMealCard() {
            CardInfo info = CardLoader.GetCardByName(Cards.eternalMeal);
            return CardSpawner.SpawnPlayableCard(info);
        }
    }
}