using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_GreedyHealing()
        {
            const string rulebookName = "Greedy Healing";
            const string rulebookDescription = "At the end of its owner's turn, this card gains 2 Health. If 2 turns pass without this card taking damage, it will die.";
            const string dialogue = "Your beast has Health in excess.";
            const string triggerText = "[creature] gives itself more Health!";
            GreedyHealing.ability = AbnormalAbilityHelper.CreateAbility<GreedyHealing>(
                "sigilGreedyHealing",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: true, opponent: false, canStack: false).Id;
        }
    }
    public class GreedyHealing : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int turnCount = 0;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override bool RespondsToTakeDamage(PlayableCard source) => true;

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            turnCount++;
            yield return PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board);

            bool faceDown = base.Card.FaceDown;

            // flip up
            yield return base.Card.FlipFaceDown(false);

            if (turnCount < 2)
            {
                base.Card.Anim.LightNegationEffect();
                base.Card.HealDamage(2);
                yield return new WaitForSeconds(0.3f);
                yield return base.LearnAbility();
                
                // if we were submerged at the start, resubmerge
                yield return base.Card.FlipFaceDown(faceDown);
                yield break;
            }

            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            for (int i = 0; i < 4; i++)
            {
                yield return base.Card.TakeDamage(Mathf.Min(-1, -base.Card.Health * i), null);
                yield return new WaitForSeconds(0.2f);
            }
            yield return base.Card.Die(false, null);
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("RegeneratorOverheal");
        }

        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            // set to -1 to keep things consistent
            turnCount = -1;
            yield break;
        }
    }
}
