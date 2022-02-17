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
        private NewAbility Ability_TheTrain()
        {
            const string rulebookName = "The Train";
            const string rulebookDescription = "One turn after this card is played, kill all cards on the board. If this card is not the ticket taker, kill only the card's allies at a 20% chance.";
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
            count++;
            yield return PreSuccessfulTriggerSequence();
            if (count >= 2)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Board);

                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.55f);
                if (base.Card.Info.name.ToLowerInvariant().Equals("wstl_expresshelltrain"))
                {
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
                }
                else
                {
                    int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;
                    int rand = SeededRandom.Range(0, 5, randomSeed);
                    if (rand == 0)
                    {
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
                    }
                    else
                    {
                        foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(base.Card.Slot.IsPlayerSlot).Where(slot => slot.Card != base.Card))
                        {
                            if (slot.Card != null)
                            {
                                slot.Card.Anim.StrongNegationEffect();
                            }
                        }
                        yield return new WaitForSeconds(0.55f);
                        foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(base.Card.Slot.IsPlayerSlot).Where(slot => slot.Card != base.Card))
                        {
                            if (slot.Card != null)
                            {
                                yield return slot.Card.Die(false, base.Card);
                            }
                        }
                    }
                }
                yield return new WaitForSeconds(0.55f);
                yield return this.Card.Die(false, base.Card);
                yield return new WaitForSeconds(0.4f);
                yield return LearnAbility(0.4f);
            }
        }
    }
}
