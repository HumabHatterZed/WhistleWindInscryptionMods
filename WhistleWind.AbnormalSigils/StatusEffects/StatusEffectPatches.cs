using DiskCardGame;
using GBC;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using static InscryptionAPI.Slots.SlotModificationManager;

namespace WhistleWind.AbnormalSigils.Core
{
    [HarmonyPatch]
    internal class StatusEffectPatches // Adds extra icon slots for rendering status effects
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CardAbilityIcons), nameof(CardAbilityIcons.GetDistinctShownAbilities))]
        [HarmonyPatch(typeof(InscryptionCommunityPatch.Card.TempModPixelSigilsFix), nameof(InscryptionCommunityPatch.Card.TempModPixelSigilsFix.RenderTemporarySigils))]
        private static void StatusEffectsDontRenderNormally(List<Ability> __result)
        {
            __result.RemoveAll(x => AbilitiesUtil.GetInfo(x).IsStatusEffect());
            __result.Remove(SeeMore.ability);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CardInfo), nameof(CardInfo.SpecialAbilities), MethodType.Getter)]
        private static void StatusEffectsArentNormalSpecialAbilities(List<SpecialTriggeredAbility> __result)
        {
            __result.RemoveAll(x => StatusEffectManager.AllStatusEffects.EffectByID(x) != null);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CardAbilityIcons), nameof(CardAbilityIcons.UpdateAbilityIcons))]
        public static void UpdateStatusEffects(CardAbilityIcons __instance, PlayableCard playableCard)
        {
            if (__instance == null)
                return;

            StatusEffectIconsManager controller = __instance.GetComponent<StatusEffectIconsManager>();
            if (controller == null)
            {
                controller = __instance.gameObject.AddComponent<StatusEffectIconsManager>();
                controller.statusEffectMat = __instance.emissiveIconMat ?? __instance.defaultIconMat;
                if (__instance.transform.Find("StatusEffectIcons_1") == null)
                    AddStatusIconsToCard(controller, __instance.transform);
            }
            __instance.abilityIcons.RemoveAll(controller.abilityIcons.Contains);
            controller.abilityIcons.Clear();

            foreach (GameObject defaultIconGroup in controller.statusEffectIconGroups)
                defaultIconGroup.SetActive(false);

            List<Ability> distinct = GetDistinctStatusEffects(playableCard);
            if (distinct == null || controller.statusEffectIconGroups.Count < distinct.Count)
                return;

            GameObject group = controller.statusEffectIconGroups[distinct.Count - 1];
            AbilityIconInteractable[] componentsInChildren = group.GetComponentsInChildren<AbilityIconInteractable>();
            group.SetActive(true);

            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                AbilityIconInteractable icon = componentsInChildren[i];
                icon.gameObject.SetActive(true);
                icon.SetMaterial(new(__instance.defaultIconMat)
                {
                    color = AbilitiesUtil.GetInfo(distinct[i]).colorOverride, // SetColour doesn't work for some reason???
                });

                icon.AssignAbility(distinct[i], playableCard.Info, playableCard);

                __instance.abilityIcons.Add(icon);
                controller.abilityIcons.Add(icon);
            }
        }
        
        public static List<Ability> GetDistinctStatusEffects(PlayableCard card)
        {
            if (card == null)
                return null;

            List<Ability> abilities = card.GetDisplayedStatusEffects(false);
            card.TemporaryMods.RemoveAll(x => x.abilities.Contains(SeeMore.ability));

            if (abilities.Count < 6)
            {
                card.TriggerHandler.RemoveAbility(SeeMore.ability);
                if (abilities.Count == 0)
                    return null;
            }

            abilities.Sort((a, b) => Mathf.Abs(AbilitiesUtil.GetInfo(b).powerLevel) - Mathf.Abs(AbilitiesUtil.GetInfo(a).powerLevel));
            if (abilities.Count > 5)
            {
                if (!card.TriggerHandler.triggeredAbilities.Exists(x => x.Item1 == SeeMore.ability))
                {
                    card.TriggerHandler.AddAbility(SeeMore.ability);
                }
                SeeMore behav = card.transform.GetComponent<SeeMore>();

                if (!behav.switchingPages) // update all pages if not switching pages
                {
                    int newPage = 0;
                    behav.AllPages.Clear();
                    for (int i = 0; i < abilities.Count; i++)
                    {
                        if (!behav.AllPages.ContainsKey(newPage))
                        {
                            behav.AllPages.Add(newPage, new());
                        }
                        
                        behav.AllPages[newPage].Add(abilities[i]);
                        if (behav.AllPages[newPage].Count == 4)
                        {
                            newPage++;
                        }
                    }

                    if (behav.currentPage >= behav.AllPages.Count)
                        behav.currentPage = 0;
                }

                behav.switchingPages = false;
                abilities = new(behav.AllPages[behav.currentPage])
                {
                    SeeMore.ability
                };

                card.TemporaryMods.Add(new(SeeMore.ability) { singletonId = SEEMORE });
            }

            //Debug.Log($"First shown: {AbilitiesUtil.GetInfo(abilities[0]).rulebookName}");
            return abilities;
        }

        private const string SEEMORE = "SeeMore";

        private static void AddStatusIconsToCard(StatusEffectIconsManager controller, Transform abilityIconParent)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject iconGroup = NewIconGroup(controller, abilityIconParent, i + 1);
                List<Transform> icons = NewIcons(iconGroup, i + 1);
                for (int j = 0; j < icons.Count; j++)
                {
                    AbilityIconInteractable interactable = icons[j].GetComponent<AbilityIconInteractable>();
                    icons[j].transform.localScale = LocalScaleBase3D;

                    if (SaveManager.SaveFile.IsPart1)
                    {
                        icons[j].localPosition = new(-0.375f + 0.1875f * j, yPositionPart1, 0f);

                        GameObject back = GameObject.Instantiate(abilityIconParent.Find("CardMergeIcon_1/Back").gameObject, icons[j]);
                        back.name = "Back";
                        back.transform.localScale = new(1.5f, 1.5f, 1f);

                        Renderer rend1 = icons[j].transform.GetComponent<Renderer>();
                        Renderer rend = back.transform.GetComponent<Renderer>();
                        rend.material.mainTexture = StatusEffectManager.StatusEffectPatch;

                        rend.sortingLayerID = rend1.sortingLayerID;
                        rend.sortingOrder = rend1.sortingOrder;
                        
                        GameObject.Destroy(back.transform.GetComponent<AbilityIconInteractable>());
                        GameObject.Destroy(back.transform.GetComponent<BoxCollider>());
                    }
                    else
                    {
                        icons[j].localPosition = new(-0.5f + 0.1f * j, yPositionPart3, 0f);
                    }

                    
                    interactable.OriginalLocalPosition = icons[j].localPosition;
                }
            }
        }
        private static GameObject NewIconGroup(StatusEffectIconsManager controller, Transform parent, int newSlotNum)
        {
            GameObject prevIconGroup = parent.Find($"DefaultIcons_{newSlotNum}Abilit{(newSlotNum == 1 ? "y" : "ies")}").gameObject;
            GameObject newIconGroup = UnityEngine.Object.Instantiate(prevIconGroup, parent);
            newIconGroup.name = $"StatusEffectIcons_{newSlotNum}";
            controller.statusEffectIconGroups.Add(newIconGroup);
            return newIconGroup;
        }
        private static List<Transform> NewIcons(GameObject newIconGroup, int slotNum)
        {
            List<Transform> icons = new();

            if (slotNum == 1)
            {
                icons.Add(newIconGroup.transform);
            }
            else
            {
                foreach (Transform icon in newIconGroup.transform)
                {
                    icon.name = "StatusEffectIcon";
                    icons.Add(icon);
                }
            }

            return icons;
        }

        // keep z scale at 1 to not mess with icon interactiveness
        private static readonly Vector3 LocalScaleBase3D = new(0.15f, 0.10f, 1f);
        private const float yPositionPart1 = 0.20f;
        private const float yPositionPart3 = 1f;

        [HarmonyPrefix, HarmonyPatch(typeof(CreateCardsAdjacent), nameof(CreateCardsAdjacent.ModifySpawnedCard))]
        private static bool ModifyInheritedEffects(CreateCardsAdjacent __instance, CardInfo card)
        {
            List<Ability> abilities = __instance.Card.AllAbilities();
            abilities.RemoveAll(x => x == __instance.Ability);
            abilities.RemoveAll(x => __instance.Card.HasStatusEffect(x) && !__instance.Card.GetStatusEffect(x).EffectCanBeInherited);
            if (abilities.Count > 4)
            {
                abilities.RemoveRange(3, abilities.Count - 4);
            }
            CardModificationInfo cardModificationInfo = new()
            {
                fromCardMerge = true,
                abilities = abilities
            };
            card.Mods.Add(cardModificationInfo);
            return false;
        }
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