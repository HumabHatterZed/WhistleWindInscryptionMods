using DiskCardGame;
using GBC;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Animations;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils.Patches
{
    public class StatusEffectAbilityIcons : ManagedBehaviour
    {
        public List<GameObject> statusEffectIconGroups = new();
        public List<AbilityIconInteractable> abilityIcons = new();
        public Material statusEffectMat = null;
    }
    public class PixelStatusEffectAbilityIcons : ManagedBehaviour
    {
        public List<GameObject> statusEffectIconGroups = new();
        public List<AbilityIconInteractable> statusEffectIcons = new();

        public Material statusEffectMat = null;

        public List<AbilityIconInteractable> abilityIcons = new();
    }
    [HarmonyPatch]
    internal class RenderStatusEffects // Adds extra icon slots for rendering status effects
    {
        private static readonly Vector3 LocalScaleBase3D = new(0.18f, 0.12f, 1f);
        private static readonly float yPosition = 0.22f;
        private static readonly float yPositionPart3 = 1f;

        /*        [HarmonyPrefix, HarmonyPatch(typeof(PixelCardAbilityIcons), nameof(PixelCardAbilityIcons.DisplayAbilities),
            new Type[] { typeof(CardRenderInfo), typeof(PlayableCard) })]
        private static void AddExtraPixelAbilityIcons(CardAbilityIcons __instance) => AddPixelEffectIconSlotsToCard(__instance.transform);*/

        [HarmonyPatch(typeof(CardAbilityIcons), nameof(CardAbilityIcons.GetDistinctShownAbilities))]
        [HarmonyPostfix]
        private static void DontRenderStatusEffects(ref List<Ability> __result)
        {
            // Don't render Status Effects regularly
            __result.RemoveAll(x => x.GetExtendedProperty("wstl:StatusEffect") != null);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(CardAbilityIcons), nameof(CardAbilityIcons.UpdateAbilityIcons))]
        private static void AddStatusEffectIcons(CardAbilityIcons __instance)
        {
            if (SaveManager.SaveFile.IsPart1 || SaveManager.SaveFile.IsPart3)
            {
                StatusEffectAbilityIcons component = __instance.GetComponent<StatusEffectAbilityIcons>() ?? __instance.gameObject.AddComponent<StatusEffectAbilityIcons>();
                component.statusEffectMat ??= new(__instance.defaultIconMat);

                AddStatusEffectSlots(__instance.transform, component);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CardAbilityIcons), nameof(CardAbilityIcons.UpdateAbilityIcons))]
        private static void UpdateStatusEffects(CardAbilityIcons __instance, PlayableCard playableCard)
        {
            List<Ability> abilities = AbilitiesUtil.GetAbilitiesFromMods(playableCard?.Info?.Mods ?? new());
            abilities.RemoveAll(x => string.IsNullOrEmpty(x.GetExtendedProperty("wstl:StatusEffect")));

            List<Ability> distinct = abilities.Distinct().ToList();
            distinct.Sort((a, b) => a.CompareTo(b));
            StatusEffectAbilityIcons controller = __instance.GetComponent<StatusEffectAbilityIcons>();
            controller.abilityIcons.Clear();

            foreach (GameObject obj in controller.statusEffectIconGroups)
                obj.SetActive(false);

            if (distinct.Count > 0 && controller.statusEffectIconGroups.Count >= distinct.Count)
            {
                GameObject obj = controller.statusEffectIconGroups[distinct.Count - 1];
                obj.SetActive(true);
                AbilityIconInteractable[] componentsInChildren = obj.GetComponentsInChildren<AbilityIconInteractable>();

                for (int i = 0; i < componentsInChildren.Length; i++)
                {
                    Material mat = new(controller.statusEffectMat);
                    string colourName = distinct[i].GetExtendedProperty("wstl:StatusEffect");
                    
                    if (!string.IsNullOrEmpty(colourName))
                    {
                        switch (colourName)
                        {
                            case "red":
                                mat.color = GameColors.Instance.red;
                                break;
                            case "green":
                                mat.color = SaveManager.SaveFile.IsPart1 ? GameColors.Instance.darkBlue : GameColors.Instance.limeGreen;
                                break;
                            case "brown":
                                mat.color = GameColors.Instance.brown;
                                break;
                        };
                    }
                    componentsInChildren[i].gameObject.SetActive(true);
                    componentsInChildren[i].SetMaterial(mat);
                    componentsInChildren[i].AssignAbility(distinct[i], playableCard.Info, playableCard);
                    controller.abilityIcons.Add(componentsInChildren[i]);
                    __instance.abilityIcons.Add(componentsInChildren[i]);
                }
            }
        }

        private static void AddStatusEffectSlots(Transform abilityIconParent, StatusEffectAbilityIcons controller)
        {
            if (abilityIconParent == null || abilityIconParent.gameObject.GetComponent<CardAbilityIcons>() == null)
                return;

            // create the ability icon groups if they don't exist
            if (abilityIconParent.Find("StatusEffectIcon_1") == null)
                AddSingleIconSlotToCard(controller, abilityIconParent);

            if (abilityIconParent.Find("StatusEffectIcon_2") == null)
                AddDoubleIconSlotToCard(controller, abilityIconParent);

            if (abilityIconParent.Find("StatusEffectIcon_3") == null)
                AddTripleIconSlotToCard(controller, abilityIconParent);

            if (abilityIconParent.Find("StatusEffectIcon_4") == null)
                AddQuadrupleIconSlotToCard(controller, abilityIconParent);

            if (abilityIconParent.Find("StatusEffectIcon_5") == null)
                AddQuintupleIconSlotToCard(controller, abilityIconParent);
        }

        private static GameObject NewIconGroup(StatusEffectAbilityIcons controller, Transform parent, int newSlotNum)
        {
            GameObject prevIconGroup = parent.Find($"DefaultIcons_{newSlotNum}Abilit{(newSlotNum == 1 ? "y" : "ies")}").gameObject;
            GameObject newIconGroup = UnityEngine.Object.Instantiate(prevIconGroup, parent);
            newIconGroup.name = $"StatusEffectIcon_{newSlotNum}";
            controller.statusEffectIconGroups.Add(newIconGroup);
            return newIconGroup;
        }
        private static List<Transform> NewIcons(GameObject newIconGroup, int slotNum)
        {
            List<Transform> icons = new();

            if (slotNum == 1)
                icons.Add(newIconGroup.transform);
            else
            {
                foreach (Transform icon in newIconGroup.transform)
                    icons.Add(icon);
            }

            // change the names and scale
            // keep z scale at 1 to not mess with icon interactiveness
            foreach (Transform icon in icons)
            {
                icon.name = "StatusEffectIcon";
                icon.localScale = LocalScaleBase3D;
            }

            return icons;
        }

        #region 3DIcons
        private static void AddSingleIconSlotToCard(StatusEffectAbilityIcons controller, Transform abilityIconParent)
        {
            GameObject iconGroup = NewIconGroup(controller, abilityIconParent, 1);
            List<Transform> icons = NewIcons(iconGroup, 1);

            if (SaveManager.SaveFile.IsPart1)
                icons[0].localPosition = new Vector3(-0.375f, yPosition, 0f);
            else
                icons[0].localPosition = new Vector3(-0.42f, yPositionPart3, 0f);
        }
        private static void AddDoubleIconSlotToCard(StatusEffectAbilityIcons controller, Transform abilityIconParent)
        {
            GameObject iconGroup2 = NewIconGroup(controller, abilityIconParent, 2);
            List<Transform> icons = NewIcons(iconGroup2, 2);

            if (SaveManager.SaveFile.IsPart1)
            {
                icons[0].localPosition = new Vector3(-0.375f, yPosition, 0f);
                icons[1].localPosition = new Vector3(-0.1875f, yPosition, 0f);
            }
            else
            {
                icons[0].localPosition = new Vector3(-0.42f, yPositionPart3, 0f);
                icons[1].localPosition = new Vector3(-0.14f, yPositionPart3, 0f);
            }
        }
        private static void AddTripleIconSlotToCard(StatusEffectAbilityIcons controller, Transform abilityIconParent)
        {
            GameObject iconGroup3 = NewIconGroup(controller, abilityIconParent, 3);
            List<Transform> icons = NewIcons(iconGroup3, 3);

            if (SaveManager.SaveFile.IsPart1)
            {
                icons[0].localPosition = new Vector3(-0.375f, yPosition, 0f);
                icons[1].localPosition = new Vector3(-0.1875f, yPosition, 0f);
                icons[2].localPosition = new Vector3(0f, yPosition, 0f);
            }
            else
            {
                icons[0].localPosition = new Vector3(-0.42f, yPositionPart3, 0f);
                icons[1].localPosition = new Vector3(-0.14f, yPositionPart3, 0f);
                icons[2].localPosition = new Vector3(0.14f, yPositionPart3, 0f);
            }
        }
        private static void AddQuadrupleIconSlotToCard(StatusEffectAbilityIcons controller, Transform abilityIconParent)
        {
            GameObject iconGroup4 = NewIconGroup(controller, abilityIconParent, 4);
            List<Transform> icons = NewIcons(iconGroup4, 4);

            if (SaveManager.SaveFile.IsPart1)
            {
                icons[0].localPosition = new Vector3(-0.375f, yPosition, 0f);
                icons[1].localPosition = new Vector3(-0.1875f, yPosition, 0f);
                icons[2].localPosition = new Vector3(0f, yPosition, 0f);
                icons[3].localPosition = new Vector3(0.1875f, yPosition, 0f);
            }
            else
            {
                icons[0].localPosition = new Vector3(-0.42f, yPositionPart3, 0f);
                icons[1].localPosition = new Vector3(-0.14f, yPositionPart3, 0f);
                icons[2].localPosition = new Vector3(0.14f, yPositionPart3, 0f);
                icons[3].localPosition = new Vector3(0.42f, yPositionPart3, 0f);
            }
        }
        private static void AddQuintupleIconSlotToCard(StatusEffectAbilityIcons controller, Transform abilityIconParent)
        {
            GameObject iconGroup4 = NewIconGroup(controller, abilityIconParent, 5);
            List<Transform> icons = NewIcons(iconGroup4, 5);

            if (SaveManager.SaveFile.IsPart1)
            {
                icons[0].localPosition = new Vector3(-0.375f, yPosition, 0f);
                icons[1].localPosition = new Vector3(-0.1875f, yPosition, 0f);
                icons[2].localPosition = new Vector3(0f, yPosition, 0f);
                icons[3].localPosition = new Vector3(0.1875f, yPosition, 0f);
                icons[4].localPosition = new Vector3(0.375f, yPosition, 0f);
            }
            else
            {
                icons[0].localPosition = new Vector3(-0.42f, yPositionPart3, 0f);
                icons[1].localPosition = new Vector3(-0.14f, yPositionPart3, 0f);
                icons[2].localPosition = new Vector3(0.14f, yPositionPart3, 0f);
                icons[3].localPosition = new Vector3(0.42f, yPositionPart3, 0f);
                icons[4].localPosition = new Vector3(0.5f, yPositionPart3, 0f);
            }
        }
        #endregion

        private static void AddPixelEffectIconSlotsToCard(Transform pixelAbilityIconParent)
        {
            if (pixelAbilityIconParent == null)
                return;

            PixelCardAbilityIcons controller = pixelAbilityIconParent.gameObject.GetComponent<PixelCardAbilityIcons>();
            if (controller == null)
                return;

            // create the ability icon groups if they don't exist
            if (pixelAbilityIconParent.Find("AbilityIcons_1") == null)
                AddSinglePixelIcon(controller, pixelAbilityIconParent);

            if (pixelAbilityIconParent.Find("AbilityIcons_2") == null)
                AddDoublePixelIcon(controller, pixelAbilityIconParent);

            if (pixelAbilityIconParent.Find("AbilityIcons_3") == null)
                AddTriplePixelicon(controller, pixelAbilityIconParent);

            if (pixelAbilityIconParent.Find("AbilityIcons_4") == null)
                AddQuadruplePixelIcon(controller, pixelAbilityIconParent);
        }

        private static GameObject NewPixelIconGroup(PixelCardAbilityIcons controller, Transform parent, int newSlotNum)
        {
            GameObject prevIconGroup = parent.Find($"AbilityIcons_{newSlotNum}").gameObject;

            GameObject newIconGroup = UnityEngine.Object.Instantiate(prevIconGroup, parent);
            newIconGroup.name = $"StatusEffectIcons_{newSlotNum}";

            controller.abilityIconGroups.Add(newIconGroup);

            return newIconGroup;
        }
        private static List<Transform> NewPixelIcons(GameObject newIconGroup, string newIconName, float scaleMult = 1f)
        {
            List<Transform> icons = new();
            foreach (Transform icon in newIconGroup.transform)
                icons.Add(icon);

            GameObject newIcon = UnityEngine.Object.Instantiate(icons[0].gameObject, newIconGroup.transform);
            newIcon.name = newIconName;
            icons.Add(newIcon.transform);

            Vector3 newScale = Vector3.one * scaleMult;
            foreach (Transform icon in icons)
            {
                icon.localScale = newScale;

                foreach (Transform subIcon in icon)
                    subIcon.localScale = Vector3.one * 0.5f;
            }

            return icons;
        }

        #region PixelIcons
        private static void AddSinglePixelIcon(PixelCardAbilityIcons controller, Transform pixelParent)
        {
            GameObject pixelIconGroup6 = NewPixelIconGroup(controller, pixelParent, 6);

            List<Transform> icons = NewPixelIcons(pixelIconGroup6, "Bottom Right", 0.4706f); // ~8 pixels

            icons[4].name = "Bottom Center";

            icons[3].localPosition = new Vector3(-0.13f, -0.04f, 0f);
            icons[4].localPosition = new Vector3(0f, -0.04f, 0f);
            icons[5].localPosition = new Vector3(0.12f, -0.04f, 0f);
        }
        private static void AddDoublePixelIcon(PixelCardAbilityIcons controller, Transform pixelParent)
        {
            GameObject pixelIconGroup5 = NewPixelIconGroup(controller, pixelParent, 5);

            List<Transform> icons = NewPixelIcons(pixelIconGroup5, "Bottom Right", 0.5294f); // ~9 pixels

            icons[0].name = "Center Left";
            icons[1].name = "Center Right";

            icons[0].localPosition = new Vector3(-0.08f, 0.04f, 0f);
            icons[1].localPosition = new Vector3(yPosition, 0.04f, 0f);
        }
        private static void AddTriplePixelicon(PixelCardAbilityIcons controller, Transform pixelParent)
        {
            GameObject pixelIconGroup3 = NewPixelIconGroup(controller, pixelParent, 3);

            List<Transform> icons = NewPixelIcons(pixelIconGroup3, "Right", 0.6471f); // ~11 pixels

            icons[0].name = "Left";
            icons[1].name = "Center";

            icons[0].localPosition = new Vector3(-0.13f, 0f, 0f);
            icons[1].localPosition = new Vector3(0f, 0f, 0f);
            icons[2].localPosition = new Vector3(0.12f, 0f, 0f);
        }
        private static void AddQuadruplePixelIcon(PixelCardAbilityIcons controller, Transform pixelParent)
        {
            GameObject pixelIconGroup4 = NewPixelIconGroup(controller, pixelParent, 4);

            List<Transform> icons = NewPixelIcons(pixelIconGroup4, "Bottom Right", 0.5294f); // ~ 9 pixels

            icons[1].name = "Right";
            icons[2].name = "Bottom Left";

            icons[0].localPosition = new Vector3(-0.42f, yPosition, 0f);
            icons[1].localPosition = new Vector3(-0.14f, yPosition, 0f);
            icons[2].localPosition = new Vector3(0.14f, yPosition, 0f);
            icons[3].localPosition = new Vector3(0.42f, yPosition, 0f);
        }
        #endregion
    }
}