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
            const string rulebookDescription = "When this card is played, create a Block of Ice in each opposing space to the left and right of this card. If either slot is occupied by a card with 1 Health, kill it and create a Frozen Heart in its place.";
            const string dialogue = "With a wave of her hand, the Snow Queen blocked the path.";
            FrostRuler.ability = AbilityHelper.CreateAbility<FrostRuler>(
                Resources.sigilFrostRuler, Resources.sigilFrostRuler_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5,
                addModular: false, opponent: false, canStack: false, isPassive: false,
                flipY: true).Id;
        }
    }
    public class FrostRuler : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string failDialogue = "The snow melts away. Perhaps spring is coming.";
        private readonly string kissDialogue = "With a single kiss, the Snow Queen froze their hearts.";

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);

            CardSlot opposingSlotLeft = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: true);
            CardSlot opposingSlotRight = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: false);
            bool spawnedBlock = false;
            bool spawnedHeart = false;

            yield return new WaitForSeconds(0.1f);
            yield return base.PreSuccessfulTriggerSequence();

            if (opposingSlotLeft != null)
            {
                opposingSlotLeft = opposingSlotLeft.opposingSlot;
                if (opposingSlotLeft.Card == null)
                {
                    spawnedBlock = true;
                    yield return SpawnCard(opposingSlotLeft, "wstl_snowQueenIceBlock");
                }
                else if (opposingSlotLeft.Card != null && opposingSlotLeft.Card.Health == 1 &&
                    !opposingSlotLeft.Card.HasAbility(Burning.ability) && !opposingSlotLeft.Card.HasAbility(TrueSaviour.ability) &&
                    !opposingSlotLeft.Card.HasAbility(Apostle.ability) && !opposingSlotLeft.Card.HasAbility(Confession.ability))
                {
                    spawnedHeart = true;
                    opposingSlotLeft.Card.Anim.LightNegationEffect();
                    yield return new WaitForSeconds(0.15f);
                    yield return opposingSlotLeft.Card.Die(false, base.Card);
                    yield return SpawnCard(opposingSlotLeft, "wstl_snowQueenIceHeart");
                    if (!WstlSaveManager.HasSeenSnowQueenFreeze)
                    {
                        WstlSaveManager.HasSeenSnowQueenFreeze = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(kissDialogue, -0.65f, 0.4f);
                    }
                }
            }
            if (opposingSlotRight != null)
            {
                opposingSlotRight = opposingSlotRight.opposingSlot;
                if (opposingSlotRight.Card == null)
                {
                    spawnedBlock = true;
                    yield return SpawnCard(opposingSlotRight, "wstl_snowQueenIceBlock");
                }
                else if (opposingSlotRight.Card != null && opposingSlotRight.Card.Health == 1 &&
                    !opposingSlotLeft.Card.HasAbility(Burning.ability) && !opposingSlotLeft.Card.HasAbility(TrueSaviour.ability) &&
                    !opposingSlotLeft.Card.HasAbility(Apostle.ability) && !opposingSlotLeft.Card.HasAbility(Confession.ability))
                {
                    spawnedHeart = true;
                    opposingSlotRight.Card.Anim.LightNegationEffect();
                    yield return new WaitForSeconds(0.15f);
                    yield return opposingSlotRight.Card.Die(false, base.Card);
                    yield return SpawnCard(opposingSlotRight, "wstl_snowQueenIceHeart");
                    if (!WstlSaveManager.HasSeenSnowQueenFreeze)
                    {
                        WstlSaveManager.HasSeenSnowQueenFreeze = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(kissDialogue, -0.65f, 0.4f);
                    }
                }
            }
            if (spawnedBlock)
            {
                yield return base.LearnAbility();
            }
            else if (!spawnedBlock && !spawnedHeart)
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                if (!WstlSaveManager.HasSeenSnowQueenFail)
                {
                    WstlSaveManager.HasSeenSnowQueenFail = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(failDialogue, -0.65f, 0.4f);
                }
            }
        }
        private IEnumerator SpawnCard(CardSlot slot,string name)
        {
            CardInfo cardByName = CardLoader.GetCardByName(name);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, slot, 0.15f);
        }
    }
}
