using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_FlagBearer() {
            const string rulebookName = "Flag Bearer";
            const string rulebookDescription = "While this card is on the board, adjacent creatures gain 2 Health.";
            const string dialogue = "Morale runs high.";

            FlagBearer.ID = AbnormalAbilityHelper.CreateAbility<FlagBearer>(
                "sigilFlagBearer",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: true)
                .Id;
        }
    }
    /// <summary>
    /// While this card is on the board, adjacent creatures gain 2 Health.
    /// </summary>
    public class FlagBearer : AbilityBehaviour, IPassiveHealthBuff {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override bool RespondsToResolveOnBoard() => base.Card.Slot.GetAdjacentCards().Count > 0;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => otherCard.Slot.GetAdjacentCards().Contains(base.Card);

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            foreach (PlayableCard card in base.Card.Slot.GetAdjacentCards().Where(x => x.Health <= 2)) {
                yield return card.Heal(2);
            }
        }
        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.4f);
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => base.LearnAbility(0.4f);

        public int GetPassiveHealthBuff(PlayableCard target) {
            if (base.Card.OnBoard && target.OpponentCard == base.Card.OpponentCard)
                return target.Slot.GetAdjacentCards().Contains(base.Card) ? 2 : 0;

            return 0;
        }
    }
}
