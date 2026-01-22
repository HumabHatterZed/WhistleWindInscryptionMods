using DiskCardGame;
using InscryptionAPI.RuleBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    internal class OrdealPages {
        // 008b02
        internal static void AddPages() {
            //MechanicPages.CreateNewMechanicPage("Ordeal Battle", "An encounter wherein you must kill all of Leshy's cards in order to win. Direct damage you deal above the maximum scale value is converted into a maximum of 8 Bones at the end of the combat phase.", OrdealUtils.NoonAnim[0]);

            //CreateNewPage("The Ordeals of Green", "Mechanical beings with piercing weaponry. Can appear at Dawn, Noon, Dusk, or Midnight.", null);

            CreateNewPage("Light of the End", "Split into two pillars, one will remain stationary while the other will move right at the end of the opponent's combat. All cards caught in the light will be destroyed.", TextureLoader.LoadSpriteFromFile("sigilTower_rulebook.png", asm: LobotomyPlugin.ModAssembly));
            CreateNewPage("Pale Eye", "While visible: at the end of every turn, the card under the Eye's gaze will lose a quarter of their current Health, rounded up. This damage does not trigger sigils.", TextureLoader.LoadSpriteFromFile("sigilGodPale_rulebook.png", asm: LobotomyPlugin.ModAssembly));

            //CreateNewPage("The Ordeals of Violet", "Divine beings that directly target your mind and body. Can appear at Dawn, Noon, or Midnight.", null);
            //CreateNewPage("The Ordeals of Crimson", "Fae-like beings that multiply as they are struck down. Can appear at Dawn, Noon, or Dusk.", null);
            //CreateNewPage("The Ordeals of Amber", "Insectoid creatures that burrow and consume endlessly. Can appear at Dawn, Dusk, or Midnight.", null);
            //CreateNewPage("The Ordeal of Indigo", "Humanoid beings that persistently hunt for replenishing meat. Can appear at Noon.", null);

            RuleBookManager.New(
                modGuid: LobotomyPlugin.pluginGuid,
                pageType: PageRangeType.Items,
                subsectionName: "Ordeals",
                getInsertPositionFunc: GetInsertPosition,
                createPagesFunc: CreatePages,
                fillPageAction: FillPage);
        }

        private static int GetInsertPosition(PageRangeInfo pageRangeInfo, List<RuleBookPageInfo> pages) {
            return pages.FindLastIndex(rbi => rbi.pagePrefab == RuleBookController.Instance.bookInfo.pageRanges.Find(x => x.type == PageRangeType.Items).rangePrefab) + 1;
        }
        private static List<RuleBookPageInfo> CreatePages(RuleBookInfo instance, PageRangeInfo currentRange, AbilityMetaCategory metaCategory) => NewOrdealPages.Select(x => x.Item1).ToList();

        private static void FillPage(RuleBookPage page, string pageId, object[] otherArgs) {
            if (page is ItemPage itemPage) {
                string name = pageId.Replace("wstl:Ordeals_", "");
                Tuple<RuleBookPageInfo, string, string, Sprite> mechanic = NewOrdealPages.FirstOrDefault(x => x.Item2 == name);
                itemPage.nameTextMesh.text = mechanic.Item2;
                itemPage.descriptionTextMesh.text = mechanic.Item3;
                itemPage.iconRenderer.sprite = mechanic.Item4;
            }
        }

        private static void CreateNewPage(string name, string description, Sprite texture) {
            RuleBookPageInfo pageInfo = new() {
                pageId = "wstl:Ordeals_" + name
            };
            NewOrdealPages.Add(new(pageInfo, name, description, texture));
        }

        private static ObservableCollection<Tuple<RuleBookPageInfo, string, string, Sprite>> NewOrdealPages = new();
    }
}