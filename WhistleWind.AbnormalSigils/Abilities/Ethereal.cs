using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Ethereal() {
            const string rulebookName = "Ethereal";
            const string rulebookDescription = "[creature] cannot be damaged indirectly, and attacks from or towards this card will always strike directly instead.";
            Ethereal.ability = AbnormalAbilityHelper.CreateAbility<Ethereal>(
                "sigilEthereal",
                rulebookName, rulebookDescription, powerLevel: 0,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// [creature] cannot be damaged indirectly, and attacks from or towards this card will always strike directly instead.
    /// </summary>
    [HarmonyPatch]
    public class Ethereal : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.TakeDamage))]
        private static IEnumerator PreventDamageFromIndirectSources(IEnumerator result, PlayableCard __instance, int damage, PlayableCard attacker) {
            // allow for hammer usage (hammer deals 100 damage flat, but we want to account for damage reduction
            if (__instance.HasAbility(ability) && (attacker != null || damage < 90)) {
                yield break;
            }
            if (attacker != null && attacker.HasAbility(ability)) {
                yield break;
            }
            yield return result;
        }
    }
}