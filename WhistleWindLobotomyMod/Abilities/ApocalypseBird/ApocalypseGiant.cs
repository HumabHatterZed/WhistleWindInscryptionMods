using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod
{
    public partial class Abilities
    {
        private static void AddApocalypseGiant()
        {
            const string rulebookName = "The Monster";
            ApocalypseGiant.ability = AbilityHelper.New<ApocalypseGiant>(LobotomyPlugin.pluginGuid, "sigilApocalypse", rulebookName,
                "This card will attack marked spaces. Red spaces will take double damage, and white spaces will take half damage then heal this card equal to its Power.", 0, true).Id;
        }
    }

    public class ApocalypseGiant : AbilityBehaviour, ISetupAttackSequence
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
}
