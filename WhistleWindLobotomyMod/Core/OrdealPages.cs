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
            CreateNewPage("Light of the End", "Destroy any card caught in the light. At the end of combat, move one pillar to the right, looping to the other side. The other pillar remains stationary.", TextureLoader.LoadSpriteFromFile("sigilTower_rulebook.png", asm: LobotomyPlugin.ModAssembly));
            
            CreateNewPage("Red Hand", "Destroy the top [X] cards in the targeted draw pile. The number of cards destroyed increases with each boss defeated this run.", TextureLoader.LoadSpriteFromFile("sigilGodRed_rulebook.png", asm: LobotomyPlugin.ModAssembly));

            CreateNewPage("White Tentacle", "Apply [X] Sinking to affected cards. The amount of Sinking applied increases with each boss defeated this run.", TextureLoader.LoadSpriteFromFile("sigilGodRed_rulebook.png", asm: LobotomyPlugin.ModAssembly))
                .SetAbilityRedirect("Sinking", Sinking.iconId, GameColors.Instance.seafoam);

            RuleBookPageInfo spike = CreateNewPage("Purple Spike", "Pierce through the opposing space in this lane, dealing [X] damage to any occupying card. Damage dealt increases with each boss defeated this run.", TextureLoader.LoadSpriteFromFile("sigilGodBlack_rulebook.png", asm: LobotomyPlugin.ModAssembly))
                .SetAbilityRedirect("Pierce", Piercing.ID, GameColors.Instance.fuschia);

            CreateNewPage("Pale Eye", "While visible: at the end of every turn, the card under the Eye's gaze will lose 1/[X] their current Health, rounded up and ignoring sigils. Health lost increases with each boss defeated this run.", TextureLoader.LoadSpriteFromFile("sigilGodPale_rulebook.png", asm: LobotomyPlugin.ModAssembly));

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
            if (pageId == "Red Hand") {
                return GodRed.RedHoofCards().ToString();
            }
            else if (pageId == "White Tentacle") {
                return GodWhite.SinkingStacks().ToString();
            }
            else if (pageId == "Purple Spike") {
                return GodBlack.BlackSpikeDamage().ToString();
            }
            else if (pageId == "Pale Eye") {
                return GodPale.PaleEyePercentage().ToString();
            }

            return "";
        }
    }
}