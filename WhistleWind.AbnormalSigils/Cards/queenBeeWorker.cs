using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_QueenBeeWorker_T0450()
        {
            List<Ability> abilities = new() { Ability.Brittle };
            List<Tribe> tribes = new() { Tribe.Insect };
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_queenBeeWorker", "Worker Bee",
                "A blind servant of the hive.",
                atk: 1, hp: 1,
                blood: 0, bones: 1, energy: 1,
                Artwork.queenBeeWorker, Artwork.queenBeeWorker_emission,
                abilities: abilities,
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}