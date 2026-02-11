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
        
        // use to track how much excess damage has been dealt past the maximum allowed scale balance
        // reset every round end/direct attack dealt
        protected int directDamageCache;

        protected int bonesToGive; // use to determine how many bones should be given for excess damage
        protected int bonesDugUpThisTurn = 0; // keep track of num of bones dug up each turn

        public GameObject targetIconPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CannonTargetIcon");
        public readonly List<GameObject> targetIcons = new();

        /// <summary>
        /// How far the scales can tip towards the opponent. Values below 5 mean the player cannot win by dealing direct damage.
        /// </summary>
        public int HighestPositiveScaleBalance { get; set; } = 5;
        public bool PlayerCanWinThroughScaleDamage => HighestPositiveScaleBalance > 4;
        public virtual bool DirectDamageGivesBones { get; set; } = true;

        public virtual int MaxBonesPerAttack { get; } = 2;
        public virtual int MaxBonesPerTurn { get; } = 8;
        public virtual int MaxBonesOwned { get; } = 15;

        /// <summary>
        /// Resets the scales to 0 or below if HighestPositiveScaleBalance is negative.
        /// Must use this for LobotomyOpponent's since they otherwise prevent the skip sequence from playing
        /// </summary>
        public IEnumerator ShowResetSequence(bool changeView = true) {
            //allowReset = true;
            // recreate ShowResetSequence with optional view change
            if (Singleton<PlayerHand>.Instance != null) {
                Singleton<PlayerHand>.Instance.PlayingLocked = true;
            }
            if (LifeManager.Instance.scales != null) {
                if (changeView) {
                    Singleton<ViewManager>.Instance.SwitchToView(LifeManager.Instance.scalesView);
                }
                yield return new WaitForSeconds(0.75f);
                yield return LifeManager.Instance.scales.ClearDamage();
            }
            LifeManager.Instance.Reset();
            yield return new WaitForSeconds(0.75f);
            if (changeView) {
                Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.DefaultView);
            }
            
            if (Singleton<PlayerHand>.Instance != null) {
                Singleton<PlayerHand>.Instance.PlayingLocked = false;
            }

            if (HighestPositiveScaleBalance < 0) {
                yield return LifeManager.Instance.ShowDamageSequence(-HighestPositiveScaleBalance, 1, true);
            }
        }

        public virtual IEnumerator MoveOpponentCards() {
            int rand = base.GetRandomSeed() + TurnNumber;
            List<CardSlot> validSlots = CardScramble.GetOccupiedSlotsMovable(BoardManager.Instance.OpponentSlotsCopy);
            List<CardSlot> slotsToRandomise = new();
            for (int i = 0; i < validSlots.Count; i++) {
                if (validSlots[i].Card.Info.HasTrait(LobotomyCardManager.PriorityMovement) || SeededRandom.Value(rand++) <= (0.95f - slotsToRandomise.Count * 0.15f)) {
                    slotsToRandomise.Add(validSlots[i]);
                }
            }
            //LobotomyPlugin.Log.LogDebug($"[LobotomyBattle.MoveOpponentCards] # to move: {slotsToRandomise.Count}");
            if (slotsToRandomise.Count > 0) {
                ViewManager.Instance.SwitchToView(View.Board);
                yield return CardScramble.RandomiseCardsInSlots(slotsToRandomise, BoardManager.Instance.OpponentSlotsCopy, rand, sortPredicate: MoveOpponentCardsSortFunc);
            }
        }

        public virtual int MoveOpponentCardsSortFunc(CardSlot slot) {
            return slot.Card.Info.HasTrait(LobotomyCardManager.PriorityMovement) ? 100 : 0;
        }
        protected void ResetPerRoundVariables() {
            bonesDugUpThisTurn = directDamageCache = 0;
        }
        #region Triggers
        public bool RespondsToOpponentTurnEnd(bool opponentTurnSkipped) => true;
        public int OpponentTurnEndPriority(bool opponentTurnSkipped) => 0;
        public virtual IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) {
            ResetPerRoundVariables();
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

            if (PlayerCanWinThroughScaleDamage) {
                return damage;
            }
            return CalculateExcessDirectDamage(damage);
        }

        public int CalculateExcessDirectDamage(int directDamage) {
            int damageToHighestBalance = Mathf.Max(0, HighestPositiveScaleBalance - LifeManager.Instance.Balance - TurnManager.Instance.DamageDealtThisTurn);
            int excessDamageDealt = directDamage - damageToHighestBalance;

            LobotomyPlugin.Log.LogInfo($"[CalculateExcessDirectDamage] Dmg:{directDamage} ToBal:{damageToHighestBalance} Ex:{excessDamageDealt} {Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase}");

            // if damage will exceed the highest balance, only deal enough damage to reach it
            if (excessDamageDealt > 0) {
                // excess bones are only gained in battles where the player cannot win via scale damage
                // they also cannot be gained if the player already owns a certain amount of Bones, to prevent excessive token gain
                if (DirectDamageGivesBones && ResourcesManager.Instance.PlayerBones < MaxBonesOwned && bonesDugUpThisTurn < MaxBonesPerTurn) {
                    int maxToGive = Mathf.Min(MaxBonesPerAttack, MaxBonesOwned - ResourcesManager.Instance.PlayerBones);
                    bonesToGive = Mathf.Min(maxToGive, directDamage);
                }

                directDamageCache = excessDamageDealt;
                TurnManager.Instance.CombatPhaseManager.DamageDealtThisPhase += directDamageCache;
                return damageToHighestBalance;
            }

            return directDamage;
        }

        public virtual bool RespondsToCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) => bonesToGive > 0;
        public virtual IEnumerator OnCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) {
            yield return new WaitForSeconds(0.01f);
            DigUpBones(damage, bonesToGive, opposingSlot);
            bonesDugUpThisTurn += bonesToGive;
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
