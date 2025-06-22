using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using MagnificusMod;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BonniesBakingPack {
    public static class MagnificusPatches {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(SigilCode.FecundityCycle), nameof(SigilCode.FecundityCycle.CardToDraw), MethodType.Getter)]
        private static void ChangeMouseCopyForEachCose(SigilCode.FecundityCycle __instance, ref CardInfo __result) {
            if (__result.name.StartsWith("bbp_magnificus_mouseApprentice")) {
                switch (__result.gemsCost[0]) {
                    case GemType.Green:
                        __result = CardLoader.GetCardByName("bbp_magnificus_mouseApprentice_blue");
                        break;
                    case GemType.Orange:
                        __result = CardLoader.GetCardByName("bbp_magnificus_mouseApprentice_green");
                        break;
                    case GemType.Blue:
                        __result = CardLoader.GetCardByName("bbp_magnificus_mouseApprentice_orange");
                        break;
                }
            }
            else if (__result.name.StartsWith("bbp_magnificus_mouseWizard")) {
                switch (__result.gemsCost[0]) {
                    case GemType.Green:
                        __result = CardLoader.GetCardByName("bbp_magnificus_mouseWizard_blue");
                        break;
                    case GemType.Orange:
                        __result = CardLoader.GetCardByName("bbp_magnificus_mouseWizard_green");
                        break;
                    case GemType.Blue:
                        __result = CardLoader.GetCardByName("bbp_magnificus_mouseWizard_orange");
                        break;
                }
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(SigilCode.MoxCycling), nameof(SigilCode.MoxCycling.OnUpkeep))]
        private static IEnumerator CycleGemFood(IEnumerator result, SigilCode.MoxCycling __instance) {
            yield return FoodCycleSupport(__instance);
        }

        private static IEnumerator FoodCycleSupport(SigilCode.MoxCycling __instance) {
            List<CardSlot> slots = Singleton<BoardManager>.Instance.GetSlots(__instance.Card.slot.IsPlayerSlot);
            foreach (CardSlot slot in slots) {
                if (slot.Card != null && slot.Card.Info.HasTrait(Trait.Gem) && !slot.Card.Info.name.Contains("mag_maux")) {
                    List<string> cardNames = new List<string> { "MoxSapphire", "MoxRuby", "MoxEmerald" };
                    if (slot.Card.Info.name.Contains("moxrabbit")) {
                        cardNames = new List<string> { "mag_moxrabbit", "mag_orangemoxrabbit", "mag_greenmoxrabbit" };
                    }
                    else if (slot.Card.Info.name.Contains("MoxDual")) {
                        cardNames = new List<string> { "MoxDualOB", "MoxDualGO", "MoxDualBG" };
                    }
                    else if (slot.Card.Info.name.Contains("mag_moxmage")) {
                        cardNames = new List<string> { "mag_moxmage_sapphire", "mag_moxmage", "mag_moxmage_emerald" };
                    }
                    else if (slot.Card.Info.name.Contains("mag_crystalworm")) {
                        cardNames = new List<string> { "mag_crystalworm_blue", "mag_crystalworm_orange", "mag_crystalworm_green" };
                    }
                    else if (slot.Card.Info.name.Contains("bbp_magnificus")) {
                        cardNames = new List<string> { "bbp_magnificus_whiteDonut", "bbp_magnificus_redVelvet", "bbp_magnificus_eggTart" };
                    }
                    slot.Card.SetCardback(BakingPlugin.GetTexture("magcardback.png", BakingPlugin.ScrybeCompat.MagnificusAsm));
                    slot.Card.Anim.SetFaceDown(faceDown: true);
                    CardInfo info = slot.Card.Info;
                    switch (slot.Card.Info.abilities[0]) {
                        case Ability.GainGemGreen:
                            info = CardLoader.GetCardByName(cardNames[0]);
                            break;
                        case Ability.GainGemBlue:
                            info = CardLoader.GetCardByName(cardNames[1]);
                            break;
                        case Ability.GainGemOrange:
                            info = CardLoader.GetCardByName(cardNames[2]);
                            break;
                    }
                    slot.Card.SetInfo(info);
                    yield return new WaitForSeconds(0.25f);
                    slot.Card.Anim.SetFaceDown(faceDown: false);
                    Singleton<ResourcesManager>.Instance.ForceGemsUpdate();
                }
            }
        }
    }
}
