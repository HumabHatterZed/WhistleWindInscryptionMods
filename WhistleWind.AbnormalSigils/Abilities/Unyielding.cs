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
        private void Ability_Unyielding()
        {
            const string rulebookName = "Unyielding";
            const string rulebookDescription = "[creature] cannot be moved from its current space on the board.";
            const string dialogue = "This beast is stubborn. It refuses to move.";
            const string triggerText = "[creature] drives into its prey.";
            Unyielding.ability = AbnormalAbilityHelper.CreateAbility<Unyielding>(
                "sigilUnyielding",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 1,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Unyielding : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            if (!wasSacrifice && killer != null && !killer.Dead && killer.Health != 0)
                return killer.LacksAbility(Ability.MadeOfStone);

            return false;
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (killer.HasAnyOfTraits(Trait.Giant, Trait.Uncuttable))
            {
                killer.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("UnyieldingFail");
                yield break;
            }
            yield return PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board);
            yield return new WaitForSeconds(0.2f);
            killer.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            yield return killer.TransformIntoCard(this.Card.Info);
            killer.Status.damageTaken = 0;
            killer.TemporaryMods.RemoveAll(x => x.nonCopyable || !x.fromTotem);
            yield return new WaitForSeconds(0.4f);
            yield return LearnAbility();
        }
    }
}
