using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FrostRuler()
        {
            const string rulebookName = "Ruler of Frost";
            const string rulebookDescription = "Once per turn, pay 2 Bones to choose a space on the board and create a Block of Ice, or pay 4 Bones to kill a card and create a Frozen Heart.";
            const string dialogue = "With a wave of her hand, the Snow Queen blocked the path.";
            const string triggerText = "[creature] freezes the path.";
            FrostRuler.ability = AbnormalAbilityHelper.CreateActivatedAbility<FrostRuler>(
                "sigilFrostRuler",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }

    public class FrostRuler : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override string InvalidTargetDialogue(CardSlot slot)
        {
            if (slot.Card.HasAbility(Scorching.ability))
                return "This creature burns with passion. It cannot freeze.";

            if (slot.Card.HasAnyOfTraits(Trait.Terrain, Trait.Pelt))
                return "The cold cannot turn one without a heart. Choose another.";
            
            return "Frost cannot penetrate this one. Choose another.";
        }
        public override int StartingBonesCost => 2;
        public override int TurnDelay => 1;

        public override bool CanActivate()
        {
            // if we can only target an occupied space and can't afford it.
            if (BoardManager.Instance.CardsOnBoard.Count == BoardManager.Instance.AllSlotsCopy.Count && ResourcesManager.Instance.PlayerBones < 4)
            {
                return false;
            }
            return base.CanActivate();
        }
        public override bool AIEvaluatePositiveEffect(CardSlot slot)
        {
            if (slot.IsPlayerSlot == base.Card.Slot.IsPlayerSlot)
            {
                if (slot.Card == null)
                {
                    foreach (PlayableCard card in BoardManager.Instance.GetCards(base.Card.OpponentCard))
                    {
                        // if this slot is empty and an opponent card can attack this space
                        if (card.Attack > 0 && card.GetOpposingSlots().Contains(slot))
                            return true;
                    }
                }
                return false;
            }
            else if (slot.Card == null)
            {
                foreach (PlayableCard card in BoardManager.Instance.GetCards(!base.Card.OpponentCard))
                {
                    // if this slot is empty and an opponent card can attack this space
                    if (card.Attack > 0 && card.GetOpposingSlots().Contains(slot))
                        return false;
                }
            }
            return base.AIEvaluatePositiveEffect(slot);
        }
        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            if (slot.Card != null)
            {
                if (!slot.Card.OpponentCard)
                    yield return ResourcesManager.Instance.SpendBones(2);

                yield return slot.Card.Die(false, base.Card);
                if (slot.Card == null)
                    yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_snowQueenIceHeart"), slot, 0.15f);
                
                yield return new WaitForSeconds(0.6f);
                yield return DialogueHelper.PlayDialogueEvent("FrostRulerKiss");
            }
            else
            {
                CardInfo cardByName = CardLoader.GetCardByName("wstl_snowQueenIceBlock");
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, slot, 0.15f);
            }

            yield return base.LearnAbility();
        }

        public override bool IsValidTarget(CardSlot slot)
        {
            if (slot == base.Card.Slot)
                return false;

            if (slot.Card != null)
            {
                if (base.Card.OpponentCard || ResourcesManager.Instance.PlayerBones > 3)
                    return slot.Card.LacksAllTraits(Trait.Uncuttable, Trait.Terrain, Trait.Pelt, Trait.Giant) && slot.Card.LacksAbility(Scorching.ability);
                
                return false;
            }
            return true;
        }
    }
}
