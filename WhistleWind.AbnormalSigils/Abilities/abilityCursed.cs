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
        private void Ability_Cursed()
        {
            const string rulebookName = "Cursed";
            const string rulebookDescription = "When [creature] dies, the killer transforms into this card.";
            const string dialogue = "The curse continues unabated.";
            Cursed.ability = AbnormalAbilityHelper.CreateAbility<Cursed>(
                Artwork.sigilCursed, Artwork.sigilCursed_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                modular: true, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Cursed : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            if (!wasSacrifice && killer != null && !killer.Dead && killer.Health != 0)
            {
                return killer.LacksAbility(Ability.MadeOfStone) && killer.LacksAllTraits(Trait.Giant, Trait.Uncuttable);
            }
            return false;
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return AbnormalMethods.ChangeCurrentView(View.Board);
            yield return new WaitForSeconds(0.2f);
            killer.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            yield return killer.TransformIntoCard(this.Card.Info);
            yield return new WaitForSeconds(0.4f);
            yield return LearnAbility();
        }
    }
}