using DiskCardGame;
using HarmonyLib;
using Pixelplacement;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.Core {
    /// <summary>
    /// Contains methods for creating and cleaning up target icon GameObjects.
    /// </summary>

    [HarmonyPatch]
    public class TargetIconHelper {
        public static GameObject targetIconPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CannonTargetIcon");

        public static readonly List<GameObject> AllCreatedTargetIcons = new();

        public static GameObject CreateTargetIcon(CardSlot targetSlot, GameObject targetPrefab, Color materialColour = default) {
            GameObject gameObject = GameObject.Instantiate(targetPrefab, targetSlot.transform);
            gameObject.transform.localPosition = new Vector3(0f, 0.25f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;

            if (materialColour != default)
                gameObject.GetComponentInChildren<MeshRenderer>().material.color = materialColour;

            AllCreatedTargetIcons.Add(gameObject);
            return gameObject;
        }
        public static GameObject CreateTargetIcon(CardSlot targetSlot, Color materialColour = default) {
            return CreateTargetIcon(targetSlot, targetIconPrefab, materialColour);
        }
        public static void CleanUpTargetIcon(GameObject icon) {
            if (icon != null) {
                Tween.LocalScale(icon.transform, Vector3.zero, 0.1f, 0f, Tween.EaseIn, Tween.LoopType.None, null, delegate {
                    AllCreatedTargetIcons.Remove(icon);
                    GameObject.Destroy(icon);
                });
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(TurnManager), nameof(TurnManager.CleanupPhase))]
        private static void DestroyAllRemainingTargetIcons() {
            foreach (GameObject obj in AllCreatedTargetIcons) {
                CleanUpTargetIcon(obj);
            }
        }
    }
}
