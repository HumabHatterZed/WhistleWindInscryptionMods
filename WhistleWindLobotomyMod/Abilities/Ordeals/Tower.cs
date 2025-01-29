using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;


namespace WhistleWindLobotomyMod
{
    public class Tower : AbilityBehaviour, ISetupAttackSequence, IModifyDirectDamage
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool doubleDirectDamage = false;

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return card == base.Card && modType == OpposingSlotTriggerPriority.Normal;
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot)
        {
            OrdealGreenMidnight sequencer = TurnManager.Instance.SpecialSequencer as OrdealGreenMidnight;
            List<CardSlot> slots = new()
            {
                sequencer.target1 ?? card.OpposingSlot()
            };
            doubleDirectDamage = sequencer.target1 == null && sequencer.target2 == null;

            if (!doubleDirectDamage)
                slots.Add(sequencer.target2);

            return slots;
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return 0;
        }

        public bool RespondsToModifyDirectDamage(CardSlot target, int damage, PlayableCard attacker, int originalDamage) => attacker == base.Card && doubleDirectDamage;
        public int OnModifyDirectDamage(CardSlot target, int damage, PlayableCard attacker, int originalDamage) => damage * 2;
        public int TriggerPriority(CardSlot target, int damage, PlayableCard attacker) => 0;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Tower()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The Tower";
            info.rulebookDescription = "This card changes state when this sigil's count reaches 0. In an active state, create two Lights on the opposing side of the board.";
            info.powerLevel = 5;
            Tower.ability = AbilityManager.Add(pluginGuid, info, typeof(Tower), TextureLoader.LoadTextureFromFile("sigilTower.png")).Id;
        }
    }
}
