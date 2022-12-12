using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Bloodfiend()
        {
            const string rulebookName = "Bloodfiend";
            const string rulebookDescription = "When [creature] deals damage to an opposing card, it gains 1 Health.";
            const string dialogue = "Accursed fiend.";

            Bloodfiend.ability = AbnormalAbilityHelper.CreateAbility<Bloodfiend>(
                Artwork.sigilBloodfiend, Artwork.sigilBloodfiend_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: true, opponent: true, canStack: true).Id;
        }
    }
    public class Bloodfiend : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            return amount > 0 && !base.Card.Dead && base.Card.Health != 0;
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.4f);
            base.Card.Anim.LightNegationEffect();
            base.Card.HealDamage(1);
            base.Card.OnStatsChanged();
            yield return new WaitForSeconds(0.2f);
            yield return base.LearnAbility(0.2f);
            base.Card.Info.RemoveBaseAbilities(ability, Ability.Evolve);
        }
    }
}
