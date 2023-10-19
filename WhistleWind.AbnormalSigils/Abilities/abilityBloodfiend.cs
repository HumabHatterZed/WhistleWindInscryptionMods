using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Bloodfiend()
        {
            const string rulebookName = "Bloodfiend";
            const string rulebookDescription = "When this card deals damage, it gains 1 Health.";
            const string dialogue = "Accursed fiend.";
            const string triggerText = "[creature] sucks out fresh life!";
            Bloodfiend.ability = AbnormalAbilityHelper.CreateAbility<Bloodfiend>(
                "sigilBloodfiend",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: true, opponent: true, canStack: true).Id;
        }
    }
    public class Bloodfiend : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && base.Card.NotDead() && base.Card.Health > 0;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            base.Card.HealDamage(1);
            yield return base.LearnAbility(0.4f);
        }
    }
}
