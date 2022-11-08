using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;

namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch(typeof(ResourcesManager))]
    public static class AbnormalResourcesManagerPatch
    {
        // Prevents bones from dropping under certain conditions
        [HarmonyPostfix, HarmonyPatch(nameof(ResourcesManager.AddBones))]
        public static IEnumerator AddBones(IEnumerator enumerator, CardSlot slot)
        {
            if (slot != null && slot.Card != null)
            {
                bool train = slot.Card.Info.GetExtendedPropertyAsBool("wstl:KilledByTrain") ?? false;
                bool noBones = slot.Card.Info.HasSpecialAbility(ImmuneToInstaDeath.specialAbility);
                if (train || noBones)
                {
                    if (train)
                        slot.Card.Info.SetExtendedProperty("wstl:KilledByTrain", false);

                    yield break;
                }
            }
            yield return enumerator;
        }
    }
}
