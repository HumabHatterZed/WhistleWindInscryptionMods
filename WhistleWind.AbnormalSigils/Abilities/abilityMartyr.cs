using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Martyr()
        {
            const string rulebookName = "Martyr";
            const string rulebookDescription = "When [creature] dies, all allied creatures gain 2 Health.";
            const string dialogue = "A selfless death to cleanse your beasts of evil.";

            Martyr.ability = AbnormalAbilityHelper.CreateAbility<Martyr>(
                Artwork.sigilMartyr, Artwork.sigilMartyr_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 1,
                modular: true, opponent: false, canStack: true).Id;
        }
    }
    public class Martyr : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice;
        // original code taken from SigilADay - julianperge
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            List<CardSlot> validSlots = Singleton<BoardManager>.Instance.GetSlotsCopy(!base.Card.OpponentCard).FindAll(s => s.Card && s.Card != base.Card);

            // if no other cards on the board except this card
            if (validSlots.Count == 0)
                yield break;

            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            foreach (var slot in validSlots)
            {
                slot.Card.Anim.LightNegationEffect();
                slot.Card.HealDamage(2);
                yield return new WaitForSeconds(0.15f);
            }
            yield return base.LearnAbility(0.25f);
        }
    }
}
