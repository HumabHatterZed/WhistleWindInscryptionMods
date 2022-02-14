using APIPlugin;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_TeamLeader()
        {
            const string rulebookName = "Team Leader";
            const string rulebookDescription = "While this card is on the board, all ally cards gain 1 Power.";
            const string dialogue = "Your beast emboldens its allies.";

            return WstlUtils.CreateAbility<TeamLeader>(
                Resources.sigilTeamLeader,
                rulebookName, rulebookDescription, dialogue, 5);
        }
    }
    public class TeamLeader : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard()
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.LearnAbility(0.5f);
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.LearnAbility(0.5f);
        }
        public bool ActivateOnPlay()
        {
            int num = 0;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(true))
            {
                if (slot.Card != null)
                {
                    num++;
                }
            }
            return num > 1;
        }
    }
}
