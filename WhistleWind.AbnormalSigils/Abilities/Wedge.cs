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
        private void Ability_Wedge()
        {
            const string rulebookName = "Drive It In";
            const string rulebookDescription = "[creature] deals 1 additional damage when striking uninjured creatures.";
            const string dialogue = "A hard beginning blow.";
            const string triggerText = "[creature] drives into its prey.";
            Wedge.ability = AbnormalAbilityHelper.CreateAbility<Wedge>(
                "sigilWedge",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: false, opponent: true, canStack: true).Id;
        }
    }
    public class Wedge : AbilityBehaviour
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
                yield return DialogueHelper.PlayDialogueEvent("WedgeFail");
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
