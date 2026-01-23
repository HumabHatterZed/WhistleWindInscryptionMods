using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Bloodfiend() {
            const string rulebookName = "Bloodfiend";
            const string rulebookDescription = "When [creature] strikes a creature, it gains 1 Health, up to 2 above its maximum Health.";
            const string dialogue = "Accursed fiend.";
            const string triggerText = "[creature] satiates its thirst!";
            Bloodfiend.ability = AbnormalAbilityHelper.CreateAbility<Bloodfiend>(
                "sigilBloodfiend",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: true, opponent: true, canStack: true)
                .Id;
        }
    }
    /// <summary>
    /// When [creature] strikes a creature, it gains 1 Health, up to 2 above its maximum Health.
    /// </summary>
    public class Bloodfiend : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && base.Card.Health > 0 && !base.Card.Dead;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target) {
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.3f);
            if (base.Card.Health < base.Card.MaxHealth + 2) {
                yield return base.Card.Heal(1);
                yield return base.LearnAbility(0.3f);
            }
        }
    }
}
