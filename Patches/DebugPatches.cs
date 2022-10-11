using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System;
using InscryptionAPI.Card;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
	[HarmonyPatch(typeof(PaperGameMap), "TryInitializeMapData")]
	public class RunState_TryInitializeMapData
	{
		public static void Prefix(ref PaperGameMap __instance)
		{
			if (RunState.Run.map == null)
			{
				PredefinedNodes nodes = ScriptableObject.CreateInstance<PredefinedNodes>();
				PredefinedScenery predefinedScenery2 = ScriptableObject.CreateInstance<PredefinedScenery>();
				NodeData start = new();
				CardMergeNodeData merge = new();
				CardBattleNodeData battle = new();
				TradePeltsNodeData trader = new();
				nodes.nodeRows.Add(new() { start });
				nodes.nodeRows.Add(new() { merge });
				nodes.nodeRows.Add(new() { battle });
				//nodes.nodeRows.Add(new() { trader });
				__instance.PredefinedNodes = nodes;
			}
		}
	}
}
