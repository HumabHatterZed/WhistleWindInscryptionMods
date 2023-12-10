using DiskCardGame;
using HarmonyLib;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(Evolve), nameof(Evolve.OnUpkeep))]
    internal static class SpecialEvolutionDialoguePatch
    {
        [HarmonyPostfix]
        private static IEnumerator PlaySpecialEvolveDialogue(IEnumerator enumerator, Evolve __instance)
        {
            if (__instance.Card == null)
            {
                yield return enumerator;
                yield break;
            }

            // only show dialogue if we're actually evolving
            int turnsToEvolve = __instance.Card.Info.evolveParams?.turnsToEvolve ?? 1;
            if (Mathf.Max(1, turnsToEvolve - (__instance.numTurnsInPlay + 1)) < turnsToEvolve)
            {
                yield return enumerator;
                yield break;
            }

            // grab the pre-evolution card name
            // since __instance can be null after calling enumerator and we thus can't reliably check the Card
            string preEvolutionName = __instance.Card.Info.name;

            // pre-evolution dialogue
            if (preEvolutionName == "wstl_nothingThereTrue")
            {
                ViewManager.Instance.SwitchToView(View.Board);
                yield return new WaitForSeconds(0.15f);
                __instance.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.45f);
                yield return DialogueHelper.PlayDialogueEvent("NothingThereTransformTrue");
            }

            // base OnUpkeep code
            yield return enumerator;

            // dialogue plays after evolution
            switch (preEvolutionName)
            {
                case "wstl_magicalGirlDiamond":
                    yield return new WaitForSeconds(0.2f);
                    yield return DialogueHelper.PlayDialogueEvent("KingOfGreedTransform");
                    yield break;
                case "wstl_nothingThereEgg":
                    yield return new WaitForSeconds(0.2f);
                    yield return DialogueHelper.PlayDialogueEvent("NothingThereTransformEgg");
                    yield break;
            }
        }
    }
}
