using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_PotshotPop() {
            const string rulebookName = "Potshot Pop";
            const string rulebookDescription = "The selected card will gain Sentry for 2 turns.";
            PotshotPop.ability = AbnormalAbilityHelper.CreateAbility<PotshotPop>(
                "sigilPotshotPop",
                rulebookName, rulebookDescription, powerLevel: 2,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    /// <summary>
    /// The selected card will gain Sentry for 2 turns.
    /// </summary>
    public class PotshotPop : SodaAbilityBehaviour {
        public const Ability abilityToAdd = Ability.Sentry;
        public const string id = "PotshotPopped";

        public static Ability ability;
        public override Ability Ability => ability;
        public override string SingletonId => id;
        public override Ability AbilityToAdd => abilityToAdd;
        public override SpecialTriggeredAbility StatusEffectId => PotshotPopEffect.specialAbility;
        public override int BasePotency => 2;
    }
}
