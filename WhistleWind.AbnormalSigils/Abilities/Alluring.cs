using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Alluring() {
            const string rulebookName = "Alluring";
            const string rulebookDescription = "At the end of the owner's turn, [creature] attracts an opposing adjacent creature to the space across from this card if possible.";
            const string dialogue = "A sweet scent.";

            Alluring.ability = AbnormalAbilityHelper.CreateAbility<Alluring>(
                "sigilAlluring",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook()
                .Id;
        }
    }
    /// <summary>
    /// At the end of the owner's turn, [creature] attracts an opposing adjacent creature to the space across from this card if possible.
    /// </summary>
    public class Alluring : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd && base.Card.OpposingCard() == null;

        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            CardSlot slot = base.Card.OpposingSlot().GetAdjacent(true);

            // if left AO is not valid, get the right AO
            if (!IsValid(slot)) {
                slot = base.Card.OpposingSlot().GetAdjacent(false);
                if (!IsValid(slot)) {
                    yield break;
                }
            }

            yield return base.PreSuccessfulTriggerSequence();
            slot.Card.Anim.LightNegationEffect();
            yield return BoardManager.Instance.AssignCardToSlot(slot.Card, base.Card.OpposingSlot());
            yield return base.LearnAbility(0.5f);
        }

        private bool IsValid(CardSlot slot) {
            if (slot != null && slot.Card != null) {
                return slot.Card.LacksAllAbilities(Ability.MadeOfStone, Unyielding.ability) && slot.Card.LacksTrait(Trait.Giant);
            }
            return false;
        }
    }
}
