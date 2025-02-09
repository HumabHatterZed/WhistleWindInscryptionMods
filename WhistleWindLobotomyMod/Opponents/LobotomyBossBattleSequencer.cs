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

        public int reactiveDifficulty = 0;
        public int ReactiveDifficulty => RunState.Run.DifficultyModifier + reactiveDifficulty;
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
        public IEnumerator MoveOpponentCards()
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

        public override List<CardInfo> GetFixedOpeningHand() => drewInitialHand ? CardDrawPiles.Instance.Deck.GetFairHand(5, false) : null;
        public override IEnumerator PreDrawOpeningHand()
        {
            if (drewInitialHand)
            {
                CardDrawPiles3D.Instance.sidePile.Draw();
                yield return CardDrawPiles3D.Instance.DrawFromSidePile();
                yield return new WaitForSeconds(0.1f);
            }
        }
        public override IEnumerator PostDrawOpeningHand()
        {
            ViewManager.Instance.SwitchToView(View.Hand);
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_RETURN_CARD"));
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_RETURN_CARD_ALL"));
            yield return new WaitForSeconds(0.4f);
        }
    }

    [HarmonyPatch]
    internal static class LobotomyBossSetUpPatch
    {
        [HarmonyPostfix, HarmonyPatch(typeof(CardDrawPiles3D), nameof(CardDrawPiles3D.DrawOpeningHand))]
        public static IEnumerator CallPostDrawOpeningHand(IEnumerator enumerator)
        {
            LobotomyBattleSequencer sequence = TurnManager.Instance.SpecialSequencer as LobotomyBattleSequencer;
            if (!SaveManager.SaveFile.IsPart1 || sequence == null)
            {
                yield return enumerator;
                yield break;
            }

            yield return sequence.PreDrawOpeningHand();
            yield return enumerator;
            yield return sequence.PostDrawOpeningHand();
            sequence.drewInitialHand = true;
        }
    }
}
