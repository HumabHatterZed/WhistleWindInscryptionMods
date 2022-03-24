using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Punisher()
        {
            const string rulebookName = "Punisher";
            const string rulebookDescription = "When a card bearing this sigil is struck, the striker is killed.";
            const string dialogue = "Retaliation is swift, but death is slow.";
            Punisher.ability = WstlUtils.CreateAbility<Punisher>(
                Resources.sigilPunisher,
                rulebookName, rulebookDescription, dialogue, 4,
                addModular: true).Id;
        }
    }
    public class Punisher : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            bool whiteNightEvent = !source.HasAbility(TrueSaviour.ability) && !source.HasAbility(Apostle.ability) && !source.HasAbility(Confession.ability);
            if (source != null && !source.Dead && whiteNightEvent)
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
