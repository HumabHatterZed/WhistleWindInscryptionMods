using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_ThickSkin() {
            const string rulebookName = "Thick Skin";
            const string rulebookDescription = "Whenever [creature] is struck by a creature, reduce the damage taken by 1.";
            const string dialogue = "Your creature's hide absorbs the blow.";
            const string triggerText = "[creature] absorbs the blow.";
            ThickSkin.ID = AbnormalAbilityHelper.CreateAbility<ThickSkin>(
                "sigilThickSkin",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: false, opponent: false, canStack: true)
                .Id;
        }
    }
    /// <summary>
    /// Whenever [creature] is struck by a creature, reduce the damage taken by 1.
    /// </summary>
    public class ThickSkin : AbilityBehaviour, IModifyDamageTaken {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool RespondsToTakeDamage(PlayableCard source) => source != null;
        public override IEnumerator OnTakeDamage(PlayableCard source) {
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.LearnAbility(0.4f);
        }

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            if (base.Card == target && damage > 0)
                return attacker != null && attacker.LacksAbility(Piercing.ID);

            return false;
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => --damage;
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }
}
