/*using DiskCardGame;
using EasyFeedback.APIs;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.TalkingCards.Create;
using InscryptionAPI.Triggers;
using Pixelplacement;
using Pixelplacement.TweenSystem;
using Steamworks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UIElements;
using UnityEngine.UIElements.StyleSheets;
using static UnityEngine.ParticleSystem;

namespace WhistleWind.AbnormalSigils.Patches
{
    // we want modders to be able to set multiple redirects that can go to different types of pages
    // eg: abilities, stat icons, boons, items
    public class PageTextInteractable : AlternateInputInteractable
    {
        public override CursorType CursorType => CursorType.Inspect;
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
                PageRangeType.Boons => BoonsUtil.AllData.Find(x => x.type.ToString() == textToLink).type.ToString(),
                PageRangeType.Items => textToLink,
                _ => textToLink,
            };
        }

        public override void OnAlternateSelectStarted()
        {
            Debug.Log($"AlternateSelect {redirectPageType} {redirectPageId}");
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
                case PageRangeType.Items:
                    Singleton<RuleBookController>.Instance.OpenToItemPage(redirectPageId);
                    break;
            }
        }

        private void Start()
        {
            GameObject lineObject = new("DebugOutline");
            line = lineObject.AddComponent<LineRenderer>();
            line.transform.SetParent(transform);
            line.transform.localPosition = Vector3.zero;
            //line.material = Material.GetDefaultLineMaterial();
            line.startColor = Color.red;
            //line.endColor = Color.green;
            //line.startWidth = 0.01f;
            //line.endWidth = 0.01f;
        }

        public LineRenderer line;
        public override void ManagedUpdate()
        {
            HiliteBox();
        }
        private void HiliteBox()
        {
            Vector3[] positions = new Vector3[4];
            positions[0] = transform.TransformPoint(new Vector3(Collider.size.x, 0, 0));
            positions[1] = transform.TransformPoint(new Vector3(-Collider.size.x, 0, 0));
            positions[2] = transform.TransformPoint(new Vector3(-Collider.size.x, -0, -0));
            positions[3] = transform.TransformPoint(new Vector3(Collider.size.x, -0, -0));
            line.SetPositions(positions);
        }
    }

    // FFFFFF & FFFF00 - invisible, identical - more white/yellow = more transparency
    // A00000 - pretty good median red colour
    // 008b02 - good dark-ish green colour
    // Multiply - 160 alpha - for testing in an image editor/maker program thingy <color=#...> <color=\"red\">
    [HarmonyPatch]
    public static class PageTextInteractablePatches
    {
        [HarmonyPrefix, HarmonyPatch(typeof(RulebookPageFlipper), nameof(RulebookPageFlipper.RenderPages))]
        private static void GetCurrentTopPage(RulebookPageFlipper __instance, Transform topPage)
        {
            //if (layer == -1)
                //layer = __instance.gameObject.layer;

            currentTopPage = topPage.Find("Plane01"); // retrieve the correct page object before rendering the pages
        }

        [HarmonyPrefix, HarmonyPatch(typeof(RuleBookController), nameof(RuleBookController.SetShown))]
        private static void DisableInteractable(bool shown)
        {
            activeInteractableObjects.ForEach(x => x.SetActive(false));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PageFlipper), nameof(PageFlipper.LoadPageContent))]
        private static void AddInteractablesToTopPage(PageFlipper __instance, PageContentLoader loader, int index)
        {
            // Part 3 rulebook only has 1 'page' object, so retrieve it here
            //if (SaveManager.SaveFile.IsPart3 && currentTopPage == null)
            //    currentTopPage = Singleton<RuleBookController>.Instance.rigParent.FindChild("Anim").transform;

            AddPageInteractables(__instance, loader, index);
        }

        public static AbilityManager.FullAbility SetTextRedirect(this AbilityManager.FullAbility fullAbility, string clickableText, Ability abilityRedirect)
        {
            fullAbility.Info.rulebookDescription = fullAbility.Info.rulebookDescription.Replace(clickableText, $"<color=#A00000>{clickableText}</color>");
            fullAbility.SetExtendedProperty("AbilityRedirect", $"{clickableText},{(int)abilityRedirect}");
            return fullAbility;
        }

        //[HarmonyPostfix, HarmonyPatch(typeof(RuleBookController), nameof(RuleBookController.Start))]
        //private static void AddPageInteractablesToRuleBookRig() => AddPageInteractables();

        //[HarmonyPostfix, HarmonyPatch(typeof(AbilityIconInteractable), nameof(AbilityIconInteractable.OnAlternateSelectStarted))]
        private static void Test(AbilityIconInteractable __instance)
        {
            BoxCollider collider = __instance.coll as BoxCollider;
            Debug.Log($"Coll: {collider?.size.x} {collider?.size.y} {collider?.size.z}");
            Debug.Log($"Rot: {__instance.transform.localRotation.x} {__instance.transform.localRotation.y} {__instance.transform.localRotation.z} {__instance.transform.localRotation.w}");
        }

        // https://stackoverflow.com/questions/6865419/indexof-for-multiple-results
        public static IEnumerable<int> AllIndexesOf(this string str, string searchstring)
        {
            int minIndex = str.IndexOf(searchstring);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
            }
        }

        //[HarmonyPostfix, HarmonyPatch(typeof(PageFlipper), nameof(PageFlipper.Flip))]
        private static void UpdatePageInteractableRuleBook(PageFlipper __instance, bool forwards)
        {
            //currentInteractableObject = __instance.transform.gameObject.FindChild("RuleBookPageInteractable");
            //currentTextInteractable = currentInteractableObject.GetComponent<PageTextInteractable>();
            //Debug.Log($"Current Obj {currentInteractableObject.transform.parent.parent.name}");
            bool makeActive = false;            
            //currentInteractableObject.SetActive(false);

            // get the info for the current rulebook page
            RuleBookPageInfo info = Singleton<RuleBookController>.Instance.PageData[__instance.currentPageIndex];
            if (info.ability != 0)
            {
                string redirect = info.ability.GetExtendedProperty("RuleBook_Redirect");
                //Debug.Log($"Ability {AbilitiesUtil.GetInfo(info.ability).rulebookName} {redirect}");
                if (redirect != null) // if the page we're flipping to has a redirect
                {
                    makeActive = true;
                    //currentTextInteractable.SetRedirect(
                    //    redirect, PageRangeType.Abilities
                    //    );
                }
                else if (info.boon != 0)
                {
                    //currentTextInteractable.SetRedirect(info.pageId, PageRangeType.Boons);
                }
                else if (StatIconManager.AllStatIconInfos.Exists(x => x.iconType.ToString() == info.pageId))
                {
                    //currentTextInteractable.SetRedirect(info.pageId, PageRangeType.StatIcons);
                }
                else
                {
                    //currentTextInteractable.SetRedirect(info.pageId, PageRangeType.Items);
                }
            }

            if (makeActive)
            {
                //UpdateColliderSize(__instance, forwards);
                //currentInteractableObject.SetActive(true);
            }
        }

        private static void UpdateColliderSize(PageFlipper flipper, PageContentLoader loader, GameObject interactableObject)
        {
            Debug.Log($"UpdateColliderSize");
            // grab the content loader for the top page
            // update the current interactable based on what the next top page is going to be

            // get the description mesh from the currently displayed rulebook page
            // also get the rendering camera so we can position the whats-it
            RuleBookPage ruleBookPage = loader.currentPageObj.GetComponent<RuleBookPage>();
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

            PageTextInteractable interactable = interactableObject.GetComponent<PageTextInteractable>();
            Camera pageCamera = loader.GetComponentInParent<Camera>();

            // since rulebook names can be multiple words long, we want to get the first character index of the first word
            // and the last character index of the last word
            // then we use those indeces to both determine the interactable's position and its x-length
            // names that wrap around the rulebook aren't accounted for, so piss
            int first = -1, last = -1;
            float length = 0;
            List<string> pageIds = new();
            TMP_WordInfo[] wordInfos = descriptionMesh.textInfo.wordInfo;
            string[] redirectTextWords = interactable.redirectText.Split();
            
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
            //interactable.Collider.size = new(colliderSizeX, 0.14f, 0.14f);

            Vector3 wordLocation;
            Vector3 targetTextureDim = new Vector3(pageCamera.targetTexture.width, pageCamera.targetTexture.height);
            //float ortho = currentCamera.orthographicSize * 2;
            //Vector2 camSize = new(currentCamera.scaledPixelHeight / ortho, currentCamera.scaledPixelWidth / ortho);

            RectTransform rectTransform = descriptionMesh.rectTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, firstCharInfo.bottomLeft,
                pageCamera, out Vector2 localPoint);

            // scale local point by size of mesh
            localPoint.x /= rectTransform.rect.width;
            localPoint.y /= rectTransform.rect.height;
            
            Ray viewportPoint = pageCamera.ViewportPointToRay(localPoint);

            // project ray onto plane
            Plane plane = new(Vector3.up, Vector3.zero);
            plane.Raycast(viewportPoint, out float d);

            //wordLocation = GetWordLocation(descriptionMesh, firstCharInfo.bottomLeft, currentCamera);
            //wordLocation = descriptionMesh.transform.InverseTransformPoint(firstCharInfo.topLeft);

            wordLocation = viewportPoint.GetPoint(d);
            Debug.Log($"Loc: {wordLocation.x} {wordLocation.y}");

            Vector3 worldPos = descriptionMesh.transform.TransformPoint(firstCharInfo.bottomLeft);
            Vector3 localPos = interactableObject.transform.InverseTransformPoint(worldPos);
            //Vector3 texturePos = localPos + interactable.Collider.size / 2f;
            //Vector3 rayPos = Vector3.Scale(texturePos, targetTextureDim);
            //Ray rayRay = pageCamera.ScreenPointToRay(rayPos);
            //Physics.Raycast(rayRay, out RaycastHit hit);
            //Debug.Log($"Pos: {firstCharInfo.bottomLeft.x} : {worldPos.x} --> {localPos.x} --> {texturePos.x} --> {rayPos.x} {hit.point.x}");

            Vector3 screenPos = pageCamera.WorldToViewportPoint(worldPos);
            //Debug.Log($"Screen1: {screenPos.x}");
            screenPos.Scale(new Vector3(Screen.width, Screen.height, 1f));
            //Debug.Log($"Screen2: {screenPos.x}");

            Vector3 test = pageCamera.WorldToScreenPoint(firstCharInfo.bottomLeft);
            Vector3 test2 = pageCamera.transform.InverseTransformPoint(test);
            Debug.Log($"Test: {test2.x}");

            Vector3 test3 = descriptionMesh.transform.InverseTransformPoint(firstCharInfo.bottomLeft);
            Vector3 test31 = pageCamera.WorldToScreenPoint(test3);
            Vector3 test32 = interactableObject.transform.InverseTransformPoint(test31);
            Vector3 test33 = interactableObject.transform.TransformPoint(test32);
            Vector3 test34 = Vector3.Scale(test33, targetTextureDim);
            Debug.Log($"Test2: {test3.x} --> {test31.x} -->  {test32.x} --> {test33.x} | {test34.x}");

            Vector3 test4 = descriptionMesh.transform.TransformPoint(firstCharInfo.bottomLeft);
            Vector3 test41 = pageCamera.WorldToScreenPoint(test4);
            Vector3 test42 = interactableObject.transform.InverseTransformPoint(test41);
            Vector3 test43 = interactableObject.transform.TransformPoint(test42);
            Vector3 test44 = Vector3.Scale(test43, targetTextureDim);
            Debug.Log($"Test3: {test4.x} --> {test41.x} -->  {test42.x} --> {test43.x} | {test44.x}");

            // most promising
            // probably need to translate the point since the mesh is 100+ units away from where the interactable is
            Vector3 test5 = descriptionMesh.transform.TransformPoint(firstCharInfo.bottomLeft);
            Vector3 test51 = interactableObject.transform.InverseTransformPoint(test4);
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
            Vector3 buttonSpacePos = camera.transform.InverseTransformPoint(firstCharBottomLeft);
            Debug.Log($"Location: {buttonSpacePos.x} {buttonSpacePos.y} {camera.transform.InverseTransformPoint(worldBottomLeft).x} {camera.transform.InverseTransformPoint(worldBottomLeft).y}");
            return new(firstCharBottomLeft.x, buttonSpacePos.y, -3f);
        }

        private static void AddPageInteractables(PageFlipper pageFlipper, PageContentLoader loader, int pageIndex)
        {
            // RuleBookRig/RigParent/RuleBook/Anim/PageFlipper/BookPage_1/Plane01
            // RuleBookRig_Part3/RigParent/Tablet/Anim
            // RulebookRig_Grimora/RigParent/RuleBook/Anim/PageFlipper/BookPage_1/Plane01
            // CardBattle_Magnificus/RuleBookRig_Magnificus/RigParent/RuleBook/Anim/PageFlipper/BookPage_1/Plane01

            // grab all the words/phrases that have redirects
            // gonna do a rulebookpagemanager i think, for convenience's sake
            // for now, just make sure the bugger works

            //Debug.Log($"ActiveObjects: {activeInteractableObjects.Count} {activeInteractableObjects.Exists(x => x != null)}");
            activeInteractableObjects.Clear();
            //Debug.Log($"Current top: {pageIndex} {pageFlipper.currentPageIndex}");
            if (pageIndex != pageFlipper.currentPageIndex)
                return;

            RuleBookPage component = loader.currentPageObj.GetComponent<RuleBookPage>();
            RuleBookPageInfo pageInfo = pageFlipper.PageData[pageIndex];

            if (component is AbilityPage)
            {
                //Debug.Log($"Ability: {pageInfo.ability} {pageInfo.ability == NimbleFoot.ability}");
                if (pageInfo.ability == NimbleFoot.ability)
                {
                    AbilityPage page = component as AbilityPage;
                    CreatePageInteractables(page.mainAbilityGroup.descriptionTextMesh, pageFlipper, loader, "Haste");
                }
            }
            else if (component is StatIconPage)
            {
                Debug.Log($"Stat Icon: {pageInfo.pageId} {pageInfo.pageId == SlimeIcon.Icon.ToString()}");
                if (pageInfo.pageId == SlimeIcon.Icon.ToString())
                {
                    StatIconPage page = component as StatIconPage;
                    CreatePageInteractables(page.descriptionTextMesh, pageFlipper, loader, "value");
                }
            }
            else if (component is BoonPage)
            {
                Debug.Log($"Boon: {pageInfo.boon} {pageInfo.boon == BoonData.Type.DoubleDraw}");
                if (pageInfo.boon == BoonData.Type.DoubleDraw)
                {
                    BoonPage page = component as BoonPage;
                    CreatePageInteractables(page.descriptionTextMesh, pageFlipper, loader, "twice");
                }

            }
            else if (component is ItemPage)
            {
                Debug.Log($"Item: {pageInfo.pageId} {pageInfo.pageId == "Single Recall in a Bottle"}");
                if (pageInfo.pageId == "Single Recall in a Bottle")
                {
                    ItemPage page = component as ItemPage;
                    CreatePageInteractables(page.descriptionTextMesh, pageFlipper, loader, "Recall");
                }
            }
        }

        private static void CreatePageInteractables(TextMeshPro descriptionTextMesh, PageFlipper pageFlipper, PageContentLoader loader, string keyText)
        {
            descriptionTextMesh.ForceMeshUpdate();
            foreach (int index in descriptionTextMesh.text.AllIndexesOf(keyText))
            {
                //currentTopPage = RuleBookController.Instance.rigParent.transform.Find("RuleBookCamera");ViewManager.Instance.CameraParent;
                // we want to attach this to the actual page object, NOT the render object
                Camera mainCam = ViewManager.Instance.CameraParent.GetComponentInChildren<Camera>();
                Transform parent = descriptionTextMesh.rectTransform;//currentTopPage;//mainCam.transform//RuleBookController.Instance.rigParent.transform.Find("RuleBookCamera");
                if (layer == -1)
                    layer = parent.gameObject.layer;

                GameObject interactableObject = CreateInteractableObject(parent, keyText, PageRangeType.Abilities);
                SetInteractableSizePosition(pageFlipper, loader, descriptionTextMesh, interactableObject, keyText, index);

                interactableObject.SetActive(true);
                activeInteractableObjects.Add(interactableObject);
                loader.currentAdditiveObjects.Add(interactableObject);
            }
        }

        private static void SetInteractableSizePosition(PageFlipper pageFlipper, PageContentLoader loader,
            TextMeshPro descriptionMesh, GameObject interactableObject,
            string keyText, int wordIndex)
        {
            if (descriptionMesh.textInfo.lineCount == 0)
                return;

            descriptionMesh.ForceMeshUpdate();
            Debug.Log("<color=#A00000>SetInteractableSizePosition</color>\n-------------------------");
            PageTextInteractable interactable = interactableObject.GetComponent<PageTextInteractable>();
            // what we need to do is take the word position, translate it from the render camera

            float length = 0f;
            int firstCharacterIndex = -1, lastCharacterIndex = -1;

            List<string> pageIds = new();
            TMP_WordInfo[] descriptionWordInfos = descriptionMesh.textInfo.wordInfo;
            string[] redirectTextWords = keyText.Split();

            for (int i = 0; i < descriptionWordInfos.Length; i++)
            {
                if (descriptionWordInfos[i].characterCount == 0)
                    continue;

                string word = descriptionWordInfos[i].GetWord();
                //Debug.Log($"Word: {word}");
                
                if (!redirectTextWords.Contains(word))
                    continue;

                TMP_Text text = descriptionWordInfos[i].textComponent;
                //Debug.Log($"Bounds: {word} {text.textBounds.size.x} {text.textBounds.size.y}");
                //Debug.Log($"Scale: {text.transform.localScale.x} {text.transform.localScale.y}");

                pageIds.Add(word);
                length += word.Length;

                if (pageIds.Count == 1)
                    firstCharacterIndex = descriptionWordInfos[i].firstCharacterIndex;
                else
                    length++; // account for spaces past the first word

                if (pageIds.Count == redirectTextWords.Length)
                {
                    lastCharacterIndex = descriptionWordInfos[i].lastCharacterIndex;
                    break;
                }
            }

            if (firstCharacterIndex == -1 || lastCharacterIndex == -1)
                return;

            TMP_CharacterInfo firstCharInfo = descriptionMesh.textInfo.characterInfo[firstCharacterIndex];
            TMP_CharacterInfo lastCharInfo = descriptionMesh.textInfo.characterInfo[lastCharacterIndex];
            
            #region Scale
            float scalar = descriptionMesh.transform.localScale.x;
            float colliderSizeX = (lastCharInfo.origin - firstCharInfo.origin) / 2f;// * scalar;//(Mathf.Abs(lastCharInfo.bottomLeft.x - firstCharInfo.bottomLeft.x));// / length * 0.1f;// / length;//(lastCharInfo.origin - firstCharInfo.origin) / length;
            float colliderSizeY = (firstCharInfo.ascender - firstCharInfo.descender) / 2f;// * scalar;//(Mathf.Abs(lastCharInfo.topLeft.y) - Mathf.Abs(firstCharInfo.bottomLeft.y));// * 0.1f;

            //colliderSizeX = Mathf.Abs(lastCharInfo.bottomLeft.x - firstCharInfo.bottomLeft.x);// / 2f;// * scalar;
            //colliderSizeY = Mathf.Abs(lastCharInfo.topLeft.y - firstCharInfo.bottomLeft.y);// / 2f;// * scalar;

            Debug.Log($"Size: {colliderSizeX}, {colliderSizeY} | {Mathf.Abs(lastCharInfo.bottomLeft.x - firstCharInfo.bottomLeft.x) / 2f * scalar}, {Mathf.Abs(lastCharInfo.topLeft.y - firstCharInfo.bottomLeft.y) / 2f * scalar}");// | {firstCharInfo.bottomLeft.x} {firstCharInfo.bottomLeft.y}");
            //Debug.Log($"X: {(lastCharInfo.origin - firstCharInfo.origin) / length} {(lastCharInfo.bottomLeft.x - firstCharInfo.bottomLeft.x) / length}");
            //Debug.Log($"Y: {lastCharInfo.topLeft.y - firstCharInfo.bottomLeft.y} {lastCharInfo.topRight.y - firstCharInfo.bottomRight.y}");
            //Debug.Log($"Length: {length}");
            interactableObject.transform.localScale = new(colliderSizeX, colliderSizeY, 0.001f);

            //Debug.Log($"Origin: {firstCharInfo.origin} {firstCharInfo.origin / 2f * scalar} | {descriptionMesh.transform.InverseTransformPoint(firstCharInfo.origin, 0f, 0f).x} {descriptionMesh.transform.TransformPoint(firstCharInfo.origin, 0f, 0f).x}");
            //Debug.Log($"X: {firstCharInfo.bottomLeft.x} {firstCharInfo.bottomLeft.x / 2f * scalar} | {descriptionMesh.transform.InverseTransformPoint(firstCharInfo.bottomLeft.x, 0f, 0f).x} {descriptionMesh.transform.TransformPoint(firstCharInfo.bottomLeft.x, 0f, 0f).x}");
            #endregion

            Bounds pageBounds = currentTopPage.GetComponent<SkinnedMeshRenderer>().sharedMesh.bounds;
            float pageX = pageBounds.extents.x - colliderSizeX / 2f;
            float pageY = pageBounds.extents.y - colliderSizeY / 2f;
            float pageZ = pageBounds.extents.z;

            Vector3 pageCorner = new(-pageX, pageY, -pageZ);

            Bounds borderbounds = loader.currentPageObj.transform.Find("Border").GetComponent<SpriteRenderer>().bounds;
            Vector2 meshBorderTopRight = new(-((borderbounds.extents.x - colliderSizeX / 2f) / scalar), (borderbounds.extents.y - colliderSizeY / 2f) / scalar);

            Rect descriptionRect = descriptionMesh.rectTransform.rect;
            Vector3 descriptionTopRight = new(-(descriptionRect.x - colliderSizeX), -(descriptionRect.y - colliderSizeY), 0f);
            Vector3 descriptionTopLeft = new(descriptionRect.x - colliderSizeX, -(descriptionRect.y - colliderSizeY), 0f);
            Vector3 descriptionBottomRight = new(-(descriptionRect.x - colliderSizeX), descriptionRect.y - colliderSizeY, 0f);
            Vector3 descriptionBottomLeft = new(descriptionRect.x - colliderSizeX, descriptionRect.y - colliderSizeX, 0f);


            float meshXDifference = 0;//Mathf.Abs(Mathf.Abs(descriptionBounds.max.x) - Mathf.Abs(descriptionBounds.min.x));
            float meshYDifference = 0;// Mathf.Abs(Mathf.Abs(descriptionBounds.max.y) - Mathf.Abs(descriptionBounds.min.y));

            //Bounds descriptionMeshBounds = descriptionMesh.GetTextBounds();

            Debug.Log($"Page Corner: {pageX} {pageY} {pageCorner.z}");
            Debug.Log($"Mesh Border: {meshBorderTopRight.x} {meshBorderTopRight.y}");

            //Debug.Log($"ExtentX: {pageX} {descriptionBounds.extents.x * scalar - colliderSizeX / 2f}");

            // [2, -2] --> [1.3, -1.3]
            
            Camera mainCam = ViewManager.Instance.CameraParent.GetComponentInChildren<Camera>();
            Camera rulebookCam = RuleBookController.Instance.rigParent.transform.Find("RuleBookCamera").GetComponent<Camera>();
            
            Vector3 pageCornerToWorld = currentTopPage.TransformPoint(pageCorner);
            Vector3 pageCornerToScreen = rulebookCam.WorldToScreenPoint(pageCornerToWorld);
            Vector3 pageToMainWorld = mainCam.ScreenToWorldPoint(pageCornerToScreen);//currentTopPage.transform.InverseTransformPoint(camToWorld);
            Vector3 mainWorldToLocal = mainCam.transform.InverseTransformPoint(pageToMainWorld);

            //Debug.Log($"Page to World: {pageCornerToWorld.x} {pageCornerToWorld.y} {pageCornerToWorld.z}");
            //Debug.Log($"World to Screen: {pageCornerToScreen.x}, {pageCornerToScreen.y} {pageCornerToScreen.z}");
            //Debug.Log($"Screen to Main: {pageToMainWorld.x}, {pageToMainWorld.y} {pageToMainWorld.z}");
            //Debug.Log($"Main to Local: {mainWorldToLocal.x}, {mainWorldToLocal.y} {mainWorldToLocal.z}");

            interactableObject.transform.localPosition = Vector3.zero;// pageCorner;//mainWorldToLocal;

            GameObject a = GameObject.Instantiate(interactableObject, interactableObject.transform.parent);
            a.name = "TopRight";
            a.GetComponent<MeshRenderer>().material.color = Color.red;
            a.transform.localPosition = descriptionTopRight;//pageCorner;
            a.SetActive(true);

            GameObject b = GameObject.Instantiate(interactableObject, interactableObject.transform.parent);
            b.name = "TopLeft";
            b.GetComponent<MeshRenderer>().material.color = Color.blue;
            b.transform.localPosition = descriptionTopLeft;//new(pageX, pageY, pageZ);
            b.SetActive(true);

            GameObject c = GameObject.Instantiate(interactableObject, interactableObject.transform.parent);
            c.name = "BottomRight";
            c.GetComponent<MeshRenderer>().material.color = Color.green;
            c.transform.localPosition = descriptionBottomRight;//new(-pageX, -pageY + meshYDifference, -pageZ);
            c.SetActive(true);

            GameObject d = GameObject.Instantiate(interactableObject, interactableObject.transform.parent);
            d.name = "BottomLeft";
            d.GetComponent<MeshRenderer>().material.color = Color.cyan;
            d.transform.localPosition = descriptionBottomLeft;//new(pageX, -pageY + meshYDifference, pageZ);
            d.SetActive(true);

            Vector3 screenOriginToWorld = mainCam.ScreenToWorldPoint(new(0, 0, pageCornerToScreen.z));
            Vector3 originToLocal = mainCam.transform.InverseTransformPoint(screenOriginToWorld);
            Vector3 correctedLocal = new(originToLocal.x - colliderSizeX / 2f, originToLocal.y - colliderSizeY / 2f, originToLocal.z);

            //Debug.Log($"Origin to World: {screenOriginToWorld.x} {screenOriginToWorld.y} {screenOriginToWorld.z}");
            //Debug.Log($"Origin to Local: {originToLocal.x}, {originToLocal.y} {originToLocal.z}");
            //Debug.Log($"Corrected Local: {correctedLocal.x}, {correctedLocal.y} {correctedLocal.z}");

            //Vector3 newWorld = ViewManager.Instance.CameraParent.GetComponentInChildren<Camera>().ScreenToWorldPoint(screenPoint);

            //Vector3 camToWorld = rulebookCam.WorldToScreenPoint(pageWorld);//rulebookCam.ScreenToWorldPoint(new(0f, 1f));//ViewManager.Instance.CameraParent.GetComponentInChildren<Camera>().ScreenToWorldPoint(new(0f, 1f));

            float distanceToCam = Vector3.Distance(currentTopPage.position, rulebookCam.transform.position);
            float distance = Mathf.Abs(currentTopPage.transform.position.z - rulebookCam.transform.position.z);
            //Debug.Log($"Distance: {distanceToCam} | {distance}");
            Vector3 camForward = mainCam.transform.forward;
            camForward.y = 0;
            //interactableObject.transform.rotation = Quaternion.LookRotation(camForward);
            //interactableObject.transform.LookAt(mainCam.transform, )
            //float ii = -meshBorderTopRight.x;

            //Vector2 bottomLeft = new Vector2(meshBounds.extents.x - colliderSizeX / 2f, -(meshBounds.extents.y - colliderSizeY / 2f));

            //Debug.Log($"Corners: TR ({topRight.x}, {topRight.y}) BL ({bottomLeft.x}, {bottomLeft.y})");

            // 2936.338 515.6567 1.5012

            //interactableObject.transform.localPosition = new(worldToLocal.x - colliderSizeX / 2f, worldToLocal.y - colliderSizeY / 2f, 0f);// worldToLocal - new Vector3(colliderSizeX /2f, colliderSizeY /2f, -worldToLocal.z);//new(rulebookPageTopRight.x, rulebookPageTopRight.y, 0f);


            //Debug.Log($"Plane01: {currentTopPage.GetComponent<SkinnedMeshRenderer>().bounds.max.x} {currentTopPage.GetComponent<SkinnedMeshRenderer>().bounds.max.y} {currentTopPage.GetComponent<SkinnedMeshRenderer>().bounds.max.z}");
            //interactable.Collider.size = new(colliderSizeX, colliderSizeY);
        }

        private static Vector3 ConvertRuleBookToMainView(Transform parent, Camera ruleBookCam, Camera mainCam, Vector3 localPosition)
        {
            Debug.Log($"<color=#A00000>ConvertRuleBookToMainView</color>");
            Vector3 localToWorld = parent.TransformPoint(localPosition);
            Debug.Log($"localToWorld: {localToWorld.x} {localToWorld.y} {localToWorld.z}");
            Vector3 worldToScreen = ruleBookCam.WorldToScreenPoint(localToWorld);
            Debug.Log($"worldToScreen: {worldToScreen.x} {worldToScreen.y} {worldToScreen.z}");
            Vector3 ruleBookToMain = mainCam.ScreenToWorldPoint(worldToScreen);
            Debug.Log($"ruleBookToMain: {ruleBookToMain.x} {ruleBookToMain.y} {ruleBookToMain.z}");
            Vector3 mainToLocal = mainCam.transform.InverseTransformPoint(ruleBookToMain);
            Debug.Log($"mainToLocal: {mainToLocal.x} {mainToLocal.y} {mainToLocal.z}");
            return mainToLocal;
        }
        private static GameObject CreateInteractableObject(Transform parent, string keyText, PageRangeType type)
        {
            // create a clickable object using PageTextInteractable
            GameObject retval = GameObject.CreatePrimitive(PrimitiveType.Cube);// new($"RuleBookPageInteractable ({keyText})");
            retval.name = $"RuleBookPageInteractable ({keyText})";
            retval.transform.SetParent(parent);
            retval.layer = layer;
            retval.transform.localPosition = new(0, 0, -parent.localPosition.z);//Vector3.zero;//new(-0.5f, 3.3f, -3f);
            retval.transform.localRotation = Quaternion.identity;

            PageTextInteractable currentTextInteractable = retval.AddComponent<PageTextInteractable>();
            currentTextInteractable.coll = retval.GetComponent<BoxCollider>();
            currentTextInteractable.SetRedirect(keyText, type);
            retval.SetActive(false);

            return retval;
        }

        private static void AddPageInteractables()
        {
            if (!AddedInteractables)
            {
                if (SaveManager.SaveFile.IsPart3)
                {
                    GameObject tabletAnim = Singleton<RuleBookController>.Instance.rigParent.FindChild("Anim");
                    //currentInteractableObject = CreateInteractableObject(tabletAnim.transform, "");
                }
                else
                {
                    GameObject page1 = Singleton<RuleBookController>.Instance.rigParent.FindChild("BookPage_1").FindChild("Plane01");
                    //CreateInteractableObject(page1.transform, "");
                    GameObject page2 = Singleton<RuleBookController>.Instance.rigParent.FindChild("BookPage_2").FindChild("Plane01");
                    //CreateInteractableObject(page2.transform, "");
                }
            }
        }

        //public static GameObject currentInteractableObject = null;
        //public static PageTextInteractable currentTextInteractable = null;
        //public static bool AddedInteractables = false;

        public static readonly List<GameObject> activeInteractableObjects = new();
        public static Transform currentTopPage = null;
        
        public static int layer = -1;
    }
}
*/