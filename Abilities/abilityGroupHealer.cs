using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_GroupHealer()
        {
            const string rulebookName = "Group Healer";
            const string rulebookDescription = "While a card bearing this sigil is on the board, all allies regain 1 Health at the end of the opponent's turn.";
            const string dialogue = "You only delay the inevitable.";
            return WstlUtils.CreateAbility<GroupHealer>(
                Resources.sigilGroupHealer,
                rulebookName, rulebookDescription, dialogue, 4);
        }
    }
    public class GroupHealer : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (!Card.Slot.IsPlayerSlot)
            {
                return playerTurnEnd;
            }
            return !playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);

            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.25f);
            if (Card.Slot.IsPlayerSlot)
            {
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot.Card != Card))
                {
                    if (slot.Card != null && slot.Card.Health < slot.Card.MaxHealth)
                    {
                        slot.Card.HealDamage(1);
                        slot.Card.OnStatsChanged();
                        slot.Card.Anim.StrongNegationEffect();
                    }
                }
            }
            if (!base.Card.Slot.IsPlayerSlot)
            {
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(false).Where(slot => slot.Card != Card))
                {
                    if (slot.Card != null && slot.Card.Health < slot.Card.MaxHealth)
                    {
                        slot.Card.HealDamage(1);
                        slot.Card.OnStatsChanged();
                        slot.Card.Anim.StrongNegationEffect();
                    }
                }
            }
            yield return new WaitForSeconds(0.4f);
            yield return LearnAbility(0.4f);
        }
    }
}
