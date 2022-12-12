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
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };

            List<Tribe> tribes = new();
            if (TribalAPI.Enabled)
                TribalAPI.AddTribalTribe(tribes, "plant");

            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_ozmaPumpkinJack", "Jack",
                "A child borne of an orange gourd.",
                atk: 1, hp: 3,
                blood: 1, bones: 0, energy: 0,
                Artwork.ozmaPumpkinJack, Artwork.ozmaPumpkinJack_emission,
                abilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(), appearances: new());
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_ozmaPumpkin", "Pumpkin",
                "An orange gourd.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.ozmaPumpkin,
                abilities: abilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                evolveName: "wstl_ozmaPumpkinJack");
        }
    }
}