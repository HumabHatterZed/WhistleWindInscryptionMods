using DiskCardGame;
using HarmonyLib;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils.Core {
    [HarmonyPatch]
    public static partial class StatusEffectPatches {
        [HarmonyPrefix, HarmonyPatch(typeof(RenderStatsLayer), nameof(RenderStatsLayer.DisableEmission))]
        private static bool AlwaysEmitForStatusEffects(RenderStatsLayer __instance) {
            if (__instance.PlayableCard != null && __instance.PlayableCard.HasStatusEffect()) {
                return false;
            }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(RenderLiveStatsLayer), nameof(RenderLiveStatsLayer.RenderCard))]
        private static bool AlwaysEnableEmissionForStatuses(RenderLiveStatsLayer __instance, CardRenderInfo info) {
            if (__instance.PlayableCard != null && __instance.PlayableCard.HasStatusEffect()) {

                if (__instance.EmissionTexture != null) {
                    __instance.Material.SetTexture("_EmissionMap", __instance.EmissionTexture);
                }

                if (!__instance.PlayableCard.Dead) {
                    Singleton<CardRenderCamera>.Instance.LiveRenderCard(info, __instance, __instance.PlayableCard);
                }
                return false;
            }
            return true;
        }



        [HarmonyPrefix, HarmonyPatch(typeof(CardRenderCamera), nameof(CardRenderCamera.LiveRenderCard))]
        private static bool FixStatusesForLiveRenders(CardRenderCamera __instance, CardRenderInfo info, RenderStatsLayer layer, PlayableCard playableCard) {
            if (playableCard == null || !playableCard.HasStatusEffect()) {
                return true;
            }

            __instance.GetLiveRenderCamera(layer).GetComponentInChildren<CardDisplayer3D>().DisplayInfo(info, playableCard);
            __instance.GetLiveRenderCamera(layer).transform.Find("EmissionRenderCamera").gameObject.SetActive(true);
            return false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(CardRenderCamera), nameof(CardRenderCamera.QueueStatsLayerForRender))]
        private static bool AlwaysEmitForStatusEffects(PlayableCard playableCard, ref bool updateEmission) {
            if (playableCard != null && playableCard.HasStatusEffect()) {
                updateEmission = true;
            }
            return true;
        }
    }
}