using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public abstract class PanicBase : ModifyOnTurnEndStatusEffectBehaviour, ISetupAttackSequence {
        public override int MaxPotency => 1;
        public override int PotencyModification => -EffectPotency;

        public override bool RespondsToStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect) {
            return target == base.PlayableCard && statusEffect.IconAbility == this.IconAbility;
        }
        public override IEnumerator OnStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect) {
            // remove all Sinking from this card when ending the panic
            yield return target.RemoveStatusEffect<Sinking>();
        }

        public abstract List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot);

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot) {
            return card == base.Card && modType == OpposingSlotTriggerPriority.PostAdditionModification;
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot) {
            return 0;
        }

        public static bool IsPanicking(PlayableCard card) {
            return card.HasStatusEffect<PanicBase>();
        }
    }
}
