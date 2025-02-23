using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Linq;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal class UniqueInteractionPatches
    {
        // Corrects the possible chooseable cards to exclude certain cards and to include non-Nature Temple cards
        [HarmonyPrefix, HarmonyPatch(typeof(DrawRabbits), nameof(DrawRabbits.CardToDraw), MethodType.Getter)]
        private static bool InfiniteTrainingDummies(DrawRabbits __instance, ref CardInfo __result)
        {
            if (__instance.Card.Info.name == Cards.trainingDummy)
            {
                __result = CardLoader.GetCardByName(Cards.trainingDummy);
                __result.Mods.AddRange(__instance.GetNonDefaultModsFromSelf(Ability.None));
                return false;
            }
            return true;
        }
    }
}
