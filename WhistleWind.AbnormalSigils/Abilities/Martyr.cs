using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Martyr() {
            const string rulebookName = "Martyr";
            const string rulebookDescription = "When [creature] perishes, allied cards gain 2 Health and are cured of status ailments. If this card was sacrificed, also apply this effect to the card it was sacrificed for.";
            const string dialogue = "A selfless death to cleanse your beasts of evil.";
            const string triggerText = "[creature]'s death cleanses your other creatures!";
            Martyr.ID = AbnormalAbilityHelper.CreateAbility<Martyr>(
                "sigilMartyr",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 1,
                modular: true, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// When [creature] perishes, allied creatures gain 2 Health and lose any negative status effects.
    /// </summary>
    public class Martyr : AbilityBehaviour // original code taken from SigilADay - julianperge
    {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            List<PlayableCard> validCards = Singleton<BoardManager>.Instance.GetCards(!base.Card.OpponentCard);
            validCards.Remove(base.Card);

            // heal card Martyr is being sacrificed to
            if (wasSacrifice && BoardManager.Instance.CurrentSacrificeDemandingCard != null) {
                validCards.Add(BoardManager.Instance.CurrentSacrificeDemandingCard);
            }

            if (validCards.Count > 0) {
                yield return base.PreSuccessfulTriggerSequence();
                yield return new WaitForSeconds(0.1f);
                foreach (PlayableCard card in validCards) {
                    yield return HelperMethods.HealCard(2, card, 0.1f);
                    yield return card.RemoveStatusEffects(false);
                }
                yield return base.LearnAbility(0.25f);
            }
        }
    }
}
