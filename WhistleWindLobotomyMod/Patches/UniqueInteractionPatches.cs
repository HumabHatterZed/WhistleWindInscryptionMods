using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Spells.Patchers;

namespace WhistleWindLobotomyMod.Patches {
    [HarmonyPatch]
    internal class UniqueInteractionPatches {
        [HarmonyPrefix, HarmonyPatch(typeof(Deck), nameof(Deck.CardCanBePlayedByTurn2WithHand))]
        private static bool SpellsCannotBePlayedByTurn2(CardInfo card, ref bool __result) {
            if (card.IsSpell()) {
                __result = false;
                return false;
            }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(DrawRabbits), nameof(DrawRabbits.CardToDraw), MethodType.Getter)]
        private static bool InfiniteTrainingDummies(DrawRabbits __instance, ref CardInfo __result) {
            if (__instance.Card.Info.name == Cards.trainingDummy) {
                __result = CardLoader.GetCardByName(Cards.trainingDummy);
                __result.Mods.AddRange(__instance.GetNonDefaultModsFromSelf(Ability.None));
                return false;
            }
            return true;
        }
    }
}
