using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Burning()
        {
            const string rulebookName = "Burning";
            const string rulebookDescription = "The opposing card takes 1 damage at the end of their owner's turn.";
            const string dialogue = "A slow and painful death.";

            Burning.ability = AbnormalAbilityHelper.CreateAbility<Burning>(
                Artwork.sigilBurning, Artwork.sigilBurning_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: true, isPassive: false).Id;
        }
    }
    public class Burning : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (base.Card.Slot.opposingSlot.Card != null)
            {
                return base.Card.Slot.opposingSlot.Card.OpponentCard != playerTurnEnd;
            }
            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return AbnormalMethods.ChangeCurrentView(View.Board);
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.2f);
            yield return base.Card.Slot.opposingSlot.Card.TakeDamage(1, null);
            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility();
        }
    }
}
