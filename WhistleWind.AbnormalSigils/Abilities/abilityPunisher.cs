using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Punisher()
        {
            const string rulebookName = "Punisher";
            const string rulebookDescription = "When [creature] is struck, the striker is killed.";
            const string dialogue = "Retaliation is switft and brutal.";
            const string triggerText = "[creature] swiftly retaliates!";
            Punisher.ability = AbnormalAbilityHelper.CreateAbility<Punisher>(
                "sigilPunisher",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class Punisher : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source != null && !source.Dead && source != base.Card)
                return source.Health > 0 && !source.HasAbility(Ability.MadeOfStone) && !source.HasTrait(AbnormalPlugin.ImmuneToInstaDeath);

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
