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
        private NewAbility Ability_Burning()
        {
            const string rulebookName = "Burning";
            const string rulebookDescription = "The opposing card takes 1 damage at the end of their turn.";
            const string dialogue = "A slow and painful death.";

            return WstlUtils.CreateAbility<Burning>(
                Resources.sigilBurning,
                rulebookName, rulebookDescription, dialogue, 2, true);
        }
    }
    public class Burning : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (!(base.Card.Slot.opposingSlot.Card != null))
            {
                return false;
            }
            return base.Card.Slot.opposingSlot.Card.Slot.IsPlayerSlot ? !playerTurnEnd : playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return PreSuccessfulTriggerSequence();
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
            base.Card.Slot.opposingSlot.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            yield return base.Card.Slot.opposingSlot.Card.TakeDamage(1, null);
            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility(0.4f);
        }
    }
}
