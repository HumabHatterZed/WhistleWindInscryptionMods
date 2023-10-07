using DiskCardGame;
using HarmonyLib;
using InscryptionAPI;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal static class ApocalypseBossPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.Attack), MethodType.Getter)]
        private static void NegatePowerChange(PlayableCard __instance, ref int __result)
        {
            if (TurnManager.Instance?.Opponent != null && TurnManager.Instance.Opponent is ApocalypseBossOpponent boss)
            {
                if (boss.BattleSequence.CurrentBossPhase == ApocalpyseBossPhase.BigEyes)
                    __result = Mathf.Max(0, __instance.Info.Attack);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CombatPhaseManager3D), nameof(CombatPhaseManager3D.VisualizeCardAttackingDirectly))]
        private static IEnumerator DirectDamageBones(IEnumerator enumerator, CardSlot attackingSlot, CardSlot targetSlot, int damage)
        {
            if (targetSlot.IsPlayerSlot || !LobotomyPlugin.PreventOpponentDamage
                || TurnManager.Instance?.Opponent == null || TurnManager.Instance.Opponent is not ApocalypseBossOpponent)
            {
                yield return enumerator;
                yield break;
            }

            int maxBones = Mathf.Min(3, damage);

            attackingSlot.Card.Anim.PlayAttackAnimation(attackPlayer: true, targetSlot, delegate
            {
                ResourcesManager.Instance.PlayerBones += maxBones;
                Singleton<TableVisualEffectsManager>.Instance?.ThumpTable(0.075f * (float)Mathf.Min(10, damage));

                for (int i = 0; i < maxBones; i++)
                {
                    Part1ResourcesManager manager = ResourcesManager.Instance as Part1ResourcesManager;
                    GameObject gameObject = Object.Instantiate(manager.boneTokenPrefab);
                    BoneTokenInteractable component = gameObject.GetComponent<BoneTokenInteractable>();
                    Rigidbody tokenRB = gameObject.GetComponent<Rigidbody>();
                    Vector3 vector = new(0f, 0f, 0.75f);

                    tokenRB.Sleep();

                    gameObject.transform.position = targetSlot.transform.position + vector + new Vector3(i * 0.1f, 0f, i * 0.1f);
                    gameObject.transform.eulerAngles = Random.insideUnitSphere;

                    Vector3 endValue = manager.GetRandomLandingPosition() + Vector3.up;
                    Tween.Position(component.transform, endValue, 0.25f, 0.5f, Tween.EaseInOut, Tween.LoopType.None, null, delegate
                    {
                        tokenRB.WakeUp();
                        manager.PushTokenDown(tokenRB);
                    });

                    manager.boneTokens.Add(component);
                    manager.isOrganized = false;
                }
            });
            yield return new WaitForSeconds(0.2f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBossBoneGain");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(LifeManager), nameof(LifeManager.ShowResetSequence))]
        internal static IEnumerator PreventScalesResetting(IEnumerator enumerator)
        {
            if (TurnManager.Instance.Opponent != null && TurnManager.Instance.Opponent is ApocalypseBossOpponent)
                yield break;

            yield return enumerator;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(MapDataReader), nameof(MapDataReader.SpawnMapObjects))]
        private static void ChangeTreesToBlack(MapDataReader __instance)
        {
            if (RunState.CurrentMapRegion == null || RunState.CurrentMapRegion != CustomBossUtils.apocalypseRegion)
                return;

            foreach (var i in __instance.scenery)
                i.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", Texture2D.blackTexture);
        }
    }
}
