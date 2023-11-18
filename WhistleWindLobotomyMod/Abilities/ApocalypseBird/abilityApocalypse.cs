using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod
{
    public class ApocalypseAbility : AbilityBehaviour, ISetupAttackSequence
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return card == base.Card && base.Card.HasTrait(Trait.Giant) && modType == OpposingSlotTriggerPriority.Normal;
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot)
        {
            ApocalypseBattleSequencer sequencer = TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer;
            List<CardSlot> slots = new();
            bool attackingNull = false;
            foreach (CardSlot slot in sequencer.specialTargetSlots)
            {
                if (slot.Card == null)
                {
                    if (attackingNull)
                    {
                        sequencer.CleanUpGiantTarget(slot);
                        continue;
                    }

                    attackingNull = true;
                }
                else if (base.Card.CanAttackDirectly(slot))
                {
                    sequencer.CleanUpGiantTarget(slot);
                    continue;
                }
                slots.Add(slot);
            }
            return slots;
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return 0;
        }
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Apocalypse()
        {
            const string rulebookName = "Monster in the Black Forest";
            ApocalypseAbility.ability = LobotomyAbilityHelper.CreateAbility<ApocalypseAbility>(
                "sigilApocalypse", rulebookName,
                "'Once upon a time, three birds lived happily in the lush forest...'",
                "The three birds, now one, wandered vainly looking for the monster.", powerLevel: 0, canStack: false).Id;
        }
    }
}
