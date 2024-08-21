using DiskCardGame;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BonniesBakingPack
{
    public class PandaAbility : SpecialCardBehaviour, IOnPreSlotAttackSequence, IOnPostSlotAttackSequence
    {
        public static SpecialTriggeredAbility SpecialAbility;

        public bool RespondsToPreSlotAttackSequence(CardSlot attackingSlot) => attackingSlot == base.PlayableCard.Slot;
        public IEnumerator OnPreSlotAttackSequence(CardSlot attackingSlot)
        {
            attackingSlot.Card.SwitchToAlternatePortrait();
            yield return new WaitForSeconds(0.2f);
        }

        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) => RespondsToPreSlotAttackSequence(attackingSlot);

        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot)
        {
            yield return new WaitForSeconds(0.2f);
            attackingSlot.Card.SwitchToDefaultPortrait();
        }
    }
}
