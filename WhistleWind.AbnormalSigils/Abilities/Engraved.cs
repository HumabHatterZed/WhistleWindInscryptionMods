using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Engraved() {
            const string rulebookName = "Deeply Engraved";
            const string rulebookDescription = "[creature] cannot be sacrificed for its sigils at The Stones.";
            Engraved.ability = AbnormalAbilityHelper.CreateAbility<Engraved>(
                "sigilEngraved",
                rulebookName, rulebookDescription, powerLevel: -1,
                modular: false, opponent: false, canStack: false)
                .Info.SetPassive(true)
                .ability;
        }
    }
    /// <summary>
    /// [creature] cannot give its sigils at The Stones.
    /// </summary>
    public class Engraved : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
