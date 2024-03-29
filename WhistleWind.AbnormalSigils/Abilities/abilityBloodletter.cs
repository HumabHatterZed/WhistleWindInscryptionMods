﻿using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Bloodletter()
        {
            const string rulebookName = "Bloodletter";
            const string rulebookDescription = "When this card deals damage, it gains 1 Health.";
            const string dialogue = "The blood runs warm with sweet vitality.";
            const string triggerText = "[creature] absorbs nutrients!";
            Bloodletter.ability = AbnormalAbilityHelper.CreateAbility<Bloodletter>(
                "sigilBloodletter",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: true, canStack: true).Id;
        }
    }
    public class Bloodletter : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source != null)
                return source.Health > 0 && !base.Card.Dead && base.Card.Health > 0;

            return false;
        }

        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            yield return source.TakeDamage(1, base.Card);
            base.Card.HealDamage(1);
            base.Card.Anim.LightNegationEffect();
            yield return base.LearnAbility(0.4f);
        }
    }
}
