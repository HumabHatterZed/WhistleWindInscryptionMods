﻿using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Punisher()
        {
            const string rulebookName = "Punisher";
            const string rulebookDescription = "When [creature] is struck, the striker is killed.";
            const string dialogue = "Retaliation is swift, but death is slow.";
            Punisher.ability = AbilityHelper.CreateAbility<Punisher>(
                Artwork.sigilPunisher, Artwork.sigilPunisher_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                addModular: true, opponent: true, canStack: false, isPassive: false).Id;
        }
    }
    public class Punisher : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            bool whiteNightEvent = source.HasAbility(TrueSaviour.ability) || source.HasAbility(Apostle.ability) || source.HasAbility(Confession.ability);
            if (source != null && !source.Dead && !whiteNightEvent)
            {
                return source.Health > 0 && !source.HasAbility(Ability.MadeOfStone);
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
