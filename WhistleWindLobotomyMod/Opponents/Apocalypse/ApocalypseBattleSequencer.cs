using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents.Apocalypse {
    public class ApocalypseBattleSequencer : LobotomyBossBattleSequencer, IModifyDirectDamage, IItemCanBeUsed, IOnItemPreventedFromUse {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "ApocalypseBattleSequencer", typeof(ApocalypseBattleSequencer)).Id;
        public override Opponent.Type BossType => LobOpponentUtils.ApocalypseBossID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.ApocalypseBossDefeated;
        public override int HighestPositiveScaleBalance { get => 4; set => base.HighestPositiveScaleBalance = value; }
        private ApocalypseBossOpponent BossOpponent => TurnManager.Instance.Opponent as ApocalypseBossOpponent;

        private readonly Dictionary<ActiveEggEffect, string[]> AllBossPhases = new()
        {
            { ActiveEggEffect.BigEyes, new string[2] {
                Cards.eyeballChick,
                Cards.bigEgg} },
            { ActiveEggEffect.SmallBeak, new string[2] {
                Cards.forestKeeper,
                Cards.littleEgg } },
            { ActiveEggEffect.LongArms, new string[2] {
                Cards.runawayBird,
                Cards.longEgg } }
        };

        public ActiveEggEffect ActiveEggEffect = ActiveEggEffect.None;
        public readonly List<ActiveEggEffect> DisabledEggEffects = new();

        public override int BossHealthThreshold(int remainingLives) => remainingLives switch {
            4 => 70,
            3 => 50,
            2 => 30,
            _ => 1
        };

        public string ActiveEggMinion = null;

        public bool justSwitchedEffect = false;
        private bool seenMouthAttack = false;
        private bool seenEyeAttack = false;
        private bool seenArmsAttack = false;

        public readonly List<CardSlot> specialTargetSlots = new();
        public readonly List<CardSlot>[] giantTargetSlots = new List<CardSlot>[2]
        {
            new(), // red targets
            new() // white targets
        };

        private GameObject bossMouthPrefab;
        public readonly Dictionary<CardSlot, GameObject> mouthIcons = new();

        #region Big Eyes
        private IEnumerator BigBirdEnchantCards() {
            List<CardSlot> possibleTargetSlots;
            int maxCount;
            int randomSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;

            if (BossOpponent.NumLives < 3 || ReactiveDifficulty > 8) {
                possibleTargetSlots = BoardManager.Instance.PlayerSlotsCopy;
                maxCount = possibleTargetSlots.Count;
            }
            else {
                possibleTargetSlots = BoardManager.Instance.AllSlotsCopy;
                maxCount = 2 + PhaseDifficulty;
            }
            possibleTargetSlots.RemoveAll(x => x.Card == null || x.Card == BossCard);

            while (possibleTargetSlots.Count > 0) {
                if (specialTargetSlots.Count == maxCount)
                    break;

                CardSlot target = possibleTargetSlots[SeededRandom.Range(0, possibleTargetSlots.Count, randomSeed++)];
                specialTargetSlots.Add(target);
                possibleTargetSlots.Remove(target);
            }

            yield return HelperMethods.ChangeCurrentView(View.Default, 0f);
            BossOpponent.MasterAnimator.SetBool("Flare", true);
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossEyePreAttack", 0f, repeatLines: !seenEyeAttack);

            if (specialTargetSlots.Count == 0) {
                BossCard.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.45f);
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossEyeFailAttack", 0f, repeatLines: !seenEyeAttack);
            }
            else {
                foreach (CardSlot slot in specialTargetSlots) {
                    yield return new WaitForSeconds(0.05f);
                    CreateTargetIcon(slot, GameColors.Instance.yellow);
                }
                yield return new WaitForSeconds(0.5f);

                List<Transform> leftSources = new(BossOpponent.LeftEyes);
                List<Transform> rightSources = new(BossOpponent.RightEyes);
                int enchantCount = ReactiveDifficulty > 13 ? 2 : 1;

                AudioController.Instance.PlaySound2D("bird_laser_fire", MixerGroup.TableObjectsSFX);
                for (int i = 0; i < specialTargetSlots.Count; i++) {
                    Transform source;
                    for (int j = 0; j < enchantCount; j++) {
                        if (specialTargetSlots[i].Index % 2 == 0) {
                            source = leftSources[UnityEngine.Random.Range(0, leftSources.Count)];
                            leftSources.Remove(source);
                        }
                        else {
                            source = rightSources[UnityEngine.Random.Range(0, rightSources.Count)];
                            rightSources.Remove(source);
                        }
                        FireLaser(source.gameObject, specialTargetSlots[i], specialTargetSlots[i].IsPlayerSlot);
                    }
                    yield return new WaitForSeconds(0.1f);
                    specialTargetSlots[i].Card.Anim.StrongNegationEffect();

                    yield return specialTargetSlots[i].Card.AddStatusEffectToFaceDown<Enchanted>(enchantCount, modifyTurnGained: (int turn) => turn + 1); // increase the turn gained by 1 so it disappears at the correct time
                    CleanUpTargetIcon(targetIcons[i]);
                }

                yield return HelperMethods.ChangeCurrentView(View.Board, 0.2f, 0f);
                yield return new WaitForSeconds(0.3f);
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossEyePostAttack", 0f, repeatLines: !seenEyeAttack);
                seenEyeAttack = true;
            }

            CleanupTargetIcons();
            specialTargetSlots.Clear();
            yield return BossOpponent.ResetToIdle();
        }
        #region Lasers
        private void UpdateAttackColours() {
            foreach (PlayableCard c in BoardManager.Instance.CardsOnBoard.Concat(PlayerHand.Instance.CardsInHand)) {
                c.OnStatsChanged();
            }
        }
        private void FireLaser(GameObject source, CardSlot targetSlot, bool attackPlayer) {
            GameObject gameObject = new("Line");
            LineRenderer line = gameObject.AddComponent<LineRenderer>();
            line.material = Material.GetDefaultLineMaterial();
            line.startColor = line.endColor = Color.yellow;
            line.startWidth = 1f;
            line.endWidth = 0f;
            line.widthCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.125f, 1.7f), new Keyframe(0.25f, 3f), new Keyframe(1f, 3f));
            line.widthMultiplier = 0f;

            Vector3 vector = source.transform.position + Vector3.up * 0.05f + Vector3.right * 2.05f + Vector3.forward;
            Vector3 vector2 = targetSlot.transform.position + (attackPlayer ? Vector3.back : Vector3.zero) + (attackPlayer ? Vector3.zero : (Vector3.down * 0.05f));

            line.SetPositions(new Vector3[2] { vector, vector2 });
            line.alignment = LineAlignment.TransformZ;
            CustomCoroutine.Instance.StartCoroutine(TweenLineWidth(line, attackPlayer, 0.2f));
        }
        private IEnumerator TweenLineWidth(LineRenderer line, bool attackPlayer, float time = 0.25f) {
            float ela2 = line.widthMultiplier = 0f;
            while (ela2 < time) {
                if (line == null) {
                    if (attackPlayer)
                        yield break;

                    Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.2f);
                    yield break;
                }
                ela2 += UnityEngine.Time.deltaTime;
                line.widthMultiplier = Mathf.Lerp(0f, 1f, ela2 / time);
                yield return new WaitForEndOfFrame();
            }

            if (line == null) {
                yield return new WaitForSeconds(0.1f);
                yield break;
            }
            ela2 = 0f;
            line.widthMultiplier = 1f;
            while (ela2 < time) {
                if (line == null) {
                    yield return new WaitForSeconds(0.1f);
                    yield break;
                }
                ela2 += UnityEngine.Time.deltaTime;
                line.widthMultiplier = Mathf.Lerp(1f, 0f, ela2 / time);
                yield return new WaitForEndOfFrame();
            }

            if (line != null)
                Destroy(line.gameObject);

            yield return new WaitForSeconds(0.1f);
        }
        #endregion

        #endregion

        #region Small Beak
        private IEnumerator SmallBirdTargetLanes() {
            List<CardSlot> lanes = BoardManager.Instance.OpponentSlotsCopy;
            int rand = base.GetRandomSeed();
            int rowsToTarget = 1 + PhaseDifficulty;
            for (int i = 0; i < rowsToTarget; i++) {
                // add rows of card slots
                int laneIndex = SeededRandom.Range(0, lanes.Count, rand++);
                CardSlot lane = lanes[laneIndex];
                specialTargetSlots.Add(lane);
                specialTargetSlots.Add(BoardManager.Instance.PlayerSlotsCopy[lane.Index]);
                lanes.Remove(lane);
            }

            BossOpponent.MasterAnimator.SetBool("Mouth", true);
            yield return new WaitForSeconds(0.25f);

            // create the mouth objects relative to the opponent slots' positions
            foreach (CardSlot slot in specialTargetSlots.Where(x => x.IsOpponentSlot())) {
                yield return new WaitForSeconds(0.1f);
                GameObject obj = Instantiate(bossMouthPrefab);
                obj.transform.localPosition = slot.transform.position + new Vector3(0f, 1.2f, 0.4f);
                mouthIcons.Add(slot, obj);
            }
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossMouthPreAttack", repeatLines: !seenMouthAttack);
        }
        private IEnumerator SmallBirdAttackLanes() {
            bool killedCard = false;
            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);
            for (int i = 0; i < specialTargetSlots.Count; i++) {
                GameObject mouthAnim = null;
                PlayableCard target = specialTargetSlots[i].Card;

                // mouth anim pivot is on opponent slot
                if (specialTargetSlots[i].IsOpponentSlot()) {
                    mouthAnim = mouthIcons[specialTargetSlots[i]];
                    mouthAnim.GetComponent<Animator>().Play("mouthShut");
                    yield return new WaitForSeconds(0.05f);
                    AudioController.Instance.PlaySound2D("bird_mouth", MixerGroup.TableObjectsSFX);
                    yield return new WaitForSeconds(0.05f);
                }

                if (target != null && target != BossCard) {
                    killedCard = true;
                    yield return target.FlipFaceUp(target.FaceDown);
                    yield return target.Die(false, BossCard);
                }

                if (mouthAnim != null) {
                    yield return new WaitForSeconds(0.05f);
                    CleanUpTargetIcon(mouthAnim);
                }
            }

            mouthIcons.Clear();
            specialTargetSlots.Clear();
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent(killedCard ? "ApocalypseBossMouthPostAttack" : "ApocalypseBossMouthFailAttack",
                0f, repeatLines: !seenMouthAttack);

            yield return BossOpponent.ResetToIdle();
            seenMouthAttack = true;
        }
        #endregion

        #region Long Arms
        private IEnumerator ArmAttackSequence() {
            List<PlayableCard> cardsOnBoard = BoardManager.Instance.CardsOnBoard;
            cardsOnBoard.RemoveAll(x => x == null || x.GetStatusEffectPotency<Sin>() < 3);
            cardsOnBoard.Remove(BossCard);
            if (cardsOnBoard.Count == 0)
                yield break;

            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);
            foreach (PlayableCard c in cardsOnBoard) {
                c.Anim.SetMarkedForSacrifice(marked: true);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossArmsPreAttack", 0f, repeatLines: !seenArmsAttack);

            foreach (PlayableCard c in cardsOnBoard) {
                c.Anim.PlaySacrificeSound();
                c.Anim.DeactivateSacrificeHoverMarker();
                yield return c.Die(false, BossCard);
            }
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossArmsPostAttack", 0f, repeatLines: !seenArmsAttack);
            seenArmsAttack = true;
        }
        #endregion

        #region Giant Logic
        public IEnumerator GiantPhaseLogic(bool firstStrike) {
            int maxRedTargets = 3, maxWhiteTargets = BossCard.Health < 16 ? 1 : 0;
            int randomSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;
            List<CardSlot> playerSlots = BoardManager.Instance.PlayerSlotsCopy;
            playerSlots.Randomize();

            if (firstStrike) {
                maxRedTargets--;
            }
            if (ReactiveDifficulty < 14) {
                maxRedTargets--;
            }

            if (ReactiveDifficulty < 8 || SeededRandom.Value(randomSeed++) <= 0.6f) {
                playerSlots.RemoveAt(0);
            }
            if (SeededRandom.Value(randomSeed++) <= 0.2f) {
                playerSlots.RemoveAt(0);
            }

            CleanupTargetIcons();
            specialTargetSlots.Clear();
            giantTargetSlots[0].Clear();
            giantTargetSlots[1].Clear();

            yield return SelectGiantTargets(maxRedTargets, maxWhiteTargets, randomSeed, playerSlots);
        }
        private IEnumerator SelectGiantTargets(int numRedTargets, int numWhiteTargets, int randomSeed, List<CardSlot> targetSlots) {
            int numTargets = 0;
            int numDirectHits = 0;
            float chanceForRed = 0.17f, chanceForWhite = 0.19f;

            if (ReactiveDifficulty > 3) {
                float baseChance = Mathf.Min(0.17f, (ReactiveDifficulty - 3) / 100f);
                chanceForRed += baseChance * 0.87f;
                chanceForWhite += baseChance * 1.2f;
            }

            if (BossCard.Health < 16) {
                chanceForRed += 0.02f;
            }

            foreach (CardSlot slot in targetSlots) {
                float chanceToIgnore = numTargets * 0.25f + numDirectHits * 0.33f;
                if (SeededRandom.Value(randomSeed++) <= chanceToIgnore) {
                    continue;
                }
                Color targetColour = GameColors.Instance.yellow;
                GameObject prefab = TargetIconHelper.targetIconPrefab;
                if (numRedTargets > 0 && SeededRandom.Value(randomSeed++) <= chanceForRed) {
                    targetColour = GameColors.Instance.glowRed;
                    giantTargetSlots[0].Add(slot);
                    prefab = AssetManager.warningTargetPrefab;
                    numRedTargets--;
                }
                else if (numWhiteTargets > 0 && SeededRandom.Value(randomSeed++) <= chanceForWhite) {
                    targetColour = GameColors.Instance.brightNearWhite;
                    giantTargetSlots[1].Add(slot);
                    numWhiteTargets--;
                }

                yield return new WaitForSeconds(0.05f);
                CreateTargetIcon(slot, prefab, targetColour);
                specialTargetSlots.Add(slot);
                if (slot.Card == null) {
                    numDirectHits++;
                }
            }

            yield return new WaitForSeconds(0.05f);
        }
        public void CleanUpGiantTarget(CardSlot slot) {
            GameObject obj = targetIcons.Find(x => x.transform.parent == slot.transform);
            if (obj == null)
                return;

            giantTargetSlots[0].Remove(slot);
            giantTargetSlots[1].Remove(slot);
            targetIcons.Remove(obj);

            CleanUpTargetIcon(obj);
        }
        #endregion

        #region Turn Plan
        /// <summary>
        /// Determines how many cards should be played each turn.
        /// In order of priority:
        /// Every 4 turns: queue 2 cards.
        /// Every even turn: queue 0 cards.
        /// Every odd turn: queue 1 card.
        /// </summary>
        private int GetStartingCardCount(bool opponentWinning) {
            int count = 0;
            if (TurnNumber > 0 && TurnManager.Instance.TurnNumber % 4 == 0) {
                count += 2;
            }
            else if (TurnManager.Instance.TurnNumber % 2 != 0) {
                count++;
            }

            if (opponentWinning && count > 0) {
                count--;
            }
            else if (TurnNumber > 2 && LifeManager.Instance.Balance == HighestPositiveScaleBalance) {
                count++;
            }

            //LobotomyPlugin.Log.LogDebug($"[ApocalypseBoss] StartingCardCount: {count}");
            return count;
        }

        public override List<CardInfo> CreateNextTurnPlan(int randomSeed, bool opponentWinning) {
            int cardNum = GetStartingCardCount(opponentWinning);
            List<CardInfo> nextTurn = new();

            // threshold for whether to give queued card a mod
            // if the difficulty modifier is 6 or higher, guaranteed to modify stats, other chance is dependent on difficulty and cards being cued
            float gateValue;
            if (ReactiveDifficulty > 11) {
                gateValue = opponentWinning ? 0.65f + ((ReactiveDifficulty - 12) * 0.02f) : 1f; // guaranteed to give a mod at 12+ reactive if scale is losing
                cardNum++;
            }
            else
                gateValue = (4 - cardNum - (opponentWinning ? 1 : 0)) / Mathf.Max(1f, 7f - RunState.Run.DifficultyModifier);

            // if the queue is full, reduce the cardNum
            if (BossOpponent.Queue.Count == 4) {
                // if the latest added turn was also full, add an empty turn plan
                if (BossOpponent.TurnPlan.Last().Count == 4 && ReactiveDifficulty < 11)
                    cardNum = 0;
                else
                    cardNum -= ReactiveDifficulty > 7 ? 1 : (ReactiveDifficulty > 4 ? 2 : 2);
            }

            for (int i = 0; i < cardNum; i++) {
                CardInfo clone = CardLoader.GetCardByName(ActiveEggMinion);
                float randomValue = SeededRandom.Value(randomSeed++);
                nextTurn.Add(clone);
                if (randomValue > gateValue) {
                    continue;
                }

                int attack = 0, health = ReactiveDifficulty / 5;

                if (ReactiveDifficulty > 3) {
                    health++;
                    if (ReactiveDifficulty > 7) {
                        health++;
                    }
                }
                if (SeededRandom.Bool(randomSeed++)) {
                    attack++;
                    if (ReactiveDifficulty < 12 && clone.baseHealth + health > 1) {
                        attack++;
                        health--;
                    }
                }

                clone.Mods.Add(new(attack, health));
            }

            return nextTurn;
        }
        #endregion

        #region Reactive Difficulty
        public IEnumerator UpdateReactiveDifficulty() {
            int raiseDifficulty = 0;
            if (damageTakenThisTurn > 5) {
                raiseDifficulty += damageTakenThisTurn / 6;
                if (timesHitThisTurn < 3) {
                    raiseDifficulty += damageTakenThisTurn / 4;
                }
            }
            if (timesHitThisTurn > 2) {
                raiseDifficulty += timesHitThisTurn - 2;
            }
            if (TurnManager.Instance.DamageDealtThisTurn > 6) {
                raiseDifficulty += TurnManager.Instance.DamageDealtThisTurn / 7;
            }

            ClearTempMods();
            if (raiseDifficulty > 0) {
                yield return IncreaseReactiveDifficulty(raiseDifficulty);
            }

            LobotomyPlugin.Log.LogDebug($"[TurnEnd] Update variables: timesHit: {timesHitThisTurn} damageTaken: {damageTakenThisTurn} DamageThisPhase: {TurnManager.Instance.DamageDealtThisTurn}");
        }

        /// <summary>
        /// Increase boss's power and play dialogue based on reactive difficulty.
        /// </summary>
        public override IEnumerator OnReactiveDifficultyIncreased(int amount) {
            if (amount < 1) {
                yield break;
            }

            if (amount > 1) {
                Singleton<CameraEffects>.Instance.Shake(0.25f, 0.125f);
                BossCard.Anim.StrongNegationEffect();
            }

            if (ReactiveDifficulty > 7 && !BossCard.Info.Mods.Exists(x => x.singletonId == "ReactiveStrength")) {
                BossCard.Info.Mods.Add(new(1, 0) { singletonId = "ReactiveStrength", nonCopyable = true });
                BossCard.OnStatsChanged();
            }
            yield return new WaitForSeconds(0.15f);

            if (ReactiveDifficulty > 17)
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossReactive3");
            else if (ReactiveDifficulty > 7)
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossReactive2");
            else
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossReactive1");
        }
        #endregion

        /// <summary>
        /// Handles egg effects and phase changes.
        /// Phase changes always occur. If the opponent's turn is skipped, egg effects are not triggered and phase countdown is paused.
        /// When the phase countdown hits 0, the phase will change. When a life is lost, the phase is forcefully changed.
        /// </summary>
        public override IEnumerator OnPlayerTurnEnd() {
            yield return HelperMethods.ChangeCurrentView(View.Board);

            if (changeToNextPhase) {
                BossOpponent.NumLives--;
                DisabledEggEffects.Add(ActiveEggEffect);
                yield return BossOpponent.LifeLostSequence();
                if (BossOpponent.NumLives == 1) {
                    ResetVariablesTurnEnd();
                    ActiveEggEffect = ActiveEggEffect.None;
                    finalPhase = true;
                }
                yield return BossOpponent.PostResetScalesSequence(); // calls StartNewPhaseSequence
                changeToNextPhase = false;
            }

            // don't activate or update egg effects/attacks
            if (TurnManager.Instance.Opponent.SkipNextTurn) {
                justSwitchedEffect = false;
                yield break;
            }

            if (justSwitchedEffect)
                justSwitchedEffect = false;
            else
                turnsToNextPhase--;

            if (ActiveEggEffect == ActiveEggEffect.LongArms) {
                yield return ArmAttackSequence();
            }
            else if (ActiveEggEffect == ActiveEggEffect.SmallBeak) {
                if (turnsToNextPhase == 2)
                    yield return SmallBirdTargetLanes();
                else if (turnsToNextPhase == 1)
                    yield return SmallBirdAttackLanes();
            }

            UpdateCounter();
            if (DisabledEggEffects.Count < 2) // only switch phase if there's more than 1 remaining egg effect
            {
                if (turnsToNextPhase == 0 && !changeToNextPhase)
                    yield return ResetAndChangeEggEffect(false);
            }
            else {
                if (turnsToNextPhase == 0)
                    turnsToNextPhase = 3;
                //UpdateCounter();
            }

            AddNextTurnToPlan();
        }

        /// <summary>
        /// Updates reactive difficulty. If the phase/battle isn't over, also moves cards and triggers certain egg effects
        /// </summary>
        public override IEnumerator OpponentCombatEnd() {
            yield return UpdateReactiveDifficulty();
            ResetVariablesTurnEnd();

            if (TurnManager.Instance.LifeLossConditionsMet())
                yield break;

            AudioController.Instance.SetLoopVolume(0.3f, 1f);
            if (finalPhase) {
                yield return GiantPhaseLogic(false);
                yield break;
            }
            else {
                yield return MoveOpponentCards();
                if (ActiveEggEffect == ActiveEggEffect.BigEyes && turnsToNextPhase == 2) {
                    yield return BigBirdEnchantCards();
                }
                else if (ActiveEggEffect == ActiveEggEffect.LongArms) {
                    List<PlayableCard> cardsOnBoard = BoardManager.Instance.CardsOnBoard;
                    cardsOnBoard.Remove(BossCard);
                    if (cardsOnBoard.Count == 0) {
                        yield break;
                    }
                    foreach (PlayableCard c in cardsOnBoard) {
                        bool facedown = c.FaceDown;
                        yield return c.FlipFaceUp(facedown);
                        c.Anim.StrongNegationEffect();
                        yield return c.AddStatusEffectToFaceDown<Sin>(1);
                        yield return new WaitForSeconds(0.1f);
                        yield return c.FlipFaceDown(facedown);
                    }
                }
            }
        }

        public IEnumerator ResetAndChangeEggEffect(bool lostLife) {
            turnsToNextPhase = 3;
            justSwitchedEffect = true;

            specialTargetSlots.Clear();
            CleanupTargetIcons();
            foreach (GameObject obj in mouthIcons.Values)
                CleanUpTargetIcon(obj);

            mouthIcons.Clear();
            yield return BossOpponent.ClearQueue();
            yield return new WaitForSeconds(0.4f);

            Action transformCallback = UpdateCounter;

            bool updatingColours = false;
            if (ActiveEggEffect == ActiveEggEffect.BigEyes) {
                transformCallback += UpdateAttackColours;
                updatingColours = true;
            }

            ChangeActiveEggEffect();

            if (!updatingColours && ActiveEggEffect == ActiveEggEffect.BigEyes)
                transformCallback += UpdateAttackColours;

            // transform into the next egg card
            CardInfo bossEggInfo = CardLoader.GetCardByName(AllBossPhases[ActiveEggEffect][1]);
            if (ReactiveDifficulty > 7)
                bossEggInfo.Mods.Add(new(1, 0) { singletonId = "ReactiveStrength", nonCopyable = true });

            ClearTempMods();
            yield return BossCard.TransformIntoCard(bossEggInfo, transformCallback);
            yield return new WaitForSeconds(0.4f);
            yield return BossCard.RemoveStatusEffects();

            //// if we're switching due to losing a life, reset the turn plan and create the next turn plan
            //if (lostLife) {

            //    //BossOpponent.NumTurnsTaken = 0; // reset NumTurnsTaken so the turn plan doesn't break
            //    //CreateNextTurnPlan();


            //}
        }

        /// <summary>
        /// Handles changing the effect and main mook card for the current subphase.
        /// </summary>
        private void ChangeActiveEggEffect() {
            if (finalPhase || DisabledEggEffects.Count == 3)
                return;

            List<ActiveEggEffect> possiblePhases = AllBossPhases.Keys.Where(x => !DisabledEggEffects.Contains(x)).ToList();
            possiblePhases.Remove(ActiveEggEffect);

            ActiveEggEffect = possiblePhases[SeededRandom.Range(0, possiblePhases.Count, base.GetRandomSeed() + TurnManager.Instance.TurnNumber)];
            ActiveEggMinion = AllBossPhases[ActiveEggEffect][0];
        }

        #region Triggers
        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target) => true;
        public override IEnumerator OnOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target) {
            // if the boss dealt damage
            if (attacker == BossCard && finalPhase) {
                if (giantTargetSlots[1].Contains(target.Slot)) // if white target, heal the boss
                    BossCard.HealDamage(BossCard.Attack);

                CleanUpGiantTarget(target.Slot);
                yield break;
            }

            if (target != BossCard) {
                yield break;
            }

            IncrementStatsThisTurn(1, amount);

            if (ActiveEggEffect == ActiveEggEffect.SmallBeak)
                BossCard.AddTemporaryMod(new(timesHitThisTurn, 0) { singletonId = "SmallBeak" });

            // if dealt 5+ damage in a single attack, gain shielding and increase reactive difficulty
            if (amount > 4 && !attacker.HasStatusEffect<Enchanted>()) {
                CardModificationInfo skinMod = BossCard.TemporaryMods.Find(x => x.singletonId == "ReactiveSkin");
                bool alreadyReacted = skinMod != null;

                if (!alreadyReacted) {
                    skinMod = new() {
                        singletonId = "ReactiveSkin",
                        nonCopyable = true,
                        fromCardMerge = true
                    };
                }
                for (int i = 0; i < amount / 5; i++)
                    skinMod.AddAbilities(ThickSkin.ability);

                if (alreadyReacted)
                    BossCard.OnStatsChanged();
                else
                    BossCard.AddTemporaryMod(skinMod);

                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossReactiveSkin");
                yield return IncreaseReactiveDifficulty(amount / 5);
            }

            // don't switch phase if we're above the threshold
            if (BossCard.Health > BossHealthThreshold(BossOpponent.NumLives))
                yield break;

            if (finalPhase) {
                BossOpponent.NumLives--;
                yield return BossOpponent.LifeLostSequence();
            }
            else {
                changeToNextPhase = true;
            }

            AudioController.Instance.SetLoopVolume(0.1f, 1f);
        }

        public override bool RespondsToCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) => true;
        public override IEnumerator OnCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) {
            if (!opposingSlot.IsPlayerSlot && directDamageCache > 2) {
                yield return IncreaseReactiveDifficulty(directDamageCache % 3);
            }

            if (finalPhase && attacker == BossCard) {
                if (giantTargetSlots[1].Contains(opposingSlot))
                    BossCard.HealDamage(damage * 2);

                CleanUpGiantTarget(opposingSlot);
            }
            else if (base.RespondsToCardDealtDamageDirectly(attacker, opposingSlot, damage)) {
                yield return base.OnCardDealtDamageDirectly(attacker, opposingSlot, damage);
                if (!DialogueEventsData.EventIsPlayed("ApocalypseBossBoneGain") && currentExcessBones > 0) {
                    yield return new WaitForSeconds(0.5f);
                    yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossBoneGain", TextDisplayer.MessageAdvanceMode.Input);
                }
            }
        }

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => BossCard == null || otherCard == BossCard;
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard) {
            if (BossCard == null) {
                BossCard = otherCard;
                if (!finalPhase)
                    UpdateCounter();
            }

            // if on the player side for whatever reason, return to opponent side
            if (otherCard.Slot.IsPlayerSlot) {
                CardSlot newSlot;
                List<CardSlot> opponentSlots = BoardManager.Instance.OpponentSlotsCopy.FindAll(x => x.Card == null);

                yield return new WaitForSeconds(0.5f);
                BossCard.Anim.StrongNegationEffect();

                if (opponentSlots.Count == 0) {
                    newSlot = BossCard.OpposingSlot();
                    yield return newSlot.Card.DieTriggerless();
                }
                else {
                    newSlot = opponentSlots[SeededRandom.Range(0, opponentSlots.Count, base.GetRandomSeed() + TurnManager.Instance.TurnNumber)];
                }

                yield return new WaitForSeconds(0.45f);
                yield return BoardManager.Instance.AssignCardToSlot(BossCard, newSlot);
                yield return new WaitForSeconds(0.75f);
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossReturnEgg");
            }
        }

        public override bool RespondsToModifyDirectDamage(CardSlot target, int damage, PlayableCard attacker, int originalDamage) => true;
        public override int OnModifyDirectDamage(CardSlot target, int damage, PlayableCard attacker, int originalDamage) {
            if (finalPhase && attacker == BossCard) {
                return GiantCardDirectDamage(target, damage);
            }
            else if (base.RespondsToModifyDirectDamage(target, damage, attacker, originalDamage)) {
                return base.OnModifyDirectDamage(target, damage, attacker, originalDamage);
            }

            return damage;
        }

        public override int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            if (finalPhase && attacker == BossCard) {
                return GiantCardDirectDamage(target.Slot, damage);
            }
            return base.OnModifyDamageTaken(target, damage, attacker, originalDamage);
        }
        private int GiantCardDirectDamage(CardSlot targetSlot, int damage) {
            if (giantTargetSlots[0].Contains(targetSlot))
                return damage * 2;
            if (giantTargetSlots[1].Contains(targetSlot))
                return Mathf.Max(1, damage / 2);
            return damage;
        }
        #endregion

        /// <summary>
        /// Updates the appearance of the Apocalypse sigil based on the current phase and subphase.
        /// </summary>
        public void UpdateCounter() {
            if (turnsToNextPhase < 1 || DisabledEggEffects.Count == 3) {
                BossCard.RenderInfo.OverrideAbilityIcon(ApocalypseAbility.ability, AbilityManager.AllAbilities.AbilityByID(ApocalypseAbility.ability).Texture);
            }
            else {
                BossCard.RenderInfo.OverrideAbilityIcon(ApocalypseAbility.ability, TextureLoader.LoadTextureFromFile($"sigilApocalypse_{turnsToNextPhase}.png", LobotomyPlugin.ModAssembly));
            }

            BossCard.RenderCard();
        }
        private void ClearTempMods() {
            BossCard.RemoveTemporaryMod(BossCard.TemporaryMods.Find(x => x.singletonId == "SmallBeak"));
            BossCard.RemoveTemporaryMod(BossCard.TemporaryMods.Find(x => x.singletonId == "ReactiveSkin"));
        }
        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData) {
            ChangeActiveEggEffect();
            bossMouthPrefab = AssetManager.BossBundle.LoadAsset<GameObject>("ApocalypseMouth");

            EncounterData data = base.BuildCustomEncounter(nodeData);
            CardInfo startingEgg = CardLoader.GetCardByName(AllBossPhases[ActiveEggEffect][1]);

            if (ActiveEggEffect == ActiveEggEffect.BigEyes)
                UpdateAttackColours();

            else if (ActiveEggEffect == ActiveEggEffect.LongArms)
                startingEgg.AddTraits(AbnormalPlugin.ImmuneToAilments);

            data.Blueprint = ApocalypseBossUtils.CreateStartingBlueprint();
            data.startConditions = new()
            {
                new() {
                    cardsInOpponentSlots = new CardInfo[1] { startingEgg }
                }
            };
            data.opponentTurnPlan = DiskCardGame.EncounterBuilder.BuildOpponentTurnPlan(data.Blueprint, 20, false);
            return data;
        }

        public bool RespondsToItemCanBeUsed(string itemname, bool currentValue) => itemname == "Hourglass" && !DisabledEggEffects.Contains(ActiveEggEffect.LongArms);
        public bool CollectItemCanBeUsed(string itemname, bool currentValue) => false;
        public bool RespondsToItemPreventedFromUse(string itemName) => RespondsToItemCanBeUsed(itemName, false);

        public IEnumerator OnItemPreventedFromUse(string itemName) {
            Singleton<CameraEffects>.Instance.Shake(0.25f, 0.125f);
            yield return DialogueHelper.ShowUntilInput("The Long Bird's arms conceal time.");
        }
    }

    public enum ActiveEggEffect {
        BigEyes = 0,
        SmallBeak = 1,
        LongArms = 2,
        None = 3
    }
}
