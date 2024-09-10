﻿using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Bloodfiend()
        {
            const string rulebookName = "Bloodfiend";
            const string rulebookDescription = "When [creature] deals damage, it gains 1 Health.";
            const string dialogue = "Accursed fiend.";
            const string triggerText = "[creature] satiates its thirst!";
            Bloodfiend.ability = AbnormalAbilityHelper.CreateAbility<Bloodfiend>(
                "sigilBloodfiend",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: true, opponent: true, canStack: true)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Bloodfiend : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && base.Card.Health > 0 && !base.Card.Dead;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.3f);
            base.Card.HealDamage(1);
            base.Card.Anim.LightNegationEffect();
            yield return base.LearnAbility(0.3f);
        }
    }
}
