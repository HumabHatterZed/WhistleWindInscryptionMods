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
        private void Ability_RightStrike()
        {
            const string rulebookName = "Right-Veering Strike";
            const string rulebookDescription = "[creature] will strike the opposing space to the right of the space across from it.";
            RightStrike.ability = AbnormalAbilityHelper.CreateAbility<RightStrike>(
                "sigilRightStrike",
                rulebookName, rulebookDescription, powerLevel: 0,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class RightStrike : AbilityBehaviour, ISetupAttackSequence
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot)
        {
            currentSlots.Remove(base.Card.OpposingSlot());
            if (base.Card.OpposingSlot().GetAdjacent(false) != null)
                currentSlots.Add(base.Card.OpposingSlot().GetAdjacent(false));

            return currentSlots;
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot) => 0;

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return card == base.Card && modType == OpposingSlotTriggerPriority.Normal;
        }
    }
}
