using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Damsel() {
            const string rulebookName = "Damsel";
            const string rulebookDescription = "Attacks from creatures adjacent to [creature] are redirected to creatures targeting this card.";
            const string dialogue = "The damsel demands warriors to destroy its tormentor.";
            Damsel.ability = AbnormalAbilityHelper.CreateAbility<Damsel>(
                "sigilDamsel",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// Attacks from creatures adjacent to [creature] are redirected to creatures targeting this card.
    /// </summary>
    [HarmonyPatch]
    public class Damsel : AbilityBehaviour, IOnPreSlotAttackSequence, IOnPostSlotAttackSequence {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            return attacker != null && RespondsToPostSlotAttackSequence(attacker.Slot);
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            yield return base.LearnAbility();
        }

        private List<CardSlot> GetTormentorSlots(PlayableCard damselCard) {
            return BoardManager.Instance.GetCardSlots(damselCard.OpponentCard, x => x.Card != null && x.Card.GetOpposingSlots().Contains(damselCard.Slot));
        }

        public bool RespondsToPreSlotAttackSequence(CardSlot attackingSlot) {
            return base.Card.Slot.GetAdjacentSlots().Contains(attackingSlot) && GetTormentorSlots(base.Card).Count > 0;
        }
        public IEnumerator OnPreSlotAttackSequence(CardSlot attackingSlot) {
            AbnormalPlugin.Log.LogDebug($"[Damsel] Overriding opposingSlot for {attackingSlot}");
            attackingSlot.opposingSlot = GetTormentorSlots(base.Card)[0];
            yield break;
        }

        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) {
            return base.Card.Slot.GetAdjacentSlots().Contains(attackingSlot) && attackingSlot.opposingSlot.Index != attackingSlot.Index;
        }
        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot) {
            AbnormalPlugin.Log.LogDebug($"[Damsel] Resetting opposingSlot for {attackingSlot}");
            attackingSlot.opposingSlot = BoardManager.Instance.GetSlotsCopy(!attackingSlot.IsPlayerSlot).Find(x => x.Index == attackingSlot.Index);
            yield break;
        }
    }
}
