using DiskCardGame;
using HarmonyLib;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(Evolve), nameof(Evolve.OnUpkeep))]
    internal static class PostEvolveDialoguePatch
    {
        [HarmonyPostfix]
        private static IEnumerator PlaySpecialEvolveDialogue(IEnumerator enumerator, Evolve __instance)
        {
            if (__instance.Card == null)
            {
                yield return enumerator;
                yield break;
            }

            // dialogue plays before evolution
            if (__instance.Card.Info.name == "wstl_nothingThereTrue")
            {
                __instance.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("NothingThereTransformTrue");
            }

            yield return enumerator;

            // dialogue plays after evolution
            switch (__instance.Card.Info.name)
            {
                case "wstl_kingOfGreed":
                    yield return new WaitForSeconds(0.2f);
                    yield return DialogueHelper.PlayDialogueEvent("KingOfGreedTransform");
                    yield break;
                case "wstl_nothingThereFinal":
                    yield return new WaitForSeconds(0.2f);
                    yield return DialogueHelper.PlayDialogueEvent("NothingThereTransformEgg");
                    yield break;
            }
        }
    }
}
