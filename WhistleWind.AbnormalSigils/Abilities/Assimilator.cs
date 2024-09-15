﻿using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Assimilator()
        {
            const string rulebookName = "Assimilator";
            const string rulebookDescription = "When [creature] attacks an opposing creature and it perishes, this card gains 1 Power and 1 Health.";
            const string dialogue = "From the many, one.";
            const string triggerText = "[creature] makes its victim a part of itself.";
            Assimilator.ability = AbnormalAbilityHelper.CreateAbility<Assimilator>(
                "sigilAssimilator",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: true, canStack: true)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Assimilator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return killer == base.Card && !base.Card.Dead;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.3f);
            base.Card.AddTemporaryMod(new(1, 1));
            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.3f);
            yield return base.LearnAbility();
        }
    }
}