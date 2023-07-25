using DiskCardGame;
using HarmonyLib;
using System.Collections;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(Strafe), nameof(Strafe.PostSuccessfulMoveSequence))]
    internal static class StrafePatch // Adds a check for whether the card instance is null or not
    {
        [HarmonyPostfix]
        private static IEnumerator PostSuccessfulMoveSequenceNullCheck(IEnumerator enumerator, PlayableCard __instance)
        {
            if (!__instance)
                yield break;

            yield return enumerator;
        }
    }
    [HarmonyPatch(typeof(AbilityBehaviour))]
    internal static class AbilityBehaviourPatch // Adds a check for whether the ability behaviour instance is null or not
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(AbilityBehaviour.LearnAbility))]
        [HarmonyPatch(nameof(AbilityBehaviour.PreSuccessfulTriggerSequence))]
        private static IEnumerator LearnAbilityNullCheck(IEnumerator enumerator, AbilityBehaviour __instance)
        {
            if (!__instance)
                yield break;

            yield return enumerator;
        }
    }
}
