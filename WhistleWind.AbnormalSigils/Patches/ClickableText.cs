/*using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using Pixelplacement.TweenSystem;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UIElements;

namespace WhistleWind.AbnormalSigils.Patches
{
    public class PageTextInteractable : AlternateInputInteractable
    {
        public override CursorType CursorType => CursorType.Info;
        public BoxCollider Collider => base.coll as BoxCollider;

        public PageRangeType redirectPageType;
        public string redirectPageId;
        public string redirectText;
        public void SetRedirect(string textToLink, PageRangeType rangeType)
        {
            redirectText = textToLink;
            redirectPageType = rangeType;

            redirectPageId = redirectPageType switch
            {
                PageRangeType.Abilities => AbilityManager.AllAbilityInfos.Find(x => x.rulebookName == textToLink).ability.ToString(),
                PageRangeType.StatIcons => StatIconManager.AllStatIconInfos.Find(x => x.rulebookName == textToLink).iconType.ToString(),
                PageRangeType.Boons => BoonsUtil.AllData.Find(x => x.type.ToString() == textToLink).ToString(),
                PageRangeType.Items => ItemsUtil.AllData.Find(x => x.name == textToLink).name,
                _ => textToLink,
            };
        }

        public override void OnAlternateSelectStarted()
        {
            Debug.Log($"AlternateSelectStarted {redirectPageType} {redirectPageId}");
            switch (redirectPageType)
            {
                case PageRangeType.Abilities:
                    Singleton<RuleBookController>.Instance.OpenToAbilityPage(redirectPageId, null);
                    break;
                case PageRangeType.StatIcons:
                    Singleton<RuleBookController>.Instance.OpenToStatIconPage(redirectPageId, null);
                    break;
                case PageRangeType.Boons:
                    Singleton<RuleBookController>.Instance.OpenToBoonPage(redirectPageId);
                    break;
                default:
                    Singleton<RuleBookController>.Instance.OpenToItemPage(redirectPageId);
                    break;
            }
        }

*//*        public override void ManagedUpdate()
        {
            Bounds bounds = Collider.bounds;
            Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y, bounds.min.z), new Vector3(bounds.max.x, bounds.min.y, bounds.min.z), Color.red);
            Debug.DrawLine(new Vector3(bounds.max.x, bounds.min.y, bounds.min.z), new Vector3(bounds.max.x, bounds.max.y, bounds.min.z), Color.red);
            Debug.DrawLine(new Vector3(bounds.max.x, bounds.max.y, bounds.min.z), new Vector3(bounds.min.x, bounds.max.y, bounds.min.z), Color.red);
            Debug.DrawLine(new Vector3(bounds.min.x, bounds.max.y, bounds.min.z), new Vector3(bounds.min.x, bounds.min.y, bounds.min.z), Color.red);
            Debug.DrawLine(bounds.min, bounds.max, Color.red);
        }*//*
    }

    // FFFFFF - invisible - more white = more invisibile
    // FFFF00 - invisible - more yellow = more invisible
    // FF0000 & FF00FF - identical
    // A00000 - pretty good median colour
    // Multiply - 160 alpha
    [HarmonyPatch]
    public static class PageTextInteractablePatches
    {
        public static GameObject currentInteractableObject = null;
        public static PageTextInteractable currentTextInteractable = null;
        public static bool AddedInteractables = false;

        [HarmonyPostfix, HarmonyPatch(typeof(RuleBookController), nameof(RuleBookController.Start))]
        private static void AddPageInteractablesToRuleBookRig() => AddPageInteractables();

*//*        [HarmonyPostfix, HarmonyPatch(typeof(AbilityIconInteractable), nameof(AbilityIconInteractable.OnAlternateSelectStarted))]
        private static void Test(AbilityIconInteractable __instance)
        {
            BoxCollider collider = __instance.coll as BoxCollider;
            Debug.Log($"Coll: {collider?.size.x} {collider?.size.y} {collider?.size.z}");
            Debug.Log($"Rot: {__instance.transform.localRotation.x} {__instance.transform.localRotation.y} {__instance.transform.localRotation.z} {__instance.transform.localRotation.w}");
        }*//*

        [HarmonyPostfix, HarmonyPatch(typeof(PageFlipper), nameof(PageFlipper.Flip))]
        private static void UpdatePageInteractableRuleBook(PageFlipper __instance, bool forwards)
        {
            currentInteractableObject = __instance.transform.gameObject.FindChild("RuleBookPageInteractable");
            currentTextInteractable = currentInteractableObject.GetComponent<PageTextInteractable>();
            Debug.Log($"Current Obj {currentInteractableObject.transform.parent.parent.name}");
            bool makeActive = false;            
            currentInteractableObject.SetActive(false);
            RuleBookPageInfo info = Singleton<RuleBookController>.Instance.PageData[__instance.currentPageIndex];
            if (info.ability != 0)
            {
                string redirect = info.ability.GetExtendedProperty("RuleBook_Redirect");
                //Debug.Log($"Ability {AbilitiesUtil.GetInfo(info.ability).rulebookName} {redirect}");
                if (redirect != null) // if the page we're flipping to has a redirect
                {
                    makeActive = true;
                    currentTextInteractable.SetRedirect(
                        redirect, PageRangeType.Abilities
                        );
                }
*//*                else if (info.boon != 0)
                {
                    pageTextInteractable.SetRedirect(info.pageId, null, PageRangeType.Boons);
                }
                else if (StatIconManager.AllStatIconInfos.Exists(x => x.iconType.ToString() == info.pageId))
                {
                    pageTextInteractable.SetRedirect(info.pageId, null, PageRangeType.StatIcons);
                }
                else
                {
                    pageTextInteractable.SetRedirect(info.pageId, null, PageRangeType.Items);
                }*//*
            }

            if (makeActive)
            {
                UpdateColliderSize(__instance, forwards);
                currentInteractableObject.SetActive(true);
            }
        }

        private static void UpdateColliderSize(PageFlipper instance, bool forwards)
        {
            Debug.Log($"UpdateColliderSize");
            // grab the content loader for the top page
            // update the current interactable based on what the next top page is going to be
            PageContentLoader currentLoader = null;
            if (instance is RulebookPageFlipper ruleBook)
            {
                PageContentLoader loader1 = (ruleBook.topPage == ruleBook.page1) ? ruleBook.pageLoader1 : ruleBook.pageLoader2;
                PageContentLoader loader2 = (ruleBook.topPage == ruleBook.page1) ? ruleBook.pageLoader2 : ruleBook.pageLoader1;
                currentLoader = forwards ? loader1 : loader2;
            }
            else if (instance is TabletPageFlipper tablet)
            {
                currentLoader = tablet.pageLoader;
            }

            if (currentLoader == null)
                return;

            // get the description mesh from the currently displayed rulebook page
            // also get the rendering camera so we can position the whats-it
            RuleBookPage ruleBookPage = currentLoader.currentPageObj.GetComponent<RuleBookPage>();
            Camera currentCamera = currentLoader.GetComponentInParent<Camera>();
            TextMeshPro descriptionMesh = null;
            if (ruleBookPage is AbilityPage)
            {
                //Debug.Log($"AbilityPage");
                descriptionMesh = (ruleBookPage as AbilityPage).mainAbilityGroup.descriptionTextMesh;
            }
            else if (ruleBookPage is StatIconPage)
            {
                //Debug.Log($"StatIconPage");
                descriptionMesh = (ruleBookPage as StatIconPage).descriptionTextMesh;
            }
            else if (ruleBookPage is BoonPage)
            {
                //Debug.Log($"BoonPage");
                descriptionMesh = (ruleBookPage as BoonPage).descriptionTextMesh;
            }
            else if (ruleBookPage is ItemPage)
            {
                //Debug.Log($"ItemPage");
                descriptionMesh = (ruleBookPage as ItemPage).descriptionTextMesh;
            }
            if (descriptionMesh == null)
                return;

            // force update the mesh to populate the mesh with info we need
            descriptionMesh.ForceMeshUpdate();

            // since rulebook names can be multiple words long, we want to get the first character index of the first word
            // and the last character index of the last word
            // then we use those indeces to both determine the interactable's position and its x-length
            // names that wrap around the rulebook aren't accounted for, so piss
            int first = -1, last = -1;
            float length = 0;
            List<string> pageIds = new();
            TMP_WordInfo[] wordInfos = descriptionMesh.textInfo.wordInfo;
            string[] redirectTextWords = currentTextInteractable.redirectText.Split();
            
            for (int i = 0; i < wordInfos.Length; i++)
            {
                if (wordInfos[i].characterCount == 0)
                    continue;

                string word = wordInfos[i].GetWord();
                if (!redirectTextWords.Contains(word))
                    continue;
                Debug.Log($"Word: {word} {wordInfos[i].textComponent.textBounds.size.x} {wordInfos[i].textComponent.textBounds.size.y} | {wordInfos[i].textComponent.transform.localScale.x} {wordInfos[i].textComponent.transform.localScale.y}");
                pageIds.Add(word);
                length += word.Length;
                // use two ifs to account for one-word-long names
                if (pageIds.Count == 1)
                    first = wordInfos[i].firstCharacterIndex;
                else
                    length++;
                if (pageIds.Count == redirectTextWords.Length)
                {
                    last = wordInfos[i].lastCharacterIndex;
                    break;
                }
            }

            if (first == -1 || last == -1)
                return;

            TMP_CharacterInfo firstCharInfo = descriptionMesh.textInfo.characterInfo[first];
            TMP_CharacterInfo lastCharInfo = descriptionMesh.textInfo.characterInfo[last];
            float colliderSizeX = (lastCharInfo.origin - firstCharInfo.origin) / length;

            Debug.Log($"Size: {colliderSizeX} | {firstCharInfo.origin} {firstCharInfo.bottomLeft.x}");
            currentTextInteractable.Collider.size = new(colliderSizeX, 0.14f, 0.14f);

            Vector3 wordLocation;
            Vector3 targetTextureDim = new Vector3(currentCamera.targetTexture.width, currentCamera.targetTexture.height);
            //float ortho = currentCamera.orthographicSize * 2;
            //Vector2 camSize = new(currentCamera.scaledPixelHeight / ortho, currentCamera.scaledPixelWidth / ortho);

            RectTransform rectTransform = descriptionMesh.rectTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, firstCharInfo.bottomLeft,
                currentCamera, out Vector2 localPoint);

            // scale local point by size of mesh
            localPoint.x /= rectTransform.rect.width;
            localPoint.y /= rectTransform.rect.height;
            
            Ray viewportPoint = currentCamera.ViewportPointToRay(localPoint);

            // project ray onto plane
            Plane plane = new(Vector3.up, Vector3.zero);
            plane.Raycast(viewportPoint, out float d);

            //wordLocation = GetWordLocation(descriptionMesh, firstCharInfo.bottomLeft, currentCamera);
            //wordLocation = descriptionMesh.transform.InverseTransformPoint(firstCharInfo.topLeft);

            wordLocation = viewportPoint.GetPoint(d);
            Debug.Log($"Loc: {wordLocation.x} {wordLocation.y}");

            Vector3 worldPos = descriptionMesh.transform.TransformPoint(firstCharInfo.bottomLeft);
            Vector3 localPos = currentInteractableObject.transform.InverseTransformPoint(worldPos);
            Vector3 texturePos = localPos + currentTextInteractable.Collider.size / 2f;
            Vector3 rayPos = Vector3.Scale(texturePos, targetTextureDim);
            Ray rayRay = currentCamera.ScreenPointToRay(rayPos);
            Physics.Raycast(rayRay, out RaycastHit hit);
            Debug.Log($"Pos: {firstCharInfo.bottomLeft.x} : {worldPos.x} --> {localPos.x} --> {texturePos.x} --> {rayPos.x} {hit.point.x}");

            Vector3 screenPos = currentCamera.WorldToViewportPoint(worldPos);
            //Debug.Log($"Screen1: {screenPos.x}");
            screenPos.Scale(new Vector3(Screen.width, Screen.height, 1f));
            //Debug.Log($"Screen2: {screenPos.x}");

            Vector3 test = currentCamera.WorldToScreenPoint(firstCharInfo.bottomLeft);
            Vector3 test2 = currentInteractableObject.transform.InverseTransformPoint(test);
            Debug.Log($"Test: {test2.x}");

            Vector3 test3 = descriptionMesh.transform.InverseTransformPoint(firstCharInfo.bottomLeft);
            Vector3 test31 = currentCamera.WorldToScreenPoint(test3);
            Vector3 test32 = currentInteractableObject.transform.InverseTransformPoint(test31);
            Vector3 test33 = currentInteractableObject.transform.TransformPoint(test32);
            Vector3 test34 = Vector3.Scale(test33, targetTextureDim);
            Debug.Log($"Test2: {test3.x} --> {test31.x} -->  {test32.x} --> {test33.x} | {test34.x}");

            Vector3 test4 = descriptionMesh.transform.TransformPoint(firstCharInfo.bottomLeft);
            Vector3 test41 = currentCamera.WorldToScreenPoint(test4);
            Vector3 test42 = currentInteractableObject.transform.InverseTransformPoint(test41);
            Vector3 test43 = currentInteractableObject.transform.TransformPoint(test42);
            Vector3 test44 = Vector3.Scale(test43, targetTextureDim);
            Debug.Log($"Test3: {test4.x} --> {test41.x} -->  {test42.x} --> {test43.x} | {test44.x}");

            // most promising
            // probably need to translate the point since the mesh is 100+ units away from where the interactable is
            Vector3 test5 = descriptionMesh.transform.TransformPoint(firstCharInfo.bottomLeft);
            Vector3 test51 = currentInteractableObject.transform.InverseTransformPoint(test4);
            Debug.Log($"Test4: {test5.x} --> {test51.x}");
            //wordLocation = rayRay.GetPoint();
            //Debug.Log($"Loc2: {wordLocation.x} {wordLocation.y}");

            // pageTextInteractable.transform.localPosition = new(-firstCharInfo.origin, 3.3f, -3f);

            // what needs to happen:
            // get the word position in the description mesh
            // then translate that to where the render texture is (the rulebook)

            // target x pos ~= -.8
            // target x pos (0, -1.2~)
        }
        private static Vector3 GetWordLocation(TextMeshPro descriptionMesh, Vector3 firstCharBottomLeft, Camera camera)
        {
            //camera.world
            Vector3 worldBottomLeft = descriptionMesh.transform.TransformPoint(firstCharBottomLeft);
            Vector3 buttonSpacePos = descriptionMesh.transform.InverseTransformPoint(firstCharBottomLeft);
            Debug.Log($"Location: {buttonSpacePos.x} {buttonSpacePos.y} {descriptionMesh.transform.InverseTransformPoint(worldBottomLeft).x} {descriptionMesh.transform.InverseTransformPoint(worldBottomLeft).y}");
            return new(-firstCharBottomLeft.x, buttonSpacePos.y, -3f);
        }
        private static void AddPageInteractables()
        {
            // RuleBookRig/RigParent/RuleBook/Anim/PageFlipper/BookPage_1/Plane01
            // RuleBookRig_Part3/RigParent/Tablet/Anim
            // RulebookRig_Grimora/RigParent/RuleBook/Anim/PageFlipper/BookPage_1/Plane01
            // CardBattle_Magnificus/RuleBookRig_Magnificus/RigParent/RuleBook/Anim/PageFlipper/BookPage_1/Plane01
            if (!AddedInteractables)
            {
                if (SaveManager.SaveFile.IsPart3)
                {
                    GameObject tabletAnim = Singleton<RuleBookController>.Instance.rigParent.FindChild("Anim");
                    currentInteractableObject = CreateInteractableObject(tabletAnim.transform);
                }
                else
                {
                    GameObject page1 = Singleton<RuleBookController>.Instance.rigParent.FindChild("BookPage_1").FindChild("Plane01");
                    CreateInteractableObject(page1.transform);
                    GameObject page2 = Singleton<RuleBookController>.Instance.rigParent.FindChild("BookPage_2").FindChild("Plane01");
                    CreateInteractableObject(page2.transform);
                }
            }
        }
        private static GameObject CreateInteractableObject(Transform parent)
        {
            // create a clickable object using PageTextInteractable
            GameObject retval = new("RuleBookPageInteractable");
            PageTextInteractable currentTextInteractable = retval.AddComponent<PageTextInteractable>();
            retval.transform.SetParent(parent);
            retval.transform.localPosition = new(-0.5f, 3.3f, -3f);
            retval.transform.localRotation = Quaternion.identity;
            
            currentTextInteractable.coll = retval.AddComponent<BoxCollider>();
            currentTextInteractable.Collider.size = new(1f, 0.14f, 0.14f);
            retval.SetActive(false);

            return retval;
        }
    }
}
*/