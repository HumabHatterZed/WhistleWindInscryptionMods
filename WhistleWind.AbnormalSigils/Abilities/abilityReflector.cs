using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Reflector()
        {
            const string rulebookName = "Reflector";
            const string rulebookDescription = "When this card is struck, the striker is dealt damage equal to the striker's Power.";
            const string dialogue = "What goes around comes around.";
            const string triggerText = "[creature] returns the damage.";
            Reflector.ability = AbnormalAbilityHelper.CreateAbility<Reflector>(
                "sigilReflector",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: true, opponent: true, canStack: false).Id;
        }
    }
    public class Reflector : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source != null)
                return source.Health > 0;

            return false;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            yield return source.TakeDamage(source.Attack, base.Card);
            yield return base.LearnAbility(0.4f);
        }
    }
}
