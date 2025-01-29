using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents
{
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
    public class OrdealGreenMidnight : OrdealBattleSequencer
    {
        private PlayableCard Helix;
        private Animator HelixAnimator;

        private readonly int MaxCooldownPeriod = 4 - RunState.Run.regionTier;
        private readonly int MaxActivePeriod = 3 + RunState.Run.regionTier; // account for initial turn of activation

        public int phaseCountdown = 0;

        public bool isActive = false;
        public bool justActivated = false;

        public int activePeriod = 0;
        private int cooldownPeriod = 0;

        private int currentTargetIndex = -1;

        private bool laserExists = false;

        public CardSlot target1 = null;
        public CardSlot target2 = null;

        //private HelixLaserDestructionBehaviour standingLaserBehav = null;
        //private HelixLaserDestructionBehaviour movingLaserBehav = null;

        public bool CleanUpLasers = false;

        public override int GetMinCardsRequired(EncounterData data) => 1;

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) => true;
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (card.OpponentCard && base.RespondsToOtherCardDie(card, deathSlot, fromCombat, killer))
            {
                yield return EndActivePhase(true);
                yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
            }
        }
        private IEnumerator StartLasering()
        {

            yield break;
        }

        private IEnumerator HandleLaser()
        {
            AudioController.Instance.PlaySound3D("uberbot_beam_activate#2", MixerGroup.TableObjectsSFX, Helix.Slot.transform.position);
            yield return new WaitForSeconds(0.5f);
            AudioController.Instance.SetLoopAndPlay("uberbot_beam_looping");

        }

        public override IEnumerator OpponentCombatStart()
        {
            Debug.Log("OpponentCombatEnd");
            if (isActive && justActivated) // the turn after Helix activates
            {
                justActivated = false;
                yield return new WaitForSeconds(1.5f);
                yield return BeginActivePhase();
            }

            yield return base.OpponentCombatStart();
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
        public override IEnumerator OpponentCombatEnd()
        {
            LobotomyPlugin.Log.LogDebug("OpponentCombatEnd");

            phaseCountdown--;
            UpdateCounter();

            if (phaseCountdown > 0)
            {
                if (isActive)
                {

                }
                else
                {
                    yield break;
                }
            }

            if (isActive)
            {
                LobotomyPlugin.Log.LogDebug("Deactivate Helix");

                isActive = false;
                phaseCountdown = MaxCooldownPeriod;
                yield return EndActivePhase(false);
            }
            else
            {
                LobotomyPlugin.Log.LogDebug("Activate Helix");

                isActive = true;
                phaseCountdown = MaxActivePeriod;
                yield return BeginActivePhase();
            }

        }

        private IEnumerator BeginActivePhase()
        {
            //isActive = false;
            /*if (standingLaserBehav != null)
                yield break;*/

            int randomIdx = UnityEngine.Random.RandomRangeInt(0, BoardManager.Instance.PlayerSlotsCopy.Count);
            target1 = BoardManager.Instance.PlayerSlotsCopy[randomIdx];

            HelixAnimator.SetTrigger("set_active_phase");
            AudioController.Instance.PlaySound2D("helix_open", MixerGroup.TableObjectsSFX);
            yield return new WaitForSeconds(5f);
        }
        private IEnumerator EndActivePhase(bool bossDead)
        {
            if (bossDead)
                yield break;

            /*standingLaserBehav.CleanUp();
            movingLaserBehav.CleanUp();
            standingLaserBehav = movingLaserBehav = null;*/
            cooldownPeriod = MaxCooldownPeriod;
            activePeriod = 0;


            yield return HelperMethods.ChangeCurrentView(View.Board);
            AudioController.Instance.PlaySound2D("helix_deactivate", MixerGroup.TableObjectsSFX);
            yield return new WaitForSeconds(3f);
            AudioController.Instance.PlaySound2D("helix_open", MixerGroup.TableObjectsSFX);
            yield return new WaitForSeconds(4f);

            UpdateCounter();

        }

        private void UpdateCounter()
        {
            Debug.Log($"UpdateCounter: {phaseCountdown}");
            string newTex = phaseCountdown <= 0 ? "sigilTower.png" : ("sigilTower_" + phaseCountdown + ".png");
            Helix.RenderInfo.OverrideAbilityIcon(Tower.ability, TextureLoader.LoadTextureFromFile(newTex));
            Helix.RenderCard();
        }

        public override void ModifySpawnedCard(PlayableCard card)
        {
            LobotomyPlugin.Log.LogDebug("ModifySpawnedCard: " + card.Info.name);
            if (card.Info.name != "wstl_lastHelix")
                return;

            Helix = card;
            Helix.Status.damageTaken -= RunState.Run.regionTier * 5;
            HelixAnimator = Helix.Anim.Anim;

            UpdateCounter();
        }
        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            isActive = false;
            phaseCountdown = Mathf.Max(2, MaxCooldownPeriod - 1);
            EncounterData.StartCondition cond = new()
            {
                cardsInOpponentSlots = new CardInfo[] { null, CardLoader.GetCardByName("wstl_lastHelix") }
            };
            encounterData.startConditions.Add(cond);
            return encounterData;
        }

        public override List<Ability> GetBlacklistedAbilities(List<Ability> redundantAbilities)
        {
            return new(redundantAbilities)
            {
                Ability.WhackAMole,
                Ability.Strafe,
                Ability.StrafePush,
                Ability.StrafeSwap,
                Ability.GuardDog,
                Ability.TailOnHit,
                Cycler.ability,
                Barreler.ability
            };
        }
    }

    public class HelixLaserManager : ManagedBehaviour
    {
        public CardSlot homeSlot = null;
        public CardSlot wanderingSlot;

        public PlayableCard helixCard;
        public GameObject homeLaser;
        public GameObject wanderingLaserObj;


        public void Initialise(PlayableCard helix, CardSlot home, CardSlot wandering)
        {
            helixCard = helix;
            homeSlot = home;
            wanderingSlot = wandering;

            UpdateHomeSlot();
            UpdateWanderingSlot();
        }

        public void UpdateHomeSlot()
        {

        }
        public void UpdateWanderingSlot()
        {

        }
    }
}