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
            if (base.Card.Slot.opposingSlot != null)
            {
                if (base.Card.Slot.opposingSlot.IsPlayerSlot)
                {
                    return playerTurnEnd;
                }
                return !playerTurnEnd;
            }
            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (base.Card.Slot.opposingSlot.Card != null)
            {
                yield return PreSuccessfulTriggerSequence();
                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
                base.Card.Slot.opposingSlot.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.55f);
                yield return base.Card.Slot.opposingSlot.Card.TakeDamage(1, base.Card.Slot.opposingSlot.Card);
                yield return new WaitForSeconds(0.4f);
                yield return base.LearnAbility(0.4f);
            }

        }
    }
}
