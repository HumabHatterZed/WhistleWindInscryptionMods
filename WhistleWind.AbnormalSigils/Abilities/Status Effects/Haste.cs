using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Haste : StatusEffectBehaviour, IGetAttackingSlots, IOnPostSlotAttackSequence
    {
        public static SpecialTriggeredAbility specialAbility;
        public static Ability iconId;
        public override string CardModSingletonName => "haste";

        public bool RespondsToGetAttackingSlots(bool playerIsAttacker, List<CardSlot> originalSlots, List<CardSlot> currentSlots)
        {
            return true;
        }
        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot)
        {
            return attackingSlot == base.PlayableCard.Slot;
        }

        public List<CardSlot> GetAttackingSlots(bool playerIsAttacker, List<CardSlot> originalSlots, List<CardSlot> currentSlots)
        {
            // if an opponent card has 4+ Haste, they will attack during the player's turn
            if (base.PlayableCard.OpponentCard && StatusEffectCount >= 4)
            {
                if (!playerIsAttacker)
                    currentSlots.Remove(base.PlayableCard.Slot);
                else if (!currentSlots.Contains(base.PlayableCard.Slot))
                    currentSlots.Add(base.PlayableCard.Slot);
            }
            return null;
        }

        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot)
        {
            // LightNegationEffect sets DoingAttackAnimation to false for some reason, which causes visual glitches
            // so do it this way
            base.PlayableCard.Anim.NegationEffect(false);
            UpdateStatusEffectCount(-StatusEffectCount, false);

            // remove this trigger from the card
            base.PlayableCard.TriggerHandler.specialAbilities.RemoveAll(x => x.Item1 == specialAbility);
            Destroy();

            yield return new WaitForSeconds(0.25f);
        }
        int IGetAttackingSlots.Priority(bool playerIsAttacker, List<CardSlot> originalSlots) => 0;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Haste()
        {
            const string rName = "Haste";
            const string rDesc = "This card attacks before ally cards with less Haste. At 4 Haste, attack before opposing cards as well. After this card attacks it loses all Haste.";

            StatusEffectManager.FullStatusEffect data = StatusEffectManager.NewStatusEffect<Haste>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilHaste", pixelIconTexture: "sigilHaste_pixel",
                powerLevel: 1, iconColour: GameColors.Instance.orange,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect });

            Haste.specialAbility = data.BehaviourId;
            Haste.iconId = data.IconId;
        }
    }
}
