using DiskCardGame;
using Pixelplacement;
using System.Collections;
using UnityEngine;

namespace WhistleWindLobotomyMod.Opponents {
    public class HelixLight : ManagedBehaviour {

        public ParticleSystem system;

        public CardSlot currentSlot;
        CardSlot nextSlot;

        private IEnumerator MoveLaserToNewPosition() {

            // if nextSlot is adjacent to currentSlot
            if (Mathf.Abs(nextSlot.Index - currentSlot.Index) == 1) {
                // slide laser to next position
                Tween.Position(transform, nextSlot.transform.position, 2f, 0f);
            }
            else {
                // if we're moving to the other side of the board, move laser up then shoot back down on other side of board
                transform.position = nextSlot.transform.position;
            }
            yield return new WaitForSeconds(1f);
            currentSlot = nextSlot;
        }
        public IEnumerator UpdateCurrentSlot() {
            if (nextSlot == null) {
                DetermineNextSlot();
            }

            yield return MoveLaserToNewPosition();

            DetermineNextSlot();
        }

        private void DestroyCardViaLaser(HelixLight laser) {
            if (laser.currentSlot.Card != null && !laser.currentSlot.Card.Dead) {
                base.StartCoroutine(DestroyWhenStackIsClear(laser.currentSlot.Card));
            }
        }

        private IEnumerator DestroyWhenStackIsClear(PlayableCard card) {
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Singleton<GlobalTriggerHandler>.Instance.StackSize == 0);
            if (card != null && !card.Dead) {
                card.Dead = true;

                card.Anim.SetShielded(false);
                card.Anim.ClearLatchAbility();
                card.Anim.PlayPermaDeathAnimation();
                card.UnassignFromSlot();
                card.StartCoroutine(card.DestroyWhenStackIsClear());
            }
        }

        public override void ManagedFixedUpdate() {
            DestroyCardViaLaser(this);
        }

        public void DetermineNextSlot() {
            int index = (currentSlot.Index + 1 >= BoardManager.Instance.PlayerSlotsCopy.Count) ? 0 : currentSlot.Index + 1;
            nextSlot = BoardManager.Instance.PlayerSlotsCopy[index];
        }

        public void Initialise(CardSlot startingSlot) {
            currentSlot = startingSlot;
            nextSlot = null;
            system ??= GetComponent<ParticleSystem>();
            transform.position = currentSlot.transform.position;
        }
    }
}
