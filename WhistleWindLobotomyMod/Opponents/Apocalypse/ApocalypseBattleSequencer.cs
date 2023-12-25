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
    public class ApocalypseBattleSequencer : Part1BossBattleSequencer, IModifyDamageTaken, IOnPreScalesChangedRef, IOnCardDealtDamageDirectly, IModifyDirectDamage
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "ApocalypseBattleSequencer", typeof(ApocalypseBattleSequencer)).Id;
        public override Opponent.Type BossType => ApocalypseBossOpponent.ID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.ApocalypseBossDefeated;
        private ApocalypseBossOpponent Opponent => TurnManager.Instance.Opponent as ApocalypseBossOpponent;

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

        // Giant card Health + product of other phases' Health
        // honestly should make these static values, but while I'm testing this'll make it easier to modify the Health
        //private int StartingHealth => 40 + 3 * HealthPerPhase;
        //private const int HealthPerPhase = 20;

        public int BossHealthThreshold(int remainingLives) => remainingLives switch
        {
            4 => 80,//StartingHealth - HealthPerPhase,
            3 => 60,//StartingHealth - HealthPerPhase * 2,
            2 => 40,//StartingHealth - HealthPerPhase * 3,
            _ => 1
        };

        public string ActiveEggMinion = null;
        public PlayableCard BossCard = null;

        public int turnsToNextPhase = 3; // number of turns until the active egg effect automatically changes
        public int damageTakenThisTurn = 0;
        public int timesHitThisTurn = 0;

        // makes things more difficult based on how much damage is dealt to the boss
        private readonly int[] ReactiveGates = new int[] { 3, 8, 15 };
        private int reactiveDifficulty = 0;
        private int ReactiveDifficulty => RunState.Run.DifficultyModifier + reactiveDifficulty;
        private int PhaseDifficulty => 4 - Opponent.NumLives;

        private bool finalPhase = false;
        private bool changeToNextPhase = false;
        private bool justSwitchedEffect = false;

        // controls dialogue
        private bool seenMouthAttack = false;
        private bool seenEyeAttack = false;
        private bool seenArmsAttack = false;

        // list of cardslots being targeted by special attacks during the final phase
        public readonly List<CardSlot>[] giantTargetSlots = new List<CardSlot>[2]
        {
            new(), // red targets
            new() // white targets
        };
        public readonly List<CardSlot> specialTargetSlots = new();
        private readonly List<GameObject> targetIcons = new();

        private readonly Dictionary<CardSlot, GameObject> mouthIcons = new();
        private GameObject targetIconPrefab;
        private GameObject bossMouthPrefab;

        #region Special Attacks
        private IEnumerator BigBirdEnchantCards()
        {
            int randomSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;
            List<CardSlot> possibleTargetSlots;
            if (PhaseDifficulty > 1 || ReactiveDifficulty > 8) // ReactiveDifficulty
                possibleTargetSlots = BoardManager.Instance.PlayerSlotsCopy;
            else
                possibleTargetSlots = BoardManager.Instance.AllSlotsCopy;

            possibleTargetSlots.RemoveAll(x => x.Card == null || x.Card == BossCard);
            int maxCount = possibleTargetSlots.Count;
            while (possibleTargetSlots.Count > 0)
            {
                if (specialTargetSlots.Count == 3)
                    break;

                CardSlot target = possibleTargetSlots[SeededRandom.Range(0, possibleTargetSlots.Count, randomSeed++)];
                possibleTargetSlots.Remove(target);
                specialTargetSlots.Add(target);
            }

            yield return HelperMethods.ChangeCurrentView(View.Default, 0f);
            Opponent.MasterAnimator.SetBool("Flare", true);
            yield return new WaitForSeconds(0.5f);
            if (!seenEyeAttack)
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossEyePreAttack", TextDisplayer.MessageAdvanceMode.Input);

            foreach (CardSlot slot in specialTargetSlots)
            {
                yield return new WaitForSeconds(0.05f);
                CreateTargetIcon(slot, GameColors.Instance.yellow);
            }
            yield return new WaitForSeconds(0.5f);

            if (specialTargetSlots.Count == 0)
            {
                BossCard.Anim.StrongNegationEffect();
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossEyeFailAttack", 0f);
            }
            else
            {
                List<Transform> leftSources = new(Opponent.LeftEyes);
                List<Transform> rightSources = new(Opponent.RightEyes);
                int amountToEnchant = ReactiveDifficulty > 13 ? 2 : 1;

                AudioController.Instance.PlaySound2D("bird_laser_fire", MixerGroup.TableObjectsSFX);
                for (int i = 0; i < specialTargetSlots.Count; i++)
                {
                    Transform source;
                    for (int j = 0; j < amountToEnchant; j++)
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

                    // increase the turn gained by 1 so it disappears at the correct time
                    yield return specialTargetSlots[i].Card.AddStatusEffectFlipCard<Enchanted>(amountToEnchant, modifyTurnGained: (int turn) => turn + 1);
                    CleanUpTargetIcon(targetIcons[i]);
                }

                yield return HelperMethods.ChangeCurrentView(View.Board, 0.2f, 0f);
                yield return new WaitForSeconds(0.3f);

                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossEyePostAttack", 0f);
                seenEyeAttack = true;
            }

            CleanupTargetIcons();
            specialTargetSlots.Clear();
            yield return Opponent.ResetToIdle();
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

            Opponent.MasterAnimator.SetBool("Mouth", true);
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

            if (!seenMouthAttack)
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossMouthPreAttack", TextDisplayer.MessageAdvanceMode.Input);
        }
        private IEnumerator SmallBirdAttackLanes()
        {
            bool killedCard = false;

            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);
            for (int i = 0; i < specialTargetSlots.Count; i++)
            {
                GameObject mouthAnim = null;
                PlayableCard target = specialTargetSlots[i].Card;

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

            if (!killedCard)
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossMouthFailAttack", 0f);
            else
            {
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossMouthPostAttack", 0f);
            }

            yield return Opponent.ResetToIdle();
            seenMouthAttack = true;
        }
        private IEnumerator ArmAttackSequence()
        {
            List<CardSlot> slots = BoardManager.Instance.AllSlotsCopy;
            slots.RemoveAll(x => x.Card == null || x.Card.GetStatusEffectStacks<Sin>() < 3 || x.Card == BossCard);
            if (slots.Count == 0)
                yield break;

            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);
            foreach (CardSlot slot in slots)
            {
                slot.Card.Anim.SetMarkedForSacrifice(marked: true);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
            if (!seenArmsAttack)
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossArmsPreAttack", TextDisplayer.MessageAdvanceMode.Input);

            foreach (CardSlot slot in slots)
            {
                slot.Card.Anim.PlaySacrificeSound();
                slot.Card.Anim.DeactivateSacrificeHoverMarker();
                yield return slot.Card.Die(false, BossCard);
            }

            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossArmsPostAttack", 0f);
            seenArmsAttack = true;
        }
        #endregion

        #region Giant Logic
        public void CleanUpGiantTarget(CardSlot slot)
        {
            GameObject obj = targetIcons.Find(x => x.transform.parent == slot.transform);

            if (obj == null)
                return;

            if (giantTargetSlots[0].Contains(slot))
                giantTargetSlots[0].Remove(slot);

            if (giantTargetSlots[1].Contains(slot))
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

            // at difficulty 8+
            if (ReactiveDifficulty > 3)
            {
                float baseChance = (ReactiveDifficulty - 3) / 100f;
                redChance += baseChance * 1.2f;
                whiteChance += baseChance;
                directChance += baseChance * 1.4f;
            }

            // if 3+ cards on the opposing side
            if (playerSlots.Count(x => x.Card != null) >= 2)
            {
                redChance += 0.1f;
            }

            // if the boss has taken no damage
            if (BossCard.Health >= 40)
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

        public override IEnumerator PreDeckSetup()
        {
            ViewManager.Instance.SwitchToView(View.Hand);
            PlayerHand.Instance.InspectingLocked = false;
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_RETURN_CARD"));
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_RETURN_CARD_ALL"));
            PlayerHand.Instance.InspectingLocked = true;
            yield return new WaitForSeconds(0.4f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossRecall", TextDisplayer.MessageAdvanceMode.Input);
        }
        public override IEnumerator OpponentUpkeep()
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);

            if (changeToNextPhase)
            {
                Opponent.NumLives--;
                DisabledEggEffects.Add(ActiveEggEffect);
                yield return Opponent.LifeLostSequence();
                if (Opponent.NumLives == 1)
                {
                    ActiveEggEffect = ActiveEggEffect.None;
                    finalPhase = true;
                    damageTakenThisTurn = 0;
                }
                yield return Opponent.PostResetScalesSequence();
                changeToNextPhase = false;
            }

            if (finalPhase)
            {
                if (damageTakenThisTurn > 0)
                {
                    int scaleDamage = Mathf.Min(LifeManager.Instance.DamageUntilPlayerWin - 1, damageTakenThisTurn / 3);
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

            switch (ActiveEggEffect)
            {
                case ActiveEggEffect.SmallBeak:
                    if (turnsToNextPhase == 2)
                        yield return SmallBirdTargetLanes();
                    else if (turnsToNextPhase == 1)
                        yield return SmallBirdAttackLanes();
                    break;

                case ActiveEggEffect.LongArms:
                    yield return ArmAttackSequence();
                    break;
            }

            // only switch phase if there's more than 1 remaining egg effect
            UpdateCounter();
            if (DisabledEggEffects.Count < 2)
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
            if (timesHitThisTurn > 4) // if timesHit is over 4 then there's a strike sigil on the board
            {
                addedReactive = true;
                reactiveDifficulty += timesHitThisTurn - 4;
            }

            if (damageTakenThisTurn > 5) // if damageTaken is above 4 then the boss is dying too quickly
            {
                addedReactive = true;
                reactiveDifficulty += damageTakenThisTurn / 6;
            }

            if (addedReactive)
                yield return OnReactiveIncreased();

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
                        List<CardSlot> allSlots = BoardManager.Instance.AllSlotsCopy;
                        allSlots.RemoveAll(x => x.Card == null || x.Card == BossCard);
                        if (allSlots.Count == 0)
                            break;

                        AudioController.Instance.PlaySound2D("bird_down", MixerGroup.TableObjectsSFX);
                        foreach (CardSlot s in allSlots)
                        {
                            if (!s.Card.FaceDown)
                                s.Card.Anim.StrongNegationEffect();

                            yield return s.Card.AddStatusEffectFlipCard<Sin>(1);
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
            if (Opponent.Queue.Count == 4)
            {
                // if the latest added turn was also full, add an empty turn plan
                if (Opponent.TurnPlan.Last().Count == 4 && ReactiveDifficulty < 11)
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
                            health++;
                    }
                    if (SeededRandom.Bool(randomSeed++))
                    {
                        attack++;
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
                    if (clone.baseHealth + health > 0) // health check
                        clone.baseHealth += health;
                }
                nextTurn.Add(clone);
            }

            Opponent.TurnPlan.Add(nextTurn);
        }
        #endregion

        private void ClearTempMods()
        {
            BossCard.RemoveTemporaryMod(BossCard.TemporaryMods.Find(x => x.singletonId == "SmallBeak"));
            BossCard.RemoveTemporaryMod(BossCard.TemporaryMods.Find(x => x.singletonId == "ReactiveSkin"));
        }
        private void UpdateCounter()
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
            yield return Opponent.ClearQueue();
            yield return new WaitForSeconds(0.4f);

            Action transformCallback = () =>
            {
                UpdateCounter(); // update the turn counter and clear all negative statuses
                List<CardModificationInfo> negativeAbilities = BossCard.TemporaryMods.FindAll(x => x.IsStatusMod(false));
                List<StatusEffectBehaviour> negativeEffects = BossCard.GetStatusEffects(false);

                for (int i = 0; i < negativeEffects.Count; i++)
                    Destroy(negativeEffects[i]);

                if (negativeEffects.Count > 0)
                    BossCard.Anim.LightNegationEffect();

                if (negativeAbilities.Count > 0)
                    BossCard.RemoveTemporaryMods(negativeAbilities.ToArray());

                damageTakenThisTurn = 0;
            };

            if (ActiveEggEffect == ActiveEggEffect.BigEyes) // if we're changing from Big Eyes, update the attack colours
                transformCallback += UpdateAttackColours;

            ChangeActiveEggEffect();

            // transform into the next egg card
            CardInfo bossEggInfo = CardLoader.GetCardByName(AllBossPhases[ActiveEggEffect][1]);
            bossEggInfo.baseAttack = ReactiveDifficulty > 4 ? 2 : 1;
            //bossEggInfo.baseHealth = StartingHealth;
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

            // if we're switching due to losing a life, reset the turn plan and create the next turn plan
            if (lostLife)
            {
                Opponent.NumTurnsTaken = 0; // reset NumTurnsTaken so the turn plan doesn't break
                CreateNextTurnPlan();

                if (Opponent.NumLives == 1)
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
            // Set up start of battle
            ChangeActiveEggEffect();
            targetIconPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CannonTargetIcon");
            bossMouthPrefab = CustomBossUtils.bossBundle.LoadAsset<GameObject>("ApocalypseMouth");
            CardInfo startingEgg = CardLoader.GetCardByName(AllBossPhases[ActiveEggEffect][1]);
            //startingEgg.baseHealth = StartingHealth;
            if (ActiveEggEffect == ActiveEggEffect.BigEyes)
                UpdateAttackColours();

            if (ActiveEggEffect == ActiveEggEffect.LongArms)
                startingEgg.AddTraits(AbnormalPlugin.ImmuneToAilments);

            EncounterData data = base.BuildCustomEncounter(nodeData);
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

        private bool reactive1 = false;
        private bool reactive2 = false;
        private bool reactive3 = false;
        private bool reactiveSkin = false;
        private IEnumerator OnReactiveIncreased()
        {
            Singleton<CameraEffects>.Instance.Shake(0.5f, 0.25f);
            BossCard.Anim.StrongNegationEffect();
            
            if (ReactiveDifficulty > 3 && BossCard.Info.baseAttack < 2)
            {
                BossCard.Info.baseAttack = 2;
                BossCard.OnStatsChanged();
            }
            yield return new WaitForSeconds(0.15f);

            if (!reactive1)
            {
                reactive1 = true;
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("ApocalypseBossReactive1", TextDisplayer.MessageAdvanceMode.Input);
                yield break;
            }
            if (ReactiveDifficulty < 8)
                yield break;

            if (!reactive2)
            {
                reactive2 = true;
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("ApocalypseBossReactive2", TextDisplayer.MessageAdvanceMode.Input);
                yield break;
            }
            if (ReactiveDifficulty < 13)
                yield break;

            if (!reactive3)
            {
                reactive3 = true;
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("ApocalypseBossReactive3", TextDisplayer.MessageAdvanceMode.Input);
                yield break;
            }
        }

        #region Triggers 1
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => BossCard == null || otherCard == BossCard;
        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target) => true;

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => true;
        public bool RespondsToPreScalesChangedRef(int damage, int numWeights, bool toPlayer) => !toPlayer && LobotomyPlugin.PreventOpponentDamage;

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            if (finalPhase && attacker == BossCard)
            {
                if (giantTargetSlots[0].Contains(target.Slot))
                    return damage * 2;
                if (giantTargetSlots[1].Contains(target.Slot))
                    return damage / 2;
            }
            else if (target == BossCard)
            {
                // modify damage so it does not reduce health below the current threshold
                int threshold = BossHealthThreshold(Opponent.NumLives);

                if (target.Health - damage >= threshold)
                    return damage;

                return target.Health - threshold;
            }

            return damage;
        }
        public int CollectPreScalesChangedRef(int damage, ref int numWeights, ref bool toPlayer)
        {
            if (LifeManager.Instance.DamageUntilPlayerWin == 1)
                return numWeights = 0;

            if (damage >= LifeManager.Instance.DamageUntilPlayerWin)
            {
                numWeights = Mathf.Min(LifeManager.Instance.DamageUntilPlayerWin - 1, numWeights);
                return LifeManager.Instance.DamageUntilPlayerWin - 1;
            }

            return damage;
        }
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => finalPhase ? int.MaxValue : int.MinValue;

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
                timesHitThisTurn++;
                damageTakenThisTurn += amount;

                if (ActiveEggEffect == ActiveEggEffect.SmallBeak)
                    BossCard.AddTemporaryMod(new(timesHitThisTurn, 0) { singletonId = "SmallBeak" });

                if (amount > 4 && !attacker.HasStatusEffect<Enchanted>())
                {
                    // reactive is also increased at combat's end for every 6 damage taken so we account for that
                    reactiveDifficulty += amount - (4 + amount / 6);

                    if (amount > 5 && BossCard.TemporaryMods.Find(x => x.singletonId == "ReactiveSkin") == null)
                    {
                        CardModificationInfo mod = new() { fromCardMerge = true, singletonId = "ReactiveSkin", nonCopyable = true };
                        mod.AddAbilities(ThickSkin.ability, ThickSkin.ability, ThickSkin.ability);
                        BossCard.AddTemporaryMod(mod);

                        if (!reactiveSkin)
                        {
                            reactiveSkin = true;
                            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossReactiveSkin");
                        }
                    }
                    yield return OnReactiveIncreased();
                }

                // don't switch phase if we're above the threshold
                if (BossCard.Health > BossHealthThreshold(Opponent.NumLives))
                    yield break;

                if (finalPhase)
                {
                    Opponent.NumLives--;
                    yield return Opponent.LifeLostSequence();
                }
                else
                    changeToNextPhase = true;

                AudioController.Instance.SetLoopVolume(0.1f, 1f);
            }
        }
        #endregion

        #region Triggers 2
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
                reactiveDifficulty += (damage * -1) - 4;
                yield return OnReactiveIncreased();
            }

            else if (!attacker.OpponentCard && damage > 4)
            {
                reactiveDifficulty += damage - 4;
                yield return OnReactiveIncreased();
            }
            yield break;
        }

        public bool RespondsToModifyDirectDamage(CardSlot target, int damage, PlayableCard attacker, int originalDamage)
        {
            return finalPhase && attacker == BossCard;
        }
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

        #region Move Cards
        public IEnumerator MoveOpponentCards()
        {
            List<PlayableCard> cards = BoardManager.Instance.GetCards(false);
            // if all opponent slots are full
            if (cards.Count == 4)
                yield break;

            int rand = base.GetRandomSeed() + TurnNumber;
            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);

            // guaranteed to move the boss
            if (damageTakenThisTurn > 0 || changeToNextPhase || SeededRandom.Bool(rand++))
            {
                yield return MoveToNewSlot(BossCard);
                cards.RemoveAll(x => x.Slot == BossCard.Slot);
            }

            // random chance of each card moving
            for (int i = 0; i < cards.Count; i++)
            {
                if (SeededRandom.Bool(rand++))
                    yield return MoveToNewSlot(cards[i], cards.Count > 1 ? 0.2f : 0.4f);
            }
        }
        private IEnumerator MoveToNewSlot(PlayableCard card, float waitAfter = 0.4f)
        {
            List<CardSlot> openSlots = BoardManager.Instance.GetOpponentOpenSlots();
            if (openSlots.Count == 0)
            {
                BossCard.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(waitAfter);
                yield break;
            }
            openSlots.Sort((a, b) =>
            ((b.opposingSlot.Card?.CanAttackDirectly(card.Slot) ?? true) ? 0 : b.opposingSlot.Card.Attack)
            - ((a.opposingSlot.Card?.CanAttackDirectly(card.Slot) ?? true) ? 0 : a.opposingSlot.Card.Attack));

            CardSlot newSlot = card.HasAbility(HighStrung.ability)
                ? openSlots[SeededRandom.Range(0, openSlots.Count, base.GetRandomSeed() + TurnManager.Instance.TurnNumber)]
                : openSlots.Last();

            float x = (newSlot.transform.position.x + card.Slot.transform.position.x) / 2f;
            float y = newSlot.transform.position.y + 0.5f;
            float z = newSlot.transform.position.z;

            yield return new WaitForSeconds(0.05f);
            GameObject gameObject = GameObject.Instantiate(targetIconPrefab, newSlot.transform);
            gameObject.transform.localPosition = new Vector3(0f, 0.25f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;

            Tween.Position(card.transform, new Vector3(x, y, z), 0.2f, 0f, Tween.EaseOut);
            yield return new WaitForSeconds(0.4f);
            yield return Singleton<BoardManager>.Instance.AssignCardToSlot(card, newSlot, tweenCompleteCallback: () =>
            {
                CleanUpTargetIcon(gameObject);
            });
            yield return new WaitForSeconds(waitAfter);
        }
        #endregion

        #region Target Icons
        private void CreateTargetIcon(CardSlot targetSlot, Color materialColour = default)
        {
            GameObject gameObject = Instantiate(targetIconPrefab, targetSlot.transform);
            gameObject.transform.localPosition = new Vector3(0f, 0.25f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;

            if (materialColour != default)
                gameObject.GetComponentInChildren<MeshRenderer>().material.color = materialColour;

            targetIcons.Add(gameObject);
        }
        public void CleanupTargetIcons()
        {
            targetIcons.ForEach(delegate (GameObject x)
            {
                if (x != null) CleanUpTargetIcon(x);
            });
            targetIcons.Clear();
        }
        private void CleanUpTargetIcon(GameObject icon)
        {
            Tween.LocalScale(icon.transform, Vector3.zero, 0.1f, 0f, Tween.EaseIn, Tween.LoopType.None, null, delegate
            {
                Destroy(icon);
            });
        }
        #endregion

        #region Big Eyes
        private void UpdateAttackColours()
        {
            foreach (PlayableCard c in BoardManager.Instance.GetCards(true)
                .Concat(BoardManager.Instance.GetCards(false))
                .Concat(PlayerHand.Instance.CardsInHand))
            {
                c.OnStatsChanged();
            }
        }
        private void FireLaser(GameObject source, CardSlot targetSlot, bool attackPlayer)
        {
            GameObject gameObject = new("Line");
            LineRenderer line = gameObject.AddComponent<LineRenderer>();
            line.material = Material.GetDefaultLineMaterial();
            line.startColor = Color.yellow;
            line.endColor = Color.yellow;
            line.startWidth = 1f;
            line.endWidth = 0f;
            line.widthCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.125f, 1.7f), new Keyframe(0.25f, 3f), new Keyframe(1f, 3f));
            line.widthMultiplier = 0f;
            Vector3 vector = source.transform.position + Vector3.up * 0.05f + Vector3.right * 2.05f + Vector3.forward;
            Vector3 vector2 = targetSlot.transform.position + (attackPlayer ? Vector3.back : Vector3.zero) + (attackPlayer ? Vector3.zero : (Vector3.down * 0.05f));
            //Vector3 vector3 = vector + Vector3.back;
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
            float ela2 = 0f;
            line.widthMultiplier = 0f;
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
