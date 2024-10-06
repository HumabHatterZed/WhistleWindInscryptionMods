using DiskCardGame;
using EasyFeedback.APIs;
using Infiniscryption.P03KayceeRun.Encounters;
using InscryptionAPI.Encounters;
using System;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    public class OrdealDawn : OrdealBattleSequencer
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealDawn", typeof(OrdealDawn)).Id;

        public override Dictionary<OrdealType, List<string>> OrdealCards => throw new NotImplementedException();

        /*public override Dictionary<OrdealType, List<Tuple<string, int>>> OrdealPointValues => new() {
            { OrdealType.Green, new() {
                new("wstl_doubtA", 1),
                new("wstl_doubtB", 1),
                new("wstl_doubtY", 1),
                new("wstl_doubtO", 1) }},
            { OrdealType.Violet, new() {
                new("wstl_fruitUnderStanding", 1) }},
            { OrdealType.Crimson, new() {
                new("wstl_skinCheers", 1) }},
            { OrdealType.Amber, new() {
                new("wstl_perfectFood", 1) }},
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
                OrdealType.Green => 6 + difficultyMod,
                OrdealType.Violet => 3 + (difficultyMod + currentRegionTier) / 2,
                OrdealType.Crimson => 3 + difficultyMod + currentRegionTier,
                OrdealType.Amber => 7 + difficultyMod + currentRegionTier,
                OrdealType.Indigo => 4 + difficultyMod,
                _ => 1,
            };
        }*/

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            /*List<Tuple<string, int>> template1 = new()
            {
                new(CardNameOrReplacement("wstl_doubtA", "wstl_doubtB", 10), 1)
            };
            List<Tuple<string, int>> template2 = new()
            {
                new(CardNameOrReplacement("wstl_doubtA", "wstl_doubtB", 11), 1),
                new(CardNameOrReplacement("wstl_doubtA", "wstl_doubtB", 11), 1)
            };
            List<Tuple<string, int>> template3 = new()
            {
                new(CardNameOrReplacement("wstl_doubtB", "wstl_doubtY", 13), 1),
                new(CardNameOrReplacement("wstl_doubtY", "wstl_doubtO", 13), 1)
            };
            List<Tuple<string, int>> template4 = new()
            {
                new(CardNameOrReplacement("wstl_doubtB", "wstl_doubtY", 12), 1),
                new("wstl_doubtO", 1)
            };
            templateTurns.Add(template1);
            templateTurns.Add(template2);
            templateTurns.Add(template3);
            templateTurns.Add(template4);*/

            /*encounterData.Blueprint.AddTurns(
                EncounterManager.CreateTurn(EncounterManager.NewCardBlueprint(template1[0].Item1), EncounterManager.NewCardBlueprint(template1[1].Item1)),
                EncounterManager.CreateTurn(),
                EncounterManager.CreateTurn(EncounterManager.NewCardBlueprint(template2[1].Item1), EncounterManager.NewCardBlueprint(template2[1].Item1), EncounterManager.NewCardBlueprint(template2[2].Item1)),
                EncounterManager.CreateTurn(),
                EncounterManager.CreateTurn(EncounterManager.NewCardBlueprint(template3[0].Item1), EncounterManager.NewCardBlueprint(template3[1].Item1)),
                EncounterManager.CreateTurn(),
                EncounterManager.CreateTurn(EncounterManager.NewCardBlueprint(template4[0].Item1), EncounterManager.NewCardBlueprint(template4[1].Item1), EncounterManager.NewCardBlueprint(template4[2].Item1))
                );
*/
            encounterData.Blueprint.turns.Randomize();
            return encounterData;
        }
    }
}