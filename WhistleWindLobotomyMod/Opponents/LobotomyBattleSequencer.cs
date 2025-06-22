using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Abstract class containing logic shared by all custom opponents.
    /// </summary>
    public abstract class LobotomyBattleSequencer : BossBattleSequencer, IOpponentTurnEnd, IOnPreScalesChangedRef, IOnCardDealtDamageDirectly {
        public int currentExcessBones = 0;
        public bool drewInitialHand = false;

        public GameObject targetIconPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CannonTargetIcon");
        public readonly List<GameObject> targetIcons = new();

        /// <summary>
        /// How far the scales can tip towards the opponent. Values below 5 mean the player cannot win by dealing direct damage.
        /// </summary>
        public virtual int HighestPositiveScaleBalance { get; set; } = 5;
        public bool PlayerCanWinThroughScaleDamage => HighestPositiveScaleBalance > 4;
        public virtual bool DirectDamageGivesBones { get; set; } = true;
        public virtual int MaxExcessBones { get; } = 2;

        public bool RespondsToOpponentTurnEnd(bool opponentTurnSkipped) => true;
        public int OpponentTurnEndPriority(bool opponentTurnSkipped) => 0;
        public virtual IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) {
            currentExcessBones = 0;
            yield break;
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

        public virtual bool RespondsToPreScalesChangedRef(int damage, int numWeights, bool toPlayer) {
            return !toPlayer && !PlayerCanWinThroughScaleDamage;
        }
        public virtual int CollectPreScalesChangedRef(int damage, ref int numWeights, ref bool toPlayer) {
            if (LifeManager.Instance.DamageUntilPlayerWin == 1)
                return numWeights = 0;

            if (damage >= LifeManager.Instance.DamageUntilPlayerWin) {
                numWeights = Mathf.Min(LifeManager.Instance.DamageUntilPlayerWin - 1, numWeights);
                return LifeManager.Instance.DamageUntilPlayerWin - 1;
            }

            return damage;
        }

        public void CreateTargetIcon(CardSlot targetSlot, Color materialColour = default) {
            GameObject gameObject = TargetIconHelper.CreateTargetIcon(targetSlot, materialColour);
            targetIcons.Add(gameObject);
        }
        public void CleanUpTargetIcon(GameObject icon) {
            TargetIconHelper.CleanUpTargetIcon(icon);
        }
        public void CleanupTargetIcons() {
            targetIcons.ForEach(delegate (GameObject x) {
                if (x != null) CleanUpTargetIcon(x);
            });
            targetIcons.Clear();
        }

        public virtual bool RespondsToCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) {
            if (!opposingSlot.IsPlayerSlot) {
                return attacker.OpponentCard ? damage < 0 : damage > 0;
            }
            return false;
        }

        public virtual IEnumerator OnCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) {
            if (!DirectDamageGivesBones || PlayerCanWinThroughScaleDamage || currentExcessBones >= MaxExcessBones) {
                yield break;
            }

            int bonesToGive = Mathf.Min(MaxExcessBones - currentExcessBones, damage) - (HighestPositiveScaleBalance - LifeManager.Instance.Balance);
            
            if (bonesToGive > 0) {
                yield return new WaitForSeconds(0.01f);
                DigUpBones(damage, bonesToGive, opposingSlot);
                currentExcessBones += bonesToGive;
                Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase -= bonesToGive;
            }
            LobotomyPlugin.Log.LogDebug($"[LobotomyBattleSequencer] Dmg: {damage} currentBones/toGive: {currentExcessBones}/{bonesToGive} Balance: {LifeManager.Instance.Balance}");
        }

        public virtual void DigUpBones(int damage, int bonesToGive, CardSlot targetSlot) {
            ResourcesManager.Instance.PlayerBones += bonesToGive;
            Singleton<TableVisualEffectsManager>.Instance?.ThumpTable(0.075f * (float)Mathf.Min(10, bonesToGive));

            for (int i = 0; i < bonesToGive; i++) {
                Part1ResourcesManager manager = ResourcesManager.Instance as Part1ResourcesManager;
                GameObject gameObject = GameObject.Instantiate(manager.boneTokenPrefab);
                BoneTokenInteractable component = gameObject.GetComponent<BoneTokenInteractable>();
                Rigidbody tokenRB = gameObject.GetComponent<Rigidbody>();
                Vector3 vector = new(0f, 0f, 0.75f);

                tokenRB.Sleep();
                gameObject.transform.position = targetSlot.transform.position + vector + new Vector3(i * 0.1f, 0f, i * 0.1f);
                gameObject.transform.eulerAngles = UnityEngine.Random.insideUnitSphere;

                Vector3 endValue = manager.GetRandomLandingPosition() + Vector3.up;
                Tween.Position(component.transform, endValue, 0.25f, 0.5f, Tween.EaseInOut, Tween.LoopType.None, null, delegate {
                    tokenRB.WakeUp();
                    manager.PushTokenDown(tokenRB);
                });

                manager.boneTokens.Add(component);
                manager.isOrganized = false;
            }
        }

        public override List<CardInfo> GetFixedOpeningHand() => drewInitialHand ? CardDrawPiles.Instance.Deck.GetFairHand(5, false) : null;
        public virtual IEnumerator PreDrawOpeningHand() {
            if (drewInitialHand) {
                CardDrawPiles3D.Instance.sidePile.Draw();
                yield return CardDrawPiles3D.Instance.DrawFromSidePile();
                yield return new WaitForSeconds(0.1f);
            }
        }
        public virtual IEnumerator PostDrawOpeningHand() {
            ViewManager.Instance.SwitchToView(View.Hand);
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_RETURN_CARD"));
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_RETURN_CARD_ALL"));
            yield return new WaitForSeconds(0.4f);
            if (TurnNumber == 0) {
                if (LobOpponentUtils.IsCustomBoss(out ApocalypseBossOpponent opp)) {
                    yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossRecall", TextDisplayer.MessageAdvanceMode.Input);
                }
                else if (!DialogueEventsData.EventIsPlayed("OrdealRecall")) {
                    yield return TextDisplayer.Instance.PlayDialogueEvent("OrdealRecall", TextDisplayer.MessageAdvanceMode.Input);
                }

                if (!DialogueEventsData.EventIsPlayed("RecallMechanic")) {
                    yield return TextDisplayer.Instance.PlayDialogueEvent("RecallMechanic", TextDisplayer.MessageAdvanceMode.Input);
                }
            }
        }
    }

    [HarmonyPatch]
    internal static class LobotomyBattleSetUpPatch {
        [HarmonyPostfix, HarmonyPatch(typeof(CardDrawPiles3D), nameof(CardDrawPiles3D.DrawOpeningHand))]
        public static IEnumerator CallPrePostDrawOpeningHand(IEnumerator enumerator) {
            if (!SaveManager.SaveFile.IsPart1 || TurnManager.Instance.SpecialSequencer is not LobotomyBattleSequencer sequence) {
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
