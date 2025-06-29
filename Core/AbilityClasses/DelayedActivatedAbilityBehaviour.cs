using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionCommunityPatch.Card;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWind.Core.AbilityClasses {
    /// <summary>
    /// Extension of the API's activated ability behaviour that adds support for turn delays and opponent-usage.
    /// </summary>
    public abstract class DelayedActivatedAbilityBehaviour : ExtendedActivatedAbilityBehaviour {
        private int currentTurnDelay = 0;
        public virtual int TurnDelay => -1;// by default, can always activate

        public override bool CanActivate() => currentTurnDelay <= 0;
        public override IEnumerator Activate() {
            if (currentTurnDelay == 0) {
                currentTurnDelay = TurnDelay;
            }

            yield break;
        }

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            if (currentTurnDelay > 0) // if turnDelay is above 0, reduce it by 1
            {
                currentTurnDelay--;
                if (currentTurnDelay == 0 && !base.Card.OpponentCard) {
                    yield return HelperMethods.ChangeCurrentView(View.Board);
                    base.Card.Anim.LightNegationEffect();
                    yield return new WaitForSeconds(0.2f);
                }
            }

            if (base.Card.OpponentCard && CanActivateOpponent()) {
                yield return Activate();
            }
        }

        public virtual bool CanActivateOpponent() {
            if (!CanActivate()) {
                return false;
            }

            // energy costs cannot be used until enough time has passed to afford it
            if (EnergyCost == 0 || EnergyCost <= TurnManager.Instance.TurnNumber) {
                return SeededRandom.Range(0, AbilitiesUtil.GetInfo(this.Ability).powerLevel, base.GetRandomSeed()) == 0;
            }
            return false;
        }
    }
}