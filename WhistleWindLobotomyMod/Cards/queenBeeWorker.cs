using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void QueenBeeWorker_T0450()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            CardHelper.CreateCard(
                "wstl_queenBeeWorker", "Worker Bee",
                "A blind servant of the hive.",
                1, 1, 0, 1,
                Resources.queenBeeWorker, Resources.queenBeeWorker_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                evolveName: "wstl_queenBee");
        }
    }
}