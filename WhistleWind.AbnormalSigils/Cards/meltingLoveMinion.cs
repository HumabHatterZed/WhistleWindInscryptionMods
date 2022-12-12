using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_MeltingLoveMinion_D03109()
        {
            List<Ability> abilities = new()
            {
                Slime.ability
            };
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_meltingLoveMinion", "Slime",
                "Don't let your beasts get too close now.",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 0,
                Artwork.meltingLoveMinion, Artwork.meltingLoveMinion_emission,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}
