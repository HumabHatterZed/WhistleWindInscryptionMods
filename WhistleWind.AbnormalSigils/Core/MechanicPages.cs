using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;
using WhistleWind.AbnormalSigils.Patches;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils.Core {
    public static class MechanicPages {
        public const string VENDETTA_FORMAT = "This card deals 1 more damage when striking cards similar to {0}{1}";

        internal static void AddMechanicEntries() {
            CreateNewMechanicPage(
                "Speed",
                "Determines the order that cards attack in during combat. All cards have a base Speed of 0, with movement priority to player-owned cards.",
                TextureLoader.LoadTextureFromFile("sigilSpeed.png", AbnormalPlugin.Assembly)
            );

            CreateNewMechanicPage(
                "Bitter Vendetta",
                "This card deals 1 more damage when striking cards similar to its vendetta target. In battles, this description will be updated with information on this card's vendetta.",
                TextureLoader.LoadTextureFromFile("sigilBitterEnemies.png", AbnormalPlugin.Assembly)
            );

            RuleBookManager.New(
                modGuid: AbnormalPlugin.pluginGuid,
                pageType: PageRangeType.Abilities,
                subsectionName: "Mechanics",
                getInsertPositionFunc: GetInsertPosition,
                createPagesFunc: CreatePages,
                fillPageAction: FillPage);
        }

        private static int GetInsertPosition(PageRangeInfo pageRangeInfo, List<RuleBookPageInfo> pages) {
            PageRangeInfo pagePrefabRef = RuleBookController.Instance.bookInfo.pageRanges.Find(x => x.type == PageRangeType.Items);
            pagePrefabRef ??= RuleBookController.Instance.bookInfo.pageRanges.Find(x => x.type == PageRangeType.Abilities);
            return pages.FindLastIndex(rbi => rbi.pagePrefab == pagePrefabRef.rangePrefab) + 1;
        }
        private static List<RuleBookPageInfo> CreatePages(RuleBookInfo instance, PageRangeInfo currentRange, AbilityMetaCategory metaCategory) => NewMechanicPages.Select(x => x.Item1).ToList();

        private static void FillPage(RuleBookPage page, string pageId, object[] otherArgs) {
            if (page is AbilityPage abilityPage) {
                string name = pageId.Replace("wstl:Mechanic_", "");
                Tuple<RuleBookPageInfo, string, string, Texture> mechanic = NewMechanicPages.FirstOrDefault(x => x.Item2 == name);
                abilityPage.mainAbilityGroup.nameTextMesh.text = mechanic.Item2;
                abilityPage.mainAbilityGroup.descriptionTextMesh.text = ModifyMechanicDescription(mechanic.Item2, mechanic.Item3);
                abilityPage.mainAbilityGroup.iconRenderer.material.mainTexture = mechanic.Item4;
            }
        }

        public static string ModifyMechanicDescription(string pageName, string originalDesc) {
            if (RuleBookPatches.CardForRuleBook != null) {
                if (RuleBookPatches.CardForRuleBook.HasAbility(BitterEnemies.ID) && pageName == "Bitter Vendetta") {
                    BitterEnemies com = RuleBookPatches.CardForRuleBook.TriggerHandler.triggeredAbilities.Find(x => x.Item1 == BitterEnemies.ID)?.Item2 as BitterEnemies;
                    if (com != null && !string.IsNullOrEmpty(com.TargetName)) {
                        string displayName = com.TargetDisplayedName ?? "[nameless]";
                        string ending;
                        if (com.TargetTribes != null && com.TargetTribes.Count > 0) {
                            StringBuilder builder = new();
                            foreach (Tribe tribe in com.TargetTribes) {
                                if (builder.Length > 0) {
                                    builder.Append(", ");
                                }
                                if (TribeManager.IsCustomTribe(tribe)) {
                                    TribeManager.TribeInfo info = TribeManager.NewTribes.First(x => x.tribe == tribe);
                                    builder.Append(info?.name ?? info.tribe.ToString());
                                }
                                else {
                                    builder.Append(tribe.ToString());
                                }
                            }

                            ending = string.Format(",or belonging to the {0} Tribe(s).", builder.ToString());
                        }
                        else {
                            ending = ".";
                        }
                        return string.Format(VENDETTA_FORMAT, displayName, ending);
                    }
                }
            }

            return originalDesc;
        }

        public static void CreateNewMechanicPage(string name, string description, Texture texture) {
            RuleBookPageInfo pageInfo = new() {
                pageId = "wstl:Mechanic_" + name
            };
            NewMechanicPages.Add(new(pageInfo, name, description, texture));
        }

        internal static ObservableCollection<Tuple<RuleBookPageInfo, string, string, Texture>> NewMechanicPages = new();

        public static AbilityManager.FullAbility SetMechanicRedirect(this AbilityManager.FullAbility ability, string redirect, string pageName, Color redirectColour) {
            return ability.SetUniqueRedirect(redirect, "wstl:Mechanic_" + pageName, redirectColour);
        }
        public static AbilityInfo SetMechanicRedirect(this AbilityInfo ability, string redirect, string pageName, Color redirectColour) {
            return ability.SetUniqueRedirect(redirect, "wstl:Mechanic_" + pageName, redirectColour);
        }
    }
}
