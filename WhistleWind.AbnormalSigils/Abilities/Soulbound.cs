using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_SoulboundFlesh() {
            const string rulebookName = "Soulbound";
            const string rulebookDescription = "Whenever [creature] takes damage, its owner takes an equal amount of damage.";
            const string dialogue = "So this is what they feel...";
            Soulbound.ability = AbnormalAbilityHelper.CreateAbility<Soulbound>(
                "sigilSoulboundFlesh",
                rulebookName, rulebookDescription, dialogue, powerLevel: -5,
                modular: false, opponent: false, canStack: false)


.Id;
        }
    }
    /// <summary>
    /// Whenever [creature] takes damage, its owner takes an equal amount of damage.
    /// </summary>
    public class Soulbound : AbilityBehaviour, IPreTakeDamage {
        public static Ability ability;
        public override Ability Ability => ability;
        int damageTaken = 0;

        public bool RespondsToPreTakeDamage(PlayableCard source, int damage) => damage > 0;
        public IEnumerator OnPreTakeDamage(PlayableCard source, int damage) {
            damageTaken += damage;
            yield break;
        }

        public override bool RespondsToTakeDamage(PlayableCard source) => damageTaken > 0;
        public override IEnumerator OnTakeDamage(PlayableCard source) {
            yield return base.PreSuccessfulTriggerSequence();
            yield return LifeManager.Instance.ShowDamageSequence(damageTaken, damageTaken, !base.Card.OpponentCard, changeView: false);
            yield return new WaitForSeconds(0.3f);
            yield return base.LearnAbility(0.4f);
            damageTaken = 0;
        }
    }
}