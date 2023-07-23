using DiskCardGame;
using System;
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
            const string rulebookDescription = "Once per turn, pay 3 Bones to choose a space on the board. If the space is occupied by a killable card, transform it into a Frozen Heart. Otherwise create a Block of Ice.";
            const string dialogue = "With a wave of her hand, the Snow Queen blocked the path.";
            const string triggerText = "[creature] freezes the path.";
            FrostRuler.ability = AbnormalAbilityHelper.CreateActivatedAbility<FrostRuler>(
                "sigilFrostRuler",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4).Id;
        }
    }

    public class FrostRuler : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override string NoTargetsDialogue => "The enemy is immune to the cold.";
        public override string InvalidTargetDialogue => "Frost cannot penetrate this one. Choose another.";
        public override int StartingBonesCost => 3;
        public override int TurnDelay => 1;

        public override bool CardSlotCanBeTargeted(CardSlot slot) => slot != base.Card.Slot;

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
                yield return DialogueHelper.PlayDialogueEvent("FrostRulerKiss");
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
            if (card.HasAbility(Scorching.ability))
                return false;

            return card.Health > base.Card.Attack;
        }

        private IEnumerator SpawnCard(CardSlot slot, string name)
        {
            CardInfo cardByName = CardLoader.GetCardByName(name);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, slot, 0.15f);
        }
    }
}
