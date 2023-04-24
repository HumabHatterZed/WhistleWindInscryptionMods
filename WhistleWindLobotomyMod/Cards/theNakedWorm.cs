using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheNakedWorm_O0274()
        {
            List<Tribe> tribes = new() { Tribe.Insect };
            List<Trait> traits = new() { NakedSerpent };

            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_theNakedWorm", "Naked Worm",
                "It can enter your body through any aperture.",
                atk: 1, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.theNakedWorm, pixelTexture: Artwork.theNakedWorm_pixel,
                abilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits);
        }
    }
}