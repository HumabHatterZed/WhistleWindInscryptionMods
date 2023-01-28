using DiskCardGame;
using HarmonyLib;
using System.Collections;

namespace WhistleWind.LobotomyMod.Patches
{
    [HarmonyPatch(typeof(Strafe))]
    public static class StrafePatch
    {
        // Adds a check for whether the card instance is null or not
        [HarmonyPostfix, HarmonyPatch(nameof(Strafe.PostSuccessfulMoveSequence))]
        public static IEnumerator PostSuccessfulMoveSequenceNullCheck(IEnumerator enumerator, PlayableCard __instance)
        {
            if (!(__instance != null))
                yield break;

            yield return enumerator;
        }
    }
    [HarmonyPatch(typeof(AbilityBehaviour))]
    public static class AbilityBehaviourPatch
    {
        // Adds a check for whether the ability behaviour instance is null or not
        [HarmonyPostfix, HarmonyPatch(nameof(AbilityBehaviour.PreSuccessfulTriggerSequence))]
        public static IEnumerator PreSuccessfulTriggerSequenceNullCheck(IEnumerator enumerator, AbilityBehaviour __instance)
        {
            if (!(__instance != null))
                yield break;

            yield return enumerator;
        }

        // Adds a check for whether the ability behaviour instance is null or not
        [HarmonyPostfix, HarmonyPatch(nameof(AbilityBehaviour.LearnAbility))]
        public static IEnumerator LearnAbilityNullCheck(IEnumerator enumerator, AbilityBehaviour __instance)
        {
            if (!(__instance != null))
                yield break;

            yield return enumerator;
        }
    }
}
