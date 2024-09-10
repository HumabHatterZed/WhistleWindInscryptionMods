using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Slots;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Scorching()
        {
            const string rulebookName = "Scorching";
            const string rulebookDescription = "At the end of its owner's turn, the creature opposing [creature] will take 1 damage. This card cannot be frozen.";
            const string dialogue = "A slow and painful death.";
            const string triggerText = "The creature opposing [creature] is burned!";
            Scorching.ability = AbnormalAbilityHelper.CreateAbility<Scorching>(
                "sigilScorching",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: true, opponent: true, canStack: true)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Scorching : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            if (otherCard == base.Card && base.Card.Slot.GetSlotModification() != SlotModificationManager.ModificationType.NoModification)
                return FloodedSlot.CardIsGrounded(base.Card);
            
            return false;
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            SlotModificationManager.ModificationType mod = base.Card.Slot.GetSlotModification();
            if (mod == FloodedSlot.Id)
            {
                yield return ExtinguishCard(base.Card, true);
                
            }
            else if (mod == FloodedSlotShallow.Id)
            {
                yield return base.Card.Slot.SetSlotModification(SlotModificationManager.ModificationType.NoModification);
                yield return new WaitForSeconds(0.3f);
                yield return DialogueHelper.PlayDialogueEvent("FloodedSlotDried", card: base.Card);
            }
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (base.Card.OpposingCard() != null) return base.Card.OpposingCard().OpponentCard != playerTurnEnd;

            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            PlayableCard opposingCard = base.Card.OpposingCard();
            SlotModificationManager.ModificationType modType = opposingCard.Slot.GetSlotModification();
            yield return PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board);
            yield return opposingCard.TakeDamage(1, null);
            if (modType == FloodedSlot.Id)
            {
                yield return base.Card.OpposingSlot().SetSlotModification(FloodedSlotShallow.Id);
                yield return new WaitForSeconds(0.3f);
                yield return DialogueHelper.PlayDialogueEvent("FloodedSlotDried", card: base.Card);
            }
            else if (modType == FloodedSlot.Id)
            {
                yield return base.Card.OpposingSlot().SetSlotModification(SlotModificationManager.ModificationType.NoModification);
                yield return new WaitForSeconds(0.3f);
                yield return DialogueHelper.PlayDialogueEvent("FloodedSlotDried", card: base.Card);
            }
            yield return base.LearnAbility();
        }

        public static IEnumerator ExtinguishCard(PlayableCard card, bool playEvent)
        {
            card.Anim.StrongNegationEffect();
            card.AddTemporaryMod(new() { negateAbilities = new() { Scorching.ability }, singletonId = "ScorchingExtinguished" });
            yield return card.Slot.SetSlotModification(FloodedSlotShallow.Id);
            if (playEvent)
            {
                yield return new WaitForSeconds(0.3f);
                yield return DialogueHelper.PlayDialogueEvent("ScorchingExtinguished", card: card);
            }
        }
    }
}
