using DiskCardGame;
using EasyFeedback.APIs;
using Infiniscryption.P03KayceeRun.Encounters;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    /// <summary>
    /// Last Helix:
    ///     Stage 1: Cooldown
    ///         Last Helix is inactive, player can use this period to prepare for active phase and deal damage
    ///         - Reduce countdown at the end of opponent combat
    ///         - When countdown reaches 0, enter stage 2
    ///     Stage 2: Active
    ///         Last Helix lasers 2 spaces on the board. Target 1 does not move while Target 2 moves clockwise across the board lanes in real-time.
    ///         - Cards in lasered spaces are destroyed.
    ///         - Reduce countdown at the end of opponent combat
    ///         - When countdown reaches 0, reset cooldown, disable lasers and enter stage 1
    /// </summary>
    public class OrdealGreenMidnight : OrdealBattleSequencer
    {
        private PlayableCard Helix;

        private readonly int MaxCooldownPeriod = 4 - RunState.Run.regionTier;
        private readonly int MaxActivePeriod = 3 + RunState.Run.regionTier; // account for initial turn of activation

        public int activePeriod = 0;
        private int cooldownPeriod = 0;

        private int currentTargetIndex = -1;

        private bool laserExists = false;

        public CardSlot target1 = null;
        public CardSlot target2 = null;

        private HelixLaserDestructionBehaviour standingLaserBehav = null;
        private HelixLaserDestructionBehaviour movingLaserBehav = null;

        public bool CleanUpLasers = false;

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) => fromCombat;
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (card.OpponentCard && base.RespondsToOtherCardDie(card, deathSlot, fromCombat, killer))
            {
                EndActivePhase();
                yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
            }
        }
        private void BeginActivePhase()
        {
            Debug.Log("Begin Active Phase");
            if (standingLaserBehav != null)
                return;

            int randomIdx = UnityEngine.Random.RandomRangeInt(0, BoardManager.Instance.PlayerSlotsCopy.Count);
            
            GameObject standingLaser = new("HelixLaserStanding");
            standingLaser.transform.SetParent(base.transform);
            standingLaserBehav = standingLaser.AddComponent<HelixLaserDestructionBehaviour>();
            standingLaserBehav.Initialise(BoardManager.Instance.PlayerSlotsCopy[randomIdx]);

            GameObject movingLaser = new("HelixLaserMoving");
            movingLaser.transform.SetParent(base.transform);
            movingLaserBehav = movingLaser.AddComponent<HelixLaserDestructionBehaviour>();
            movingLaserBehav.Initialise(BoardManager.Instance.PlayerSlotsCopy[randomIdx]);
            movingLaser.AddComponent<HelixLaserMovementBehaviour>().Initialise(movingLaserBehav,
                BoardManager.Instance.PlayerSlotsCopy[0].transform.localPosition.x,
                BoardManager.Instance.PlayerSlotsCopy.Last().transform.localPosition.x,
                1f);
        }
        private void EndActivePhase()
        {
            standingLaserBehav.CleanUp();
            movingLaserBehav.CleanUp();
            standingLaserBehav = movingLaserBehav = null;
            cooldownPeriod = MaxCooldownPeriod;
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
            if (activePeriod > 0 && standingLaserBehav == null) // begin the acti
            {
                yield return new WaitForSeconds(1.5f);
                BeginActivePhase();
            }
            
            yield return base.OpponentCombatStart();
        }

        /// <summary>
        /// Updates to the laser, cooldown and active period occur here.
        /// 
        /// </summary>
        public override IEnumerator OpponentCombatEnd()
        {
            Debug.Log("OpponentCombatEnd");

            if (activePeriod > 0) // if we're in the active period
            {
                Debug.Log("Helix is active");
                activePeriod--;
                if (activePeriod == 0) // if the active period has finished, reset the cooldown and 
                {
                    Debug.Log("Deactivate");
                    EndActivePhase();
                    yield return new WaitForSeconds(2f);
                    Helix.SwitchToDefaultPortrait();
                    UpdateCounter();
                }
            }
            else // in the cooldown period
            {
                Debug.Log("Helix on cooldown");

                cooldownPeriod--;
                UpdateCounter();
                if (cooldownPeriod > 0)
                    yield break;

                Debug.Log($"Activating, Cooldown {cooldownPeriod}");
                currentTargetIndex = 0;
                target1 = target2 = null;
                activePeriod = MaxCooldownPeriod;
                Helix.SwitchToAlternatePortrait();
                yield return new WaitForSeconds(1f);
            }
        }

        private void UpdateCounter()
        {
            Debug.Log($"UpdateCounter: {cooldownPeriod}");
            string newTex = cooldownPeriod <= 0 ? "sigilTower.png" : ("sigilTower_" + cooldownPeriod + ".png");
            Helix.RenderInfo.OverrideAbilityIcon(Tower.ability, TextureLoader.LoadTextureFromFile(newTex));
            Helix.RenderCard();
        }

        public override void ModifySpawnedCard(PlayableCard card)
        {
            if (card.Info.name != "wstl_lastHelix")
                return;

            Debug.Log("Modify spawned card: assign Helix");
            Helix ??= card;
            Helix.Status.damageTaken -= RunState.Run.regionTier * 5;
            UpdateCounter();
        }

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            cooldownPeriod = Mathf.Max(2, MaxCooldownPeriod - 1);
            EncounterData.StartCondition cond = new()
            {
                cardsInOpponentSlots = new CardInfo[] { null, CardLoader.GetCardByName("wstl_lastHelix") }
            };
            encounterData.startConditions.Add(cond);
            return encounterData;
        }
    }

    public class HelixLaserDestructionBehaviour : ManagedBehaviour
    {
        public bool IsActive = true;

        public CardSlot currentLaseredSlot = null;

        public override void ManagedFixedUpdate()
        {
            base.ManagedFixedUpdate();
            if (!IsActive)
                return;

            if (currentLaseredSlot?.Card != null)
            {
                PlayableCard card = currentLaseredSlot.Card;
                card.Anim.PlayDeathAnimation(true);
                card.UnassignFromSlot();
                base.StartCoroutine(card.DestroyWhenStackIsClear());
            }
        }

        public void Initialise(CardSlot initSlot)
        {
            currentLaseredSlot = initSlot;
        }

        public void CleanUp()
        {
            IsActive = false;
        }
    }
    public class HelixLaserMovementBehaviour : ManagedBehaviour
    {
        private bool IsActive = true;
        private HelixLaserDestructionBehaviour behav;

        private float leftBound;
        private float rightBound;
        private float speed;

        public override void ManagedFixedUpdate()
        {
            base.ManagedFixedUpdate();
            if (!behav.IsActive)
            {
                //base.StartCoroutine(CleanUp());
                return;
            }
        }
        public void Initialise(HelixLaserDestructionBehaviour behav, float leftBound, float rightBound, float speed)
        {
            this.behav = behav;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
            this.speed = speed;
        }
        public void CleanUp()
        {
            IsActive = false;
        }
    }
}