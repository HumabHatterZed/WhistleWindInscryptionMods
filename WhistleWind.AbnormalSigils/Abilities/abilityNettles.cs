using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Nettles()
        {
            const string rulebookName = "Nettle Clothes";
            const string rulebookDescription = "When this card is played, fill all empty spaces on the owner's side of the board with random Brothers. This card gains the first sigil of each allied Brother.";
            const string dialogue = "These clothes will surely restore our happy days.";
            const string triggerText = "[creature] brings out its family!";
            Nettles.ability = AbnormalAbilityHelper.CreateAbility<Nettles>(
                "sigilNettles",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 5,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Nettles : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => true;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return otherCard.OpponentCard == base.Card.OpponentCard && otherCard.HasTrait(AbnormalPlugin.SwanBrother) && !otherCard.Dead && otherCard.Health > 0;
        }
        private readonly List<CardModificationInfo> CurrentMods = new();

        public override IEnumerator OnResolveOnBoard()
        {
            List<CardSlot> validSlots = Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).FindAll((CardSlot slot) => !slot.Card && slot.Card != base.Card);

            if (validSlots.Count == 0)
            {
                yield return new WaitForSeconds(0.25f);
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("NettlesFail");
                yield break;
            }

            // Create a unique Brother for each valid slot
            int randomSeed = base.GetRandomSeed();

            // Create list of all Brothers
            List<CardInfo> brothers = CardManager.AllCardsCopy.FindAll(x => x.HasTrait(AbnormalPlugin.SwanBrother));

            yield return base.PreSuccessfulTriggerSequence();
            foreach (CardSlot slot in validSlots)
            {
                int randomIdx = SeededRandom.Range(0, brothers.Count, randomSeed++);
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(brothers[randomIdx], slot, 0.15f);
                brothers.RemoveAt(randomIdx);
            }
            yield return base.LearnAbility();
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();
            CardModificationInfo abilityMod = new()
            {
                singletonId = "wstl:Swan_" + otherCard.Info.name + CurrentMods.Count(x => x.singletonId.Contains(otherCard.Info.name)),
                abilities = new() { otherCard.Info.Abilities.Count > 0 ? otherCard.Info.Abilities.First() : Ability.Sharp }
            };

            CurrentMods.Add(abilityMod);
            base.Card.AddTemporaryMod(abilityMod);
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card.OpponentCard == base.Card.OpponentCard && card.HasTrait(AbnormalPlugin.SwanBrother);
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardModificationInfo cardMod = CurrentMods.Find(
                x => x.singletonId != null && x.singletonId.StartsWith($"wstl:Swan_{card.Info.name}") &&
                x.HasAbility(card.Info.Abilities.Count > 0 ? card.Info.Abilities[0] : Ability.Sharp));

            if (cardMod == null)
                yield break;

            CurrentMods.Remove(cardMod);
            base.Card.RemoveTemporaryMod(cardMod);

            yield return DialogueHelper.PlayDialogueEvent("NettlesDie");
        }
    }
}
