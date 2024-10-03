using DiskCardGame;
using InscryptionAPI.Encounters;
using System;
using UnityEngine;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    // Ordeals require you to defeat a certain number of opponent cards; they utilise card repositioning, deck renewal, and bone harvesting
    public static class OrdealUtils
    {
        public static Opponent.Type OpponentID { get; internal set; }

        public static Texture2D[] DawnAnim;
        public static Texture2D[] DawnTotemAnim;
        public static Texture2D[] NoonAnim;
        public static Texture2D[] NoonTotemAnim;
        public static Texture2D[] DuskAnim;
        public static Texture2D[] DuskTotemAnim;
        public static Texture2D[] MidnightAnim;
        public static Texture2D[] MidnightTotemAnim;

        public static OrdealType ChooseRandomOrdealType(params OrdealType[] possibleOrdeals)
        {
            return possibleOrdeals[UnityEngine.Random.Range(0, possibleOrdeals.Length - 1)];
        }

        internal static void InitOrdeals()
        {
            OpponentID = OpponentManager.Add(
                LobotomyPlugin.pluginGuid, "OrdealOpponent",
                null, typeof(OrdealOpponent), null).Id;

            DawnAnim = NodeHelper.GetNodeTextureList("nodeOrdealDawn1", "nodeOrdealDawn2", "nodeOrdealDawn3", "nodeOrdealDawn4").ToArray();
            DawnTotemAnim = NodeHelper.GetNodeTextureList("nodeOrdealDawnTotem1", "nodeOrdealDawnTotem2", "nodeOrdealDawnTotem3", "nodeOrdealDawnTotem4").ToArray();
            NoonAnim = NodeHelper.GetNodeTextureList("nodeOrdealNoon1", "nodeOrdealNoon2", "nodeOrdealNoon3", "nodeOrdealNoon4").ToArray();
            NoonTotemAnim = NodeHelper.GetNodeTextureList("nodeOrdealNoonTotem1", "nodeOrdealNoonTotem2", "nodeOrdealNoonTotem3", "nodeOrdealNoonTotem4").ToArray();
            DuskAnim = NodeHelper.GetNodeTextureList("nodeOrdealDusk1", "nodeOrdealDusk2", "nodeOrdealDusk3", "nodeOrdealDusk4").ToArray();
            DuskTotemAnim = NodeHelper.GetNodeTextureList("nodeOrdealDuskTotem1", "nodeOrdealDuskTotem2", "nodeOrdealDuskTotem3", "nodeOrdealDuskTotem4").ToArray();
            MidnightAnim = NodeHelper.GetNodeTextureList("nodeOrdealMidnight1", "nodeOrdealMidnight2", "nodeOrdealMidnight3", "nodeOrdealMidnight4").ToArray();
            MidnightTotemAnim = NodeHelper.GetNodeTextureList("nodeOrdealMidnightTotem1", "nodeOrdealMidnightTotem2", "nodeOrdealMidnightTotem3", "nodeOrdealMidnightTotem4").ToArray();
        }
    }

    public class OrdealBattleNodeData : CardBattleNodeData
    {
        public bool totemOpponent;
        public int severity;
        public OrdealType ordealType;
    }
    
    public enum OrdealType
    {
        Green = 0,  // G G G G
        Crimson = 1,// C C C _
        Violet = 2, // V V _ V
        Amber = 3,  // A _ A A
        Indigo = 4, // _ I _ _
        White = 5   // W W W W
    }
}