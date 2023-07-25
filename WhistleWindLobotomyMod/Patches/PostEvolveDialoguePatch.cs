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
            // instance can't be null here so we can check the Card
            if (__instance.Card == null)
            {
                yield return enumerator;
                yield break;
            }

            // grab the pre-evolution card name
            string cardName = __instance.Card.Info.name;

            // dialogue plays before evolution
            if (cardName == "wstl_nothingThereTrue")
            {
                __instance.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("NothingThereTransformTrue");
            }

            // base OnUpkeep code
            yield return enumerator;

            // dialogue plays after evolution
            // check pre-evolution name since __instance can be null here and we thus can't reliably check the Card
            switch (cardName)
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
