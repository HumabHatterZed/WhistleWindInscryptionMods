using DiskCardGame;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents
{
    public abstract class LobotomyBattleSequencer : BossBattleSequencer, IOnPreScalesChangedRef, IOnCardDealtDamageDirectly, IOnRoundEnd
    {
        public int currentExcessBones = 0;
        public bool drewInitialHand = false;

        public GameObject targetIconPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CannonTargetIcon");
        public readonly List<GameObject> targetIcons = new();

        /// <summary>
        /// How far the scales can tip towards the opponent. Values below 5 mean the player cannot win by dealing direct damage.
        /// </summary>
        public virtual int HighestPositiveScaleBalance { get; set; } = 5;
        public bool PlayerCanWinThroughScaleDamage => HighestPositiveScaleBalance >= 5;
        public virtual bool DirectDamageGivesBones { get; set; } = true;
        public virtual int MaxExcessBones { get; } = 8;

        public bool RespondsToRoundEnd(bool opponentTurnSkipped) => true;
        public virtual IEnumerator OnRoundEnd(bool opponentTurnSkipped) {
            currentExcessBones = 0;
            yield break;
        }
        public int RoundEndPriority(bool opponentTurnSkipped) => 0;

        public override List<CardInfo> GetFixedOpeningHand() => drewInitialHand ? CardDrawPiles.Instance.Deck.GetFairHand(5, false) : null;
        public virtual IEnumerator PreDrawOpeningHand()
        {
            if (drewInitialHand) {
                CardDrawPiles3D.Instance.sidePile.Draw();
                yield return CardDrawPiles3D.Instance.DrawFromSidePile();
                yield return new WaitForSeconds(0.1f);
            }
        }
        public virtual IEnumerator PostDrawOpeningHand()
        {
            ViewManager.Instance.SwitchToView(View.Hand);
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_RETURN_CARD"));
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_RETURN_CARD_ALL"));
            yield return new WaitForSeconds(0.4f);
        }

        public virtual IEnumerator MoveOpponentCards() {
            LobotomyPlugin.Log.LogDebug($"[LobotomyBattleSequencer.MoveOpponentCards] Start");
            int rand = base.GetRandomSeed() + TurnNumber;
            List<CardSlot> slots = CardScramble.GetOccupiedSlotsMovable(BoardManager.Instance.OpponentSlotsCopy);
            List<CardSlot> slots2 = new();
            for (int i = 0; i < slots.Count; i++) {
                if (true || SeededRandom.Bool(rand++)) {
                    slots2.Add(slots[i]);
                }
            }
            LobotomyPlugin.Log.LogDebug($"[LobotomyBattleSequencer.MoveOpponentCards] CardsToMove: {slots2.Count}");
            ViewManager.Instance.SwitchToView(View.Board);
            yield return CardScramble.RandomiseCardsInSlots(slots2, rand, sortPredicate: delegate (CardSlot s) {
                return s.Card.HasAbility(HighStrung.ability) ? 100 : 0;
            });
        }

        public virtual bool RespondsToPreScalesChangedRef(int damage, int numWeights, bool toPlayer)
        {
            return !toPlayer && !PlayerCanWinThroughScaleDamage;
        }
        public virtual int CollectPreScalesChangedRef(int damage, ref int numWeights, ref bool toPlayer)
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

        public void CreateTargetIcon(CardSlot targetSlot, Color materialColour = default)
        {
            GameObject gameObject = TargetIconHelper.CreateTargetIcon(targetSlot, materialColour);
            targetIcons.Add(gameObject);
        }
        public void CleanUpTargetIcon(GameObject icon)
        {
            TargetIconHelper.CleanUpTargetIcon(icon);
        }
        public void CleanupTargetIcons()
        {
            targetIcons.ForEach(delegate (GameObject x)
            {
                if (x != null) CleanUpTargetIcon(x);
            });
            targetIcons.Clear();
        }

        public override IEnumerator PlayerCombatEnd()
        {
            yield return base.PlayerCombatEnd();
            currentExcessBones = 0;
        }

        public virtual bool RespondsToCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage)
        {
            if (!opposingSlot.IsPlayerSlot && (attacker.OpponentCard ? damage < 0 : damage > 0))
            {
                // if there's a cap on positive scale damage and we have hit that cap,
                if (!PlayerCanWinThroughScaleDamage && DirectDamageGivesBones && currentExcessBones < MaxExcessBones)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual IEnumerator OnCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage)
        {
            LobotomyPlugin.Log.LogDebug($"[LobotomyBattleSequencer] Direct: {damage} Balance: {LifeManager.Instance.Balance} Max: {HighestPositiveScaleBalance}");

            int bonesToGive = 0;
            // if we are already at our balance cap or we will go over it with this attack
            if (LifeManager.Instance.Balance >= HighestPositiveScaleBalance)
            {
                bonesToGive = Mathf.Min(MaxExcessBones - currentExcessBones, damage);
            }
            else if (LifeManager.Instance.Balance + damage > HighestPositiveScaleBalance)
            {
                bonesToGive = Mathf.Min(MaxExcessBones - currentExcessBones, damage - (HighestPositiveScaleBalance - LifeManager.Instance.Balance));
            }

            if (bonesToGive < 1)
                yield break;

            yield return new WaitForSeconds(0.01f);
            DigUpBones(damage, bonesToGive, opposingSlot);
            currentExcessBones += bonesToGive;
            Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase -= bonesToGive;
        }

        public virtual void DigUpBones(int damage, int bonesToGive, CardSlot targetSlot)
        {
            ResourcesManager.Instance.PlayerBones += bonesToGive;
            Singleton<TableVisualEffectsManager>.Instance?.ThumpTable(0.075f * (float)Mathf.Min(10, bonesToGive));

            for (int i = 0; i < bonesToGive; i++)
            {
                Part1ResourcesManager manager = ResourcesManager.Instance as Part1ResourcesManager;
                GameObject gameObject = GameObject.Instantiate(manager.boneTokenPrefab);
                BoneTokenInteractable component = gameObject.GetComponent<BoneTokenInteractable>();
                Rigidbody tokenRB = gameObject.GetComponent<Rigidbody>();
                Vector3 vector = new(0f, 0f, 0.75f);

                tokenRB.Sleep();
                gameObject.transform.position = targetSlot.transform.position + vector + new Vector3(i * 0.1f, 0f, i * 0.1f);
                gameObject.transform.eulerAngles = UnityEngine.Random.insideUnitSphere;

                Vector3 endValue = manager.GetRandomLandingPosition() + Vector3.up;
                Tween.Position(component.transform, endValue, 0.25f, 0.5f, Tween.EaseInOut, Tween.LoopType.None, null, delegate
                {
                    tokenRB.WakeUp();
                    manager.PushTokenDown(tokenRB);
                });

                manager.boneTokens.Add(component);
                manager.isOrganized = false;
            }
        }
    }
}
