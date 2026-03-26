using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddApocalypseGiant() {
            const string rulebookName = "The Monster";
            ApocalypseGiant.ID = AbilityHelper.New<ApocalypseGiant>(LobotomyPlugin.pluginGuid, "sigilApocalypse", rulebookName,
                "This card will attack marked spaces. Red spaces will take double damage, and white spaces will take half damage then heal this card equal to its Power.",
                0, true).Id;
        }
    }

    public class ApocalypseGiant : AbilityBehaviour, ISetupAttackSequence {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot) {
            return card == base.Card && modType == OpposingSlotTriggerPriority.PostAdditionModification;
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot) {
            ApocalypseBattleSequencer sequencer = TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer;
            foreach (CardSlot slot in sequencer.specialTargetSlots) {
                sequencer.CleanUpGiantTarget(slot);
            }
            return sequencer.specialTargetSlots;
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot) {
            return 0;
        }
    }
}
