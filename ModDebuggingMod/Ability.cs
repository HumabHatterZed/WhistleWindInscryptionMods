using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
//using InscryptionAPI.Slots;
using WhistleWind.Core.Helpers;

namespace ModDebuggingMod {
    public partial class Plugin {
        private void Ability_Test() {
            const string rulebookName = "Test";
            const string rulebookDescription = "When [creature] dies, the killer transforms into a copy of this card.";
            const string dialogue = "The curse continues unabated.";
            const string triggerText = "[creature] passes the curse on.";
            Test.ability = AbilityHelper.New<Test>(
                pluginGuid, "sigilCursed",
                rulebookName, rulebookDescription, 0, true, dialogue, triggerText,
                modular: true, opponent: false, canStack: true).Id;
        }
    }
    public class Test : AbilityBehaviour, ISetupAttackSequence {
        public static Ability ability;
        public override Ability Ability => ability;

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot) {
            Plugin.Log.LogInfo("Modify");
            return card.Slot.opposingSlot.GetAdjacentSlots(true);
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot) {
            return 0;
        }

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot) {
            return card == base.Card && modType == OpposingSlotTriggerPriority.ReplacesDefaultOpposingSlot;
        }
    }

    /*    public class PavedSlot : SlotModificationGainAbilityBehaviour, IPassiveAttackBuff
        {
            public static readonly SlotModificationManager.ModificationType ID = SlotModificationManager.New(
                "MyPluginGuid",
                "PavedSlot",
                typeof(PavedSlot),
                TextureHelper.GetImageAsTexture("slotPavedRoad.png", typeof(PavedSlot).Assembly),
                TextureHelper.GetImageAsTexture("slotPavedRoad_pixel.png", typeof(PavedSlot).Assembly)
            );

            public override bool RespondsToTurnEnd(bool playerTurnEnd) => playerTurnEnd == Slot.IsPlayerSlot;

            public override IEnumerator OnTurnEnd(bool playerTurnEnd)
            {
                if (Slot.Card != null)
                    yield return Slot.Card.TakeDamage(1, null);
            }
            public int GetPassiveAttackBuff(PlayableCard target)
            {
                return Slot.Card == target ? 1 : 0;
            }
            protected override Ability AbilityToGain => Ability.Sharp;
        }*/
}
