using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// The second-strongest Violet Ordeal.
    /// Violet Ordeals are religious themed.
    /// Noon will begin with the Violet Dawn, with Noon appearing a few turns after or once all Dawns are defeated.
    /// Cards required: 3, 4, 5
    /// Valid regions: 0, 1, 2
    /// </summary>
    public class OrdealVioletNoon : OrdealVioletDawn, IPlayerTurnEnd {
        private CardSlot[] loveSlots = null;
        private bool spawnedNoon = false;
        private int minTurnToForceNoon = 5;

        public override bool ShouldExtendBattle() {
            if (loveSlots != null || spawnedNoon) {
                return false;
            }
            return base.ShouldExtendBattle();
        }

        public override IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) {
            if (BeginNoon()) {
                yield return HelperMethods.ChangeCurrentView(View.Board);
                int slotIndex = UnityEngine.Random.Range(0, BoardManager.Instance.OpponentSlotsCopy.Count - 1);
                loveSlots = new CardSlot[] { BoardManager.Instance.OpponentSlotsCopy[slotIndex], BoardManager.Instance.OpponentSlotsCopy[slotIndex + 1] };
                CreateTargetIcon(loveSlots[0], GameColors.Instance.glowRed);
                CreateTargetIcon(loveSlots[1], GameColors.Instance.glowRed);
                yield return new WaitForSeconds(0.5f);
            }
            yield return base.OnOpponentTurnEnd(opponentTurnSkipped);
        }

        private bool BeginNoon() {
            if (loveSlots == null && !spawnedNoon) {
                return Opponent.NumTurnsTaken >= minTurnToForceNoon;
            }
            return false;
        }

        public override void ModifySpawnedCard(PlayableCard card) {
            if (card.Info.name == Cards.grantUsLove) {
                int tier = RunState.CurrentRegionTier;
                if (tier > 0) {
                    CardModificationInfo mod = new(0, tier * 3);
                    if (tier > 1) {
                        mod.attackAdjustment++;
                    }
                    card.Info.Mods.Add(mod);
                }
            }
        }

        public override void TryAddOrdealRandomBuff(PlayableCard card) {
            
        }

        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            if (encounterData.Difficulty > 5) {
                minTurnToForceNoon--;
            }
            targetIconPrefab = AssetManager.warningTargetPrefab;
            return 1 + base.ConstructOrdealBlueprint(encounterData, baseDifficulty);
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
            if (OrdealCounterManager.Instance.amountLeft - amountKilledThisTurn == 1) {
                ValidCards.Add(Cards.grantUsLove); // prevent Ordeal from ending before Noon is killed
                minTurnToForceNoon = Opponent.NumTurnsTaken;
            }
        }



        public IEnumerator OnPlayerTurnEnd() {
            CleanupTargetIcons();

            if (loveSlots[0].Card != null) yield return loveSlots[0].Card.DieTriggerless();

            if (loveSlots[1].Card != null) yield return loveSlots[1].Card.DieTriggerless();

            yield return HelperMethods.ChangeCurrentView(View.OpponentQueue);
            yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName(Cards.grantUsLove), loveSlots[0]);
            CameraEffects.Instance.Shake(1f, 0.75f);
            yield return new WaitForSeconds(0.2f);
            AudioController.Instance.PlaySound3D("map_slam", MixerGroup.TableObjectsSFX, Singleton<BoardManager>.Instance.transform.position);
            yield return new WaitForSeconds(1f);
            loveSlots = null;
            spawnedNoon = true;
        }

        public bool RespondsToPlayerTurnEnd() => loveSlots != null && !spawnedNoon;
        public int PlayerTurnEndPriority() => 0;
    }
}