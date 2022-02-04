using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Bloodfiend()
        {
            const string rulebookName = "Bloodfiend";
            const string rulebookDescription = "When a card bearing this sigil deals damage to an opposing card, it gains 1 Health.";
            const string dialogue = "Accursed fiend.";

            return WstlUtils.CreateAbility<Bloodfiend>(
                Resources.sigilBloodfiend,
                rulebookName, rulebookDescription, dialogue, 3, true);
        }
    }
    public class Bloodfiend : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            return amount > 0;
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.HealDamage(1);
            base.Card.OnStatsChanged();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility(0.4f);
            yield break;
        }
    }
}
