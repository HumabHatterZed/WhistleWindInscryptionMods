using DiskCardGame;
using HarmonyLib;
using UnityEngine;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal static class ApocalypseBossPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.GetPassiveAttackBuffs))]
        private static void BigEyesPhaseChangesAttackColour(ref int __result)
        {
            if (LobOpponentUtils.IsCustomBoss(out ApocalypseBossOpponent boss) && boss.BattleSequencer.ActiveEggEffect == ActiveEggEffect.BigEyes)
            {
                // easiest way to change the text colour
                // since big eyes ignores passive attack buffs, this won't have an effect on cards' actual Power
                __result = -1;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.Attack), MethodType.Getter)]
        private static void BigEyesPhaseNegatesAttackBuffs(PlayableCard __instance, ref int __result)
        {
            if (LobOpponentUtils.IsCustomBoss(out ApocalypseBossOpponent boss) && boss.BattleSequencer.ActiveEggEffect == ActiveEggEffect.BigEyes)
            {
                __result = Mathf.Max(0, __instance.Info.Attack);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(MapDataReader), nameof(MapDataReader.SpawnMapObjects))]
        private static void MakeTheBlackForestBlack(MapDataReader __instance)
        {
            if (RunState.CurrentMapRegion != LobOpponentUtils.apocalypseRegion)
                return;

            foreach (MapElement i in __instance.scenery)
                i.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", Texture2D.blackTexture);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ConsumableItem), nameof(ConsumableItem.OnExtraActivationPrerequisitesNotMet))]
        private static void PreventHourglassItemDialogue(ConsumableItem __instance) {
            if (__instance is HourglassItem
                && LobOpponentUtils.IsCustomBoss(out ApocalypseBossOpponent boss)
                && !boss.BattleSequencer.DisabledEggEffects.Contains(ActiveEggEffect.LongArms)) {
                if (!TextDisplayer.Instance.textMesh.gameObject.activeSelf) {
                    CustomCoroutine.Instance.StartCoroutine(TextDisplayer.Instance.ShowUntilInput("The Long Bird's arms conceal time."));
                }
            }
        }
    }
}
