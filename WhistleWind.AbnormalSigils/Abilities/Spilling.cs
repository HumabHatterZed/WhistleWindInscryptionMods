using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Slots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Spilling() {
            const string rulebookName = "Spilling";
            const string rulebookDescription = "When [creature] perishes, Flood all spaces on the board and extinguish grounded Scorching cards.";
            const string dialogue = "Don't worry, it will dry soon enough.";
            const string triggerText = "[creature]'s insides flood the board!";
            Spilling.ability = AbnormalAbilityHelper.CreateAbility<Spilling>(
                "sigilSpilling",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 0,
                modular: false, opponent: false, canStack: false)
                .SetSlotRedirect("Flood", FloodedSlot.Id, GameColors.Instance.brightSeafoam)
                .SetAbilityRedirect("Scorching", Scorching.ability, GameColors.Instance.red)
                .Id;
        }
    }
    /// <summary>
    /// When [creature] perishes, Flood all spaces on the board based on their distance from this card and extinguish Scorching cards.
    /// </summary>
    public class Spilling : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !base.Card.Info.IsSpell();
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) => Sequence(base.Card.Slot);

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => attacker == base.Card && base.Card.Info.IsTargetedSpell();
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => Sequence(slot);

        private IEnumerator Sequence(CardSlot startingSlot) {
            bool extinguishedCard = false;
            List<CardSlot> slots = new(BoardManager.Instance.AllSlotsCopy);
            slots.Remove(startingSlot);
            slots.Sort((CardSlot a, CardSlot b) => GetSlotDistance(startingSlot, a) - GetSlotDistance(startingSlot, b));

            yield return base.PreSuccessfulTriggerSequence();
            startingSlot.StartCoroutine(HelperMethods.PlayTruncated3DSound("ocean_fall", 0.1f, startingSlot));
            yield return startingSlot.SetSlotModification(FloodedSlot.Id);
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < slots.Count; i++) {
                int distance = GetSlotDistance(startingSlot, slots[i]);
                if (slots[i].Card != null && slots[i].Card.HasAbility(Scorching.ability) && slots[i].Card.LacksAbility(Ability.Flying)) {
                    extinguishedCard = true;
                    yield return Scorching.ExtinguishCard(slots[i].Card, false);
                }
                else {
                    yield return slots[i].SetSlotModification(FloodedSlot.Id);
                    slots[i].GetComponent<FloodedSlot>().Severity += distance;
                }

                if (i + 1 < slots.Count && GetSlotDistance(startingSlot, slots[i + 1]) != distance)
                    yield return new WaitForSeconds(0.25f);
            }
            if (extinguishedCard) {
                yield return DialogueHelper.PlayDialogueEvent("ScorchingExtinguished");
            }
        }

        private int GetSlotDistance(CardSlot refSlot, CardSlot slot) {
            return (refSlot.IsPlayerSlot == slot.IsPlayerSlot ? 0 : 1) + Mathf.Abs(refSlot.Index - slot.Index);
        }
    }
}
