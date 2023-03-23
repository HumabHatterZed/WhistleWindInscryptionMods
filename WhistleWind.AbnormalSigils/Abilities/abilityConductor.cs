using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Conductor : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int turnCount = 0;

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
    }

    public partial class AbnormalPlugin
    {
        private void Ability_Conductor()
        {
            const string rulebookName = "Conductor";
            const string rulebookDescription = "While this card is on the board, reduce all other card's Power by the number of turns this card has been on the board, up to 3.";
            const string dialogue = "From break and ruin, the most beautiful performance begins.";

            Conductor.ability = AbnormalAbilityHelper.CreateAbility<Conductor>(
                Artwork.sigilConductor, Artwork.sigilConductor_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
}
