using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;
using System.Linq;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// The second-strongest Green Ordeal.
    /// Start with a Dusk Ordeal on the board. Once it has been killed, create the next one after the player's turn ends or immediately depending on the context.
    /// Cards required: 2
    /// Valid regions: 2
    /// </summary>
    public class OrdealGreenDusk : OrdealBattleSequencer
    {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            List<CardInfo> info = new() { null, null, null, CardLoader.GetCardByName(Cards.whereWeReach) };

            encounterData.Blueprint.AddTurn().AddTurn().AddTurn();
            for (int i = 0; i < 13 - baseDifficulty; i++) {
                encounterData.Blueprint.AddTurn();
            }
            encounterData.Blueprint.AddTurn(EncounterManager.NewCardBlueprint(Cards.whereWeReach));
            
            if (encounterData.Difficulty > 13) {
                int extraHealth = encounterData.Difficulty - 13;
                info[3].Mods.Add(new(0, extraHealth));
                encounterData.Blueprint.turns.Last()[0].card.Mods.Add(new(0, extraHealth));
            }

            info.Randomize();
            EncounterData.StartCondition start = new() {
                cardsInOpponentSlots = info.ToArray()
            };
            encounterData.startConditions.Add(start);

            LobotomyPlugin.Log.LogInfo($"[Green Dusk] Base: {baseDifficulty} Diff: {encounterData.Difficulty}");
            ValidCards.Add(Cards.whereWeReach);
            return 2;
        }
    }
}