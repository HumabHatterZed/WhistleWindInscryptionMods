using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_FrostRuler()
        {
            const string rulebookName = "Ruler of Frost";
            const string rulebookDescription = "When this card is played, create a Block of Ice in the opposing spaces to its left and right. A Block of Ice is defined as: 0 Power, 1 Health.";
            const string dialogue = "With a wave of her hand, the Snow Queen blocked the path.";
            FrostRuler.ability = WstlUtils.CreateAbility<FrostRuler>(
                Resources.sigilFrostRuler,
                rulebookName, rulebookDescription, dialogue, 5).Id;
        }
    }
    public class FrostRuler : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override int Priority => int.MaxValue;

        private readonly string failDialogue = "The snow melts away. Perhaps spring is coming.";
        private readonly string springDialogue = "Spring arrived with blossoming roses.";
        private readonly string heartDialogue = "With a single kiss, the Snow Queen froze their hearts.";

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
                toTopLeft = toLeft.opposingSlot;
            }
            if (toRightValid)
            {
                toTopRight = toRight.opposingSlot;
            }

            bool toLeftBlock = toTopLeft.Card == null;
            bool toRightBlock = toTopRight.Card == null;

            bool toLeftHeart = toTopLeft.Card != null;
            bool toRightHeart = toTopRight.Card != null;

            yield return base.PreSuccessfulTriggerSequence();

            if (toLeftBlock)
            {
                yield return this.SpawnBlockOfIce(toTopLeft);
                yield return new WaitForSeconds(0.25f);
            }
            if (toRightBlock)
            {
                yield return this.SpawnBlockOfIce(toTopRight);
                yield return new WaitForSeconds(0.25f);
            }
            if (toLeftBlock || toRightBlock)
            {
                yield return base.LearnAbility(0.25f);
            }

            if (base.Card.Info.name.ToLowerInvariant().Contains("snowqueen"))
            {
                if (toLeftHeart &&
                    !toTopLeft.Card.FaceDown &&
                    !toTopLeft.Card.Info.HasTrait(Trait.Uncuttable) &&
                    !toTopLeft.Card.Info.HasTrait(Trait.Terrain) &&
                    !toTopLeft.Card.Info.HasTrait(Trait.Pelt))
                {
                    yield return new WaitForSeconds(0.25f);
                    yield return toTopLeft.Card.Die(false, base.Card);
                    yield return this.SpawnFrozenHeart(toTopLeft);
                    yield return new WaitForSeconds(0.25f);
                }
                if (toRightHeart &&
                    !toTopRight.Card.FaceDown &&
                    !toTopRight.Card.Info.HasTrait(Trait.Uncuttable) &&
                    !toTopRight.Card.Info.HasTrait(Trait.Terrain) &&
                    !toTopRight.Card.Info.HasTrait(Trait.Pelt))
                {
                    yield return new WaitForSeconds(0.25f);
                    yield return toTopRight.Card.Die(false, base.Card);
                    yield return this.SpawnFrozenHeart(toTopRight);
                    yield return new WaitForSeconds(0.25f);
                }

                if (toLeftHeart || toRightHeart && !PersistentValues.HasSeenSnowQueenFreeze)
                {
                    PersistentValues.HasSeenSnowQueenFreeze = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(heartDialogue, -0.65f, 0.4f);
                }
                if (!toLeftBlock && !toRightBlock && !toLeftHeart && !toRightHeart)
                {
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(springDialogue, -0.65f, 0.4f);
                }
            }
            else
            {
                if (!toLeftBlock && !toRightBlock)
                {
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(failDialogue, -0.65f, 0.4f);
                }
            }

        }

        private IEnumerator SpawnFrozenHeart(CardSlot slot)
        {
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_snowQueenIceHeart"), slot, 0.15f);
        }

        private IEnumerator SpawnBlockOfIce(CardSlot slot)
        {
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_snowQueenIceBlock"), slot, 0.15f);
        }
    }
}
