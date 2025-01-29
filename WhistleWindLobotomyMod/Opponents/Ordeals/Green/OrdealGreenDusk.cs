using DiskCardGame;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// Appears in R0
    /// Difficulty range: (1 - 3) +[0,2] // 4 difficulty is for boss node w/o challlenges
    /// </summary>
    public class OrdealGreenDusk : OrdealBattleSequencer
    {
        public void ConstructGreenDusk(EncounterData encounterData)
        {

        }

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            ConstructGreenDusk(encounterData);
            return encounterData;
        }
    }
}