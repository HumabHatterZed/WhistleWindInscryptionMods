using DiskCardGame;
using GBC;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils.Core {
    [HarmonyPatch]
    public static partial class StatusEffectPatches // Adds extra icon slots for rendering status effects
    {
        private const string SEEMORE = "SeeMore";

        // keep z scale at 1 to not mess with icon interactiveness
        private static readonly Vector3 LocalScaleBase3D = new(0.2f, 0.2f, 1f);

        private static readonly Vector3 LocalScaleBase3DAct1 = new(0.15f, 0.10f, 1f);

        private static readonly Vector3 LocalScaleBase3DGiant = new(0.18f, 0.12f, 1f);

        private static Vector3 GetLocalScale3D(bool giantCard) {
            if (giantCard) {
                return LocalScaleBase3DGiant;
            }
            if (SaveManager.SaveFile.IsPart1) {
                return LocalScaleBase3DAct1;
            }
            return LocalScaleBase3D;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CardAbilityIcons), nameof(CardAbilityIcons.UpdateAbilityIcons))]
        public static void UpdateStatusEffects(CardAbilityIcons __instance, CardInfo info, PlayableCard playableCard) {
            if (__instance == null)
                return;

            StatusEffectIconsManager controller = __instance.GetComponent<StatusEffectIconsManager>();
            if (controller == null) {
                controller = __instance.gameObject.AddComponent<StatusEffectIconsManager>();
                controller.statusEffectMat = __instance.emissiveIconMat ?? __instance.defaultIconMat;

                if (__instance.transform.Find("StatusEffectIcons_1") == null) {
                    AddStatusIconsToCard(controller, __instance.transform, __instance.defaultIconGroups, info);
                }
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

            for (int i = 0; i < componentsInChildren.Length; i++) {
                AbilityIconInteractable icon = componentsInChildren[i];
                icon.gameObject.SetActive(true);
                icon.SetMaterial(new(__instance.defaultIconMat) {
                    color = !SaveManager.SaveFile.IsGrimora ? AbilitiesUtil.GetInfo(distinct[i]).colorOverride : Color.grey, // SetColour doesn't work for some reason???
                });

                icon.AssignAbility(distinct[i], playableCard.Info, playableCard);

                __instance.abilityIcons.Add(icon);
                controller.abilityIcons.Add(icon);
            }
        }

        public static List<Ability> GetDistinctStatusEffects(PlayableCard card) {
            if (card == null)
                return null;

            List<Ability> abilities = card.GetDisplayedStatusEffects(false);
            card.TemporaryMods.RemoveAll(x => x.abilities.Contains(SeeMore.ID));

            if (abilities.Count < 6) {
                card.TriggerHandler.RemoveAbility(SeeMore.ID);
                if (abilities.Count == 0)
                    return null;
            }

            abilities.Sort((a, b) => Mathf.Abs(AbilitiesUtil.GetInfo(b).powerLevel) - Mathf.Abs(AbilitiesUtil.GetInfo(a).powerLevel));
            if (abilities.Count > 5) {
                if (!card.TriggerHandler.triggeredAbilities.Exists(x => x.Item1 == SeeMore.ID)) {
                    card.TriggerHandler.AddAbility(SeeMore.ID);
                }
                SeeMore behav = card.transform.GetComponent<SeeMore>();

                if (!behav.switchingPages) // update all pages if not switching pages
                {
                    int newPage = 0;
                    behav.AllPages.Clear();
                    for (int i = 0; i < abilities.Count; i++) {
                        if (!behav.AllPages.ContainsKey(newPage)) {
                            behav.AllPages.Add(newPage, new());
                        }

                        behav.AllPages[newPage].Add(abilities[i]);
                        if (behav.AllPages[newPage].Count == 4) {
                            newPage++;
                        }
                    }

                    if (behav.currentPage >= behav.AllPages.Count)
                        behav.currentPage = 0;
                }

                behav.switchingPages = false;
                abilities = new(behav.AllPages[behav.currentPage])
                {
                    SeeMore.ID
                };

                card.TemporaryMods.Add(new(SeeMore.ID) { singletonId = SEEMORE });
            }

            //Debug.Log($"First shown: {AbilitiesUtil.GetInfo(abilities[0]).rulebookName}");
            return abilities;
        }

        private static void AddStatusIconsToCard(StatusEffectIconsManager controller, Transform abilityIconParent, List<GameObject> defaultIconGroups, CardInfo info) {
            for (int i = 0; i < Mathf.Min(5, defaultIconGroups.Count); i++) {
                GameObject iconGroup = NewIconGroup(controller, abilityIconParent, i + 1);
                List<Transform> icons = NewIcons(iconGroup, i + 1);

                for (int j = 0; j < icons.Count; j++) {
                    Renderer iconRenderer = icons[j].GetComponent<Renderer>();
                    iconRenderer.gameObject.layer = 14; // CardOffscreenEmission layer
                    iconRenderer.sortingOrder = 15;

                    AbilityIconInteractable interactable = icons[j].GetComponent<AbilityIconInteractable>();
                    GameObject back = GameObject.Instantiate(abilityIconParent.Find("CardMergeIcon_1/Back").gameObject, icons[j]);
                    back.name = "Back";
                    back.transform.localScale = new(1.5f, 1.5f, 1f);

                    GameObject.Destroy(back.GetComponent<AbilityIconInteractable>());
                    GameObject.Destroy(back.GetComponent<BoxCollider>());

                    SetSortingOrder(back, 13);

                    icons[j].transform.localScale = GetLocalScale3D(info.HasTrait(Trait.Giant));

                    if (SaveManager.SaveFile.IsPart1) {
                        Renderer backRenderer = back.GetComponent<Renderer>();
                        backRenderer.material.mainTexture = StatusEffectManager.StatusEffectPatch;
                        backRenderer.sortingLayerID = iconRenderer.sortingLayerID;
                        backRenderer.sortingOrder = 13;

                        // only for regular giant cards
                        if (info.HasTrait(Trait.Giant) && !info.HasSpecialAbility(MiniGiantCard.Id)) {
                            // since the actual card object's scale is all screwed up,
                            // we need to have two separate cases for the render icon and the (actual) interactable icon
                            if (icons[j].gameObject.GetComponentInParent<CardDisplayer3D>() != null) {
                                icons[j].localPosition = new(-0.9f, 0.175f + 0.1375f * j, -0.1f);
                            }
                            else {
                                // actual icon values
                                icons[j].localPosition = new(-0.41f, 0.175f + 0.1375f * j, -0.1f);
                                icons[j].localScale = new(0.07f, 0.09f, 1f);
                            }
                        }
                        else {
                            
                            icons[j].localPosition = new(-0.375f + 0.1875f * j, 0.2f, -0.1f);
                            
                        }

                        GameObject backMask = back.transform.GetChild(0).gameObject;
                        Renderer backMaskRenderer = backMask.GetComponent<Renderer>();

                        backMaskRenderer.sortingLayerID = iconRenderer.sortingLayerID;
                        backMaskRenderer.sortingOrder = 14;
                        SetSortingOrder(backMask, backMaskRenderer.sortingOrder);
                    }
                    else if (SaveManager.SaveFile.IsGrimora) {
                        icons[j].localPosition = new(-0.475f + 0.1875f * j, 0.3f, -0.1f);

                        /*GameObject back = new("Back", typeof(MeshRenderer));
                        back.transform.SetParent(icons[j].transform);
                        back.transform.localPosition = Vector3.zero;
                        back.layer = icons[j].gameObject.layer;
                        MeshRenderer backRenderer = back.GetComponent<MeshRenderer>();
                        backRenderer.material.mainTexture = StatusEffectManager.StatusEffectPatch;
                        backRenderer.sortingGroupID = iconRenderer.sortingGroupID;
                        backRenderer.sortingOrder = iconRenderer.sortingOrder;*/
                    }
                    else {
                        icons[j].localPosition = new(-0.5f, 1f - 0.15f * j, 0f);
                    }

                    interactable.OriginalLocalPosition = icons[j].localPosition;
                    SetSortingOrder(iconRenderer.gameObject, iconRenderer.sortingOrder);
                }
            }
        }

        private static GameObject NewIconGroup(StatusEffectIconsManager controller, Transform parent, int newSlotNum) {
            GameObject prevIconGroup = parent.Find($"DefaultIcons_{newSlotNum}Abilit{(newSlotNum == 1 ? "y" : "ies")}").gameObject;
            GameObject newIconGroup = UnityEngine.Object.Instantiate(prevIconGroup, parent);
            newIconGroup.name = $"StatusEffectIcons_{newSlotNum}";
            newIconGroup.transform.localScale = Vector3.one;
            controller.statusEffectIconGroups.Add(newIconGroup);
            return newIconGroup;
        }
        private static List<Transform> NewIcons(GameObject newIconGroup, int slotNum) {
            List<Transform> icons = new();

            if (slotNum == 1) {
                icons.Add(newIconGroup.transform);
            }
            else {
                newIconGroup.transform.localPosition = Vector3.zero;
                foreach (Transform icon in newIconGroup.transform) {
                    icon.name = "StatusEffectIcon";
                    icons.Add(icon);
                }
            }

            return icons;
        }

        private static void SetSortingOrder(GameObject obj, int sortingOrder) {
            SetSortingLayer layer = obj.GetComponent<SetSortingLayer>();
            if (layer != null) {
                layer.sortingOrder = sortingOrder;
            }
        }

        public static void ModifySpawnedCardStatusEffects(PlayableCard playableCard, CardInfo cardInfo, params Ability[] exclude) {
            List<Ability> abilities = playableCard.Info.Abilities;
            foreach (CardModificationInfo temporaryMod in playableCard.TemporaryMods) {
                abilities.AddRange(temporaryMod.abilities);
            }
            foreach (Ability ab in exclude) {
                abilities.RemoveAll(x => x == ab);
            }
            abilities.RemoveAll(x => playableCard.HasStatusEffect(x) && !playableCard.GetStatusEffect(x).EffectCanBeInherited);
            abilities.RemoveAll(x => AbilitiesUtil.GetInfo(x).GetExtendedPropertyAsBool("Uninheritable") == true);
            if (abilities.Count > 4) {
                abilities.RemoveRange(3, abilities.Count - 4);
            }
            CardModificationInfo cardModificationInfo = new CardModificationInfo();
            cardModificationInfo.fromCardMerge = true;
            cardModificationInfo.abilities = abilities;
            cardInfo.Mods.Add(cardModificationInfo);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(CreateCardsAdjacent), nameof(CreateCardsAdjacent.ModifySpawnedCard))]
        private static bool ModifyInheritedEffects(CreateCardsAdjacent __instance, CardInfo card) {
            ModifySpawnedCardStatusEffects(__instance.Card, card, __instance.Ability);
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CardAbilityIcons), nameof(CardAbilityIcons.GetDistinctShownAbilities))]
        [HarmonyPatch(typeof(InscryptionCommunityPatch.Card.TempModPixelSigilsFix), nameof(InscryptionCommunityPatch.Card.TempModPixelSigilsFix.RenderTemporarySigils))]
        private static void StatusEffectsDontRenderNormally(List<Ability> __result) {
            __result.RemoveAll(x => AbilitiesUtil.GetInfo(x).IsStatusEffect());
            __result.Remove(SeeMore.ID);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CardInfo), nameof(CardInfo.SpecialAbilities), MethodType.Getter)]
        private static void StatusEffectsArentNormalSpecialAbilities(List<SpecialTriggeredAbility> __result) {
            __result.RemoveAll(x => StatusEffectManager.AllStatusEffects.EffectByID(x) != null);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(RuleBookController), nameof(RuleBookController.OpenToAbilityPage))]
        private static bool FixOpenToStatusEffectsPage(ref string abilityName, PlayableCard card) {
            if (int.TryParse(abilityName, out int ability) && StatusEffectManager.AllStatusEffects.EffectByIcon((Ability)ability) != null) {
                abilityName = "[API_Status Effects]" + abilityName;
            }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(RuleBookController), nameof(RuleBookController.OffsetViewForCard))]
        private static bool DontChangeViewForTopStatuses(ref bool __result, PlayableCard card) {
            if (card != null && card.HasTrait(Trait.Giant)) {
                __result = false;
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch]
    internal class PixelStatusEffectPatches {
        private static void AddPixelStatusIcons(PixelStatusEffectAbilityIcons controller, Transform pixelParent) {
            for (int i = 0; i < 5; i++) {
                GameObject pixelIconGroup = NewPixelIconGroup(controller, pixelParent, i + 1);
                List<Transform> icons = NewPixelIcons(pixelIconGroup, 0.5294f); // ~9 pixels

                for (int j = 0; j < icons.Count; j++) {
                    icons[j].localPosition = new(-0.2f + 0.1f * j, yPositionPixel, 0f);
                }
            }
        }

        private static GameObject NewPixelIconGroup(PixelStatusEffectAbilityIcons controller, Transform parent, int newSlotNum) {
            GameObject prevIconGroup = parent.Find($"AbilityIcons_{newSlotNum}").gameObject;
            GameObject newIconGroup = UnityEngine.Object.Instantiate(prevIconGroup, parent);
            newIconGroup.name = $"StatusIcons_{newSlotNum}";
            controller.statusEffectIcons.Add(newIconGroup);
            return newIconGroup;
        }
        private static List<Transform> NewPixelIcons(GameObject newIconGroup, float scaleMult = 1f) {
            List<Transform> icons = new();
            foreach (Transform icon in newIconGroup.transform)
                icons.Add(icon);

            Vector3 newScale = Vector3.one * scaleMult;
            foreach (Transform icon in icons) {
                icon.localScale = newScale;
                foreach (Transform subIcon in icon)
                    subIcon.localScale = Vector3.one * 0.5f;
            }

            return icons;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(PixelCardAbilityIcons), nameof(PixelCardAbilityIcons.DisplayAbilities),
            new Type[] { typeof(CardRenderInfo), typeof(PlayableCard) })]
        private static void AddPixelIconsToCard(PixelCardAbilityIcons __instance) {
            if (__instance == null)
                return;

            PixelStatusEffectAbilityIcons controller = __instance.GetComponent<PixelStatusEffectAbilityIcons>();
            if (controller == null) {
                controller = __instance.gameObject.AddComponent<PixelStatusEffectAbilityIcons>();
                // create the ability icon groups if they don't exist
                if (__instance.transform.Find("StatusIcons_1") == null)
                    AddPixelStatusIcons(controller, __instance.transform);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PixelCardAbilityIcons), nameof(PixelCardAbilityIcons.DisplayAbilities),
            new Type[] { typeof(CardRenderInfo), typeof(PlayableCard) })]
        private static void FlipStatusEffects(PixelCardAbilityIcons __instance, CardRenderInfo renderInfo, PlayableCard card) {
            List<Ability> distinct = StatusEffectPatches.GetDistinctStatusEffects(card);
            if (distinct == null)
                return;

            PixelStatusEffectAbilityIcons controller = __instance.GetComponent<PixelStatusEffectAbilityIcons>();
            SpriteRenderer[] componentsInChildren = controller.statusEffectIcons[distinct.Count - 1].GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < componentsInChildren.Length; i++) {
                componentsInChildren[i].flipX = renderInfo.flippedAbilityIcons.Contains(distinct[i]);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PixelCardAbilityIcons), nameof(PixelCardAbilityIcons.DisplayAbilities),
            new Type[] { typeof(List<Ability>), typeof(PlayableCard) })]
        private static void RenderPixelStatusEffects(PixelCardAbilityIcons __instance, PlayableCard card) {
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
            for (int i = 0; i < componentsInChildren.Length; i++) {
                AbilityInfo info = AbilitiesUtil.GetInfo(distinct[i]);
                componentsInChildren[i].sprite = info.pixelIcon;
                if (info.flipYIfOpponent && card != null && card.OpponentCard) {
                    if (info.customFlippedPixelIcon)
                        componentsInChildren[i].sprite = info.customFlippedPixelIcon;
                    else
                        componentsInChildren[i].flipY = true;
                }
                else {
                    componentsInChildren[i].flipY = false;
                }
            }
        }

        private const float yPositionPixel = -0.15f;
    }
}