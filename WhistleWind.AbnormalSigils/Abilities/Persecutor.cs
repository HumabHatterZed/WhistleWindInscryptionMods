using Core.AbilityClasses;
using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Persecutor() {
            const string rulebookName = "Persecutor";
            const string rulebookDescription = "[creature] deals 1 more damage to cards that are Unyielding or belong to the Mechanical tribe. These effects stack with each other.";
            const string dialogue = "These abominations must be purged.";
            Persecutor.ability = AbnormalAbilityHelper.CreateAbility<Persecutor>(
                "sigilPersecutor",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: true, opponent: true, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// When [creature] is played, create a Nail and Hammer in the adjacent left and right spaces respectively if they are empty.
    /// </summary>
    public class Persecutor : ModifyDamageDealtAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            int retval = damage;
            if (target.IsOfTribe(AbnormalPlugin.TribeMechanical)) {
                retval++;
            }
            if (target.HasAbility(Unyielding.ability)) {
                retval++;
            }
            return retval;
        }

        public override bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            return attacker == base.Card && (target.IsOfTribe(AbnormalPlugin.TribeMechanical) || target.HasAbility(Unyielding.ability));
        }

        public override int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) {
            return 0;
        }
    }
}
