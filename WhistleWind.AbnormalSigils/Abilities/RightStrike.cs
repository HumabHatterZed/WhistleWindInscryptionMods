using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_RightStrike() {
            const string rulebookName = "Right-Veering Strike";
            const string rulebookDescription = "[creature] will strike the opposing space to the right of the space across from it.";
            RightStrike.ID = AbnormalAbilityHelper.CreateAbility<RightStrike>(
                "sigilRightStrike",
                rulebookName, rulebookDescription, powerLevel: 0,
                modular: false, opponent: false, canStack: false)
                .Info.SetFlipYIfOpponent()
                .ability;
        }
    }
    /// <summary>
    /// [creature] will strike the opposing space to the right of the space across from it.
    /// </summary>
    public class RightStrike : AbilityBehaviour, IGetOpposingSlots {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public bool RemoveDefaultAttackSlot() => true;
        public bool RespondsToGetOpposingSlots() => true;
        public List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots) {
            List<CardSlot> retval = new();
            if (base.Card.OpposingSlot().GetAdjacent(false) != null)
                retval.Add(base.Card.OpposingSlot().GetAdjacent(false));
            return retval;
        }
    }
}
