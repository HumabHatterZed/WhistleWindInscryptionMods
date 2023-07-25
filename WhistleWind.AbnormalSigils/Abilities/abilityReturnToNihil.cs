using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_ReturnToNihil()
        {
            const string rulebookName = "Return to Nihil";
            const string rulebookDescription = "At the end of the owner's turn, deal damage equal to this card's Power to all other cards on the board.";
            const string dialogue = "One step closer to oblivion.";
            const string triggerText = "The void calls.";
            ReturnToNihil.ability = AbnormalAbilityHelper.CreateAbility<ReturnToNihil>(
                "sigilReturnToNihil",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 5,
                modular: false, special: true, opponent: false, canStack: false).Id;
        }
    }
    public class ReturnToNihil : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd && base.Card.Attack > 0;

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            List<CardSlot> slots = Singleton<BoardManager>.Instance.AllSlotsCopy.FindAll(s => s != base.Card.Slot && s.Card != null);

            if (slots.Count == 0)
                yield break;

            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, lockAfter: true);
            yield return new WaitForSeconds(0.2f);

            foreach (CardSlot slot in slots)
            {
                yield return slot.Card.TakeDamage(base.Card.Attack, null);
                yield return new WaitForSeconds(0.1f);
            }
            yield return base.LearnAbility(0.2f);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
    }
}
