using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils
{
    public class Bind : ReduceStatusEffectBehaviour, IGetAttackingSlots, IOnPostSlotAttackSequence
    {
        public static SpecialTriggeredAbility specialAbility;
        public static Ability iconId;
        public override string CardModSingletonName => "bind";
        public override int SeverityReduction => EffectSeverity;

        public bool RespondsToGetAttackingSlots(bool playerIsAttacker, List<CardSlot> originalSlots, List<CardSlot> currentSlots) => true;
        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) => attackingSlot.Card == base.PlayableCard;

        public List<CardSlot> GetAttackingSlots(bool playerIsAttacker, List<CardSlot> originalSlots, List<CardSlot> currentSlots)
        {
            // if a player card has 4+ Bind, they will attack during the opponent's turn
            if (!base.PlayableCard.OpponentCard && EffectSeverity >= 4)
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
            base.PlayableCard.Anim.NegationEffect(false);
            SetSeverity(0, false);
            yield return new WaitForSeconds(0.25f);
            Destroy();
        }

        public int TriggerPriority(bool playerIsAttacker, List<CardSlot> originalSlots) => 0;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Bind()
        {
            const string rName = "Bind";
            const string rDesc = "This card attacks after ally cards with less Bind. At 4 Bind, attack after opposing cards as well. Remove all Bind from this card when it attacks or on next upkeep.";

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
