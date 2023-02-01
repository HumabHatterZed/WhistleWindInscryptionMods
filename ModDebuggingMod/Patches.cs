using DiskCardGame;
using HarmonyLib;
using UnityEngine;

namespace ModDebuggingMod
{
    // Adds predefined nodes for testing
    [HarmonyPatch]
    public class DebugPatches
    {
        private static string LobGuid => WhistleWindLobotomyMod.LobotomyPlugin.pluginGuid;

        [HarmonyPatch(typeof(PaperGameMap), "TryInitializeMapData")]
        public static void Prefix(ref PaperGameMap __instance)
        {
            if (RunState.Run.map == null)
            {
                PredefinedNodes nodes = ScriptableObject.CreateInstance<PredefinedNodes>();
                nodes.nodeRows.Add(new() { StartNode });
                nodes.nodeRows.Add(new() { TotemNode, BattleNode, TribeChoice });
                //nodes.nodeRows.Add(new() { TotemNode, TotemNode });
                //nodes.nodeRows.Add(new() { ItemNode, BattleNode });
                //nodes.nodeRows.Add(new() { ItemNode, MergeNode, BattleNode });
                //nodes.nodeRows.Add(new() { TraderNode });
                __instance.PredefinedNodes = nodes;
            }
        }

        [HarmonyPatch(typeof(SaveFile), nameof(SaveFile.ResetPart1Run))]
        public static void Postfix(SaveFile __instance)
        {
            __instance.currentRun.consumables = new();
            for (int i = 0; i < 3; i++)
            {
                __instance.currentRun.consumables.Add(LobGuid + "_" + "BottledTrain");
            }
        }
        [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.LoadFromFile))]
        public static void Postfix()
        {
            Plugin.Log.LogInfo("LoadFromFile");
        }

        [HarmonyPatch(typeof(AscensionSaveData), nameof(AscensionSaveData.NewRun))]
        public static void Postfix(AscensionSaveData __instance)
        {
            __instance.currentRun.consumables = new();
            for (int i = 0; i < 3; i++)
            {
                //__instance.currentRun.consumables.Add("wstl_DebugItem");
            }
        }

        private static NodeData StartNode => new();
        private static CardMergeNodeData MergeNode => new();
        private static CardBattleNodeData BattleNode => new();
        private static TradePeltsNodeData TraderNode => new();
        private static GainConsumablesNodeData ItemNode => new();
        private static CardStatBoostNodeData StatNode => new();
        private static CardChoicesNodeData ChoiceNode => new();
        private static CardChoicesNodeData TribeChoice => new() { choicesType = CardChoicesType.Tribe };
        private static CardChoicesNodeData CostChoice => new() { choicesType = CardChoicesType.Cost };
        private static BuildTotemNodeData TotemNode => new();
    }
}
