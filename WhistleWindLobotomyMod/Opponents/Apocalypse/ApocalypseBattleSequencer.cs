using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents.Apocalypse
{
    public class ApocalypseBattleSequencer : Part1BossBattleSequencer, IModifyDamageTaken
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "ApocalypseBattleSequencer", typeof(ApocalypseBattleSequencer)).Id;
        public override Opponent.Type BossType => ApocalypseBossOpponent.ID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.ApocalypseBossDefeated;

        private readonly Dictionary<ApocalpyseBossPhase, string[]> AllBossPhases = new()
        {
            { ApocalpyseBossPhase.BigEyes, new []{
                "wstl_eyeballChick",
                "wstl_apocalypseEgg_big"} },
            { ApocalpyseBossPhase.SmallBeak, new [] {
                "wstl_forestKeeper",
                "wstl_apocalypseEgg_small" } },
            { ApocalpyseBossPhase.LongArms, new [] {
                "wstl_runawayBird",
                "wstl_apocalypseEgg_long" } }
        };

        private readonly int[] BossEggAttack = new int[3]
        {
            1,
            1,
            1
        };

        private readonly int StartingBossHealth = 120;
        private int BossHealthThreshold()
        {
            return TurnManager.Instance.Opponent.NumLives switch
            {
                4 => StartingBossHealth * 3 / 4,
                3 => StartingBossHealth / 2,
                2 => StartingBossHealth / 4,
                _ => 0
            };
        }

        internal PlayableCard CurrentEggCard = null;

        internal readonly List<ApocalpyseBossPhase> CompletedBossPhases = new();

        internal string CurrentBossMinion = null;

        internal ApocalpyseBossPhase CurrentBossPhase = ApocalpyseBossPhase.None;

        internal int specialCounter = 3;

        private bool finalPhase = false;

        private IEnumerator EyeAttackSequence()
        {
            // target X number of spaces on the board
            // inflict Enchanted on cards
            // Enchanted - target the Egg
            yield break;
        }
        private IEnumerator MouthAttackSequence()
        {
            // target X number of lanes on the board
            // kill/deal 10 damage to all cards in the lane (mouth extend anim)
            yield break;
        }
        private IEnumerator ArmAttackSequence()
        {
            // hover all cards with X sin above the board
            // kill them (scale tip animation)
            yield break;
        }

        public override IEnumerator OpponentUpkeep()
        {
            if (finalPhase)
                yield break;

            specialCounter--;
            UpdateCounter();
            if (specialCounter <= 0)
            {
                specialCounter = 3;
                switch (CurrentBossPhase)
                {
                    case ApocalpyseBossPhase.BigEyes:
                        yield return EyeAttackSequence();
                        break;
                    case ApocalpyseBossPhase.SmallBeak:
                        yield return MouthAttackSequence();
                        break;
                    case ApocalpyseBossPhase.LongArms:
                        yield return ArmAttackSequence();
                        break;
                }

                yield return new WaitForSeconds(0.5f);
                yield return SwitchToNextEggPhase(false);
                UpdateCounter();
            }
            CreateNextTurnPlan();
        }
        private int GetStartingCardCount()
        {
            // queue 0 cards every 5 turns
            if (TurnManager.Instance.TurnNumber % 5 == 0)
                return 0;

            // queue 1 card every 4 turns
            if (TurnManager.Instance.TurnNumber % 4 == 0)
                return 1;

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

            if (finalPhase)
                cardNum++;

            switch (CurrentBossPhase)
            {
                // more spammy
                case ApocalpyseBossPhase.BigEyes:
                    if (cardNum < 2)
                        cardNum++;
                    else
                        cardNum = 1;

                    break;
                // less spammy(?)
                case ApocalpyseBossPhase.SmallBeak:
                    if (specialCounter == 1 || TurnManager.Instance.TurnNumber % 3 == 0)
                        cardNum = 0;
                    break;
            }

            /* Example turn plan without modifiers
             * Turn | Played this turn | Queued this turn | AttackCounter this turn
             * T    P | Q | A
             * 1    2 | 1 | 2
             * 2    1 | 2 | 1
             * 3    2 | 1 | 0/3
             * 4    1 | 1 | 2
             * 5    1 | 0 | 1
             * 6    0 | 2 | 0/3
             * 7    2 | 1 | 2
             * 8    1 | 1 | 1
             * 9    1 | 1 | 0/3
             * 10   1 | 0 | 2
             * 11   0 | 1 | 1
             * 12   1 | 1 | 0/3
             * 13   1 | 1 | 2
             * 14   1 | 2 | 1
             * 15   2 | 0 | 0/3
             * 16   0 | 1 | 2
             * 17   1 | 1 | 1
             * 18   1 | 2 | 0/3
            */

            int randomSeed = base.GetRandomSeed();

            // threshold for whether to give queued card a mod
            float gateValue;
            if (RunState.Run.DifficultyModifier > 6)
                gateValue = opponentWinning ? 0.7f : 1f;
            else
                gateValue = (4 - cardNum - (opponentWinning ? 1 : 0)) / (7f - RunState.Run.DifficultyModifier);

            for (int i = 0; i < cardNum; i++)
            {
                CardInfo clone = CardLoader.GetCardByName(CurrentBossMinion);
                float randomValue = SeededRandom.Value(randomSeed++);
                if (randomValue <= gateValue)
                {
                    // either give +1/-1 or 0/+1
                    bool giveAttack = SeededRandom.Bool(randomSeed++);
                    CardModificationInfo mod = giveAttack ? new(1, -1) : new(0, 1);

                    // if randomValue is <= half the gateValue
                    // change to give +1/0 or 0/+2
                    if (randomValue <= (gateValue / 2f))
                        mod.healthAdjustment++;

                    clone.Mods.Add(mod);
                }
                nextTurn.Add(clone);
            }

            TurnManager.Instance.Opponent.TurnPlan.Add(nextTurn);
        }
        private void UpdateCounter()
        {
            string newTex = specialCounter <= 0 ? "sigilApocalypse.png" : ("sigilApocalypse_" + specialCounter + ".png");
            CurrentEggCard.RenderInfo.OverrideAbilityIcon(ApocalypseAbility.ability, TextureLoader.LoadTextureFromFile(newTex));
            CurrentEggCard.RenderCard();
        }

        public override IEnumerator PlayerUpkeep()
        {
            if (CardDrawPiles3D.Instance.Deck.CardsInDeck + CardDrawPiles3D.Instance.SideDeck.CardsInDeck <= 1)
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
            
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_RETURN_CARD"));
            yield return new WaitForSeconds(0.1f);
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_RETURN_CARD_ALL"));
            yield return new WaitForSeconds(0.4f);
            yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossRecall", TextDisplayer.MessageAdvanceMode.Input);
        }

        internal IEnumerator SwitchToNextEggPhase(bool updateTurnPlan)
        {
            // clean up the previous phase and update the last phase's attack
            BossEggAttack[(int)CurrentBossPhase] = CurrentEggCard.Attack;
            yield return TurnManager.Instance.Opponent.ClearQueue();
            yield return new WaitForSeconds(0.4f);

            ChangeCurrentPhase();

            // transform into the next egg card
            CardInfo bossEggInfo = CardLoader.GetCardByName(AllBossPhases[CurrentBossPhase][1]);
            bossEggInfo.baseHealth = StartingBossHealth;
            if (CurrentBossPhase == ApocalpyseBossPhase.LongArms)
                bossEggInfo.AddTraits(AbnormalPlugin.ImmuneToAilments);

            yield return CurrentEggCard.TransformIntoCard(bossEggInfo, () =>
            {
                // clear attack modifiers from the last phase
                CurrentEggCard.TemporaryMods.ForEach((CardModificationInfo x) => x.attackAdjustment = 0);
            }, () =>
            {
                // update to the correct attack for this phase
                CurrentEggCard.AddTemporaryMod(new(BossEggAttack[(int)CurrentBossPhase], 0));
            });
            yield return new WaitForSeconds(0.4f);

            if (updateTurnPlan)
                CreateNextTurnPlan();
        }

        private void ChangeCurrentPhase()
        {
            if (finalPhase)
                return;

            List<ApocalpyseBossPhase> possiblePhases = AllBossPhases.Keys.Where(x => !CompletedBossPhases.Contains(x)).ToList();
            possiblePhases.Remove(CurrentBossPhase);
            CurrentBossPhase = possiblePhases[SeededRandom.Range(0, possiblePhases.Count, base.GetRandomSeed())];
            CurrentBossMinion = AllBossPhases[CurrentBossPhase][0];
        }

        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            // Set up start of battle
            ChangeCurrentPhase();

            CardInfo startingEgg = CardLoader.GetCardByName(AllBossPhases[CurrentBossPhase][1]);
            startingEgg.baseHealth = StartingBossHealth;
            if (CurrentBossPhase == ApocalpyseBossPhase.LongArms)
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

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => !finalPhase && otherCard.HasAbility(ApocalypseAbility.ability);
        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target) => !finalPhase && target == CurrentEggCard;
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) => finalPhase;

        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            if (CurrentEggCard == null && !finalPhase)
            {
                CurrentEggCard = otherCard;
                UpdateCounter();
            }
            else
            {
                CurrentEggCard ??= otherCard;
            }

            // if on the player side for whatever reason, return to opponent side
            if (otherCard == CurrentEggCard && otherCard.Slot.IsPlayerSlot)
            {
                yield return new WaitForSeconds(0.5f);
                yield return ReturnEggToBasket();
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossReturnEgg", TextDisplayer.MessageAdvanceMode.Input);
            }
        }
        public override IEnumerator OnOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            if (CurrentEggCard.Health > BossHealthThreshold())
                yield break;

            TurnManager.Instance.Opponent.NumLives--;
            yield return TurnManager.Instance.Opponent.LifeLostSequence();
            if (TurnManager.Instance.Opponent.NumLives == 1)
            {
                CurrentBossPhase = ApocalpyseBossPhase.None;
                finalPhase = true;
            }
            yield return TurnManager.Instance.Opponent.PostResetScalesSequence();
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            TurnManager.Instance.Opponent.NumLives--;
            yield return TurnManager.Instance.Opponent.LifeLostSequence();
        }

        private IEnumerator ReturnEggToBasket()
        {
            CardSlot newSlot;
            List<CardSlot> opponentSlots = BoardManager.Instance.OpponentSlotsCopy.FindAll(x => x.Card == null);

            CurrentEggCard.Anim.StrongNegationEffect();

            if (opponentSlots.Count == 0)
            {
                newSlot = CurrentEggCard.OpposingSlot();
                yield return newSlot.Card.DieTriggerless();
            }
            else
            {
                newSlot = opponentSlots[SeededRandom.Range(0, opponentSlots.Count, base.GetRandomSeed())];
            }

            yield return new WaitForSeconds(0.45f);
            yield return BoardManager.Instance.AssignCardToSlot(CurrentEggCard, newSlot);
            yield return new WaitForSeconds(0.75f);
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return CurrentBossPhase == ApocalpyseBossPhase.LongArms;
        }

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => target == CurrentEggCard;
        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            int threshold = BossHealthThreshold();

            // if damage will not take the boss below the current phase threshold
            if (target.Health - damage >= threshold)
                return damage;

            // otherwise if it's greater, return the amount that will bring the boss exactly to the threshold
            return target.Health - threshold;
        }
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => int.MinValue;
    }

    public enum ApocalpyseBossPhase
    {
        BigEyes = 0,
        SmallBeak = 1,
        LongArms = 2,
        None = 3
    }
}
