using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void QueenBeeWorker_T0450()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_queenBeeWorker", "Worker Bee",
                "A blind servant of the hive.",
                1, 1, 0, 0,
                Resources.queenBeeWorker,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                tribes: tribes,
                emissionTexture: Resources.queenBeeWorker_emission,
                evolveId: new EvolveIdentifier("wstl_queenBee", 1));
        }
    }
}