using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_OzmaPumpkinJack_F04116()
        {
            List<Tribe> tribes = new();
            if (TribalAPI.Enabled)
                tribes.Add(TribalAPI.AddTribal("botanic"));

            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_ozmaPumpkinJack", "Jack",
                "A child borne of an orange gourd.",
                atk: 2, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.ozmaPumpkinJack, Artwork.ozmaPumpkinJack_emission, Artwork.ozmaPumpkinJack_pixel,
                abilities: new() { Cursed.ability },
                metaCategories: new(), tribes: tribes, traits: new(), appearances: new());
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_ozmaPumpkin", "Pumpkin",
                "An orange gourd.",
                atk: 0, hp: 4,
                blood: 0, bones: 0, energy: 0,
                Artwork.ozmaPumpkin, pixelTexture: Artwork.ozmaPumpkin_pixel,
                abilities: new() { Ability.Evolve },
                metaCategories: new(), tribes: tribes, traits: new(),
                evolveName: "wstl_ozmaPumpkinJack", numTurns: 2);
        }
    }
}