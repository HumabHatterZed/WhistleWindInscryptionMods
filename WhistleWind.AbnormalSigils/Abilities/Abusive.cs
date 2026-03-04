using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Abusive() {
            const string rulebookName = "Abusive";
            const string rulebookDescription = "At the end of the owner's turn, this card will strike an ally card, prioritising one with a Stress Response.";
            const string dialogue = "Nothing is good enough.";
            Abusive.ID = AbnormalAbilityHelper.CreateAbility<Abusive>(
                "sigilAbusive",
                rulebookName, rulebookDescription, dialogue, powerLevel: -3,
                modular: false, opponent: false, canStack: false)
                .SetAbilityRedirect("Stress Response", StressResponse.ID, GameColors.instance.glowRed)
                .Id;
        }
    }
    /// <summary>
    /// At the end of the owner's turn, this card will strike an ally card, prioritising one with a Stress Response.
    /// </summary>
    public class Abusive : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            List<PlayableCard> adjacent = BoardManager.Instance.GetCards(!base.Card.OpponentCard);
            adjacent.Remove(this.Card);
            if (adjacent.Exists(x => x.HasAbility(StressResponse.ID))) {
                adjacent.RemoveAll(x => !x.HasAbility(StressResponse.ID));
            }
            adjacent.RemoveAll(x => base.Card.CanAttackDirectly(x.Slot));
            if (adjacent.Count > 0) {
                yield return base.PreSuccessfulTriggerSequence();
                yield return new WaitForSeconds(0.3f);
                yield return Singleton<CombatPhaseManager>.Instance.SlotAttackSlot(base.Card.Slot, adjacent.GetSeededRandom(base.GetRandomSeed()).Slot, adjacent.Count > 1 ? 0.1f : 0f);
                yield return base.LearnAbility(0.5f);
            }
        }
    }
}
