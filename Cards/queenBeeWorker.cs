using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_QueenBeeWorker_T0450()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            CardHelper.CreateCard(
                "wstl_queenBeeWorker", "Worker Bee",
                "A blind servant of the hive.",
                1, 1, 0, 1,
                Artwork.queenBeeWorker, Artwork.queenBeeWorker_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                evolveName: "wstl_queenBee");
        }
    }
}