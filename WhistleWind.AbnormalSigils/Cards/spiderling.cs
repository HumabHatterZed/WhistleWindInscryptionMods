using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_Spiderling_O0243()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedRed.appearance
            };
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_spiderBrood", "Spider Brood",
                "Big and mean.",
                atk: 1, hp: 3,
                blood: 0, bones: 3, energy: 0,
                Artwork.spiderBrood, Artwork.spiderBrood_emission,
                abilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);

            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_spiderling", "Spiderling",
                "Small and defenceless.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.spiderling, Artwork.spiderling_emission,
                abilities: abilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances, evolveName: "wstl_spiderBrood");
        }
    }
}