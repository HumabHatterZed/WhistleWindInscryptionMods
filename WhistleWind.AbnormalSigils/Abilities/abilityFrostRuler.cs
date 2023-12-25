using DiskCardGame;
using InscryptionAPI.Card;
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
            const string rulebookDescription = "Once per turn, pay 2 Bones to choose a space on the board. If the space is empty, create a Block of Ice. Otherwise, if this card can kill the occupying card, transform it into a Frozen Heart.";
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
        public override string InvalidTargetDialogue(CardSlot slot)
        {
            if (slot.Card.Health > base.Card.Attack)
                return "Its heart is too strong to ensare.";

            if (slot.Card.HasAbility(Scorching.ability))
                return "This creature burns with passion. It cannot freeze.";

            return "Frost cannot penetrate this one. Choose another.";
        }
        public override int StartingBonesCost => 2;
        public override int TurnDelay => 1;

        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            if (slot.Card != null)
            {
                slot.Card.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.15f);
                yield return slot.Card.TransformIntoCard(CardLoader.GetCardByName("wstl_snowQueenIceHeart"), () => slot.Card.Status.damageTaken = 0);
                yield return new WaitForSeconds(0.5f);
                yield return DialogueHelper.PlayDialogueEvent("FrostRulerKiss");
            }
            else
                yield return SpawnCard(slot, "wstl_snowQueenIceBlock");

            yield return base.LearnAbility();
        }

        public override bool IsValidTarget(CardSlot slot)
        {
            if (slot.Card == null)
                return true;

            if (slot.Card == base.Card || slot.Card.Dead || slot.Card.HasAnyOfTraits(Trait.Uncuttable, Trait.Giant) || slot.Card.HasAbility(Scorching.ability))
                return false;

            return base.Card.Attack >= slot.Card.Health || (base.Card.HasAbility(Ability.Deathtouch));
        }

        private IEnumerator SpawnCard(CardSlot slot, string name)
        {
            CardInfo cardByName = CardLoader.GetCardByName(name);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, slot, 0.15f);
        }
    }
}
