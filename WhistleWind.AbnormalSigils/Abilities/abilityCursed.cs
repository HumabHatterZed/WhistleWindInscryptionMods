using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Cursed()
        {
            const string rulebookName = "Cursed";
            const string rulebookDescription = "When [creature] dies, the killer transforms into a copy of this card.";
            const string dialogue = "The curse continues unabated.";
            const string triggerText = "[creature] passes the curse on.";
            Cursed.ability = AbnormalAbilityHelper.CreateAbility<Cursed>(
                "sigilCursed",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 0,
                modular: true, opponent: false, canStack: false).Id;
        }
    }
    public class Cursed : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            if (!wasSacrifice && killer != null && !killer.Dead && killer.Health != 0)
                return killer.LacksAbility(Ability.MadeOfStone) && killer.LacksAllTraits(Trait.Giant, Trait.Uncuttable);

            return false;
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board);
            yield return new WaitForSeconds(0.2f);
            killer.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            yield return killer.TransformIntoCard(this.Card.Info);
            killer.Status.damageTaken = 0;
            yield return new WaitForSeconds(0.4f);
            yield return LearnAbility();
        }
    }
}
