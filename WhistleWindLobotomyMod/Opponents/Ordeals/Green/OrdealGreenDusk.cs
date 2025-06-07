using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// Appears in R0
    /// Difficulty range: (1 - 3) +[0,2] // 4 difficulty is for boss node w/o challlenges
    /// </summary>
    public class OrdealGreenDusk : OrdealBattleSequencer
    {
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty) {
            ValidCards.Add(Cards.whereWeReach);
            List<CardInfo> info = new() { null, null, null, CardLoader.GetCardByName(Cards.whereWeReach) };
            info.Randomize();
            EncounterData.StartCondition start = new() {
                cardsInOpponentSlots = info.ToArray()
            };
            encounterData.startConditions.Add(start);
            return 2;
        }
    }
}