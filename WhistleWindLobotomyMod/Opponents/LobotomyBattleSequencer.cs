using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Abstract class containing logic shared by all custom opponents.
    /// </summary>
    public abstract class LobotomyBattleSequencer : BossBattleSequencer, IOpponentTurnEnd, IOnPreScalesChangedRef, IOnCardDealtDamageDirectly, IModifyDirectDamage {
        public bool drewInitialHand = false;

        protected int bonesToGive;
        protected int directDamageCache;
        public int currentExcessBones;

        public GameObject targetIconPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CannonTargetIcon");
        public readonly List<GameObject> targetIcons = new();

        /// <summary>
        /// How far the scales can tip towards the opponent. Values below 5 mean the player cannot win by dealing direct damage.
        /// </summary>
        public int HighestPositiveScaleBalance { get; set; } = 5;
        public bool PlayerCanWinThroughScaleDamage => HighestPositiveScaleBalance > 4;
        public virtual bool DirectDamageGivesBones { get; set; } = true;
        public virtual int MaxExcessBones { get; } = 2;
        public virtual int MaxOwnedBones { get; } = 20;

        public virtual IEnumerator MoveOpponentCards() {
            int rand = base.GetRandomSeed() + TurnNumber;
            int numRemoved = 0;
            List<CardSlot> slots = CardScramble.GetOccupiedSlotsMovable(BoardManager.Instance.OpponentSlotsCopy);
            for (int i = 0; i < slots.Count; i++) {
                if (!slots[i].Card.Info.HasTrait(LobotomyCardManager.PriorityMovement) && SeededRandom.Value(rand++) <= (0.75f - numRemoved * 0.15f)) {
                    slots.Remove(slots[i]);
                    numRemoved++;
                    i--;
                }
            }

            if (slots.Count > 0) {
                ViewManager.Instance.SwitchToView(View.Board);
                yield return CardScramble.RandomiseCardsInSlots(slots, rand, sortPredicate: delegate (CardSlot s) {
                    return s.Card.Info.HasTrait(LobotomyCardManager.PriorityMovement) ? 100 : 0;
                });
            }
        }

        #region Triggers
        public bool RespondsToOpponentTurnEnd(bool opponentTurnSkipped) => true;
        public int OpponentTurnEndPriority(bool opponentTurnSkipped) => 0;
        public virtual IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) {
            currentExcessBones = 0;
            yield break;
        }
        public virtual bool RespondsToPreScalesChangedRef(int damage, int numWeights, bool toPlayer) => !toPlayer && !PlayerCanWinThroughScaleDamage;
        public virtual int CollectPreScalesChangedRef(int damage, ref int numWeights, ref bool toPlayer) {
            if (LifeManager.Instance.Balance + damage >= HighestPositiveScaleBalance) {
                numWeights = Mathf.Min(numWeights, HighestPositiveScaleBalance - LifeManager.Instance.Balance);
                return HighestPositiveScaleBalance - LifeManager.Instance.Balance;
            }

            return damage;
        }
        #endregion

        #region Excess Bones
        public virtual bool RespondsToModifyDirectDamage(CardSlot target, int damage, PlayableCard attacker, int originalDamage) {
            if (!target.IsPlayerSlot) {
                return attacker.OpponentCard ? damage < 0 : damage > 0;
            }
            return false;
        }
        public int TriggerPriority(CardSlot target, int damage, PlayableCard attacker) => int.MinValue;
        public virtual int OnModifyDirectDamage(CardSlot target, int damage, PlayableCard attacker, int originalDamage) {
            directDamageCache = 0;
            // excess bones are only gained in battles where the player cannot win via scale damage
            // they also cannot be gained if the player already owns a certain amount of Bones, to prevent excessive token gain
            if (!PlayerCanWinThroughScaleDamage && ResourcesManager.Instance.PlayerBones < MaxOwnedBones) {
                int damageToHighestBalance = Mathf.Max(0, HighestPositiveScaleBalance - LifeManager.Instance.Balance - TurnManager.Instance.DamageDealtThisTurn);
                int excessDamageDealt = damageToHighestBalance - damage;

                LobotomyPlugin.Log.LogInfo($"[LobotomyBattle] Dmg:{damage} ToBal:{damageToHighestBalance} Ex:{excessDamageDealt} {TurnManager.Instance.CombatPhaseManager.DamageDealtThisPhase}");
                // if damage will exceed the highest balance, only deal enough damage to reach it
                if (excessDamageDealt < 1) {
                    if (DirectDamageGivesBones && currentExcessBones < MaxExcessBones) {
                        bonesToGive = Mathf.Min(MaxExcessBones - currentExcessBones, -excessDamageDealt);
                    }

                    directDamageCache = damage - damageToHighestBalance;
                    TurnManager.Instance.CombatPhaseManager.DamageDealtThisPhase += directDamageCache;
                    return damageToHighestBalance;
                }
            }
            return damage;
        }

        public virtual bool RespondsToCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) => bonesToGive > 0;
        public virtual IEnumerator OnCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) {
            yield return new WaitForSeconds(0.01f);
            DigUpBones(damage, bonesToGive, opposingSlot);
            currentExcessBones += bonesToGive;
            bonesToGive = 0;
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
        #endregion

        #region Targets
        public void CreateTargetIcon(CardSlot targetSlot, GameObject prefab, Color materialColour = default) {
            GameObject gameObject = TargetIconHelper.CreateTargetIcon(targetSlot, prefab, materialColour);
            targetIcons.Add(gameObject);
        }
        public void CreateTargetIcon(CardSlot targetSlot, Color materialColour = default) {
            GameObject gameObject = TargetIconHelper.CreateTargetIcon(targetSlot, targetIconPrefab, materialColour);
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
        #endregion

        #region Opening Hoof
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
        #endregion
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
