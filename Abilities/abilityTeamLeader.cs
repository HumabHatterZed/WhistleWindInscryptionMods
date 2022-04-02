using InscryptionAPI;
using InscryptionAPI.Card;
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

            TeamLeader.ability = WstlUtils.CreateAbility<TeamLeader>(
                Resources.sigilTeamLeader,
                rulebookName, rulebookDescription, dialogue, 5).Id;
        }
    }
    public class TeamLeader : ExtendedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool ProvidesPassiveAttackBuff => true;
        public override int[] GetPassiveAttackBuffs()
        {
            List<int> slots = new();
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(base.Card.Slot.IsPlayerSlot))
            {
                if (slot.Card != null && slot.Card != base.Card)
                {
                    slots.Add(1);
                }
                else
                {
                    slots.Add(0);
                }
            }
            return slots.ToArray();
        }
        public override bool RespondsToResolveOnBoard()
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.LearnAbility(0.4f);
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.LearnAbility(0.4f);
        }
        public bool ActivateOnPlay()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(true))
            {
                if (slot.Card != null && slot.Card != base.Card)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
