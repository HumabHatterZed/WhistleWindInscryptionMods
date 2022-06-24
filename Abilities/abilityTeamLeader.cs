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

            TeamLeader.ability = WstlUtils.CreateAbility<TeamLeader>(
                Resources.sigilTeamLeader,
                rulebookName, rulebookDescription, dialogue, 5).Id;
        }
    }
    public class TeamLeader : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard()
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
        public override IEnumerator OnResolveOnBoard()
        {
            BuffAllies allies = new BuffAllies();

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(true))
            {
                if (slot.Card != null && slot.Card != base.Card)
                {
                    yield return allies.GetPassiveAttackBuff(slot.Card);
                }
            }
            yield return base.LearnAbility(0.4f);

        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return !otherCard.OpponentCard;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            BuffAllies allies = new BuffAllies();

            if (otherCard != null && otherCard != base.Card)
            {
                allies.GetPassiveAttackBuff(otherCard);
            }
            yield return base.LearnAbility(0.4f);
        }
    }

    /*public class OnBuffAllies : IOnCardPassiveAttackBuffs
    {
        public bool RespondsToCardPassiveAttackBuffs(PlayableCard card, int value)
        {
            if (card != null && card.OnBoard && !card.OpponentCard && value > 0)
            {
                WstlPlugin.Log.LogInfo("true");
                return true;
            }
            else
            {
                WstlPlugin.Log.LogInfo("false");
                return false;
            }
        }

    }*/
    
    public class BuffAllies : IPassiveAttackBuff
    {
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            if (target != null && target.OnBoard && !target.OpponentCard)
            {
                WstlPlugin.Log.LogInfo("1");
                return 1;
            }
            else
            {
                WstlPlugin.Log.LogInfo("0");
                return 0;
            }
        }
    }
}
