﻿using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Regenerator()
        {
            const string rulebookName = "Regenerator";
            const string rulebookDescription = "At the start of its owner's turn, this card heals adjacent cards by 1 Health.";
            const string dialogue = "Wounds heal, but the scars remain.";
            const string triggerText = "[creature] heals adjacent creatures.";
            Regenerator.ability = AbnormalAbilityHelper.CreateAbility<Regenerator>(
                "sigilRegenerator",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: true, opponent: false, canStack: true).Id;
        }
    }
    public class Regenerator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);

            List<CardSlot> adjacentSlots = Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).FindAll(s => s.Card != null);
            foreach (CardSlot slot in adjacentSlots)
            {
                if (slot.Card.Health < slot.Card.MaxHealth)
                {
                    bool faceDown = slot.Card.FaceDown;
                    yield return slot.Card.FlipFaceUp(false, 0.4f);
                    slot.Card.Anim.LightNegationEffect();
                    slot.Card.HealDamage(1);
                    yield return new WaitForSeconds(0.2f);
                    yield return slot.Card.FlipFaceDown(faceDown);
                }
            }
            yield return new WaitForSeconds(0.2f);
            yield return LearnAbility();
        }
    }
}
