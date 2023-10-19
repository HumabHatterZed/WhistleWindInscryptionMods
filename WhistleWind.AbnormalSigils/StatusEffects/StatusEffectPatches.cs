using DiskCardGame;
using GBC;
using GracesGames.Common.Scripts;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils.Core
{
    [HarmonyPatch(typeof(CardAbilityIcons))]
    internal class StatusEffectPatches // Adds extra icon slots for rendering status effects
    {
        [HarmonyPostfix, HarmonyPatch(nameof(CardAbilityIcons.GetDistinctShownAbilities))]
        [HarmonyPatch(typeof(InscryptionCommunityPatch.Card.TempModPixelSigilsFix), nameof(InscryptionCommunityPatch.Card.TempModPixelSigilsFix.RenderTemporarySigils))]
        private static void DontRenderStatusEffectsNormally(ref List<Ability> __result)
        {
            __result.RemoveAll(x => x.GetExtendedPropertyAsBool("wstl:StatusEffect") == true);
        }

        [HarmonyPrefix, HarmonyPatch(nameof(CardAbilityIcons.UpdateAbilityIcons))]
        private static void AddStatusEffectIcons(CardAbilityIcons __instance)
        {
            if (!__instance || SaveManager.SaveFile.IsGrimora || SaveManager.SaveFile.IsMagnificus)
                return;

            if (__instance && !SaveManager.SaveFile.IsPart2)
            {
                StatusEffectAbilityIcons component = __instance.GetComponent<StatusEffectAbilityIcons>();
                if (component == null)
                {
                    component = __instance.gameObject.AddComponent<StatusEffectAbilityIcons>();
                    component.statusEffectMat = new(__instance.emissiveIconMat ?? __instance.defaultIconMat);

                    // create the ability icon groups if they don't exist
                    if (__instance.transform.Find("StatusEffectIcon_1") == null)
                        AddStatusIconsToCard(component, __instance.transform);
                }
            }
        }

        [HarmonyPostfix, HarmonyPatch(nameof(CardAbilityIcons.UpdateAbilityIcons))]
        private static void UpdateStatusEffects(CardAbilityIcons __instance, PlayableCard playableCard)
        {
            if (!__instance || SaveManager.SaveFile.IsGrimora || SaveManager.SaveFile.IsMagnificus)
                return;

            List<Ability> distinct = GetDistinctStatusEffects(playableCard);
            StatusEffectAbilityIcons controller = __instance.GetComponent<StatusEffectAbilityIcons>();
            controller.abilityIcons.Clear();

            if (SaveManager.SaveFile.IsPart1)
            {
                foreach (AbilityIconInteractable icon in controller.part1AbilityIcons)
                    icon.gameObject.SetActive(false);

                if (playableCard?.Info.GetExtendedPropertyAsBool("StatusGlow") ?? false)
                {
                    playableCard.Info.SetExtendedProperty("StatusGlow", true);
                    playableCard.RenderInfo.forceEmissivePortrait = false;
                }

                if (distinct == null || controller.part1AbilityIcons.Count < distinct.Count)
                    return;

                for (int i = 0; i < controller.part1AbilityIcons.Count; i++)
                {
                    if (i < distinct.Count)
                    {
                        if (!CardDisplayer3D.EmissionEnabledForCard(playableCard.RenderInfo, playableCard))
                        {
                            playableCard.Info.SetExtendedProperty("StatusGlow", true);
                            playableCard.RenderInfo.forceEmissivePortrait = true;
                        }

                        Material mat = new(controller.statusEffectMat)
                        {
                            color = StatusEffectManager.AllIconColours[distinct[i]]
                        };

                        controller.part1AbilityIcons[i].gameObject.SetActive(value: true);
                        controller.part1AbilityIcons[i].SetMaterial(mat);
                        controller.part1AbilityIcons[i].AssignAbility(distinct[i], playableCard.Info, playableCard);
                        controller.abilityIcons.Add(controller.part1AbilityIcons[i]);
                        __instance.abilityIcons.Add(controller.part1AbilityIcons[i]);
                    }
                }
            }   
            else
            {
                foreach (GameObject defaultIconGroup in controller.statusEffectIconGroups)
                    defaultIconGroup.SetActive(false);

                if (distinct == null || controller.statusEffectIconGroups.Count < distinct.Count)
                    return;

                GameObject group = controller.statusEffectIconGroups[distinct.Count - 1];
                group.SetActive(true);
                AbilityIconInteractable[] componentsInChildren = group.GetComponentsInChildren<AbilityIconInteractable>();

                for (int i = 0; i < componentsInChildren.Length; i++)
                {
                    Material mat = new(controller.statusEffectMat)
                    {
                        color = StatusEffectManager.AllIconColours[distinct[i]]
                    };
                    componentsInChildren[i].gameObject.SetActive(true);
                    componentsInChildren[i].SetMaterial(mat);
                    componentsInChildren[i].AssignAbility(distinct[i], playableCard.Info, playableCard);
                    controller.abilityIcons.Add(componentsInChildren[i]);
                    __instance.abilityIcons.Add(componentsInChildren[i]);
                }
            }
        }
        private static Texture2D StatusBackground(Texture2D abilityTexture, Color color)
        {
            Texture2D emptyCardback = TextureLoader.LoadTextureFromFile("statusBackground_dark.png", Assembly.GetExecutingAssembly());

            int startX = (emptyCardback.width - 49) / 2;
            int startY = emptyCardback.height - 49 + 20;

            for (int x = startX; x < emptyCardback.width; x++)
            {
                for (int y = startY; y < emptyCardback.height; y++)
                {
                    Color bgColor = emptyCardback.GetPixel(x, y);
                    Color wmColor = color;
                    wmColor.a = abilityTexture.GetPixel(x - startX, y - startY).a;

                    Color final_color = Color.Lerp(bgColor, wmColor, wmColor.a / 1.0f);

                    emptyCardback.SetPixel(x, y, final_color);
                }
            }

            emptyCardback.Apply();
            return emptyCardback;
        }

        public static List<Ability> GetDistinctStatusEffects(PlayableCard card)
        {
            if (card == null)
                return null;

            List<Ability> abilities = card.GetDisplayedStatusEffects(false);

            if (abilities.Count == 0)
                return null;

            // sort by absolute value of power level
            abilities.Sort((a, b) => Mathf.Abs(AbilitiesUtil.GetInfo(b).powerLevel) - Mathf.Abs(AbilitiesUtil.GetInfo(a).powerLevel));
            if (abilities.Count > 5)
            {
                abilities.RemoveRange(4, abilities.Count - 4);
                abilities.Add(SeeMore.ability);
            }
            return abilities;
        }
        private static void AddStatusIconsToCard(StatusEffectAbilityIcons controller, Transform abilityIconParent)
        {
            for (int i = 0; i < 5; i++)
            {
                if (SaveManager.SaveFile.IsPart1)
                {
                    GameObject refIcon = abilityIconParent.Find("CardMergeIcon_1").gameObject;
                    GameObject newIcon = UnityEngine.Object.Instantiate(refIcon, abilityIconParent);
                    newIcon.name = $"StatusEffectIcon_{i + 1}";
                    newIcon.transform.localPosition = new(-0.375f + 0.1875f * i, yPositionPart1, 0f);
                    newIcon.transform.localScale = LocalScaleBase3D;
                    var component = newIcon.GetComponent<AbilityIconInteractable>();
                    component.OriginalLocalPosition = newIcon.transform.localPosition;
                    controller.part1AbilityIcons.Add(component);
                    newIcon.SetActive(false);
                }
                else
                {
                    GameObject iconGroup = NewIconGroup(controller, abilityIconParent, i + 1);
                    List<Transform> icons = NewIcons(iconGroup, i + 1);
                    for (int j = 0; j < icons.Count; j++)
                    {
                        icons[j].localPosition = new(-0.5f + 0.1f * j, yPositionPart3, 0f);
                    }
                }
            }
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
            foreach (Transform icon in icons)
            {
                icon.name = "StatusEffectIcon";
                icon.localScale = LocalScaleBase3D;
            }

            return icons;
        }

        // keep z scale at 1 to not mess with icon interactiveness
        private static readonly Vector3 LocalScaleBase3D = new(0.20f, 0.15f, 1f);
        private const float yPositionPart1 = 0.22f;
        private const float yPositionPart3 = 1f;
    }

    [HarmonyPatch]
    internal class PixelStatusEffectPatches
    {
        private static void AddPixelStatusIcons(PixelStatusEffectAbilityIcons controller, Transform pixelParent)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject pixelIconGroup = NewPixelIconGroup(controller, pixelParent, i + 1);
                List<Transform> icons = NewPixelIcons(pixelIconGroup, 0.5294f); // ~9 pixels

                for (int j = 0; j < icons.Count; j++)
                {
                    icons[j].localPosition = new(-0.2f + 0.1f * j, yPositionPixel, 0f);
                }
            }
        }

        private static GameObject NewPixelIconGroup(PixelStatusEffectAbilityIcons controller, Transform parent, int newSlotNum)
        {
            GameObject prevIconGroup = parent.Find($"AbilityIcons_{newSlotNum}").gameObject;
            GameObject newIconGroup = UnityEngine.Object.Instantiate(prevIconGroup, parent);
            newIconGroup.name = $"StatusIcons_{newSlotNum}";
            controller.statusEffectIcons.Add(newIconGroup);
            return newIconGroup;
        }
        private static List<Transform> NewPixelIcons(GameObject newIconGroup, float scaleMult = 1f)
        {
            List<Transform> icons = new();
            foreach (Transform icon in newIconGroup.transform)
                icons.Add(icon);

            Vector3 newScale = Vector3.one * scaleMult;
            foreach (Transform icon in icons)
            {
                icon.localScale = newScale;
                foreach (Transform subIcon in icon)
                    subIcon.localScale = Vector3.one * 0.5f;
            }

            return icons;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(PixelCardAbilityIcons), nameof(PixelCardAbilityIcons.DisplayAbilities),
            new Type[] { typeof(CardRenderInfo), typeof(PlayableCard) })]
        private static void AddPixelIconsToCard(PixelCardAbilityIcons __instance)
        {
            if (__instance == null)
                return;

            PixelStatusEffectAbilityIcons controller = __instance.GetComponent<PixelStatusEffectAbilityIcons>();
            if (controller == null)
            {
                controller = __instance.gameObject.AddComponent<PixelStatusEffectAbilityIcons>();
                // create the ability icon groups if they don't exist
                if (__instance.transform.Find("StatusIcons_1") == null)
                    AddPixelStatusIcons(controller, __instance.transform);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PixelCardAbilityIcons), nameof(PixelCardAbilityIcons.DisplayAbilities),
            new Type[] { typeof(CardRenderInfo), typeof(PlayableCard) })]
        private static void FlipStatusEffects(PixelCardAbilityIcons __instance, CardRenderInfo renderInfo, PlayableCard card)
        {
            List<Ability> distinct = StatusEffectPatches.GetDistinctStatusEffects(card);
            if (distinct == null)
                return;

            PixelStatusEffectAbilityIcons controller = __instance.GetComponent<PixelStatusEffectAbilityIcons>();
            SpriteRenderer[] componentsInChildren = controller.statusEffectIcons[distinct.Count - 1].GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                componentsInChildren[i].flipX = renderInfo.flippedAbilityIcons.Contains(distinct[i]);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PixelCardAbilityIcons), nameof(PixelCardAbilityIcons.DisplayAbilities),
            new Type[] { typeof(List<Ability>), typeof(PlayableCard) })]
        private static void RenderPixelStatusEffects(PixelCardAbilityIcons __instance, PlayableCard card)
        {
            if (__instance == null || card == null)
                return;

            List<Ability> distinct = StatusEffectPatches.GetDistinctStatusEffects(card);
            PixelStatusEffectAbilityIcons controller = __instance.GetComponent<PixelStatusEffectAbilityIcons>();

            foreach (GameObject obj in controller.statusEffectIcons)
                obj.SetActive(false);

            if (distinct == null || controller.statusEffectIcons.Count < distinct.Count)
                return;

            GameObject group = controller.statusEffectIcons[distinct.Count - 1];
            group.gameObject.SetActive(true);
            SpriteRenderer[] componentsInChildren = group.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                AbilityInfo info = AbilitiesUtil.GetInfo(distinct[i]);
                componentsInChildren[i].sprite = info.pixelIcon;
                if (info.flipYIfOpponent && card != null && card.OpponentCard)
                {
                    if (info.customFlippedPixelIcon)
                        componentsInChildren[i].sprite = info.customFlippedPixelIcon;
                    else
                        componentsInChildren[i].flipY = true;
                }
                else
                {
                    componentsInChildren[i].flipY = false;
                }
            }
        }

        private const float yPositionPixel = -0.15f;
    }
}