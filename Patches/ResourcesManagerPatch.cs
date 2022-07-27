using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
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
                bool train = slot.Card.Info.GetExtendedProperty("wstl:KilledByTrain") != null && (bool)slot.Card.Info.GetExtendedPropertyAsBool("wstl:KilledByTrain");
                bool whiteNight = slot.Card.Info.HasAbility(TrueSaviour.ability) || slot.Card.Info.HasAbility(Apostle.ability) || slot.Card.Info.HasAbility(Confession.ability);
                if (train || whiteNight)
                {
                    if (train)
                    {
                        slot.Card.Info.SetExtendedProperty("wstl:KilledByTrain", false);
                    }
                    yield break;
                }
            }
            yield return enumerator;
        }
    }
}
