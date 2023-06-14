using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Assimilator()
        {
            const string rulebookName = "Assimilator";
            const string rulebookDescription = "When this card kills an opposing card, it gains 1 Power and 1 Health.";
            const string dialogue = "From the many, one.";
            const string triggerText = "[creature] makes its victim a part of itself.";
            Assimilator.ability = AbnormalAbilityHelper.CreateAbility<Assimilator>(
                "sigilAssimilator",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: false, canStack: true).Id;
        }
    }
    public class Assimilator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly CardModificationInfo mod = new(1, 1);

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return killer == base.Card && !base.Card.Dead;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            base.Card.AddTemporaryMod(mod);

            base.Card.Anim.StrongNegationEffect();
            yield return LearnAbility(0.4f);
        }
    }
}