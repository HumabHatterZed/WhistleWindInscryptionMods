using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Martyr()
        {
            const string rulebookName = "Martyr";
            const string rulebookDescription = "When [creature] dies, all allied creatures gain 2 Health and lose any negative status effects.";
            const string dialogue = "A selfless death to cleanse your beasts of evil.";
            const string triggerText = "[creature]'s death cleanses your other creatures!";
            Martyr.ability = AbnormalAbilityHelper.CreateAbility<Martyr>(
                "sigilMartyr",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 1,
                modular: true, opponent: false, canStack: true).Id;
        }
    }
    public class Martyr : AbilityBehaviour // original code taken from SigilADay - julianperge
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            List<PlayableCard> validCards = Singleton<BoardManager>.Instance.GetCards(!base.Card.OpponentCard);
            validCards.Remove(base.Card);

            if (validCards.Count == 0)
                yield break;

            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.1f);
            foreach (PlayableCard card in validCards)
            {
                // remove all negative effects
                List<StatusEffectBehaviour> negativeEffects = card.GetStatusEffects(false);
                List<CardModificationInfo> negativeAbilities = card.TemporaryMods.FindAll(x => x.IsStatusMod(false));

                for (int i = 0; i < negativeEffects.Count; i++)
                    Destroy(negativeEffects[i]);

                yield return HelperMethods.HealCard(card.Slot, negativeAbilities.Count > 0 ? 0.15f : 0.1f, (CardSlot s) =>
                {
                    if (negativeAbilities.Count > 0)
                        s.Card.RemoveTemporaryMods(negativeAbilities.ToArray());
                });
            }
            yield return base.LearnAbility(0.25f);
        }
    }
}
