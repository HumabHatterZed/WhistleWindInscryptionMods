using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FrostRuler()
        {
            const string rulebookName = "Ruler of Frost";
            const string rulebookDescription = "Once per turn, pay 4 Bones to create a Block of Ice in an empty opposing slot. If there is an opposing card with 1 Health you may instead select it to kill it and create a Frozen Heart in its place.";
            const string dialogue = "With a wave of her hand, the Snow Queen blocked the path.";
            FrostRuler.ability = AbnormalAbilityHelper.CreateActivatedAbility<FrostRuler>(
                Artwork.sigilFrostRuler, Artwork.sigilFrostRuler_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5).Id;
        }
    }
    public class FrostRuler : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability LatchAbility => Ability.None;
        public override bool TargetAll => false;
        public override string NoTargetsDialogue => "The enemy is immune to the cold.";
        public override string InvalidTargetDialogue => "Frost cannot penetrate this one. Choose another.";
        public override int BonesCost => 4;

        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            if (slot.Card != null)
            {
                slot.Card.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.15f);
                yield return slot.Card.Die(false, base.Card);

                // if another card gets played after killing the initial, play fail dialogue then break
                if (slot.Card != null)
                {
                    yield return new WaitForSeconds(0.5f);
                    yield return AbnormalDialogueManager.PlayDialogueEvent("FrostRulerFail");
                    yield break;
                }

                yield return SpawnCard(slot, "wstl_snowQueenIceHeart");
                yield return new WaitForSeconds(0.5f);
                yield return AbnormalDialogueManager.PlayDialogueEvent("FrostRulerKiss");
            }
            else
                yield return SpawnCard(slot, "wstl_snowQueenIceBlock");

            yield return base.LearnAbility();
        }

        public override bool ValidTargets()
        {
            // can attack empty slots and valid cards
            List<CardSlot> validSlots = base.GetInitialTargets();
            validSlots.RemoveAll((CardSlot x) => x.Card != null ? (x.Card.Dead || this.CardIsNotValid(x.Card) || x.Card == base.Card) : x == null);
            return validSlots.Count() > 0;
        }
        public override bool CardIsNotValid(PlayableCard card)
        {
            // not valid if Health > 1 or has Burning or is uncuttable and isn't a mule card
            return card.Health > 1 || card.HasAbility(Burning.ability) || (card.LacksSpecialAbility(SpecialTriggeredAbility.PackMule) && card.HasTrait(Trait.Uncuttable));
        }

        private IEnumerator SpawnCard(CardSlot slot, string name)
        {
            CardInfo cardByName = CardLoader.GetCardByName(name);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, slot, 0.15f);
        }
    }
}
