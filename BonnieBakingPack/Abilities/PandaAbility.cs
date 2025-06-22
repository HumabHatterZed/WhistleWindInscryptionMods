using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
namespace BonniesBakingPack {
    public class PandaAbility : SpecialCardBehaviour, IOnPreSlotAttackSequence, IOnPostSlotAttackSequence {
        public static SpecialTriggeredAbility SpecialAbility;

        public bool RespondsToPreSlotAttackSequence(CardSlot attackingSlot) => attackingSlot == base.PlayableCard.Slot;
        public IEnumerator OnPreSlotAttackSequence(CardSlot attackingSlot) {
            attackingSlot.Card.SwitchToAlternatePortrait();
            yield return new WaitForSeconds(0.2f);
        }

        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) => RespondsToPreSlotAttackSequence(attackingSlot);

        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot) {
            yield return new WaitForSeconds(0.2f);
            attackingSlot.Card.SwitchToDefaultPortrait();
        }

        // MagnificusMod sucks and overrides the entire combat sequence in a way that removes custom triggers
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            return SaveManager.SaveFile.IsMagnificus && BakingPlugin.ScrybeCompat.MagnificusEnabled && attacker == base.Card && slot.Card != null;
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            base.StartCoroutine(HandleMagnificus(attacker));
            yield break;
        }

        private IEnumerator HandleMagnificus(PlayableCard card) {
            yield return new WaitForSeconds(0.2f);
            card.SwitchToAlternatePortrait();
            yield return new WaitForSeconds(0.5f);
            card.SwitchToDefaultPortrait();
        }
    }
}
