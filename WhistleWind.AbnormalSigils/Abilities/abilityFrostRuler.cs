using WhistleWind.Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FrostRuler()
        {
            const string rulebookName = "Ruler of Frost";
            const string rulebookDescription = "Once per turn, pay 3 Bones to either create a Block of Ice in a chosen empty slot, or turn a chosen card whose Health is less than or equal to this card's Power into a Frozen Heart.";
            const string dialogue = "With a wave of her hand, the Snow Queen blocked the path.";
            FrostRuler.ability = AbnormalAbilityHelper.CreateActivatedAbility<FrostRuler>(
                Artwork.sigilFrostRuler, Artwork.sigilFrostRuler_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4).Id;
        }
    }

    public class FrostRuler : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool TargetAll => true;
        public override string NoTargetsDialogue => "The enemy is immune to the cold.";
        public override string InvalidTargetDialogue => "Frost cannot penetrate this one. Choose another.";
        public override int StartingBonesCost => 3;
        public override int TurnDelay => 1;
        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            if (slot.Card != null)
            {
                slot.Card.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.15f);
                yield return slot.Card.DieTriggerless();

                yield return SpawnCard(slot, "wstl_snowQueenIceHeart");
                yield return new WaitForSeconds(0.5f);
                yield return AbnormalDialogueManager.PlayDialogueEvent("FrostRulerKiss");
            }
            else
                yield return SpawnCard(slot, "wstl_snowQueenIceBlock");

            yield return base.LearnAbility();
        }

        public override Predicate<CardSlot> InvalidTargets()
        {
            return (CardSlot x) => x.Card != null && (x.Card.Dead || this.CardIsNotValid(x.Card) || x.Card == base.Card);
        }
        public override bool CardIsNotValid(PlayableCard card)
        {
            // not valid if Health > 1 or has Burning or is uncuttable and isn't a mule card
            return card.Health > base.Card.Attack || card.HasAbility(Burning.ability) ||
                (card.LacksSpecialAbility(SpecialTriggeredAbility.PackMule) && card.HasTrait(Trait.Uncuttable));
        }

        private IEnumerator SpawnCard(CardSlot slot, string name)
        {
            CardInfo cardByName = CardLoader.GetCardByName(name);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, slot, 0.15f);
        }
    }
}
