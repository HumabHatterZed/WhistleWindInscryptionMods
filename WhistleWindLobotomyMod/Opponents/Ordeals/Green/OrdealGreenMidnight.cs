using DiskCardGame;
using InscryptionAPI.Slots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using static UnityEngine.GraphicsBuffer;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Last Helix:
    ///     Stage 1: Cooldown
    ///         Last Helix is inactive, player can use this period to prepare for active phase and deal damage
    ///         - Reduce countdown at the end of opponent combat
    ///         - When countdown reaches 0, enter stage 2
    ///     Stage 2: Active
    ///         Last Helix lasers 2 spaces on the board. Target 1 does not move while Target 2 moves right-to-left across the board lanes in real-time.
    ///         - Cards in lasered spaces are destroyed.
    ///         - Reduce countdown at the end of opponent combat
    ///         - When countdown reaches 0, reset cooldown, disable lasers and enter stage 1
    ///         
    /// Animation triggers:
    /// set_active_phase
    /// set_fire_laser
    /// fire_laser
    /// set_idle_phase
    /// </summary>
    public class OrdealGreenMidnight : OrdealBattleSequencer {
        private PlayableCard Helix { get; set; }
        private Animator HelixAnimator;

        private readonly int MaxCooldownPeriod = 2;
        private readonly int MaxActivePeriod = 4;

        public int phaseCountdown = 0;

        public bool isActive = false;
        public bool justActivated = false;

        public HelixLight wanderingLight;
        public HelixLight stationaryLight;

        private IEnumerator BeginLaserSequence() {
            CleanupTargetIcons();

            ViewManager.Instance.SwitchToView(View.Default);
            AudioController.Instance.PlaySound3D("uberbot_beam_activate#2", MixerGroup.TableObjectsSFX, Helix.Slot.transform.position);
            yield return new WaitForSeconds(0.5f);
            wanderingLight.gameObject.SetActive(true);
            stationaryLight.gameObject.SetActive(true);
            wanderingLight.system.Play();
            stationaryLight.system.Play();
            AudioController.Instance.StopAllLoops();
            AudioController.Instance.SetLoopAndPlay("uberbot_beam_looping");
            yield return new WaitForSeconds(0.5f);
        }

        private HelixLight CreateLaser() {
            GameObject obj = Instantiate(LobOpponentUtils.HelixBossLaserPrefab);
            return obj.AddComponent<HelixLight>();
        }

        private void CleanUpLasers() {
            AudioController.Instance.StopAllLoops();
            wanderingLight.system.Pause();
            stationaryLight.system.Pause();
            wanderingLight.gameObject.SetActive(false);
            stationaryLight.gameObject.SetActive(false);
        }

        /// <summary>
        /// Reduce counter
        /// When counter == 0
        /// -> if not active, activate Helix and set counter to MaxActive
        /// -> if active, deactivate Helix and set counter to MaxCooldown
        /// When counter > 0
        /// -> if not active, do nothing
        /// -> if active, ...
        /// </summary>
        public override IEnumerator OpponentCombatEnd() {
            phaseCountdown--;
            yield return UpdateCounterIcon();

            if (isActive) {
                if (phaseCountdown == 0) {
                    LobotomyPlugin.Log.LogDebug("Deactivate Helix");
                    isActive = false;
                    phaseCountdown = MaxCooldownPeriod;
                    yield return CleanUpActivePhase();
                    yield return QueueTheVanguard();
                }
                else {
                    ViewManager.Instance.SwitchToView(View.Default);
                    yield return wanderingLight.UpdateCurrentSlot();
                }
            }
            else if (phaseCountdown == 1) {
                // turn before activating the lasers, initialise them and play setup animations
                yield return InitialiseActivePhase();
            }
            else if (phaseCountdown == 0) {
                isActive = true;
                phaseCountdown = MaxActivePeriod;
                yield return BeginLaserSequence();
                yield return new WaitForSeconds(0.75f);
                yield return UpdateCounterIcon();
            }
        }

        private string GetVanguardByDifficulty(int difficulty, int seed) {
            string retval = null;
            int randVal = SeededRandom.Range(Mathf.Max(0, RunState.Run.DifficultyModifier - 1), difficulty, seed);

            switch (randVal) {
                case 0:
                    retval = Cards.doubtA;
                    break;
                case 1:
                    retval = Cards.doubtB;
                    break;
                case 2:
                    retval = Cards.doubtY;
                    break;
                case 3:
                    retval = Cards.doubtO;
                    break;
                case 4:
                    retval = Cards.doubtProcessDown;
                    break;
            }
            return retval;
        }
        private IEnumerator QueueTheVanguard() {
            int baseDifficulty = RunState.CurrentRegionTier + RunState.Run.DifficultyModifier;
            int seed = base.GetRandomSeed() + TurnManager.Instance.TurnNumber;
            string vanguard1 = GetVanguardByDifficulty(baseDifficulty, seed++);
            string vanguard2 = GetVanguardByDifficulty(baseDifficulty, seed);

            if (vanguard1 != null) {
                yield return Opponent.QueueCard(CardLoader.GetCardByName(vanguard1), BoardManager.Instance.OpponentSlotsCopy[0]);
            }
            if (vanguard2 != null) {
                yield return Opponent.QueueCard(CardLoader.GetCardByName(vanguard2), BoardManager.Instance.OpponentSlotsCopy[BoardManager.Instance.OpponentSlotsCopy.Count - 1]);
            }
        }

        private IEnumerator InitialiseActivePhase() {
            int randomIdx = UnityEngine.Random.RandomRangeInt(0, BoardManager.Instance.PlayerSlotsCopy.Count);
            CardSlot startingSlot = BoardManager.Instance.PlayerSlotsCopy[randomIdx];
            justActivated = true;

            if (wanderingLight == null) {
                wanderingLight = CreateLaser();
                stationaryLight = CreateLaser();
            }

            wanderingLight.Initialise(startingSlot);
            stationaryLight.Initialise(startingSlot);

            ViewManager.Instance.SwitchToView(View.Default);
            HelixAnimator.SetTrigger("set_active_phase");
            AudioController.Instance.PlaySound2D("helix_open", MixerGroup.TableObjectsSFX);
            yield return new WaitForSeconds(3f);

            foreach (CardSlot s in BoardManager.Instance.PlayerSlotsCopy) {
                CreateTargetIcon(s, AssetManager.warningTargetPrefab);
            }
        }

        private IEnumerator CleanUpActivePhase() {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            AudioController.Instance.PlaySound2D("helix_deactivate", MixerGroup.TableObjectsSFX);
            CleanUpLasers();
            yield return new WaitForSeconds(3f);
            ViewManager.Instance.SwitchToView(View.Default);
            AudioController.Instance.PlaySound2D("helix_open", MixerGroup.TableObjectsSFX);
            yield return new WaitForSeconds(3f);
            yield return UpdateCounterIcon();
        }

        private IEnumerator UpdateCounterIcon() {
            yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter, endDelay: 0.5f);
            if (OrdealCounterManager.Instance.Dirty) {
                yield return OrdealCounterManager.Instance.UpdateDisplayedValue(phaseCountdown);
            }
            else {
                OrdealCounterManager.Instance.EnableConsole(false);
                yield return new WaitForSeconds(0.8f);
                OrdealCounterManager.Instance.UpdateConsole(ordealTier, phaseCountdown, "turns left");
                OrdealCounterManager.Instance.EnableConsole(true);
            }

            yield return new WaitForSeconds(0.75f);
        }

        /// <summary>
        /// Display the turns left counter on the monitor at the start of the encounter, after the intro and deck piles have been setup.
        /// </summary>
        public override IEnumerator PreHandDraw() {
            yield return UpdateCounterIcon();
            yield return new WaitForSeconds(0.5f);
            ViewManager.Instance.SwitchToView(View.Default);
        }

        public override void ModifyQueuedCard(PlayableCard card) {
            if (TurnManager.Instance.TurnNumber > 11 - RunState.Run.DifficultyModifier - RunState.CurrentRegionTier) {
                card.Info.Mods.Add(new(Ability.Sniper));
            }
            base.ModifyQueuedCard(card);
        }

        public override void ModifySpawnedCard(PlayableCard card) {
            LobotomyPlugin.Log.LogDebug("[GreenMidnight] ModifySpawnedCard: " + card.Info.name);
            if (card.Info.name != Cards.lastHelix)
                return;

            Helix = card;
            Helix.Status.damageTaken -= RunState.Run.regionTier * 10;
            HelixAnimator = Helix.Anim.Anim;
        }
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) => true;
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            if (card == Helix) {
                CleanUpLasers();
            }
            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
        }
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            //HighestPositiveScaleBalance = 0;
            ValidCards.Add(Cards.lastHelix);
            isActive = false;
            phaseCountdown = 2;

            CardInfo start1, start2;

            switch (RunState.CurrentRegionTier + RunState.Run.DifficultyModifier - 1) {
                case 0:
                    start1 = null;
                    start2 = null;
                    break;
                case 1:
                    start1 = CardLoader.GetCardByName(Cards.doubtA);
                    start2 = CardLoader.GetCardByName(Cards.doubtA);
                    break;
                case 2:
                    start1 = CardLoader.GetCardByName(Cards.doubtB);
                    start2 = CardLoader.GetCardByName(Cards.doubtB);
                    break;
                case 3:
                    start1 = CardLoader.GetCardByName(Cards.doubtB);
                    start2 = CardLoader.GetCardByName(Cards.doubtY);
                    break;
                case 4:
                    start1 = CardLoader.GetCardByName(Cards.doubtProcessDown);
                    start2 = CardLoader.GetCardByName(Cards.doubtY);
                    break;
                default:
                    start1 = CardLoader.GetCardByName(Cards.doubtProcessDown);
                    start2 = CardLoader.GetCardByName(Cards.doubtProcessDown);
                    break;
            }

            EncounterData.StartCondition cond = new() {
                cardsInOpponentSlots = new CardInfo[] { start1, CardLoader.GetCardByName(Cards.lastHelix), null, start2 } // Last Helix is guaranteed to appear in the second slot
            };
            encounterData.startConditions.Add(cond);
            return 1;
        }
    }
}