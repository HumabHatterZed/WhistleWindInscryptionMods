using DiskCardGame;
using HarmonyLib;
using InscryptionCommunityPatch.Card;

namespace WhistleWind.AbnormalSigils.Patches {
    [HarmonyPatch]
    internal class SniperSigilAvoidancePatch {
        [HarmonyPostfix, HarmonyPatch(typeof(SniperFix), nameof(SniperFix.WillDieFromSharp))]
        private static void AddExtraChecks(PlayableCard pc, CardSlot slot, ref bool __result) {
            if (!__result) {
                __result = slot.Card.HasAbility(Punisher.ID) || (slot.Card.HasAbility(Reflector.ID) && pc.Attack >= pc.Health);
            }
        }
    }
}
