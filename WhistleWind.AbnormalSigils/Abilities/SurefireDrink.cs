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
        private void Ability_SurefireDrink() {
            const string rulebookName = "Surefire Soda";
            const string rulebookDescription = "The selected card will gain Sniper for this turn.";
            SurefireDrink.ability = AbnormalAbilityHelper.CreateAbility<SurefireDrink>(
                "sigilSurefireDrink",
                rulebookName, rulebookDescription, powerLevel: 2,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    /// <summary>
    /// The selected card will have Sniper for this turn.
    /// </summary>
    public class SurefireDrink : SodaAbilityBehaviour {
        public const Ability abilityToAdd = Ability.Sniper;
        public const string id = "SurefireDrunk";

        public static Ability ability;
        public override Ability Ability => ability;
        public override string SingletonId => id;
        public override Ability AbilityToAdd => abilityToAdd;
        public override SpecialTriggeredAbility StatusEffectId => SurefireDrinkEffect.specialAbility;
        public override int BasePotency => 1;
    }
}
