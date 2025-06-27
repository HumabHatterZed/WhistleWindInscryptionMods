using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Understanding() {
            const string rulebookName = "Understanding";
            const string rulebookDescription = "If [creature] perishes due to self-inflicted damage, deal 4 damage to opposing creatures.";
            const string dialogue = "Too slow.";
            Understanding.ability = AbnormalAbilityHelper.CreateAbility<Understanding>(
                "sigilUnderstanding",
                rulebookName, rulebookDescription, dialogue, powerLevel: 1,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    /// <summary>
    /// If [creature] perishes due to self-inflicted damage, deal 4 damage to opposing creatures.
    /// </summary>
    public class Understanding : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => killer == base.Card;

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            foreach (PlayableCard card in BoardManager.Instance.GetCards(base.Card.OpponentCard)) {
                yield return card.TakeDamage(4, base.Card);
            }

            yield return base.LearnAbility(0.4f);
        }
    }
}
