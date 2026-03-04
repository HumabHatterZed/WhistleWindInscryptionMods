using DiskCardGame;
using InscryptionAPI.RuleBook;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_NimbleFoot() {
            const string rulebookName = "Nimble-Footed";
            const string rulebookDescription = "At the start of the owner's turn, [creature] gains Haste equal to 1 plus the number of times it has moved on the board.";
            NimbleFoot.ID = AbnormalAbilityHelper.CreateAbility<NimbleFoot>(
                "sigilNimbleFoot",
                rulebookName, rulebookDescription, powerLevel: 1,
                modular: true, opponent: true, canStack: false)
                .SetAbilityRedirect("Haste", Haste.iconId, GameColors.Instance.orange)
                .Id;
        }
    }
    /// <summary>
    /// At the start of the owner's turn, [creature] gains Haste equal to 1 plus the number of times it has moved on the board.
    /// </summary>
    public class NimbleFoot : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        int extraHaste = 0;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.LightNegationEffect();
            yield return base.Card.AddStatusEffect<Haste>(extraHaste, modifyTurnGained: delegate (int i) {
                if (base.Card.HasStatusEffect<Haste>())
                    return i;

                return i + 1;
            });
            yield return new WaitForSeconds(0.2f);
            yield return base.LearnAbility(0.4f);
        }

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => otherCard == base.Card;
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard) {
            extraHaste++;
            yield break;
        }
    }
}