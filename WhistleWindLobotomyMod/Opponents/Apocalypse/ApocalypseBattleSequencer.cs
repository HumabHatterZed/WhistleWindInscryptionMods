using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents.Apocalypse
{
    public class ApocalypseBattleSequencer : LobotomyBossBattleSequencer, IOnCardDealtDamageDirectly, IModifyDirectDamage
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "ApocalypseBattleSequencer", typeof(ApocalypseBattleSequencer)).Id;
        public override Opponent.Type BossType => CustomOpponentUtils.ApocalypseBossID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.ApocalypseBossDefeated;
        private ApocalypseBossOpponent BossOpponent => TurnManager.Instance.Opponent as ApocalypseBossOpponent;

        private readonly Dictionary<ActiveEggEffect, string[]> AllBossPhases = new()
        {
            { ActiveEggEffect.BigEyes, new string[2] {
                "wstl_eyeballChick",
                "wstl_apocalypseEgg_big"} },
            { ActiveEggEffect.SmallBeak, new string[2] {
                "wstl_forestKeeper",
                "wstl_apocalypseEgg_small" } },
            { ActiveEggEffect.LongArms, new string[2] {
                "wstl_runawayBird",
                "wstl_apocalypseEgg_long" } }
        };

        public ActiveEggEffect ActiveEggEffect = ActiveEggEffect.None;
        public readonly List<ActiveEggEffect> DisabledEggEffects = new();

        public override int BossHealthThreshold(int remainingLives) => remainingLives switch
        {
            4 => 80,
            3 => 60,
            2 => 40,
            _ => 1
        };

        public int turnsToNextPhase = 3;
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

        #region Special Attacks
        private IEnumerator BigBirdEnchantCards()
        {
            int randomSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;
            List<CardSlot> possibleTargetSlots = (PhaseDifficulty > 1 || ReactiveDifficulty > 8) ? BoardManager.Instance.PlayerSlotsCopy : BoardManager.Instance.AllSlotsCopy;
            possibleTargetSlots.RemoveAll(x => x.Card == null || x.Card == BossCard);

            int maxCount = possibleTargetSlots.Count;
            while (possibleTargetSlots.Count > 0)
            {
                if (specialTargetSlots.Count > 2)
                    break;

                CardSlot target = possibleTargetSlots[SeededRandom.Range(0, possibleTargetSlots.Count, randomSeed++)];
                specialTargetSlots.Add(target);
                possibleTargetSlots.Remove(target);
            }

            yield return HelperMethods.ChangeCurrentView(View.Default, 0f);
            BossOpponent.MasterAnimator.SetBool("Flare", true);
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossEyePreAttack", 0f, repeatLines: !seenEyeAttack);

            foreach (CardSlot slot in specialTargetSlots)
            {
                yield return new WaitForSeconds(0.05f);
                CreateTargetIcon(slot, GameColors.Instance.yellow);
            }
            yield return new WaitForSeconds(0.5f);

            if (specialTargetSlots.Count == 0)
            {
                BossCard.Anim.StrongNegationEffect();
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossEyeFailAttack", 0f, repeatLines: !seenEyeAttack);
            }
            else
            {
                List<Transform> leftSources = new(BossOpponent.LeftEyes);
                List<Transform> rightSources = new(BossOpponent.RightEyes);
                int enchantCount = ReactiveDifficulty > 13 ? 2 : 1;

                AudioController.Instance.PlaySound2D("bird_laser_fire", MixerGroup.TableObjectsSFX);
                for (int i = 0; i < specialTargetSlots.Count; i++)
                {
                    Transform source;
                    for (int j = 0; j < enchantCount; j++)
                    {
                        if (specialTargetSlots[i].Index % 2 == 0)
                        {
                            source = leftSources[UnityEngine.Random.Range(0, leftSources.Count)];
                            leftSources.Remove(source);
                        }
                        else
                        {
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
        private IEnumerator SmallBirdTargetLanes()
        {
            List<CardSlot> lanes = BoardManager.Instance.OpponentSlotsCopy;
            int rand = base.GetRandomSeed();
            int rowsToTarget = 1 + PhaseDifficulty;
            for (int i = 0; i < rowsToTarget; i++)
            {
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
            foreach (CardSlot slot in specialTargetSlots.Where(x => x.IsOpponentSlot()))
            {
                yield return new WaitForSeconds(0.1f);
                GameObject obj = Instantiate(bossMouthPrefab);
                obj.transform.localPosition = slot.transform.position + new Vector3(0f, 1.2f, 0.4f);
                mouthIcons.Add(slot, obj);
            }
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossMouthPreAttack", repeatLines: !seenMouthAttack);
        }
        private IEnumerator SmallBirdAttackLanes()
        {
            bool killedCard = false;
            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);
            for (int i = 0; i < specialTargetSlots.Count; i++)
            {
                GameObject mouthAnim = null;
                PlayableCard target = specialTargetSlots[i].Card;

                // mouth anim pivot is on opponent slot
                if (specialTargetSlots[i].IsOpponentSlot())
                {
                    mouthAnim = mouthIcons[specialTargetSlots[i]];
                    mouthAnim.GetComponent<Animator>().Play("mouthShut");
                    yield return new WaitForSeconds(0.05f);
                    AudioController.Instance.PlaySound2D("bird_mouth", MixerGroup.TableObjectsSFX);
                    yield return new WaitForSeconds(0.05f);
                }

                if (target != null && target != BossCard)
                {
                    killedCard = true;
                    yield return target.FlipFaceUp(target.FaceDown);
                    yield return target.Die(false, BossCard);
                }

                if (mouthAnim != null)
                {
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
        private IEnumerator ArmAttackSequence()
        {
            List<PlayableCard> cardsOnBoard = BoardManager.Instance.CardsOnBoard;
            cardsOnBoard.RemoveAll(x => x == null || x.GetStatusEffectPotency<Sin>() < 3);
            cardsOnBoard.Remove(BossCard);
            if (cardsOnBoard.Count == 0)
                yield break;

            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);
            foreach (PlayableCard c in cardsOnBoard)
            {
                c.Anim.SetMarkedForSacrifice(marked: true);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossArmsPreAttack", 0f, repeatLines: !seenArmsAttack);

            foreach (PlayableCard c in cardsOnBoard)
            {
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
        public void CleanUpGiantTarget(CardSlot slot)
        {
            GameObject obj = targetIcons.Find(x => x.transform.parent == slot.transform);
            if (obj == null)
                return;

            giantTargetSlots[0].Remove(slot);
            giantTargetSlots[1].Remove(slot);
            targetIcons.Remove(obj);

            CleanUpTargetIcon(obj);
        }
        public IEnumerator GiantPhaseLogic(bool firstStrike)
        {
            CleanupTargetIcons();
            specialTargetSlots.Clear();
            giantTargetSlots[0].Clear();
            giantTargetSlots[1].Clear();

            List<CardSlot> playerSlots = BoardManager.Instance.PlayerSlotsCopy;
            int randomSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;
            if (firstStrike)
                randomSeed += 5;

            int maxRedTargets = 3, maxWhiteTargets = 1;
            float redChance = 0.15f, whiteChance = 0.15f, directChance = 0.2f;

            // if the opponent is winning, soften up on the attacks somewhat
            if (LifeManager.Instance.Balance < 0)
            {
                redChance = 0.08f;
                whiteChance = 0.06f;
                directChance = 0.1f;
            }

            if (ReactiveDifficulty > 3)
            {
                float baseChance = (ReactiveDifficulty - 3) / 100f;
                redChance += baseChance * 1.2f;
                whiteChance += baseChance;
                directChance += baseChance * 1.4f;
            }

            if (playerSlots.Count(x => x.Card != null) > 1)
                redChance += 0.1f;

            if (BossCard.Health >= BossHealthThreshold(2))
                whiteChance /= 2f;

            // if the boss is at half health
            else if (BossCard.Health <= 15)
            {
                redChance += 0.02f;
                whiteChance += 0.05f;
                directChance += 0.04f;
            }

            // if no opposing cards, target one random empty space
            if (playerSlots.Count(x => x.Card != null) == 0)
            {
                CardSlot slotToTarget = playerSlots[SeededRandom.Range(0, playerSlots.Count, randomSeed++)];
                directChance = 1f;
                playerSlots.Clear();
                playerSlots.Add(slotToTarget);
            }

            bool directlyAttacking = false;
            foreach (CardSlot slot in playerSlots)
            {
                if (slot.Card == null)
                {
                    // if already directly attacking this turn, or random chance
                    if (directlyAttacking || SeededRandom.Value(randomSeed *= 10) > directChance)
                        continue;

                    directlyAttacking = true;
                }

                Color targetColour = GameColors.Instance.yellow;
                float value = SeededRandom.Value(randomSeed++);

                if (maxRedTargets > 0 && SeededRandom.Value(randomSeed + 5) <= redChance)
                {
                    targetColour = GameColors.Instance.glowRed;
                    giantTargetSlots[0].Add(slot);
                    maxRedTargets--;
                }
                else if (maxWhiteTargets > 0 && SeededRandom.Value(randomSeed++) <= whiteChance)
                {
                    targetColour = GameColors.Instance.brightNearWhite;
                    giantTargetSlots[1].Add(slot);
                    maxWhiteTargets--;
                }

                yield return new WaitForSeconds(0.05f);
                CreateTargetIcon(slot, targetColour);
                specialTargetSlots.Add(slot);
            }
        }
        #endregion

        public override IEnumerator PostDrawOpeningHand()
        {
            yield return base.PostDrawOpeningHand();
            if (TurnNumber == 0)
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossRecall", TextDisplayer.MessageAdvanceMode.Input);
        }

        public override IEnumerator OpponentUpkeep()
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);

            if (changeToNextPhase)
            {
                BossOpponent.NumLives--;
                DisabledEggEffects.Add(ActiveEggEffect);
                yield return BossOpponent.LifeLostSequence();
                if (BossOpponent.NumLives == 1)
                {
                    ActiveEggEffect = ActiveEggEffect.None;
                    finalPhase = true;
                    damageTakenThisTurn = 0;
                }
                yield return BossOpponent.PostResetScalesSequence();
                changeToNextPhase = false;
            }

            if (finalPhase)
            {
                if (damageTakenThisTurn > 0)
                {
                    int scaleDamage = Mathf.Min(LifeManager.Instance.DamageUntilPlayerWin - 1, damageTakenThisTurn / 4);
                    int boneDamage = damageTakenThisTurn - scaleDamage;
                    if (scaleDamage > 0)
                        yield return LifeManager.Instance.ShowDamageSequence(scaleDamage, scaleDamage, false);

                    if (boneDamage > 0)
                    {
                        ViewManager.Instance.SwitchToView(View.BoneTokens);
                        yield return new WaitForSeconds(0.2f);
                        yield return ResourcesManager.Instance.AddBones(Mathf.Min(2, boneDamage));
                    }
                }
                yield break;
            }

            if (justSwitchedEffect)
                justSwitchedEffect = false;
            else
                turnsToNextPhase--;

            if (ActiveEggEffect == ActiveEggEffect.LongArms)
                yield return ArmAttackSequence();
            else if (ActiveEggEffect == ActiveEggEffect.SmallBeak)
            {
                if (turnsToNextPhase == 2)
                    yield return SmallBirdTargetLanes();
                else if (turnsToNextPhase == 1)
                    yield return SmallBirdAttackLanes();
            }

            UpdateCounter();
            if (DisabledEggEffects.Count < 2) // only switch phase if there's more than 1 remaining egg effect
            {
                if (turnsToNextPhase == 0 && !changeToNextPhase)
                    yield return SwitchToNextEggEffect(false);
            }
            else
            {
                if (turnsToNextPhase == 0)
                    turnsToNextPhase = 3;
                UpdateCounter();
            }

            CreateNextTurnPlan();
        }
        public override IEnumerator OpponentCombatEnd()
        {
            // if the player is dead
            if (LifeManager.Instance.Balance <= -5)
                yield break;

            ClearTempMods();
            bool addedReactive = false;
            if (timesHitThisTurn > 2) // if the boss has been hit 3+ times in a single turn
            {
                addedReactive = true;
                reactiveDifficulty += timesHitThisTurn - 2;
            }

            if (damageTakenThisTurn > 5) // if the boss took 6+ damage in a single turn
            {
                addedReactive = true;
                reactiveDifficulty += damageTakenThisTurn / 6;
            }

            if (addedReactive)
                yield return OnReactiveDifficultyIncreased(0);

            if (finalPhase)
            {
                yield return GiantPhaseLogic(false);
            }
            else
            {
                yield return MoveOpponentCards();
                switch (ActiveEggEffect)
                {
                    case ActiveEggEffect.BigEyes:
                        if (turnsToNextPhase == 2)
                            yield return BigBirdEnchantCards();
                        break;

                    case ActiveEggEffect.LongArms:
                        List<PlayableCard> cardsOnBoard = BoardManager.Instance.CardsOnBoard;
                        cardsOnBoard.Remove(BossCard);
                        if (cardsOnBoard.Count == 0)
                            break;

                        AudioController.Instance.PlaySound2D("bird_down", MixerGroup.TableObjectsSFX);
                        foreach (PlayableCard c in cardsOnBoard)
                        {
                            if (!c.FaceDown)
                                c.Anim.StrongNegationEffect();

                            yield return c.AddStatusEffectToFaceDown<Sin>(1);
                            yield return new WaitForSeconds(0.1f);
                        }
                        break;
                }
            }

            damageTakenThisTurn = timesHitThisTurn = 0;
            AudioController.Instance.SetLoopVolume(0.3f, 1f);
        }

        #region Turn Plan
        private int GetStartingCardCount()
        {
            // queue 2 cards every 4 turns
            if (TurnManager.Instance.TurnNumber % 4 == 0)
                return 2;

            // queue 0 cards every 2 turns
            if (TurnManager.Instance.TurnNumber % 2 == 0)
                return 0;

            // queue 1 card every odd turn
            return 1;
        }
        private void CreateNextTurnPlan()
        {
            List<CardInfo> nextTurn = new();
            bool opponentWinning = LifeManager.Instance.Balance < 0;

            // starting number of cards
            int cardNum = GetStartingCardCount();
            if (opponentWinning)
                cardNum--;
            else
                cardNum++;

            int randomSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;

            // threshold for whether to give queued card a mod
            // if the difficulty modifier is 6 or higher, guaranteed to modify stats, other chance is dependent on difficulty and cards being cued
            float gateValue;
            if (ReactiveDifficulty > 11)
            {
                gateValue = opponentWinning ? 0.65f + ((ReactiveDifficulty - 12) * 0.02f) : 1f; // guaranteed to give a mod at 12+ reactive if scale is losing
                cardNum++;
            }
            else
                gateValue = (4 - cardNum - (opponentWinning ? 1 : 0)) / Mathf.Max(1f, 7f - RunState.Run.DifficultyModifier);

            // if the queue is full, reduce the cardNum
            if (BossOpponent.Queue.Count == 4)
            {
                // if the latest added turn was also full, add an empty turn plan
                if (BossOpponent.TurnPlan.Last().Count == 4 && ReactiveDifficulty < 11)
                    cardNum = 0;
                else
                    cardNum -= ReactiveDifficulty > 7 ? 1 : (ReactiveDifficulty > 4 ? 2 : 2);
            }

            for (int i = 0; i < cardNum; i++)
            {
                CardInfo clone = CardLoader.GetCardByName(ActiveEggMinion);
                float randomValue = SeededRandom.Value(randomSeed++);
                if (randomValue <= gateValue) // if give stat boost
                {
                    // either give +1/-1 or 0/+1
                    int attack = 0;
                    int health = randomValue <= (gateValue / 2f) ? 1 : 0;

                    if (ReactiveDifficulty > 3)
                    {
                        health++;
                        if (ReactiveDifficulty > 7)
                        {
                            attack++;
                            health++;
                        }
                    }
                    if (SeededRandom.Bool(randomSeed++))
                    {
                        attack++;
                        if (ReactiveDifficulty <= 11)
                            health--;
                    }
                    else
                    {
                        if (ReactiveDifficulty > 11) // at 12+, give extra attack instead of health
                            attack++;
                        else
                            health++;
                    }

                    clone.baseAttack += attack;
                    clone.baseHealth += health;
                    if (clone.baseHealth <= 0)
                        clone.baseHealth = 1;
                }
                nextTurn.Add(clone);
            }

            BossOpponent.TurnPlan.Add(nextTurn);
        }
        #endregion

        private void ClearTempMods()
        {
            BossCard.RemoveTemporaryMod(BossCard.TemporaryMods.Find(x => x.singletonId == "SmallBeak"));
            BossCard.RemoveTemporaryMod(BossCard.TemporaryMods.Find(x => x.singletonId == "ReactiveSkin"));
        }
        public void UpdateCounter()
        {
            string newTex = (turnsToNextPhase <= 0 || DisabledEggEffects.Count == 3) ? "sigilApocalypse.png" : ("sigilApocalypse_" + turnsToNextPhase + ".png");
            BossCard.RenderInfo.OverrideAbilityIcon(ApocalypseAbility.ability, TextureLoader.LoadTextureFromFile(newTex));
            BossCard.RenderCard();
        }
        public IEnumerator SwitchToNextEggEffect(bool lostLife)
        {
            // clean up the previous phase and reset the counter
            turnsToNextPhase = 3;
            justSwitchedEffect = true;

            specialTargetSlots.Clear();
            CleanupTargetIcons();
            foreach (GameObject obj in mouthIcons.Values)
                CleanUpTargetIcon(obj);

            mouthIcons.Clear();
            yield return BossOpponent.ClearQueue();
            yield return new WaitForSeconds(0.4f);

            Action transformCallback = () =>
            {
                UpdateCounter(); // update the turn counter and clear all negative statuses
                List<CardModificationInfo> negativeAbilities = BossCard.TemporaryMods.FindAll(x => x.IsStatusMod(false));
                if (negativeAbilities.Count > 0)
                    BossCard.RemoveTemporaryMods(negativeAbilities.ToArray());

                damageTakenThisTurn = 0;
            };

            if (ActiveEggEffect == ActiveEggEffect.BigEyes) // if we're changing from Big Eyes, update the attack colours
                transformCallback += UpdateAttackColours;

            ChangeActiveEggEffect();

            // transform into the next egg card
            CardInfo bossEggInfo = CardLoader.GetCardByName(AllBossPhases[ActiveEggEffect][1]);
            if (ReactiveDifficulty > 4)
                bossEggInfo.Mods.Add(new(1, 0) { singletonId = "ReactiveStrength", nonCopyable = true });
            
            switch (ActiveEggEffect)
            {
                case ActiveEggEffect.BigEyes: // if we're changing to Big Eyes, update the attack colours
                    transformCallback += UpdateAttackColours;
                    break;
                case ActiveEggEffect.LongArms: // if we're changing to Long Arms, make ourselves immune to ailments
                    bossEggInfo.AddTraits(AbnormalPlugin.ImmuneToAilments);
                    break;
            }
            ClearTempMods();
            yield return BossCard.TransformIntoCard(bossEggInfo, transformCallback);
            yield return new WaitForSeconds(0.4f);
            yield return BossCard.RemoveStatusEffects();

            // if we're switching due to losing a life, reset the turn plan and create the next turn plan
            if (lostLife)
            {
                BossOpponent.NumTurnsTaken = 0; // reset NumTurnsTaken so the turn plan doesn't break
                CreateNextTurnPlan();

                if (BossOpponent.NumLives == 1)
                    yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossFinalPhase", 0f);
            }
        }
        private void ChangeActiveEggEffect()
        {
            if (finalPhase)
                return;

            List<ActiveEggEffect> possiblePhases = AllBossPhases.Keys.Where(x => !DisabledEggEffects.Contains(x)).ToList();
            possiblePhases.Remove(ActiveEggEffect);
            if (possiblePhases.Count == 0)
                return;

            ActiveEggEffect = possiblePhases[SeededRandom.Range(0, possiblePhases.Count, base.GetRandomSeed() + TurnManager.Instance.TurnNumber)];
            ActiveEggMinion = AllBossPhases[ActiveEggEffect][0];
        }

        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            ChangeActiveEggEffect();
            bossMouthPrefab = CustomOpponentUtils.bossBundle.LoadAsset<GameObject>("ApocalypseMouth");

            EncounterData data = base.BuildCustomEncounter(nodeData);
            CardInfo startingEgg = CardLoader.GetCardByName(AllBossPhases[ActiveEggEffect][1]);

            if (ActiveEggEffect == ActiveEggEffect.BigEyes)
                UpdateAttackColours();

            else if (ActiveEggEffect == ActiveEggEffect.LongArms)
                startingEgg.AddTraits(AbnormalPlugin.ImmuneToAilments);

            data.Blueprint = ApocalypseBossUtils.CreateStartingBlueprint();
            data.startConditions = new()
            {
                new()
                {
                    cardsInOpponentSlots = new CardInfo[1] { startingEgg }
                }
            };
            data.opponentTurnPlan = DiskCardGame.EncounterBuilder.BuildOpponentTurnPlan(data.Blueprint, 20, false);
            return data;
        }

        public override IEnumerator OnReactiveDifficultyIncreased(int amount)
        {
            LobotomyPlugin.Log.LogDebug($"[ApocalypseBoss] {reactiveDifficulty} (+{amount})");
            Singleton<CameraEffects>.Instance.Shake(0.5f, 0.25f);
            BossCard.Anim.StrongNegationEffect();
            
            if (ReactiveDifficulty > 3 && BossCard.Info.Mods.Exists(x => x.singletonId == "ReactiveStrength"))
            {
                BossCard.Info.Mods.Add(new(1, 0) { singletonId = "ReactiveStrength", nonCopyable = true });
                BossCard.OnStatsChanged();
            }
            yield return new WaitForSeconds(0.15f);

            if (ReactiveDifficulty >= 13)
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossReactive3");
            else if (ReactiveDifficulty >= 8)
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossReactive2");
            else
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossReactive3");
        }

        #region Triggers
        public override int OnModifyDamage(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            if (finalPhase && attacker == BossCard)
            {
                if (giantTargetSlots[0].Contains(target.Slot))
                    return damage * 2;
                if (giantTargetSlots[1].Contains(target.Slot))
                    return Mathf.Max(1, damage / 2);
            }

            return base.OnModifyDamage(target, damage, attacker, originalDamage);
        }

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => BossCard == null || otherCard == BossCard;
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            if (BossCard == null)
            {
                BossCard = otherCard;
                if (!finalPhase)
                    UpdateCounter();
            }

            // if on the player side for whatever reason, return to opponent side
            if (otherCard.Slot.IsPlayerSlot)
            {
                CardSlot newSlot;
                List<CardSlot> opponentSlots = BoardManager.Instance.OpponentSlotsCopy.FindAll(x => x.Card == null);

                yield return new WaitForSeconds(0.5f);
                BossCard.Anim.StrongNegationEffect();

                if (opponentSlots.Count == 0)
                {
                    newSlot = BossCard.OpposingSlot();
                    yield return newSlot.Card.DieTriggerless();
                }
                else
                {
                    newSlot = opponentSlots[SeededRandom.Range(0, opponentSlots.Count, base.GetRandomSeed() + TurnManager.Instance.TurnNumber)];
                }

                yield return new WaitForSeconds(0.45f);
                yield return BoardManager.Instance.AssignCardToSlot(BossCard, newSlot);
                yield return new WaitForSeconds(0.75f);
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossReturnEgg");
            }
        }

        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target) => true;
        public override IEnumerator OnOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            // if the boss dealt damage
            if (attacker == BossCard)
            {
                if (finalPhase)
                    yield break;

                // if white target, heal the boss
                if (giantTargetSlots[1].Contains(target.Slot))
                    BossCard.HealDamage(BossCard.Attack);

                CleanUpGiantTarget(target.Slot);
                yield break;
            }

            // if the boss took damage
            if (target == BossCard)
            {
                IncrementStatsThisTurn(1, amount);
                
                if (ActiveEggEffect == ActiveEggEffect.SmallBeak)
                    BossCard.AddTemporaryMod(new(timesHitThisTurn, 0) { singletonId = "SmallBeak" });

                // if dealt 5+ damage in a single attack, gain shielding and increase reactive difficulty
                if (amount > 4 && !attacker.HasStatusEffect<Enchanted>())
                {
                    CardModificationInfo skinMod = BossCard.TemporaryMods.Find(x => x.singletonId == "ReactiveSkin");
                    bool alreadyReacted = skinMod != null;
                    
                    if (!alreadyReacted)
                    {
                        skinMod = new()
                        {
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

                if (finalPhase)
                {
                    BossOpponent.NumLives--;
                    yield return BossOpponent.LifeLostSequence();
                }
                else
                    changeToNextPhase = true;

                AudioController.Instance.SetLoopVolume(0.1f, 1f);
            }
        }
        
        public bool RespondsToCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) => true;
        public IEnumerator OnCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage)
        {
            if (finalPhase && attacker == BossCard)
            {
                if (giantTargetSlots[1].Contains(opposingSlot))
                    BossCard.HealDamage(damage * 2);

                CleanUpGiantTarget(opposingSlot);
            }

            if (attacker.OpponentCard && damage < -4)
            {
                yield return OnReactiveDifficultyIncreased(Mathf.Abs(damage) - 4);
            }

            else if (!attacker.OpponentCard && damage > 4)
            {
                yield return OnReactiveDifficultyIncreased(damage - 4);
            }
            yield break;
        }

        public bool RespondsToModifyDirectDamage(CardSlot target, int damage, PlayableCard attacker, int originalDamage) => finalPhase && attacker == BossCard;
        public int OnModifyDirectDamage(CardSlot target, int damage, PlayableCard attacker, int originalDamage)
        {
            if (giantTargetSlots[0].Contains(target))
                return damage * 2;
            if (giantTargetSlots[1].Contains(target))
                return damage / 2;

            return damage;
        }
        public int TriggerPriority(CardSlot target, int damage, PlayableCard attacker) => int.MaxValue; // ModifyDirectDamage
        #endregion

        #region Big Eyes
        private void UpdateAttackColours()
        {
            foreach (PlayableCard c in BoardManager.Instance.CardsOnBoard.Concat(PlayerHand.Instance.CardsInHand))
            {
                c.OnStatsChanged();
            }
        }
        private void FireLaser(GameObject source, CardSlot targetSlot, bool attackPlayer)
        {
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
        private IEnumerator TweenLineWidth(LineRenderer line, bool attackPlayer, float time = 0.25f)
        {
            if (line == null)
            {
                if (attackPlayer)
                    yield break;

                Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.2f);
                yield break;
            }

            float ela2 = line.widthMultiplier = 0f;
            while (ela2 < time)
            {
                if (line == null)
                {
                    if (attackPlayer)
                        yield break;

                    Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.2f);
                    yield break;
                }
                ela2 += UnityEngine.Time.deltaTime;
                line.widthMultiplier = Mathf.Lerp(0f, 1f, ela2 / time);
                yield return new WaitForEndOfFrame();
            }

            if (line == null)
            {
                yield return new WaitForSeconds(0.1f);
                yield break;
            }
            line.widthMultiplier = 1f;
            ela2 = 0f;
            while (ela2 < time)
            {
                if (line == null)
                {
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
    }

    public enum ActiveEggEffect
    {
        BigEyes = 0,
        SmallBeak = 1,
        LongArms = 2,
        None = 3
    }
}
