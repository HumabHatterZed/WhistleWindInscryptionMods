using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;

namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch(typeof(ResourcesManager))]
    public static class ResourcesManagerPatch
    {
        // Prevents bones from dropping under certain conditions
        [HarmonyPostfix, HarmonyPatch(nameof(ResourcesManager.AddBones))]
        public static IEnumerator AddBones(IEnumerator enumerator, CardSlot slot)
        {
            if (slot != null && slot.Card != null)
            {
                bool killedByTrain = slot.Card.TemporaryMods.Exists(x => x.singletonId == "wstl:KilledByTrain");

                if (slot.Card.HasTrait(AbnormalPlugin.Boneless) || killedByTrain)
                    yield break;
            }
            yield return enumerator;
        }
    }
}
