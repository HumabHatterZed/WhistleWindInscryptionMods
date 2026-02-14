using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.RuleBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    [HarmonyPatch]
    internal class OrdealPages {
        // 008b02
        internal static void AddPages() {
            //MechanicPages.CreateNewMechanicPage("Ordeal Battle", "An encounter wherein you must kill all of Leshy's cards in order to win. Direct damage you deal above the maximum scale value is converted into a maximum of 8 Bones at the end of the combat phase.", OrdealUtils.NoonAnim[0]);

            //CreateNewPage("The Ordeals of Green", "Mechanical beings with piercing weaponry. Can appear at Dawn, Noon, Dusk, or Midnight.", null);

            CreateNewPage("Light of the End", "Split into two pillars, one will remain stationary while the other will move right at the end of the opponent's combat. All cards caught in the light will be destroyed.", TextureLoader.LoadSpriteFromFile("sigilTower_rulebook.png", asm: LobotomyPlugin.ModAssembly));
            
            // 2/3/4
            CreateNewPage("Red Hand", "Destroy the top [X] cards in the targeted draw pile. The number of cards destroyed increases with each boss defeated this run.", TextureLoader.LoadSpriteFromFile("sigilGodRed_rulebook.png", asm: LobotomyPlugin.ModAssembly));

            //CreateNewPage("White Tentacle", "Destroy the top [X] cards in the targeted draw pile. The number of cards destroyed increases with each boss defeated this run.", TextureLoader.LoadSpriteFromFile("sigilGodRed_rulebook.png", asm: LobotomyPlugin.ModAssembly));

            // 3/4/5
            RuleBookPageInfo spike = CreateNewPage("Purple Spike", "Pierce through the player-owned space in this lane, dealing [X] damage to any occupying card. Damage dealt increases with each boss defeated this run.", TextureLoader.LoadSpriteFromFile("sigilGodBlack_rulebook.png", asm: LobotomyPlugin.ModAssembly));
            spike.SetAbilityRedirect("Pierce", Piercing.ability, GameColors.Instance.fuschia);

            // 4/3/2
            CreateNewPage("Pale Eye", "While visible: at the end of every turn, the card under the Eye's gaze will lose 1/[X] their current Health, rounded up and ignoring sigils. Health lost increases with each boss defeated this run.", TextureLoader.LoadSpriteFromFile("sigilGodPale_rulebook.png", asm: LobotomyPlugin.ModAssembly));

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
                string description = mechanic.Item3.Replace("[X]", UpdateDynamicOrdealPages(name));
                itemPage.nameTextMesh.text = mechanic.Item2;
                itemPage.descriptionTextMesh.text = description;
                itemPage.iconRenderer.sprite = mechanic.Item4;
            }
        }

        private static RuleBookPageInfo CreateNewPage(string name, string description, Sprite texture) {
            RuleBookPageInfo pageInfo = new() {
                pageId = "wstl:Ordeals_" + name
            };
            NewOrdealPages.Add(new(pageInfo, name, description, texture));
            return pageInfo;
        }

        private static ObservableCollection<Tuple<RuleBookPageInfo, string, string, Sprite>> NewOrdealPages = new();

        private static string UpdateDynamicOrdealPages(string pageId) {
            Debug.Log($"{pageId}");
            // [0, 2]
            int difficulty = Mathf.Min(2, RunState.CurrentRegionTier + Mathf.Max(0, RunState.Run.DifficultyModifier - 1));
            if (pageId == "Red Hand") {
                return (2 + difficulty).ToString();
            }
            else if (pageId == "White Tentacle") {
                return (0 * difficulty).ToString();
            }
            else if (pageId == "Purple Spike") {
                return (3 + difficulty).ToString();
            }
            else if (pageId == "Pale Eye") {
                return (4 - difficulty).ToString();
            }

            return "";
        }
    }
}