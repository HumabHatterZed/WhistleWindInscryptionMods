using DiskCardGame;
using GBC;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.Patches {
    /// <summary>
    /// Patches to make abilities function properly
    /// </summary>
    [HarmonyPatch]
    internal class ReturnCardPatches {
        [HarmonyPostfix, HarmonyPatch(typeof(CardExtensions), nameof(CardExtensions.BloodCost))]
        private static void GuaranteeCorrectBloodCost(ref int __result, PlayableCard card) {
            CardModificationInfo recallMod = card.TemporaryMods.Find(x => x.singletonId == ReturnCard.RECALL_MOD);
            if (recallMod != null) {
                __result = 0;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CardExtensions), nameof(CardExtensions.BonesCost))]
        private static void GuarnteeCorrectBonesCost(ref int __result, PlayableCard card) {
            CardModificationInfo recallMod = card.TemporaryMods.Find(x => x.singletonId == ReturnCard.RECALL_MOD);
            if (recallMod != null) { 
                __result = recallMod.bonesCostAdjustment;
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.EnergyCost), MethodType.Getter)]
        private static void GuaranteeCorrectEnergyCost(ref int __result, PlayableCard __instance) {
            CardModificationInfo recallMod = __instance.TemporaryMods.Find(x => x.singletonId == ReturnCard.RECALL_MOD);
            if (recallMod != null) {
                __result = 0;
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(CardExtensions), nameof(CardExtensions.GemsCost))]
        private static void GuaranteeCorrectGemsCost(ref List<GemType> __result, PlayableCard card) {
            CardModificationInfo recallMod = card.TemporaryMods.Find(x => x.singletonId == ReturnCard.RECALL_MOD);
            if (recallMod != null) {
                __result = new();
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.CanPlay))]
        private static void CanPlayRecalledCards(ref bool __result, PlayableCard __instance) {
            CardModificationInfo recallMod = __instance.TemporaryMods.Find(x => x.singletonId == ReturnCard.RECALL_MOD);
            if (recallMod != null) {
                __result = recallMod.bonesCostAdjustment <= ResourcesManager.Instance.PlayerBones;
            }
        }
        [HarmonyPrefix, HarmonyPatch(typeof(HintsHandler), nameof(HintsHandler.OnNonplayableCardClicked))]
        private static bool CorrectRecalledHint(PlayableCard card) {
            CardModificationInfo recallMod = card.TemporaryMods.Find(x => x.singletonId == ReturnCard.RECALL_MOD);
            if (recallMod != null) {
                HintsHandler.notEnoughBonesHint.TryPlayDialogue(new string[2] {
                    card.Info.DisplayedNameLocalized,
                    card.BonesCost().ToString()
                    });
                return false;
            }
            return true;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(CardGainAbility), nameof(CardGainAbility.RespondsToOtherCardDrawn))]
        private static void RecalledCardsDontTriggerTotems(ref bool __result, PlayableCard card) {
            if (__result && card.TemporaryMods.Find(x => x.singletonId == ReturnCard.RECALL_MOD) != null) {
                __result = false;
            }
        }
    }
}
