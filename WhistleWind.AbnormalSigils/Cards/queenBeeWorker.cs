using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_QueenBeeWorker_T0450()
        {
            // if Lobotomy Corp mod is enabled, 
            if (!LobMod.Enabled)
            {
                List<Ability> abilities = new() { Ability.Brittle };
                List<Tribe> tribes = new() { Tribe.Insect };
                AbnormalCardHelper.CreateCard(
                    "wstl_queenBeeWorker", "Worker Bee",
                    "A blind servant of the hive.",
                    1, 1, 0, 1,
                    Artwork.queenBeeWorker, Artwork.queenBeeWorker_emission,
                    abilities: abilities,
                    metaCategories: new(), tribes: tribes, traits: new());
            }
        }
    }
}