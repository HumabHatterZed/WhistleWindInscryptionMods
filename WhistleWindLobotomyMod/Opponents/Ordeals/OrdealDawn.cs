using DiskCardGame;
using InscryptionAPI.Encounters;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    public class OrdealDawn : OrdealBattleSequencer
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "OrdealDawn", typeof(OrdealDawn)).Id;

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            return encounterData;
        }
    }
}