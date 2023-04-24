﻿using DiskCardGame;
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
            const string rulebookDescription = "When this card deals damage, it gains 1 Health.";
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
            if (amount > 0)
                return base.Card.NotDead() && base.Card.Health > 0;

            return false;
        }
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
