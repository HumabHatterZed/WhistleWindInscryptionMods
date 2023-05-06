using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace ModDebuggingMod
{
    // Adds predefined nodes for testing
    [HarmonyPatch]
    public static class DebugPatches
    {
        [HarmonyPatch(typeof(PaperGameMap), "TryInitializeMapData")]
        public static void Prefix(ref PaperGameMap __instance)
        {
            if (RunState.Run.map != null)
                return;

            PredefinedNodes nodes = ScriptableObject.CreateInstance<PredefinedNodes>();
            List<List<NodeData>> nodeRows = new()
            {
                new() { StartNode },
                new() { BattleNode },
                new() { StatNode }
            };

            nodes.nodeRows.AddRange(nodeRows);
            __instance.PredefinedNodes = nodes;
        }

        [HarmonyPatch(typeof(Part3SaveData), nameof(Part3SaveData.Initialize))]
        private static void Postfix(ref Part3SaveData __instance)
        {
            /*            __instance.deck.Cards.Clear();
                        __instance.deck.AddCard(CardLoader.GetCardByName("arackulele.inscryption.grimoramod Combustor"));
                        __instance.deck.AddCard(CardLoader.GetCardByName("arackulele.inscryption.grimoramod UndeadCivilian"));
                        __instance.deck.AddCard(CardLoader.GetCardByName("arackulele.inscryption.grimoramod UndeadVillage"));
                        __instance.deck.AddCard(CardLoader.GetCardByName("arackulele.inscryption.grimoramod Undertaker"));*/
        }

        [HarmonyPatch(typeof(SaveFile), nameof(SaveFile.ResetPart1Run))]
        public static void Postfix(SaveFile __instance) => __instance.currentRun.consumables = customItems;

        //        [HarmonyPatch(typeof(AscensionSaveData), nameof(AscensionSaveData.NewRun))]
        //        public static void Postfix(AscensionSaveData __instance) => __instance.currentRun.consumables = customItems;

        private static NodeData StartNode => new();
        private static CopyCardNodeData CopyNode => new();
        private static CardMergeNodeData MergeNode => new();
        private static BuildTotemNodeData TotemNode => new();
        private static CardBattleNodeData BattleNode => new();
        private static TradePeltsNodeData TraderNode => new();
        private static CardStatBoostNodeData StatNode => new();
        private static CardChoicesNodeData ChoiceNode => new();
        private static CardChoicesNodeData CostChoice => new() { choicesType = CardChoicesType.Cost };
        private static CardChoicesNodeData TribeChoice => new() { choicesType = CardChoicesType.Tribe };
        private static DuplicateMergeNodeData DupeNode => new();
        private static GainConsumablesNodeData ItemNode => new();
        private static readonly string LobGuid = WhistleWindLobotomyMod.LobotomyPlugin.pluginGuid;
        private static readonly List<string> customItems = new()
        {
            LobGuid + "_" + "BottledTrain",
            LobGuid + "_" + "BottledTrain",
            LobGuid + "_" + "BottledTrain"
        };
    }
}
