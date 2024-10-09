using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Tango;
using WhistleWind.AbnormalSigils;
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
            List<PlayableCard> cards = BoardManager.Instance.GetOpponentCards();
            if (cards.Count == 4) // if the board is full
                yield break;
            cards.RemoveAll(x => x.HasAbility(Unyielding.ability));

            yield return HelperMethods.ChangeCurrentView(View.Board, 0f);
            int rand = base.GetRandomSeed() + TurnNumber;

            if (damageTakenThisTurn > 0 || changeToNextPhase || SeededRandom.Bool(rand++))
            {
                yield return MoveToNewSlot(BossCard);
                cards.Remove(BossCard);
            }

            // random chance of each card moving
            for (int i = 0; i < cards.Count; i++)
            {
                if (SeededRandom.Bool(rand++))
                {
                    PlayableCard card = cards.GetSeededRandom(rand++);
                    yield return MoveToNewSlot(card, cards.Count > 1 ? 0.2f : 0.4f);
                    cards.Remove(card);
                }
            }
        }
        private IEnumerator MoveToNewSlot(PlayableCard card, float waitAfter = 0.4f, bool raiseAboveBoard = true)
        {
            List<CardSlot> openSlots = BoardManager.Instance.GetOpponentOpenSlots();
            if (openSlots.Count == 0)
            {
                BossCard?.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(waitAfter);
                yield break;
            }
            openSlots.Sort((a, b) =>
            ((b.opposingSlot.Card?.CanAttackDirectly(card.Slot) ?? true) ? 0 : b.opposingSlot.Card.Attack)
            - ((a.opposingSlot.Card?.CanAttackDirectly(card.Slot) ?? true) ? 0 : a.opposingSlot.Card.Attack));

            CardSlot newSlot = card.HasAbility(HighStrung.ability)
                ? openSlots[SeededRandom.Range(0, openSlots.Count, base.GetRandomSeed() + TurnManager.Instance.TurnNumber)]
                : openSlots.Last();

            yield return new WaitForSeconds(0.05f);
            GameObject gameObject = GameObject.Instantiate(targetIconPrefab, newSlot.transform);
            gameObject.transform.localPosition = new Vector3(0f, 0.25f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;

            if (raiseAboveBoard)
            {
                float x = (newSlot.transform.position.x + card.Slot.transform.position.x) / 2f;
                float y = newSlot.transform.position.y + 0.5f;
                float z = newSlot.transform.position.z;

                Tween.Position(card.transform, new Vector3(x, y, z), 0.2f, 0f, Tween.EaseOut);
            }

            yield return new WaitForSeconds(0.4f);
            yield return Singleton<BoardManager>.Instance.AssignCardToSlot(card, newSlot, tweenCompleteCallback: () =>
            {
                CleanUpTargetIcon(gameObject);
            });
            yield return new WaitForSeconds(waitAfter);
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
