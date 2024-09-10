using DiskCardGame;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Slots;
using InscryptionAPI.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Spilling()
        {
            const string rulebookName = "Spilling";
            const string rulebookDescription = "When [creature] perishes, Flood all spaces on the board based on their distance from this card and extinguish Scorching cards.";
            const string dialogue = "Don't worry, it will dry soon enough.";
            const string triggerText = "[creature]'s insides flood the board!";
            Spilling.ability = AbnormalAbilityHelper.CreateAbility<Spilling>(
                "sigilSpilling",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 0,
                modular: false, opponent: false, canStack: false)
                .SetSlotRedirect("Flood", FloodedSlot.Id, GameColors.Instance.brightSeafoam)
                .SetAbilityRedirect("Scorching", Scorching.ability, GameColors.Instance.red)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook()
                .Id;
        }
    }
    public class Spilling : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            bool extinguishedCard = false;
            List<CardSlot> slots = new(BoardManager.Instance.AllSlotsCopy);
            slots.Remove(base.Card.Slot);
            slots.Sort((CardSlot a, CardSlot b) => GetSlotDistance(a) - GetSlotDistance(b));

            base.StartCoroutine(PlayTruncatedOceanSound());
            yield return base.Card.Slot.SetSlotModification(FloodedSlotShallow.Id);
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < slots.Count; i++)
            {
                int distance = GetSlotDistance(slots[i]);
                if (slots[i].Card != null && slots[i].Card.HasAbility(Scorching.ability))
                {
                    extinguishedCard = true;
                    yield return Scorching.ExtinguishCard(slots[i].Card, false);
                }
                else
                {
                    yield return slots[i].SetSlotModification(FloodedSlot.Id);
                    slots[i].GetComponent<FloodedSlot>().Severity += distance;
                }

                if (i < slots.Count - 1 && GetSlotDistance(slots[i + 1]) != distance)
                    yield return new WaitForSeconds(0.25f);
            }
            if (extinguishedCard)
            {
                yield return DialogueHelper.PlayDialogueEvent("ScorchingExtinguished");
            }
        }
        private int GetSlotDistance(CardSlot slot)
        {
            return (base.Card.OpponentCard != slot.IsPlayerSlot ? 0 : 1) + Mathf.Abs(base.Card.Slot.Index - slot.Index);
        }

        private IEnumerator PlayTruncatedOceanSound()
        {
            AudioSource ocean = AudioController.Instance.PlaySound3D("ocean_fall", MixerGroup.TableObjectsSFX, base.Card.Slot.transform.position, skipToTime: 0.1f);
            yield return new WaitUntil(() => ocean.time >= (ocean.clip.length * 0.15f));
            ocean.Stop();
        }
    }
}
