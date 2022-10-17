using InscryptionAPI;
using InscryptionAPI.Triggers;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Nihil()
        {
            const string rulebookName = "Nihil";
            const string rulebookDescription = "While this card is on the board, gain 1 Power for each empty board slot. On turn's end, deal damage to all other cards on the board equal to this card's Power.";
            const string dialogue = "All returns to nihil.";

            Nihil.ability = AbilityHelper.CreateAbility<Nihil>(
                Artwork.sigilNihil, Artwork.sigilNihil_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5,
                addModular: false, opponent: false, canStack: false, isPassive: false,
                overrideModular: true).Id;
        }
    }
    public class Nihil : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return base.Card.OpponentCard != playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            List<CardSlot> slots = Singleton<BoardManager>.Instance.AllSlotsCopy.Where(s => s != base.Card.Slot && s.Card != null).ToList();

            if (slots.Count() < 1)
                yield break;

            int damage = base.Card.Attack;

            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
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
            int count = Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot != base.Card.Slot && slot.Card == null).Count();
            return this.Card.OnBoard && target == this.Card ? count : 0;
        }
    }
}
