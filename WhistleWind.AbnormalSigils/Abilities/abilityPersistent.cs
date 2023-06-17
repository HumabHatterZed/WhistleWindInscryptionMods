﻿using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Persistent()
        {
            const string rulebookName = "Persistent";
            const string rulebookDescription = "Attacks by this card cannot be avoided, redirected, or prevented by sigils like Repulsive or Waterborne. Deal 1 additional damage to cards bearing these sigils.";
            const string dialogue = "Prey cannot hide so easily.";
            const string triggerText = "[creature] chases its prey down.";
            Persistent.ability = AbnormalAbilityHelper.CreateAbility<Persistent>(
                "sigilPersistent",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: true, opponent: true, canStack: false).Id;
        }
    }
    public class Persistent : AbilityBehaviour, IOnPreSlotAttackSequence, IOnPostSlotAttackSequence
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public List<PlayableCard> previousTargets = new();
        public List<PlayableCard> currentTargets = new();
        public bool RespondsToPreSlotAttackSequence(CardSlot attackingSlot) => attackingSlot == base.Card.Slot;
        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) => attackingSlot == base.Card.Slot;

        public IEnumerator OnPreSlotAttackSequence(CardSlot attackingSlot)
        {
            currentTargets = base.Card.GetOpposingSlots().FindAll(x => x.Card).Select(x => x.Card).ToList();
            yield break;
        }

        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot)
        {
            previousTargets = new(currentTargets);
            currentTargets.Clear();
            yield break;
        }
    }
}