using InscryptionAPI;
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
        private void Ability_GreedyHealing()
        {
            const string rulebookName = "Greedy Healing";
            const string rulebookDescription = "[creature] gains 2 Health at the end of its turn. If 2 turns pass without this card taking damage, this card will perish.";
            const string dialogue = "Your beast has health in excess.";

            GreedyHealing.ability = AbilityHelper.CreateAbility<GreedyHealing>(
                Artwork.sigilGreedyHealing, Artwork.sigilGreedyHealing_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: true, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class GreedyHealing : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int turnCount = 0;

        private readonly string dialogue = "The punishment for greed.";
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return base.Card.OpponentCard != playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            turnCount++;

            yield return PreSuccessfulTriggerSequence();
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.2f);

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
            yield return new WaitForSeconds(0.25f);
            if (!WstlSaveManager.HasSeenRegeneratorExplode)
            {
                WstlSaveManager.HasSeenRegeneratorExplode = true;
                yield return CustomMethods.PlayAlternateDialogue(dialogue: dialogue);
            }
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
