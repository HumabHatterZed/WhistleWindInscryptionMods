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
        private NewAbility Ability_TheTrain()
        {
            const string rulebookName = "The Train";
            const string rulebookDescription = "One turn after this card is played, kill all cards on the board.";
            const string dialogue = "The train boards those that don't step away from the tracks.";

            return WstlUtils.CreateAbility<TheTrain>(
                Resources.sigilTheTrain,
                rulebookName, rulebookDescription, dialogue, 5);
        }
    }
    public class TheTrain : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int count = 0;

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (!base.Card.Slot.IsPlayerSlot)
            {
                return !playerTurnEnd;
            }
            return playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (count >= 1)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Board);

                yield return PreSuccessfulTriggerSequence();

                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.55f);
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != base.Card))
                {
                    if (slot.Card != null)
                    {
                        slot.Card.Anim.StrongNegationEffect();
                    }
                }
                yield return new WaitForSeconds(0.55f);
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != base.Card))
                {
                    if (slot.Card != null)
                    {
                        yield return slot.Card.Die(false, base.Card);
                    }
                }
                yield return new WaitForSeconds(0.55f);
                yield return this.Card.Die(false, base.Card);
                yield return new WaitForSeconds(0.4f);
                yield return LearnAbility(0.4f);
            }
            else
            {
                count++;
            }
            yield break;
        }
    }
}
