using InscryptionAPI;
using InscryptionAPI.Card;
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

            WstlUtils.Add(
                "wstl_queenBeeWorker", "Worker Bee",
                "A blind servant of the hive.",
                1, 1, 0, 0,
                Resources.queenBeeWorker, Resources.queenBeeWorker_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                evolveName: "wstl_queenBee");
        }
    }
}