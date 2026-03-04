using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_DeathPenalty() {
            const string rulebookName = "Death Penalty";
            const string rulebookDescription = "When [creature] is killed, its owner takes 1 damage.";
            const string dialogue = "Pay better care to your beasts.";
            DeathPenalty.ID = AbnormalAbilityHelper.CreateAbility<DeathPenalty>(
                "sigilDeathPenalty",
                rulebookName, rulebookDescription, dialogue, powerLevel: -3,
                modular: false, opponent: false, canStack: true)
                .Id;
        }
    }
    /// <summary>
    /// When [creature] is killed, its owner takes 1 damage.
    /// </summary>
    public class DeathPenalty : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            yield return base.PreSuccessfulTriggerSequence();
            yield return LifeManager.Instance.ShowDamageSequence(1, 1, !base.Card.OpponentCard, changeView: false);
            yield return new WaitForSeconds(0.3f);
            yield return base.LearnAbility(0.4f);
        }
    }
}