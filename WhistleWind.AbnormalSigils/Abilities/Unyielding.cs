using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Unyielding() {
            const string rulebookName = "Unyielding";
            const string rulebookDescription = "[creature] cannot move from its current space on the board.";
            const string dialogue = "This beast is stubborn. It refuses to move.";
            const string triggerText = "[creature] digs in its heels!";
            Unyielding.ability = AbnormalAbilityHelper.CreateAbility<Unyielding>(
                "sigilUnyielding",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 1,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    /// <summary>
    /// [creature] cannot move from its current space on the board.
    /// </summary>
    public class Unyielding : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public CardSlot homeSlot = null;
        public bool indicateStubbornness = true;
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            if (homeSlot == null) {
                yield return base.PreSuccessfulTriggerSequence();
                homeSlot = base.Card.Slot;
            }
            else {
                yield return BoardManager.Instance.AssignCardToSlot(base.Card, homeSlot, resolveTriggers: false);
            }
        }
        public override bool RespondsToUpkeep(bool playerUpkeep) => true;
        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            indicateStubbornness = true;
            yield break;
        }
        public static IEnumerator OnPreventMovement(AbilityBehaviour ability, CardSlot slotToReturnTo = null) {
            if (ability is Unyielding unyield && unyield.indicateStubbornness) {
                unyield.indicateStubbornness = false;
                ability.Card.Anim.StrongNegationEffect();
            }
            else {
                Unyielding behav = ability.Card.TriggerHandler.triggeredAbilities.Find(x => x.Item1 == Unyielding.ability)?.Item2 as Unyielding;
                if (behav != null && behav.indicateStubbornness) {
                    behav.indicateStubbornness = false;
                    ability.Card.Anim.StrongNegationEffect();
                }
            }

            if (slotToReturnTo != null) {
                ability.Card.SetIsOpponentCard(!slotToReturnTo.IsPlayerSlot);
                yield return BoardManager.Instance.AssignCardToSlot(ability.Card, slotToReturnTo, resolveTriggers: false);
            }

            yield return new WaitForSeconds(0.4f);
            yield return ability.LearnAbility();
        }
    }
}
