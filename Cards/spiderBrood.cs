using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_SpiderBrood_O0243()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedRed.appearance
            };

            CardHelper.CreateCard(
                "wstl_spiderBrood", "Spider Brood",
                "Big and mean.",
                1, 3, 1, 0,
                Artwork.spiderBrood, Artwork.spiderBrood_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances);
        }
    }
}