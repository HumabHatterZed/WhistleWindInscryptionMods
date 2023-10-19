using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils
{
    public class Haste : StatusEffectBehaviour, IGetAttackingSlots, IOnPostSlotAttackSequence
    {
        public static SpecialTriggeredAbility specialAbility;
        public static Ability iconId;
        public override string CardModSingletonName => "haste";

        public bool RespondsToGetAttackingSlots(bool playerIsAttacker, List<CardSlot> originalSlots, List<CardSlot> currentSlots) => true;
        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) => attackingSlot.Card == base.PlayableCard;

        public List<CardSlot> GetAttackingSlots(bool playerIsAttacker, List<CardSlot> originalSlots, List<CardSlot> currentSlots)
        {
            // if an opponent card has 4+ Haste, they will attack during the player's turn
            if (base.PlayableCard.OpponentCard && EffectSeverity >= 4)
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
            base.PlayableCard.Anim.NegationEffect(false);
            SetSeverity(0, false);
            yield return new WaitForSeconds(0.25f);
            Destroy();
        }

        public int TriggerPriority(bool playerIsAttacker, List<CardSlot> originalSlots) => 0;
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
