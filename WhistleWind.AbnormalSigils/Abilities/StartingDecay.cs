using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_StartingDecay() {
            const string rulebookName = "Imminent Decay";
            const string rulebookDescription = "When [creature] is played, gain 1 Decay for every stack of this sigil it has, then remove this sigil.";
            StartingDecay.ability = AbnormalAbilityHelper.CreateAbility<StartingDecay>(
                "sigilDecay",
                rulebookName, rulebookDescription, powerLevel: -2,
                modular: false, opponent: false, canStack: true)
                .SetAbilityRedirect("Decay", Decay.iconId, GameColors.Instance.darkPurple)
                .Id;
        }
    }
    /// <summary>
    /// When [creature] is played, gain 1 Decay for every stack of this sigil it has then remove this sigil.
    /// </summary>
    public class StartingDecay : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => !base.Card.Info.IsSpell();
        public override IEnumerator OnResolveOnBoard() {
            yield return ApplyDecayToCard();
        }

        public override bool RespondsToUpkeep(bool playerUpkeep) => true;
        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            yield return ApplyDecayToCard();
        }

        private IEnumerator ApplyDecayToCard() {
            if (base.Card.LacksTrait(AbnormalPlugin.ImmuneToAilments)) {
                yield return base.Card.AddStatusEffect<Decay>(1);
            }
            base.Card.AddTemporaryMod(new() { negateAbilities = new() { this.Ability } });
        }
    }
}
