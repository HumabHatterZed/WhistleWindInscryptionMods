using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.AbnormalSigils.Core;

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
                if (slot.Card.Info.HasTrait(AbnormalPlugin.Boneless))
                    yield break;

                if (slot.Card.Info.GetExtendedPropertyAsBool("wstl:KilledByTheTrainAbility") ?? false)
                {
                    slot.Card.Info.SetExtendedProperty("wstl:KilledByTheTrainAbility", false);
                    yield break;
                }
            }
            yield return enumerator;
        }
    }
}
