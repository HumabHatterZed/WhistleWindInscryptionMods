using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using Pixelplacement.TweenSystem;
using Pixelplacement;
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
            const string rulebookName = "Pin Down";
            const string rulebookDescription = "Creatures struck by [creature] gain Unyielding.";
            const string dialogue = "Like a bug to a board.";
            const string triggerText = "[creature] pins its prey.";
            Driver.ability = AbnormalAbilityHelper.CreateAbility<Driver>(
                "sigilDriver",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 1,
                modular: true, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }

    public class Driver : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => target != null && !target.Dead && target.LacksAbility(Unyielding.ability) && !target.HasTrait(Trait.Uncuttable);
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            target.AddTemporaryMod(new(Unyielding.ability) { fromCardMerge = true });
            yield return base.LearnAbility(0.3f);
        }
    }
}
