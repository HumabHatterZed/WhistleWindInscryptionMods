using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils;
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

        [HarmonyPostfix, HarmonyPatch(typeof(CardInfo), nameof(CardInfo.HasAbility))]
        private static void PpodaeStinkyIsStinky(CardInfo __instance, Ability ability, ref bool __result)
        {
            if (__result || ability != Ability.DebuffEnemy)
                return;

            __result = __instance.HasAbility(Abilities.PpodaeStinky);
        }
    }
}
