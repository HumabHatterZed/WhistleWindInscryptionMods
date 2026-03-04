using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Neutered() {
            const string rulebookName = "Neutered";
            const string rulebookDescription = "[creature] has its Power reduced to 0. At the start of the owner's next turn, remove this sigil.";
            Neutered.ID = AbnormalAbilityHelper.CreateAbility<Neutered>(
                "sigilNeutered",
                rulebookName, rulebookDescription, powerLevel: -2,
                modular: false, opponent: false, canStack: false, unobtainable: true)
                .ForceAddToRulebook()
                .Id;
        }
    }
    /// <summary>
    /// [creature] has its Power reduced to 0. At the start of the owner's next turn, remove this sigil.
    /// </summary>
    public class Neutered : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        private int TurnPlayed = 0;
        private void Start() => TurnPlayed = TurnManager.Instance.TurnNumber;
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            TurnPlayed = TurnManager.Instance.TurnNumber; // override the assignment in Start() if the card was Neutered in the hoof
            yield return base.OnResolveOnBoard();
        }
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep && TurnPlayed != TurnManager.Instance.TurnNumber;
        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.PlayTransformAnimation();
            for (CardModificationInfo temporaryNeuterMod = GetTemporaryNeuterMod(); temporaryNeuterMod != null; temporaryNeuterMod = GetTemporaryNeuterMod()) {
                base.Card.RemoveTemporaryMod(temporaryNeuterMod);
            }
            yield return new WaitForSeconds(0.5f);
        }
        private CardModificationInfo GetTemporaryNeuterMod() => base.Card.TemporaryMods.Find((CardModificationInfo x) => x.abilities.Contains(ID));
    }
}
