using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Regenerator()
        {
            const string rulebookName = "Regenerator";
            const string rulebookDescription = "At the start of the owner's turn, creatures adjacent to [creature] regain 1 Health.";
            const string dialogue = "Wounds heal, but the scars remain.";
            const string triggerText = "[creature] heals adjacent creatures.";
            Regenerator.ability = AbnormalAbilityHelper.CreateAbility<Regenerator>(
                "sigilRegenerator",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: true, opponent: false, canStack: true)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Regenerator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);

            List<PlayableCard> adjacentCards = base.Card.Slot.GetAdjacentCards();
            foreach (PlayableCard card in adjacentCards)
            {
                if (card.Health < card.MaxHealth)
                {
                    yield return HelperMethods.HealCard(1, card);
                }
            }

            yield return new WaitForSeconds(0.2f);
            yield return LearnAbility();
        }
    }
}
