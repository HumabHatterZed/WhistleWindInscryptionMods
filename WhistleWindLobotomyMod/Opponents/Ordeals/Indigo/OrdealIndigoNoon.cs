using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents {
    public class OrdealIndigoNoon : OrdealBattleSequencer {
        private int seed = -1;
        public override void ModifyQueuedCard(PlayableCard card) {
            if (seed == -1) seed = base.GetRandomSeed();

            if (SeededRandom.Range(0, 15 - TurnManager.Instance.Opponent.Difficulty, seed++) == 0) {
                card.AddTemporaryMod(new(SeededRandom.Bool(seed++) ? 1 : 0, SeededRandom.Bool(seed++) ? 1 : 0));
            }
        }
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            int numTurns = (encounterData.Difficulty - 1) / 2;
            for (int i = 0; i < numTurns; i++) {
                if (i == 0 || i == 2 || i == 4) encounterData.Blueprint.AddTurn();

                List<EncounterBlueprintData.CardBlueprint> turn = new()
                {
                    EncounterManager.NewCardBlueprint(Cards.sweeper),
                    EncounterManager.NewCardBlueprint(Cards.sweeper)
                };
                encounterData.Blueprint.AddTurn(turn);
            }
            return -1;
        }
    }
}