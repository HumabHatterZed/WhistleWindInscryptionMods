using DiskCardGame;
using EasyFeedback.APIs;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_OzmaPumpkinJack_F04116()
        {
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_ozmaPumpkinJack", "Jack",
                "A child borne of an orange gourd.",
                atk: 1, hp: 3,
                blood: 1, bones: 0, energy: 0,
                Artwork.ozmaPumpkinJack, Artwork.ozmaPumpkinJack_emission,
                abilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), appearances: new());

            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_ozmaPumpkin", "Pumpkin",
                "An orange gourd.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.ozmaPumpkin,
                abilities: abilities,
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "wstl_ozmaPumpkinJack");
        }
    }
}