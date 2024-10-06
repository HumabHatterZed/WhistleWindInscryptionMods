using DiskCardGame;
using InscryptionAPI.Encounters;
using InscryptionAPI.Nodes;
using InscryptionAPI.TalkingCards;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod
{
    public class OrdealNoon : OrdealBattleSequencer
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealNoon", typeof(OrdealNoon)).Id;

        public override Dictionary<OrdealType, List<string>> OrdealCards => throw new NotImplementedException();

        /*        public override Dictionary<OrdealType, List<Tuple<string, int>>> OrdealPointValues => new() {
{ OrdealType.Green, new() {
new("wstl_doubtProcess", 1) }},
{ OrdealType.Violet, new() {
new("wstl_grantUsLove", 5) }},
{ OrdealType.Crimson, new() {
new("wstl_skinCheers", 1),
new("wstl_skinHarmony", 2) }},
{ OrdealType.Amber, new() {
new("wstl_foodChain", 1) }},
{ OrdealType.Indigo, new() {
new("wstl_sweeper", 1) }},
{ OrdealType.White, new() {
new("wstl_fixerRed", 1),
new("wstl_fixerWhite", 1),
new("wstl_fixerBlack", 1) }}
};
public override int GetRequiredPointsForPlayerWin(int difficultyMod, int currentRegionTier)
{
return ordealType switch
{
OrdealType.Green => 4 + (difficultyMod + currentRegionTier) / 2,
OrdealType.Violet => 5 + (difficultyMod + currentRegionTier) / 2,
OrdealType.Crimson => 3 + difficultyMod + currentRegionTier,
OrdealType.Amber => 8 + difficultyMod + currentRegionTier,
OrdealType.Indigo => 4 + difficultyMod,
_ => 1,
};
}*/

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            return encounterData;
        }
    }
}