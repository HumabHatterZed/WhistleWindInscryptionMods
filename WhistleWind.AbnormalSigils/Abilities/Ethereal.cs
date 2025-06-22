using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Ethereal() {
            const string rulebookName = "Ethereal";
            const string rulebookDescription = "Attacks directed at [creature] will strike the owner instead.";
            Ethereal.ability = AbnormalAbilityHelper.CreateAbility<Ethereal>(
                "sigilEthereal",
                rulebookName, rulebookDescription, powerLevel: 0,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Ethereal : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}