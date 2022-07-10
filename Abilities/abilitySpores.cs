using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Spores()
        {
            const string rulebookName = "Spores";
            const string rulebookDescription = "Adjacent cards gain 1 Spore at the start of each turn and take damage equal to their amount of Spore at the end of each turn. If a card with Spore is killed, create a Spore Mold Creature in that card's slot.";
            const string dialogue = "Resentment bursts forth like a weed.";
            Spores.ability = WstlUtils.CreateAbility<Spores>(
                Resources.sigilSpores,
                rulebookName, rulebookDescription, dialogue, 2,
                addModular: true).Id;
        }
    }
    public class Spores : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private string altDialogue = "Not enough space for the vines to grow.";

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            CardSlot toLeft = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: true);
            CardSlot toRight = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: false);
            bool toLeftValid = toLeft != null && toLeft.Card == null;
            bool toRightValid = toRight != null && toRight.Card == null;
            yield return base.PreSuccessfulTriggerSequence();
            if (toLeftValid)
            {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toLeft);
            }
            if (toRightValid)
            {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toRight);
            }
            if (toLeftValid || toRightValid)
            {
                yield return new WaitForSeconds(0.25f);
                yield return base.LearnAbility();
            }
            else
            {
                yield return new WaitForSeconds(0.25f);
                base.Card.Anim.StrongNegationEffect();
                if (!base.HasLearned)
                {
                    yield return new WaitForSeconds(0.25f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue, -0.65f, 0.4f);
                }
                yield return new WaitForSeconds(0.25f);
            }
        }

        private IEnumerator SpawnCardOnSlot(CardSlot slot)
        {
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_snowWhitesVine"), slot, 0.15f);
        }
    }
}
