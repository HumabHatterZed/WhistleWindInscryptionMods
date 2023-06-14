﻿using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Protector()
        {
            const string rulebookName = "Protector";
            const string rulebookDescription = "Creatures adjacent to this card take 1 less damage when struck.";
            const string dialogue = "Your beast shields its ally against the blow.";
            const string triggerText = "[creature] shields its friend!";
            Protector.ability = AbnormalAbilityHelper.CreateAbility<Protector>(
                "sigilProtector",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: false, opponent: false, canStack: true).Id;
        }
    }
    public class Protector : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            // only respond if the target hasn't died
            if (amount > 0 && target.NotDead())
                return Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Contains(target.Slot);

            return false;
        }
        public override IEnumerator OnOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return base.LearnAbility(0.4f);
        }
    }
}
