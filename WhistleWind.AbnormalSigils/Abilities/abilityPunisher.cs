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
        private void Ability_Punisher()
        {
            const string rulebookName = "Punisher";
            const string rulebookDescription = "When [creature] is struck, the striker is killed.";
            const string dialogue = "Retaliation is swift, but death is slow.";
            Punisher.ability = AbnormalAbilityHelper.CreateAbility<Punisher>(
                Artwork.sigilPunisher, Artwork.sigilPunisher_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                modular: true, opponent: true, canStack: false, isPassive: false).Id;
        }
    }
    public class Punisher : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source != null && !source.Dead)
            {
                return source.Health > 0 && !source.HasAbility(Ability.MadeOfStone) && !source.HasSpecialAbility(ImmuneToInstaDeath.specialAbility);
            }
            return false;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            yield return source.Die(false, base.Card);
            yield return base.LearnAbility(0.4f);
        }
    }
}