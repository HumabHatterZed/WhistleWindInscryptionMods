using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Conductor()
        {
            const string rulebookName = "Conductor";
            const string rulebookDescription = "The effect of this sigil will change over the next 3 turns. This turn: do nothing.";
            const string dialogue = "From break and ruin, the most beautiful performance begins.";

            Conductor.ability = AbnormalAbilityHelper.CreateAbility<Conductor>(
                Artwork.sigilConductor, Artwork.sigilConductor_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class Conductor : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public int turnCount = 0;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.4f);

        public override bool RespondsToUpkeep(bool onPlayerUpkeep)
        {
            if (turnCount < 3)
                return base.Card.OpponentCard != onPlayerUpkeep;

            return false;
        }
        public override IEnumerator OnUpkeep(bool onPlayerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.Card.Anim.StrongNegationEffect();
            turnCount++;
            yield return new WaitForSeconds(0.4f);
            yield return HelperMethods.ChangeCurrentView(View.Default);
        }

        /*
         * turn 0: n/a
         * turn 1: adjacent cards +self / 2
         * turn 2: allied cards +self / 2
         * turn 3: other cards +self
         */
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            // if not on board, target is base, target is opposing, or count is 0
            if (!base.Card.OnBoard || target == base.Card || turnCount == 0)
                return 0;

            int attack = base.Card.Attack;
            if (target.HasTrait(AbnormalPlugin.Orchestral))
                attack++;

            // at 3+ turns, give all other cards Power
            if (turnCount >= 3)
                return attack;

            // if adjacent or it's been 2 turns, give Power / 2
            if (base.Card.Slot.GetAdjacentCards().Contains(target) || (turnCount >= 2 && target.OpponentCard == base.Card.OpponentCard))
                return Mathf.FloorToInt(attack / 2);

            return 0;
        }
    }
}
