using DiskCardGame;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Shadowed() {
            const string rulebookName = "Obscured Presence";
            const string rulebookDescription = "[creature] cannot take overkill damage. When this card is played, remove this sigil.";
            const string dialogue = "The beast steps out from the shadows.";
            string trigger = "[creature] emerges from the darkness!";
            Shadowed.ID = AbnormalAbilityHelper.CreateAbility<Shadowed>(
                "sigilShadowed",
                rulebookName, rulebookDescription, dialogue, trigger, powerLevel: 0,
                modular: false, opponent: true, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// [creature] cannot take overkill damage. On play, remove this sigil.
    /// </summary>
    public class Shadowed : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.AddTemporaryMod(new() { negateAbilities = new() { this.Ability } });
            base.SetLearned();
        }
    }
}
