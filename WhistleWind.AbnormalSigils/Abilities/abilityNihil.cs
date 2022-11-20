using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Nihil()
        {
            const string rulebookName = "Nihil";
            const string rulebookDescription = "While this card is on the board, gain 1 Power for each empty board slot. On turn's end, deal damage to all other cards on the board equal to this card's Power.";
            const string dialogue = "All returns to nihil.";

            Nihil.ability = AbnormalAbilityHelper.CreateAbility<Nihil>(
                Artwork.sigilNihil, Artwork.sigilNihil_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5,
                modular: false, special: true, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Nihil : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            List<CardSlot> slots = Singleton<BoardManager>.Instance.AllSlotsCopy.FindAll(s => s != base.Card.Slot && s.Card != null);

            if (slots.Count < 1)
                yield break;

            int damage = base.Card.Attack;

            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            yield return AbnormalMethods.ChangeCurrentView(View.Board);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return new WaitForSeconds(0.2f);

            foreach (CardSlot slot in slots)
            {
                yield return slot.Card.TakeDamage(damage, base.Card);
                yield return new WaitForSeconds(0.2f);
            }
            yield return base.LearnAbility(0.2f);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            int count = Singleton<BoardManager>.Instance.AllSlotsCopy.FindAll(slot => slot != base.Card.Slot && slot.Card == null).Count;
            return this.Card.OnBoard && target == this.Card ? count : 0;
        }
    }
}
