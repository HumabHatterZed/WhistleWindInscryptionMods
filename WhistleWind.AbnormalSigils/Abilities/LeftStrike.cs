using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_LeftStrike()
        {
            const string rulebookName = "Left-Veering Strike";
            const string rulebookDescription = "[creature] will strike the opposing space to the left of the space across from it.";
            LeftStrike.ability = AbnormalAbilityHelper.CreateAbility<LeftStrike>(
                "sigilLeftStrike",
                rulebookName, rulebookDescription, powerLevel: 0,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class LeftStrike : AbilityBehaviour, IGetOpposingSlots
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public bool RemoveDefaultAttackSlot() => true;
        public bool RespondsToGetOpposingSlots() => true;
        public List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots)
        {
            List<CardSlot> retval = new();
            if (base.Card.OpposingSlot().GetAdjacent(true) != null)
                retval.Add(base.Card.OpposingSlot().GetAdjacent(true));
            return retval;
        }
    }
}
