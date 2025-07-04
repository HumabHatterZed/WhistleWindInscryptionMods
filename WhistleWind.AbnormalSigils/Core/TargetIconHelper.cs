using DiskCardGame;
using Pixelplacement;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.Core {
    /// <summary>
    /// Contains methods for creating and cleaning up target icon GameObjects.
    /// </summary>
    public class TargetIconHelper {
        public static GameObject targetIconPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CannonTargetIcon");

        public static GameObject CreateTargetIcon(CardSlot targetSlot, GameObject targetPrefab, Color materialColour = default) {
            GameObject gameObject = GameObject.Instantiate(targetPrefab, targetSlot.transform);
            gameObject.transform.localPosition = new Vector3(0f, 0.25f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;

            if (materialColour != default)
                gameObject.GetComponentInChildren<MeshRenderer>().material.color = materialColour;

            return gameObject;
        }
        public static GameObject CreateTargetIcon(CardSlot targetSlot, Color materialColour = default) {
            GameObject gameObject = GameObject.Instantiate(targetIconPrefab, targetSlot.transform);
            gameObject.transform.localPosition = new Vector3(0f, 0.25f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;

            if (materialColour != default)
                gameObject.GetComponentInChildren<MeshRenderer>().material.color = materialColour;

            return gameObject;
        }
        public static void CleanUpTargetIcon(GameObject icon) {
            Tween.LocalScale(icon.transform, Vector3.zero, 0.1f, 0f, Tween.EaseIn, Tween.LoopType.None, null, delegate {
                GameObject.Destroy(icon);
            });
        }
    }
}
