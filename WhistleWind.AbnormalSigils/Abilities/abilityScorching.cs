using DiskCardGame;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Scorching()
        {
            const string rulebookName = "Scorching";
            const string rulebookDescription = "The creature opposing this card takes 1 damage at the end of its owner's turn.";
            const string dialogue = "A slow and painful death.";

            Scorching.ability = AbnormalAbilityHelper.CreateAbility<Scorching>(
                Artwork.sigilScorching, Artwork.sigilScorching_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: true).Id;
        }
    }
    public class Scorching : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (base.Card.Slot.opposingSlot.Card != null)
                return base.Card.Slot.opposingSlot.Card.OpponentCard != playerTurnEnd;

            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board);
            yield return base.Card.Slot.opposingSlot.Card.TakeDamage(1, null);
            yield return base.LearnAbility();
        }
    }
}
