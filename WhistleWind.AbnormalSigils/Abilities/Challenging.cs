using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Challenging()
        {
            const string rulebookName = "Challenging";
            const string rulebookDescription = "[creature] is considered Made of Stone and Bleachproof.";
            Challenging.ability = AbnormalAbilityHelper.CreateAbility<Challenging>(
                "sigilChallenging",
                rulebookName, rulebookDescription, powerLevel: 4,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }

    [HarmonyPatch]
    public class Challenging : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.HasAbility))]
        private static void CountsAsStoneAndBleachproof(PlayableCard __instance, Ability ability, ref bool __result)
        {
            if (__result)
                return;

            if (ability == Ability.MadeOfStone || ability == Bleachproof.ability)
            {
                if (__instance.HasAbility(Challenging.ability))
                {
                    __result = true;
                }
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(CardInfo), nameof(CardInfo.HasAbility))]
        private static void InfoIsStoneAndBleachless(CardInfo __instance, Ability ability, ref bool __result)
        {
            if (__result)
                return;

            if (ability == Ability.MadeOfStone || ability == Bleachproof.ability)
            {
                if (__instance.HasAbility(Challenging.ability))
                {
                    __result = true;
                }
            }
        }
    }
}
