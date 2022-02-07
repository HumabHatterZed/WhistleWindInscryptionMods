using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void WorkerBee_T0450()
        {
            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_workerBee", "Worker Bee",
                "A blind servant of the hive.",
                1, 1, 0, 0,
                Resources.workerBee,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                tribes: tribes, evolveId: new EvolveIdentifier("wstl_queenBee", 1));
        }
    }
}