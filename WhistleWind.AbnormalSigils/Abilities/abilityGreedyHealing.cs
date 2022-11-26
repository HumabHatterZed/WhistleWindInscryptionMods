using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_GreedyHealing()
        {
            const string rulebookName = "Greedy Healing";
            const string rulebookDescription = "[creature] gains 2 Health at the end of its turn. If 2 turns pass without this card taking damage, this card will perish.";
            const string dialogue = "Your beast has health in excess.";

            GreedyHealing.ability = AbnormalAbilityHelper.CreateAbility<GreedyHealing>(
                Artwork.sigilGreedyHealing, Artwork.sigilGreedyHealing_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: true, opponent: false, canStack: false).Id;
        }
    }
    public class GreedyHealing : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int turnCount = 0;
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return base.Card.OpponentCard != playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            turnCount++;

            yield return PreSuccessfulTriggerSequence();
            yield return AbnormalMethods.ChangeCurrentView(View.Board);

            if (base.Card.FaceDown)
            {
                base.Card.SetFaceDown(false);
                base.Card.UpdateFaceUpOnBoardEffects();
                yield return new WaitForSeconds(0.55f);
            }

            if (turnCount < 2)
            {
                base.Card.Anim.LightNegationEffect();
                base.Card.HealDamage(2);
                yield return new WaitForSeconds(0.25f);
                yield return base.LearnAbility();
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
            yield return AbnormalDialogueManager.PlayDialogueEvent("RegeneratorOverheal");
        }

        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            return true;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            // set to -1 to keep things consistent
            turnCount = -1;
            yield break;
        }
    }
}
