using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_GroupHealer()
        {
            const string rulebookName = "Group Healer";
            const string rulebookDescription = "While this card is on the board, all allies whose Health is below their maximum regain 1 Health on upkeep.";
            const string dialogue = "You only delay the inevitable.";
            GroupHealer.ability = AbilityHelper.CreateAbility<GroupHealer>(
                Resources.sigilGroupHealer, Resources.sigilGroupHealer_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class GroupHealer : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.Card.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.25f);

            int count = 0;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).Where(slot => slot.Card != base.Card))
            {
                if (slot.Card != null && slot.Card.Health < slot.Card.MaxHealth)
                {
                    count++;
                    slot.Card.Anim.LightNegationEffect();
                    slot.Card.HealDamage(1);
                    slot.Card.OnStatsChanged();
                }
            }
            if (count != 0)
            {
                yield return base.LearnAbility(0.4f);
            }
            else
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
            }
        }
    }
}
