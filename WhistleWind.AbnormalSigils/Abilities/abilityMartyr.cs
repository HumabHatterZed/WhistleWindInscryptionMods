using DiskCardGame;
using System.Collections;
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
                modular: true, opponent: false, canStack: false).Id;
        }
    }
    public class Martyr : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice;
        }
        // original code taken from SigilADay - julianperge
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            var validCards = Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).Where(s => s.Card != null && s.Card != base.Card);

            // if no other cards on the board except this card
            if (validCards.Count() == 0)
            {
                yield break;
            }
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            foreach (var slot in validCards)
            {
                slot.Card.Anim.LightNegationEffect();
                slot.Card.HealDamage(2);
                yield return new WaitForSeconds(0.2f);
            }
            yield return base.LearnAbility(0.25f);
        }
    }
}
