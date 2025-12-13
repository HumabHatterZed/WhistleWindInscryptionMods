using DiskCardGame;
using InscryptionAPI.Card;
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
            const string rulebookDescription = "At the end of the owner's turn, this card will strike an adjacent creature, prioritising one with the Stress Response sigil.";
            const string dialogue = "Nothing is good enough.";
            Abusive.ability = AbnormalAbilityHelper.CreateAbility<Abusive>(
                "sigilAbusive",
                rulebookName, rulebookDescription, dialogue, powerLevel: -3,
                modular: false, opponent: false, canStack: false)
                .SetAbilityRedirect("Stress Response", StressResponse.ability, GameColors.instance.glowRed)
                .Id;
        }
    }
    /// <summary>
    /// At the end of the owner's turn, this card will strike an adjacent creature, prioritising one with the Stress Response sigil.
    /// </summary>
    public class Abusive : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            List<PlayableCard> adjacent = base.Card.Slot.GetAdjacentCards();
            if (adjacent.Exists(x => x.HasAbility(StressResponse.ability))) {
                adjacent.RemoveAll(x => !x.HasAbility(StressResponse.ability));
            }
            if (base.Card.LacksAbility(Persistent.ability)) {
                adjacent.RemoveAll(x => x.FaceDown);
            }

            if (adjacent.Count > 0) {
                yield return base.PreSuccessfulTriggerSequence();
                yield return new WaitForSeconds(0.3f);
                yield return Singleton<CombatPhaseManager>.Instance.SlotAttackSlot(base.Card.Slot, adjacent.GetSeededRandom(base.GetRandomSeed()).Slot, adjacent.Count > 1 ? 0.1f : 0f);
                yield return base.LearnAbility(0.5f);
            }
        }
    }
}
