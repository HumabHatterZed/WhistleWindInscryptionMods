using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_FrostRuler()
        {
            const string rulebookName = "Ruler of Frost";
            const string rulebookDescription = "When this card is played, create a Block of Ice in the opposing spaces to its left and right. If they are occupied, kill the occupying card and create a Frozen Heart in their place. A Frozen Heart is defined as: 0 Power, 1 Health, and a Block of Ice is defined as: 0 Power, 3 Health.";
            const string dialogue = "With a single kiss, the Snow Queen froze their hearts.";
            return WstlUtils.CreateAbility<FrostRuler>(
                Resources.sigilFrostRuler,
                rulebookName, rulebookDescription, dialogue, 5);
        }
    }
    public class FrostRuler : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override int Priority => int.MaxValue;

        private string altDialogue = "With a wave of her hand, the Snow Queen blocked the path.";

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);

            CardSlot toLeft = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: true);
            CardSlot toRight = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: false);

            CardSlot toTopLeft = null;
            CardSlot toTopRight = null;

            bool toLeftValid = toLeft != null;
            bool toRightValid = toRight != null;

            if (toLeftValid)
            {
                toTopLeft = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: true).opposingSlot;
            }
            if (toRightValid)
            {
                toTopRight = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: false).opposingSlot;
            }

            bool toLeftHeart = toTopLeft != null && toTopLeft.Card != null;
            bool toLeftBlock = toTopLeft != null && toTopLeft.Card == null;

            bool toRightHeart = toTopRight != null && toTopRight.Card != null;
            bool toRightBlock = toTopRight != null && toTopRight.Card == null;
            yield return base.PreSuccessfulTriggerSequence();
            if (toLeftHeart)
            {
                yield return new WaitForSeconds(0.25f);
                yield return toTopLeft.Card.Die(false, base.Card);
                yield return this.SpawnFrozenHeart(toTopLeft);
                yield return new WaitForSeconds(0.25f);
            }
            if (toLeftBlock)
            {
                yield return this.SpawnBlockOfIce(toTopLeft);
                yield return new WaitForSeconds(0.25f);
            }
            if (toRightHeart)
            {
                yield return new WaitForSeconds(0.25f);
                yield return toTopRight.Card.Die(false, base.Card);
                yield return this.SpawnFrozenHeart(toTopRight);
                yield return new WaitForSeconds(0.25f);
            }
            if (toRightBlock)
            {
                yield return this.SpawnBlockOfIce(toTopRight);
                yield return new WaitForSeconds(0.25f);
            }

            if (toLeftHeart || toRightHeart)
            {
                yield return new WaitForSeconds(0.25f);
                yield return base.LearnAbility(0.25f);
            }
            else if (!base.HasLearned)
            {
                yield return new WaitForSeconds(0.25f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue, -0.65f, 0.4f);
            }
        }

        private IEnumerator SpawnFrozenHeart(CardSlot slot)
        {
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_frozenHeart"), slot, 0.15f);
        }

        private IEnumerator SpawnBlockOfIce(CardSlot slot)
        {
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_blockOfIce"), slot, 0.15f);
        }
    }
}
