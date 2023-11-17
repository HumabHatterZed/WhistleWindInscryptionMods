﻿using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Enchanted : ReduceStatusEffectBehaviour, IModifyDamageTaken, ISetupAttackSequence
    {
        public static SpecialTriggeredAbility specialAbility;
        public override string CardModSingletonName => "enchanted";

        private bool IsEnchanted => EffectSeverity > 0;
        public override int SeverityReduction => 1;

        public override List<string> EffectDecalIds() => new();

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return IsEnchanted && attacker == base.Card && target != null && target.HasAbility(Dazzling.ability);
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => 0;
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => int.MinValue;

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return IsEnchanted && card == base.Card && modType == OpposingSlotTriggerPriority.PostAdditionModification;
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot)
        {
            List<CardSlot> slots = BoardManager.Instance.AllSlotsCopy.FindAll(x => x.Card != null && x.Card.HasAbility(Dazzling.ability));

            if (slots.Count <= 1)
                return slots;

            slots.Randomize();
            slots.RemoveRange(1, slots.Count - 1);
            return slots;
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return int.MinValue;
        }
    }
    public partial class LobotomyPlugin
    {
        private void StatusEffect_Enchanted()
        {
            const string rName = "Enchanted";
            const string rDesc = "This card will target cards with the Dazzling sigil, dealing no damage to them and dying after attacking. At the start of the owner's next turn, lose 1 stack of this effect.";

            Enchanted.specialAbility = StatusEffectManager.NewStatusEffect<Enchanted>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilEnchanted",
                modAssembly: Assembly.GetCallingAssembly(),
                powerLevel: -4, iconColour: GameColors.Instance.gold,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect }).BehaviourId;
        }
    }
}
