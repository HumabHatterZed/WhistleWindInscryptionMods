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
        private void Ability_Driver()
        {
            const string rulebookName = "Driven In";
            const string rulebookDescription = "[creature] deals 1 additional damage when striking injured creatures.";
            const string dialogue = "A ferocious onslaught.";
            const string triggerText = "[creature] won't let its target go!";
            Driver.ability = AbnormalAbilityHelper.CreateAbility<Driver>(
                "sigilDriver",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: false, opponent: true, canStack: true).Id;
        }
    }
    public class Driver : AbilityBehaviour
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
                yield return DialogueHelper.PlayDialogueEvent("DriverFail");
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
