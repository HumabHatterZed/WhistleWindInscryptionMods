using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

namespace WhistleWind.AbnormalSigils {
    /// <summary>
    /// Version of giant cards that occupies 2 slots.
    /// </summary>
    public class MiniGiantCard : SpecialCardBehaviour {
        public static SpecialTriggeredAbility Id;
        public SpecialTriggeredAbility SpecialAbility => Id;

        private const int _max = int.MaxValue;

        private CardSlot secondSlot = null;
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            secondSlot = base.PlayableCard.Slot.GetAdjacent(false) ?? base.PlayableCard.Slot.GetAdjacent(true);
            if (secondSlot != null) {
                secondSlot.Card = base.PlayableCard;
            }
            yield break;
        }
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            secondSlot.Card = null;
            yield break;
        }
        public override int Priority => _max;
    }

    [HarmonyPatch]
    internal class MiniGiantPatches {
        [HarmonyPostfix, HarmonyPatch(typeof(CardDisplayer3D), nameof(CardDisplayer3D.DisplayInfo))]
        private static void RenderEmissionForMiniGiants(CardDisplayer3D __instance, CardRenderInfo renderInfo, PlayableCard playableCard) {
            if (renderInfo.hidePortrait && renderInfo.baseInfo.HasSpecialAbility(MiniGiantCard.Id) && __instance.emissivePortraitRenderer != null) {
                __instance.emissivePortraitRenderer.gameObject.SetActive(false);
                if (__instance.portraitRenderer.sprite != null) {
                    __instance.emissivePortraitRenderer.gameObject.SetActive(true);
                    __instance.emissivePortraitRenderer.sprite = __instance.portraitRenderer.sprite;
                }
            }
        }
        [HarmonyPrefix, HarmonyPatch(typeof(CardSpawner), nameof(CardSpawner.SpawnPlayableCard))]
        private static bool ResizeMiniGiantCard(CardInfo info, ref PlayableCard __result) {
            if (!info.HasSpecialAbility(MiniGiantCard.Id))
                return true;

            AbnormalPlugin.Log.LogDebug("[SpawnPlayableCard] Create MiniGiant");
            GameObject go = GameObject.Instantiate(CardSpawner.Instance.PlayableCardPrefab);
            __result = go.GetComponent<PlayableCard>();
            __result.SetInfo(info);

            Transform child = SaveManager.SaveFile.IsGrimora ? __result.transform.GetChild(0) : __result.transform;

            if (SaveManager.SaveFile.IsGrimora)
                child.localPosition = new Vector3(-0.7f, 1.05f, 0f);
            else
                child.localPosition = new Vector3(0.7f, 0.025f, 1.05f);

            child.localScale = new Vector3(2.1f, 2.1f, 1f);
            __result.Anim.Anim.runtimeAnimatorController = AbnormalPlugin.MiniGiantAnimator;
            return false;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(BoardManager), nameof(BoardManager.AssignCardToSlot), MethodType.Enumerator)]
        private static IEnumerable<CodeInstruction> ChangeFinalLocalPosition(IEnumerable<CodeInstruction> instructions) {
            List<CodeInstruction> codes = new(instructions);

            int addCode = codes.FindIndex(x => x.opcode == OpCodes.Add);
            if (addCode != -1) {
                int start = addCode - 6, end = addCode + 2;
                codes.RemoveRange(start, end - start);
                codes.Insert(start++, new(OpCodes.Ldloc_1));
                codes.Insert(start++, new(OpCodes.Ldarg_0));
                codes.Insert(start++, new(OpCodes.Ldfld, codes.Find(x => x.opcode == OpCodes.Ldfld && x.operand.ToString() == "DiskCardGame.PlayableCard card").operand));
                codes.Insert(start++, new(OpCodes.Call,
                    AccessTools.Method(typeof(MiniGiantPatches), nameof(MiniGiantPatches.ModifyFinalLocalPosition),
                    new Type[] { typeof(BoardManager), typeof(PlayableCard) }
                )));
            }

            return codes;
        }

        private static Vector3 ModifyFinalLocalPosition(BoardManager instance, PlayableCard card) {
            if (!card.Info.HasSpecialAbility(MiniGiantCard.Id))
                return Vector3.up * (instance.SlotHeightOffset + card.SlotHeightOffset);

            if (SaveManager.SaveFile.IsGrimora)
                return new Vector3(-0.7f, 1.05f, 0f);
            else
                return new Vector3(0.7f, 0.025f, 1.05f);
        }
    }

    public partial class AbnormalPlugin {
        private void SpecialAbility_MiniGiant() {
            MiniGiantCard.Id = SpecialTriggeredAbilityManager.Add(pluginGuid, "MiniGiantCard", typeof(MiniGiantCard)).Id;
        }
    }
}
