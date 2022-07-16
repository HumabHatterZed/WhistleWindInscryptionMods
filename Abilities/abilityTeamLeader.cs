using InscryptionAPI;
using InscryptionAPI.Triggers;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_TeamLeader()
        {
            const string rulebookName = "Team Leader";
            const string rulebookDescription = "While this card is on the board, all ally cards gain 1 Power.";
            const string dialogue = "Your beast emboldens its allies.";

            TeamLeader.ability = AbilityHelper.CreateAbility<TeamLeader>(
                Resources.sigilTeamLeader,
                rulebookName, rulebookDescription, dialogue, 5).Id;
        }
    }
    public class TeamLeader : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        // Check if there are already other cards on the board
        // Used for LearnAbility dialogue
        public override bool RespondsToResolveOnBoard()
        {
            List<CardSlot> otherSlots = base.Card.OpponentCard ? Singleton<BoardManager>.Instance.OpponentSlotsCopy : Singleton<BoardManager>.Instance.PlayerSlotsCopy;
            List<CardSlot> validSlots = otherSlots.FindAll((CardSlot x) => x.Card != null && x.Card != base.Card);
            if (validSlots.Count > 0)
            {
                return true;
            }
            return false;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.LearnAbility(0.4f);
        }

        // Respond to other card resolving if it and this card are on the player's side of the board and
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return base.Card.OnBoard && !base.Card.OpponentCard && !otherCard.OpponentCard;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.LearnAbility(0.4f);
        }

        // Gives +1 Power if on board and target card is on same side of the board
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            return this.Card.OnBoard && target.OpponentCard == this.Card.OpponentCard && target != base.Card ? 1 : 0;
        }
    }
}
