using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Bind : StatusEffectBehaviour, IGetAttackingSlots, IOnPostSlotAttackSequence
    {
        public static SpecialTriggeredAbility specialAbility;
        public static Ability iconId;
        public override string CardModSingletonName => "bind";

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
            // if a player card has 4+ Bind, they will attack during the opponent's turn
            if (base.PlayableCard.IsPlayerCard() && StatusEffectCount >= 4)
            {
                if (playerIsAttacker)
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
            CardModificationInfo mod = base.PlayableCard.TemporaryMods.Find(x => x.singletonId == CardModSingletonName);
            if (mod != null)
                base.PlayableCard.RemoveTemporaryMod(mod);
            // remove this trigger from the card
            base.PlayableCard.TriggerHandler.specialAbilities.RemoveAll(x => x.Item1 == specialAbility);
            Destroy();

            yield return new WaitForSeconds(0.25f);
        }

        public int TriggerPriority(bool playerIsAttacker, List<CardSlot> originalSlots) => 0;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Bind()
        {
            const string rName = "Bind";
            const string rDesc = "This card attacks after ally cards with less Bind. At 4 Bind, attack after opposing cards as well. After this card attacks it loses all Bind.";

            StatusEffectManager.FullStatusEffect data = StatusEffectManager.NewStatusEffect<Bind>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilBind", pixelIconTexture: "sigilBind_pixel",
                powerLevel: -1, iconColour: GameColors.Instance.orange,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect });

            Bind.specialAbility = data.BehaviourId;
            Bind.iconId = data.IconId;
        }
    }
}
