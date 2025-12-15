using DiskCardGame;
using HarmonyLib;
using Steamworks;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Ethereal() {
            const string rulebookName = "Ethereal";
            const string rulebookDescription = "[creature] cannot take from or deal damage to opposing creatures. Instead, attacks from and against this card will always strike directly.";
            Ethereal.ability = AbnormalAbilityHelper.CreateAbility<Ethereal>(
                "sigilEthereal",
                rulebookName, rulebookDescription, powerLevel: 0,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// [creature] cannot take from or deal damage to opposing creatures. Instead, attacks from and against this card will always strike directly.
    /// </summary>
    [HarmonyPatch]
    public class Ethereal : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.TakeDamage))]
        private static IEnumerator PreventDamageFromIndirectSources(IEnumerator result, PlayableCard __instance, PlayableCard attacker) {
            if (__instance.HasAbility(ability) || (attacker != null && attacker.HasAbility(ability))) {
                yield break;
            }
            yield return result;
        }
    }
}