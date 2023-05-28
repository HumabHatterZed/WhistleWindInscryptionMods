using DiskCardGame;
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
            const string rulebookDescription = "Affected cards gain Power equal to half this card's Power. Over the next 3 turns: affect adjacent -> allied -> all other cards and double the Power gained.";
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
            if (!base.Card.OnBoard || turnCount < 1 || target == base.Card)
                return 0;

            if (turnCount > 2)
                return base.Card.Attack;

            if (base.Card.Slot.GetAdjacentCards().Contains(target))
                return Mathf.FloorToInt(base.Card.Attack / 2);

            if (turnCount > 1 && target.OpponentCard == base.Card.OpponentCard)
                return Mathf.FloorToInt(base.Card.Attack / 2);

            return 0;
        }
    }
}
