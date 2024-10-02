using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionAPI.RuleBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils.Core
{
    public static class MechanicPages
    {
        internal static void AddMechanicEntries()
        {
            CreateNewMechanicPage(
                "Speed",
                "Determines the order that cards attack in during combat. Player-owned cards have a base Speed of 3 while opponent-owned cards have a base Speed of 0.",
                TextureLoader.LoadTextureFromFile("sigilSpeed.png", AbnormalPlugin.Assembly)
            );

            RuleBookManager.New(
                modGuid: AbnormalPlugin.pluginGuid,
                pageType: PageRangeType.Abilities,
                subsectionName: "Mechanics",
                getInsertPositionFunc: GetInsertPosition,
                createPagesFunc: CreatePages,
                fillPageAction: FillPage);
        }

        private static int GetInsertPosition(PageRangeInfo pageRangeInfo, List<RuleBookPageInfo> pages)
        {
            return pages.FindLastIndex(rbi => rbi.pagePrefab == RuleBookController.Instance.bookInfo.pageRanges.Find(x => x.type == PageRangeType.Items).rangePrefab) + 1;
        }
        private static List<RuleBookPageInfo> CreatePages(RuleBookInfo instance, PageRangeInfo currentRange, AbilityMetaCategory metaCategory) => NewMechanicPages.Select(x => x.Item1).ToList();
        
        private static void FillPage(RuleBookPage page, string pageId, object[] otherArgs)
        {
            if (page is AbilityPage abilityPage)
            {
                string name = pageId.Replace("wstl:Mechanic_", "");
                Tuple<RuleBookPageInfo, string, string, Texture> mechanic = NewMechanicPages.FirstOrDefault(x => x.Item2 == name);
                abilityPage.mainAbilityGroup.nameTextMesh.text = mechanic.Item2;
                abilityPage.mainAbilityGroup.descriptionTextMesh.text = mechanic.Item3;
                abilityPage.mainAbilityGroup.iconRenderer.material.mainTexture = mechanic.Item4;
            }
        }

        public static void CreateNewMechanicPage(string name, string description, Texture texture)
        {
            RuleBookPageInfo pageInfo = new()
            {
                pageId = "wstl:Mechanic_" + name
            };
            NewMechanicPages.Add(new(pageInfo, name, description, texture));
        }

        internal static ObservableCollection<Tuple<RuleBookPageInfo, string, string, Texture>> NewMechanicPages = new();
    }
}
