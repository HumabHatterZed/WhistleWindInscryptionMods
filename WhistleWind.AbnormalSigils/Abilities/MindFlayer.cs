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
        private void Ability_MindFlayer()
        {
            const string rulebookName = "Mind Flayer";
            const string rulebookDescription = "When [creature] strikes another creature, deal no damage and inflict Sinking equal to half this card's Health, rounded up.";
            const string dialogue = "Why destroy the flesh when you can destroy the mind?";
            const string triggerText = "[creature] deals emotional damage!";
            MindFlayer.ability = AbnormalAbilityHelper.CreateAbility<MindFlayer>(
                "sigilMindFlayer",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class MindFlayer : AbilityBehaviour
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
                yield return DialogueHelper.PlayDialogueEvent("MindFlayerFail");
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
