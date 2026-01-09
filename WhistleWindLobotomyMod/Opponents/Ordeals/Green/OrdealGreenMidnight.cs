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

        private int maxCooldownPeriod = 1;
        private readonly int MaxActivePeriod = 4;

        public int phaseCountdown = 0;

        private bool isActive = false;
        private bool justActivated = false;

        private HelixLight wanderingLight;
        private HelixLight stationaryLight;

        private HelixLight CreateLaser() {
            GameObject obj = Instantiate(LobOpponentUtils.HelixBossLaserPrefab);
            return obj.AddComponent<HelixLight>();
        }

        private void SelectStartingLaserSlot() {
            int randomIdx = UnityEngine.Random.RandomRangeInt(0, BoardManager.Instance.PlayerSlotsCopy.Count);
            CardSlot startingSlot = BoardManager.Instance.PlayerSlotsCopy[randomIdx];
            wanderingLight.Initialise(startingSlot);
            stationaryLight.Initialise(startingSlot);
        }

        private void CleanUpLasers() {
            AudioController.Instance.StopAllLoops();
            wanderingLight.system.Pause();
            stationaryLight.system.Pause();
            wanderingLight.gameObject.SetActive(false);
            stationaryLight.gameObject.SetActive(false);
        }

        public override IEnumerator OpponentCombatEnd() {
            if (justActivated) {
                isActive = true;
                justActivated = false;
                phaseCountdown = MaxActivePeriod;
                yield return BeginLaserSequence();
                yield return new WaitForSeconds(0.75f);
                yield return UpdateCounterIcon();
            }
            else {
                phaseCountdown--;
                
                if (isActive) {
                    ViewManager.Instance.SwitchToView(View.Default);
                    yield return wanderingLight.UpdateCurrentSlot();
                    yield return UpdateCounterIcon();

                    if (phaseCountdown == 0) {
                        LobotomyPlugin.Log.LogDebug("Deactivate Helix");
                        phaseCountdown = maxCooldownPeriod;
                        yield return CleanUpActivePhase();
                    }
                }
                else if (phaseCountdown == 1) {
                    // turn before activating the lasers, initialise them and play setup animations
                    yield return UpdateCounterIcon();
                    yield return PreActivePhase();
                }
            }
        }

        private IEnumerator InitialiseActivePhase() {
            wanderingLight = CreateLaser();
            stationaryLight = CreateLaser();

            // for the first activation, open the helix shell
            HelixAnimator.SetBool("open", true);
            AudioController.Instance.PlaySound2D("helix_open", MixerGroup.TableObjectsSFX);
            yield return new WaitForSeconds(8.5f);
            HelixAnimator.SetTrigger("lights"); // do initial light activation sequence
            yield return new WaitForSeconds(2f);
        }

        private IEnumerator PreActivePhase() {
            yield return HelperMethods.ChangeCurrentView(View.OpponentQueue, endDelay: 0.5f, lockAfter: true);
            if (!HelixAnimator.GetBool("open")) {
                yield return InitialiseActivePhase();
            }
            else {
                HelixAnimator.SetBool("activate", true);
                HelixAnimator.SetFloat("gears", -1f);
                AudioController.Instance.PlaySound2D("helix_open", MixerGroup.TableObjectsSFX);
                yield return new WaitForSeconds(3.5f);
                HelixAnimator.SetBool("activate", false);
                HelixAnimator.SetFloat("gears", 1f);
            }

            yield return HelperMethods.ChangeCurrentView(View.Default);
            foreach (CardSlot s in BoardManager.Instance.PlayerSlotsCopy) {
                CreateTargetIcon(s, AssetManager.warningTargetPrefab);
            }

            SelectStartingLaserSlot();

            ViewManager.Instance.Controller.LockState = ViewLockState.Unlocked;
            justActivated = true;
        }

        private IEnumerator BeginLaserSequence() {
            yield return HelperMethods.ChangeCurrentView(View.OpponentQueue, endDelay: 0.5f);
            // gears speed up and move in reverse and gun rattles around
            HelixAnimator.SetBool("fire", true);
            HelixAnimator.SetFloat("gears", -2f);
            AudioController.Instance.PlaySound2D("uberbot_beam_activate#2", MixerGroup.TableObjectsSFX, 1.5f);
            yield return new WaitForSeconds(0.75f);
            ViewManager.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.2f);
            //BeginEmissionLoop();
            // return gears to normal speed+rot
            // enable laser + light emissions (lights blink periodically)
            HelixAnimator.SetFloat("gears", 1f);
            HelixAnimator.SetBool("laser", true);
            HelixAnimator.SetTrigger("lights");
            
            CleanupTargetIcons();
            wanderingLight.gameObject.SetActive(true);
            stationaryLight.gameObject.SetActive(true);
            wanderingLight.system.Play();
            stationaryLight.system.Play();
            AudioController.Instance.StopAllLoops();
            AudioController.Instance.SetLoopAndPlay("uberbot_beam_looping");
            yield return new WaitForSeconds(0.5f);
        }

        private IEnumerator CleanUpActivePhase() {
            yield return HelperMethods.ChangeCurrentView(View.OpponentQueue);
            AudioController.Instance.PlaySound2D("helix_deactivate", MixerGroup.TableObjectsSFX);
            CleanUpLasers();

            // return gears to normal speed+rot, return gun to idle position
            // disable laser and light emissions
            HelixAnimator.SetFloat("gears", 1f);
            HelixAnimator.SetBool("laser", false);
            HelixAnimator.SetTrigger("lights");
            HelixAnimator.SetBool("fire", false);
            
            //EndEmissionLoop();
            yield return new WaitForSeconds(3.5f);
            yield return UpdateCounterIcon();
            isActive = false;
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
            if (HighestPositiveScaleBalance < 0) {
                yield return LifeManager.Instance.ShowDamageSequence(HighestPositiveScaleBalance, 1, toPlayer: true);
                yield return new WaitForSeconds(0.5f);
            }

            if (phaseCountdown > 1) {
                maxCooldownPeriod = 2;
            }
            // pre heat the active phase if the initial countdown is <= 1
            if (phaseCountdown < 2) {
                yield return PreActivePhase();
                // activate immediately if the initial countdown is <= 0
                if (phaseCountdown < 1) {
                    yield return OpponentCombatEnd();
                    yield break;
                }
            }

            yield return UpdateCounterIcon();
            yield return new WaitForSeconds(0.5f);
            ViewManager.Instance.SwitchToView(View.Default);
        }

        public override void ModifySpawnedCard(PlayableCard card) {
            if (card.Info.name != Cards.lastHelix)
                return;

            Helix = card;
            Camera liveRenderCam = Singleton<CardRenderCamera>.Instance.GetLiveRenderCamera(Helix.StatsLayer);
            // get the animator from the actual rendered portrait offscreen, not the animator on the card object on the board
            Transform t = liveRenderCam.transform.GetChild(1).GetChild(0).GetChild(0);
            HelixAnimator = t.GetChild(t.childCount - 1).GetComponentInChildren<Animator>();
            
            int gate = RunState.CurrentRegionTier + RunState.Run.DifficultyModifier - 1;
            if (gate > 0) {
                Helix.Info.baseAttack = 1;
                Helix.Info.Mods.Add(new(Ability.AllStrike));
                Helix.TriggerHandler.AddAbility(Ability.AllStrike);
                if (gate > 2) {
                    Helix.Info.baseAttack++;
                }
            }
            Helix.Info.baseHealth += RunState.CurrentRegionTier * 10;
        }

        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            HighestPositiveScaleBalance = Mathf.Max(-2, 1 - RunState.CurrentRegionTier - RunState.Run.DifficultyModifier);
            ValidCards.Add(Cards.lastHelix);
            phaseCountdown = 3 - RunState.CurrentRegionTier - RunState.Run.DifficultyModifier;
            
            EncounterData.StartCondition cond = new() {
                cardsInOpponentSlots = new CardInfo[] { CardLoader.GetCardByName(Cards.lastHelix), null, null, null }
            };
            encounterData.startConditions.Add(cond);
            return 1;
        }

        // clean up lasers before ending the battle
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            if (card == Helix) {
                CleanUpLasers();
            }
            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
        }
    }
}