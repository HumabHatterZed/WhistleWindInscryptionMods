using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Driver() {
            const string rulebookName = "Pin Down";
            const string rulebookDescription = "Creatures struck by [creature] gain Unyielding and lose Airborne.";
            const string dialogue = "Like a bug to a board.";
            const string triggerText = "[creature] pins its prey.";
            Driver.ability = AbnormalAbilityHelper.CreateAbility<Driver>(
                "sigilDriver",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 1,
                modular: true, opponent: true, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// Creatures struck by [creature] gain Unyielding and lose Airborne.
    /// </summary>
    public class Driver : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => target != null && !target.Dead && target.LacksAbility(Unyielding.ability) && target.LacksAllTraits(Trait.Giant, AbnormalPlugin.ImmuneToInstaDeath);
        public override IEnumerator OnDealDamage(int amount, PlayableCard target) {
            yield return base.PreSuccessfulTriggerSequence();
            PinDownCard(target);
            yield return base.LearnAbility(0.3f);
        }

        public static void PinDownCard (PlayableCard target) {
            target.AddTemporaryMod(new(Unyielding.ability) { fromCardMerge = true, negateAbilities = new() { Ability.Flying }, nonCopyable = true });
        }
    }
}
