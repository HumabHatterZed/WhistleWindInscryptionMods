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
        private void Ability_Conductor()
        {
            const string rulebookName = "Conductor";
            const string rulebookDescription = "While this card is on the board, adjacent cards gain 1 Power. After 1 turn on the board, all ally cards gain 1 Power instead. After 2 turns, also reduce the opposing card's Power by 1. After 3 turns, also gain Power equal to the number of cards on this side of the board.";
            const string dialogue = "From break and ruin, the most beautiful performance begins.";

            Conductor.ability = AbilityHelper.CreateAbility<Conductor>(
                Artwork.sigilConductor, Artwork.sigilConductor_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5,
                addModular: false, opponent: true, canStack: true, isPassive: false).Id;
        }
    }
    public class Conductor : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int turnCount = 0;

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.LearnAbility(0.4f);
        }

        public override bool RespondsToUpkeep(bool onPlayerUpkeep)
        {
            if (turnCount < 3)
                return base.Card.OpponentCard != onPlayerUpkeep;
            return false;
        }
        public override IEnumerator OnUpkeep(bool onPlayerUpkeep)
        {
            turnCount++;
            yield return base.PreSuccessfulTriggerSequence();
            if (Singleton<ViewManager>.Instance.CurrentView != View.Board)
            {
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Board);
                yield return new WaitForSeconds(0.2f);
            }
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
        }

        public int GetPassiveAttackBuff(PlayableCard target)
        {
            switch (turnCount)
            {
                case 0:
                    // when first played, give Power to adjacent
                    bool inAdjacent = Singleton<BoardManager>.Instance.GetAdjacentSlots(target.Slot).Where(s => s.Card != null && s.Card == base.Card).Count() > 0;
                    return this.Card.OnBoard && target.OpponentCard == this.Card.OpponentCard && target != base.Card && inAdjacent ? 1 : 0;
                case 1:
                    // after 1 turn, give Power to all allies
                    return this.Card.OnBoard && target.OpponentCard == this.Card.OpponentCard && target != base.Card ? 1 : 0;
                case 2:
                    // after 2 turns, give Power to all allies and debuff opposing
                    return this.Card.OnBoard && target.OpponentCard == this.Card.OpponentCard && target != base.Card ? 1 : (target.Slot.opposingSlot.Card == base.Card ? -1 : 0);
                case 3:
                    // after 3 turns, give Power to all allies and debuff opposing and gain Power equal to num of cards on this side of board
                    int num = Singleton<BoardManager>.Instance.GetSlots(!target.OpponentCard).Where(s => s.Card != null).Count();
                    return this.Card.OnBoard && target.OpponentCard == this.Card.OpponentCard && target != base.Card ? 1 : (target.Slot.opposingSlot.Card == base.Card ? -1 : (target == base.Card ? num : 0));
                default:
                    // catch-all
                    return 0;
            }
        }
    }
}
