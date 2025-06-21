using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents
{
    public abstract class LobotomyBossBattleSequencer : LobotomyBattleSequencer, IModifyDamageTaken
    {
        public PlayableCard BossCard = null;

        public int timesHitThisTurn = 0;
        public int damageTakenThisTurn = 0;
        protected int reactiveDifficulty = 0;

        public int ReactiveDifficulty => reactiveDifficulty + RunState.Run.DifficultyModifier;
        public int PhaseDifficulty => TurnManager.Instance.Opponent.StartingLives - TurnManager.Instance.Opponent.NumLives;

        public bool finalPhase = false;
        public bool changeToNextPhase = false;

        public virtual int BossHealthThreshold(int remainingLives) => -1;

        public void IncrementStatsThisTurn(int timesHit, int damageTaken)
        {
            timesHitThisTurn += timesHit;
            damageTakenThisTurn += damageTaken;
        }
        public IEnumerator IncreaseReactiveDifficulty(int amount)
        {
            reactiveDifficulty += amount;
            yield return OnReactiveDifficultyIncreased(amount);
        }
        public virtual IEnumerator OnReactiveDifficultyIncreased(int amount)
        {
            yield break;
        }

        #region Card movement
        public override IEnumerator MoveOpponentCards()
        {
            int rand = base.GetRandomSeed() + TurnNumber;
            List<CardSlot> slots = CardScramble.GetOccupiedSlotsMovable(BoardManager.Instance.OpponentSlotsCopy);

            for (int i = 0; i < slots.Count; i++)
            {
                if (SeededRandom.Bool(rand++))
                {
                    slots.Remove(slots[i]);
                }
            }

            if (!slots.Contains(BossCard.Slot)) // check if we need to override the boss card's movement
            {
                if (damageTakenThisTurn > 0 || changeToNextPhase || SeededRandom.Bool(rand++))
                    slots.Add(BossCard.Slot);
            }

            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);
            yield return CardScramble.RandomiseCardsInSlots(slots, rand, sortPredicate: delegate (CardSlot s)
            {
                if (s == BossCard.Slot)
                    return 1000;

                return s.Card.HasAbility(HighStrung.ability) ? 100 : 0;
            });
        }

        #endregion

        #region Triggers
        public virtual bool RespondsToModifyDamage(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => true;
        public virtual int OnModifyDamage(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            if (target == BossCard)
            {
                // modify damage so it does not reduce health below the current threshold
                int threshold = BossHealthThreshold(TurnManager.Instance.Opponent.NumLives);

                if (target.Health - damage >= threshold)
                    return damage;

                return target.Health - threshold;
            }

            return damage;
        }
        public virtual int ModifyDamagePriority(PlayableCard target, int damage, PlayableCard attacker) => (finalPhase && attacker == BossCard) ? int.MaxValue : int.MinValue;

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => RespondsToModifyDamage(target, damage, attacker, originalDamage);

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => OnModifyDamage(target, damage, attacker, originalDamage);
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => ModifyDamagePriority(target, damage, attacker);
        #endregion
    }
}
