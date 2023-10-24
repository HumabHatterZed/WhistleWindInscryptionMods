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
        internal ActiveEggEffect ActiveEggEffect = ActiveEggEffect.None;

        internal readonly List<ActiveEggEffect> DisabledEggEffects = new();

        private int BossHealthThreshold => Opponent.NumLives switch
        {
            4 => 90,
            3 => 60,
            2 => 30,
            _ => 0
        };
        

        internal string ActiveEggMinion = null;
        internal PlayableCard BossCard = null;

        internal int turnsToNextPhase = 3;
        private int damageTakenThisTurn = 0;

        private int reactiveDifficulty = 0;
        private int ReactiveDifficulty => RunState.Run.DifficultyModifier + reactiveDifficulty;
        private int PhaseDifficulty => 4 - Opponent.NumLives;

        private bool finalPhase = false;
        private bool changeToNextPhase = false;
        private bool justSwitchedEffect = false;

        private bool seenMouthAttack = false;
        private bool seenEyeAttack = false;
        private bool seenArmsAttack = false;

        internal readonly List<CardSlot>[] giantTargetSlots = new List<CardSlot>[2] 
        {
            new(),
            new()
        };
        internal readonly List<CardSlot> specialTargetSlots = new();
        private readonly List<GameObject> targetIcons = new();
        private GameObject targetIconPrefab;

        private IEnumerator BigBirdEnchantCards()
        {
            int randomSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;
            List<CardSlot> possibleTargetSlots = ReactiveDifficulty > 6 ? BoardManager.Instance.PlayerSlotsCopy : BoardManager.Instance.AllSlotsCopy;
            possibleTargetSlots.RemoveAll(x => x.Card == null || x.Card == BossCard);
            int maxCount = possibleTargetSlots.Count;
            while (possibleTargetSlots.Count > 0)
            {
                if (specialTargetSlots.Count == 3 + PhaseDifficulty)
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
                AudioController.Instance.PlaySound2D("bird_laser_fire", MixerGroup.TableObjectsSFX);
                for (int i = 0; i < specialTargetSlots.Count; i++)
                {

                    Transform source;
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
                    
                    if (ReactiveDifficulty > 7) // fire 2 lasers since we're enchanting twice
                    {
                        if (specialTargetSlots[i].Index % 2 == 0)
                        {
                            source = rightSources[UnityEngine.Random.Range(0, rightSources.Count)];
                            rightSources.Remove(source);
                        }
                        else
                        {
                            source = leftSources[UnityEngine.Random.Range(0, leftSources.Count)];
                            leftSources.Remove(source);
                        }
                        FireLaser(source.gameObject, specialTargetSlots[i], specialTargetSlots[i].IsPlayerSlot);
                    }
                    yield return new WaitForSeconds(0.1f);

                    specialTargetSlots[i].Card.Anim.StrongNegationEffect();
                    specialTargetSlots[i].Card.AddStatusEffect<Enchanted>(ReactiveDifficulty > 7 ? 2 : 1).TurnGained = TurnManager.Instance.TurnNumber + 1;
                    
                    CleanUpTargetIcon(targetIcons[i]);
                }

                yield return HelperMethods.ChangeCurrentView(View.Board, 0.2f, 0f);
                yield return new WaitForSeconds(0.3f);

                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossEyePostAttack", 0f);
                seenEyeAttack = true;

                if (reactiveDifficulty > 0)
                    reactiveDifficulty--;
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
                int laneIndex = SeededRandom.Range(0, lanes.Count, rand++);
                CardSlot lane = lanes[laneIndex];
                specialTargetSlots.Add(lane);
                specialTargetSlots.Add(BoardManager.Instance.PlayerSlotsCopy[lane.Index]);
                lanes.Remove(lane);
            }

            // mouth target animation

            foreach (CardSlot slot in specialTargetSlots)
            {
                yield return new WaitForSeconds(0.05f);
                CreateTargetIcon(slot, GameColors.Instance.glowRed);
            }
            yield return new WaitForSeconds(0.5f);

            if (!seenMouthAttack)
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossMouthPreAttack", TextDisplayer.MessageAdvanceMode.Input);
        }
        private IEnumerator SmallBirdAttackLanes()
        {
            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);
            // mouth attack animation
            AudioController.Instance.PlaySound2D("bird_mouth", MixerGroup.TableObjectsSFX);
            bool killedCard = false;
            for (int i = 0; i < specialTargetSlots.Count; i++)
            {
                PlayableCard target = specialTargetSlots[i].Card;
                if (target != null && target != BossCard)
                {
                    killedCard = true;
                    yield return target.Die(false, BossCard);
                    yield return new WaitForSeconds(0.1f);
                }
            }

            CleanupTargetIcons();
            specialTargetSlots.Clear();
            yield return new WaitForSeconds(0.5f);

            if (!killedCard)
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossEyeFailAttack", 0f);
            else
            {
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossMouthPostAttack", 0f);
                
                if (reactiveDifficulty > 0)
                    reactiveDifficulty--;
            }
           
            yield return Opponent.ResetToIdle();
            seenMouthAttack = true;
        }
        private IEnumerator ArmAttackSequence()
        {
            List<CardSlot> slots = BoardManager.Instance.AllSlotsCopy;
            slots.RemoveAll(x => x.Card == null || x.Card.GetStatusEffectStacks<Sin>() < 5 || x.Card == BossCard);
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
            
            // tip scale animation

            foreach (CardSlot slot in slots)
            {
                slot.Card.Anim.PlaySacrificeSound();
                slot.Card.Anim.DeactivateSacrificeHoverMarker();
                yield return slot.Card.Die(false, BossCard);
            }

            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossArmsPostAttack", 0f);
            yield return Opponent.ResetToIdle();
            seenArmsAttack = true;

            if (reactiveDifficulty > 0)
                reactiveDifficulty--;
        }

        internal void CleanUpGiantTarget(CardSlot slot)
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
        internal IEnumerator GiantPhaseLogic(bool firstStrike)
        {
            CleanupTargetIcons();
            specialTargetSlots.Clear();
            giantTargetSlots[0].Clear();
            giantTargetSlots[1].Clear();

            List<CardSlot> playerSlots = BoardManager.Instance.PlayerSlotsCopy;
            int randomSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;
            if (firstStrike)
                randomSeed += 5;

            int maxRedTargets = 2, maxWhiteTargets = 1;
            float redChance = 0.12f, whiteChance = 0.14f, directChance = 0.25f;

            // if the opponent is winning, soften up on the attacks somewhat
            if (LifeManager.Instance.Balance < 0)
            {
                redChance = 0.08f;
                whiteChance = 0.22f;
                directChance = 0.1f;
            }

            // at difficulty 8+
            if (ReactiveDifficulty > 6)
            {
                redChance += 0.02f;
                whiteChance += 0.04f;
                directChance += 0.03f;
            }

            // if 3+ cards on the opposing side
            if (playerSlots.Count(x => x.Card != null) >= 2)
            {
                redChance += 0.05f;
                directChance -= 0.1f;
            }

            // health is at 1/3
            if (BossCard.Health >= 30)
                whiteChance /= 2f;
            else if (BossCard.Health <= 10)
            {
                redChance += 0.02f;
                whiteChance += 0.05f;
                directChance += 0.04f;
            }

            // if no opposing cards, target one random empty space
            // randomly remove a random card slot from consideration at a 20% chance w/ reactive <= 3
            if (playerSlots.Count(x => x.Card != null) == 0)
            {
                playerSlots = new() { playerSlots[SeededRandom.Range(0, playerSlots.Count, randomSeed++)] };
                directChance = 1f;
            }
            else if (ReactiveDifficulty <= 6 && SeededRandom.Value(randomSeed++) <= 0.2f)
            {
                playerSlots.RemoveAt(SeededRandom.Range(0, playerSlots.Count, randomSeed));
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
                    redChance -= 0.06f;
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
                }
                yield return Opponent.PostResetScalesSequence();
                changeToNextPhase = false;
            }
            
            if (finalPhase)
                yield break;

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
                UpdateCounter();

            CreateNextTurnPlan();
        }
        public override IEnumerator OpponentCombatEnd()
        {
            BossCard?.TemporaryMods.RemoveAll(x => x.singletonId == "SmallBeak");

            if (finalPhase)
            {
                yield return GiantPhaseLogic(false);

                if (reactiveDifficulty > 0)
                    reactiveDifficulty--;

                yield break;
            }

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
                        yield break;

                    AudioController.Instance.PlaySound2D("bird_down", MixerGroup.TableObjectsSFX);
                    int sinCount = 2 + PhaseDifficulty;
                    if (ReactiveDifficulty > 4)
                        sinCount = Mathf.Min(5, sinCount + ReactiveDifficulty / 4);
                    foreach (CardSlot s in allSlots)
                    {
                        s.Card.Anim.StrongNegationEffect();
                        s.Card.AddStatusEffect<Sin>(sinCount);
                        yield return new WaitForSeconds(0.1f);
                    }
                    break;
            }
            damageTakenThisTurn = 0;
        }
        private int GetStartingCardCount()
        {
            // queue 2 cards every 7 turns
            if (TurnManager.Instance.TurnNumber % 7 == 0)
                return 2;

            // queue 0 cards every 4 turns
            if (TurnManager.Instance.TurnNumber % 4 == 0)
                return 0;

            // queue 2 cards every even turn
            if (TurnManager.Instance.TurnNumber % 2 == 0)
                return 2;

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
            else if (LifeManager.Instance.Balance > 3)
                cardNum++;

            switch (ActiveEggEffect)
            {
                case ActiveEggEffect.BigEyes:
                    cardNum = Mathf.Min(1, cardNum + 1);
                    break;
                case ActiveEggEffect.LongArms:
                    if (cardNum > 1)
                        cardNum--;
                    break;
            }

            // if the opponent side is full, halve the incoming card count
            if (BoardManager.Instance.GetCards(false).Count == 4)
                cardNum /= 2;

            // clamp the count to 3
            if (cardNum > 3)
                cardNum = 3;

            int randomSeed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;

            // threshold for whether to give queued card a mod
            // if the difficulty modifier is 6 or higher, guaranteed to modify stats, other chance is dependent on difficulty and cards being cued
            float gateValue;
            if (ReactiveDifficulty > 6)
                gateValue = opponentWinning ? 0.7f : 1f;
            else
                gateValue = (4 - cardNum - (opponentWinning ? 1 : 0)) / (7f - RunState.Run.DifficultyModifier);

            for (int i = 0; i < cardNum; i++)
            {
                CardInfo clone = CardLoader.GetCardByName(ActiveEggMinion);
                float randomValue = SeededRandom.Value(randomSeed++);
                if (randomValue <= gateValue)
                {
                    // either give +1/-1 or 0/+1
                    bool giveAttack = SeededRandom.Bool(randomSeed++);
                    int attack = 0, health = 0;
                    if (giveAttack)
                    {
                        attack++;
                        health--;
                    }
                    else
                        health++;

                    // if randomValue is < half the gateValue
                    // change to give +1/0 or 0/+2
                    if (randomValue < (gateValue / 2f))
                        health++;

                    clone.baseAttack += attack;
                    clone.baseHealth += health;
                }
                nextTurn.Add(clone);
            }

            Opponent.TurnPlan.Add(nextTurn);
        }
        private void UpdateCounter()
        {
            string newTex = (turnsToNextPhase <= 0 || DisabledEggEffects.Count == 3) ? "sigilApocalypse.png" : ("sigilApocalypse_" + turnsToNextPhase + ".png");
            BossCard.RenderInfo.OverrideAbilityIcon(ApocalypseAbility.ability, TextureLoader.LoadTextureFromFile(newTex));
            BossCard.RenderCard();
        }

        public override IEnumerator PlayerUpkeep()
        {
            if (CardDrawPiles3D.Instance.Deck.CardsInDeck + CardDrawPiles3D.Instance.SideDeck.CardsInDeck > 1)
                yield break;

            if (PlayerHand.Instance.CardsInHand.Exists(x => x.Info.name != "wstl_REFRESH_DECKS"))
            {
                ViewManager.Instance.SwitchToView(View.Hand);

                CardInfo refreshDecks = CardLoader.GetCardByName("wstl_REFRESH_DECKS");
                yield return CardSpawner.Instance.SpawnCardToHand(refreshDecks);
                yield return new WaitForSeconds(0.4f);
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossExhausted", TextDisplayer.MessageAdvanceMode.Input);
            }
        }
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

        internal IEnumerator SwitchToNextEggEffect(bool lostLife)
        {
            // clean up the previous phase and reset the counter
            turnsToNextPhase = 3;
            justSwitchedEffect = true;

            specialTargetSlots.Clear();
            CleanupTargetIcons();

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

                BossCard.TemporaryMods.RemoveAll(x => x.singletonId == "SmallBeak");
                damageTakenThisTurn = 0;

            };

            if (ActiveEggEffect == ActiveEggEffect.BigEyes) // if we're changing from Big Eyes, update the attack colours
                transformCallback += UpdateAttackColours;

            ChangeActiveEggEffect();

            // transform into the next egg card
            CardInfo bossEggInfo = CardLoader.GetCardByName(AllBossPhases[ActiveEggEffect][1]);
            switch (ActiveEggEffect)
            {
                case ActiveEggEffect.BigEyes: // if we're changing to Big Eyes, update the attack colours
                    transformCallback += UpdateAttackColours;
                    break;
                case ActiveEggEffect.LongArms: // if we're changing to Long Arms, make ourselves immune to ailments
                    bossEggInfo.AddTraits(AbnormalPlugin.ImmuneToAilments);
                    break;
            }

            yield return BossCard.TransformIntoCard(bossEggInfo, transformCallback);
            yield return new WaitForSeconds(0.4f);

            // if we're switching due to losing a life, reset the turn plan and increase attackStrength
            if (lostLife)
            {
                Opponent.NumTurnsTaken = 0; // reset NumTurnsTaken so the turn plan doesn't break
                CreateNextTurnPlan();

                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossAttackStrength", 0f);
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
            targetIconPrefab ??= ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CannonTargetIcon");

            CardInfo startingEgg = CardLoader.GetCardByName(AllBossPhases[ActiveEggEffect][1]);

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

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => BossCard == null || otherCard == BossCard;
        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target) => target == BossCard || (finalPhase && attacker == BossCard);
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) => card == finalPhase && !deathSlot.IsPlayerSlot;

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => target == BossCard || (finalPhase && attacker == BossCard);
        public bool RespondsToPreScalesChangedRef(int damage, int numWeights, bool toPlayer) => !toPlayer && LobotomyPlugin.PreventOpponentDamage;

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
            damageTakenThisTurn += amount;
            if (amount >= 5 && !attacker.HasStatusEffect<Enchanted>())
            {
                reactiveDifficulty += Mathf.Max(1, amount / 5);
                Debug.Log($"ReactiveDifficulty: {ReactiveDifficulty}");
                yield return new WaitForSeconds(0.4f);
                Singleton<CameraEffects>.Instance.Shake(0.5f, 0.25f);
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossAttackStrengthOvercharge");
            }

            if (finalPhase)
            {
                if (giantTargetSlots[1].Contains(target.Slot))
                    BossCard.HealDamage(amount * 2);

                CleanUpGiantTarget(target.Slot);
                yield break;
            }
            else
            {
                if (ActiveEggEffect == ActiveEggEffect.SmallBeak)
                    BossCard.AddTemporaryMod(new(damageTakenThisTurn, 0) { singletonId = "SmallBeak" });

                if (BossCard.Health > BossHealthThreshold)
                    yield break;

                changeToNextPhase = true;
                AudioController.Instance.SetLoopVolume(0.1f, 1f);
            }
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            Opponent.NumLives--;
            yield return Opponent.LifeLostSequence();
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            if (finalPhase)
            {
                if (giantTargetSlots[0].Contains(target.Slot))
                    return damage * 2;
                if (giantTargetSlots[1].Contains(target.Slot))
                    return damage / 2;

                return damage;
            }
            else
            {
                // modify damage so it does not reduce health below the current threshold
                int threshold = BossHealthThreshold;

                if (target.Health - damage >= threshold)
                    return damage;

                return target.Health - threshold;
            }
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

            if (damage > 3)
            {
                numWeights = Mathf.Min(3, numWeights);
                return 3;
            }

            return damage;
        }

        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => finalPhase ? int.MaxValue : int.MinValue;

        internal IEnumerator MoveOpponentCards()
        {
            int rand = base.GetRandomSeed();
            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);
            if (damageTakenThisTurn > 0 || changeToNextPhase || SeededRandom.Bool(rand += 10))
                yield return MoveToNewSlot(BossCard);

            List<PlayableCard> cards = BoardManager.Instance.GetCards(false);
            cards.Remove(BossCard);
            for (int i = 0; i < cards.Count; i++)
            {
                if (SeededRandom.Bool(rand += 10))
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

            CardSlot newSlot = openSlots.LastOrDefault();

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

        private void CreateTargetIcon(CardSlot targetSlot, Color materialColour = default)
        {
            GameObject gameObject = Instantiate(targetIconPrefab, targetSlot.transform);
            gameObject.transform.localPosition = new Vector3(0f, 0.25f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;

            if (materialColour != default)
                gameObject.GetComponentInChildren<MeshRenderer>().material.color = materialColour;

            targetIcons.Add(gameObject);
        }
        internal void CleanupTargetIcons()
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

        private void UpdateAttackColours()
        {
            foreach (PlayableCard c in BoardManager.Instance.GetCards(true)
                .Concat(BoardManager.Instance.GetCards(false))
                .Concat(PlayerHand.Instance.CardsInHand))
            {
                c.OnStatsChanged();
            }
        }

        public bool RespondsToCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) => finalPhase && attacker == BossCard;
        public IEnumerator OnCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage)
        {
            if (giantTargetSlots[1].Contains(opposingSlot))
                BossCard.HealDamage(damage * 2);

            CleanUpGiantTarget(opposingSlot);
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
            line.alignment = LineAlignment.Local;
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
    }

    public enum ActiveEggEffect
    {
        BigEyes = 0,
        SmallBeak = 1,
        LongArms = 2,
        None = 3
    }
}
