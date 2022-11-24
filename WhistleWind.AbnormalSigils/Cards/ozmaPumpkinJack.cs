using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_OzmaPumpkinJack_F04116()
        {
            AbnormalCardHelper.CreateCard(
                "wstl_ozmaPumpkinJack", "Jack",
                "A child borne of an orange gourd.",
                1, 3, 1, 0,
                Artwork.ozmaPumpkinJack, Artwork.ozmaPumpkinJack_emission,
                abilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), appearances: new());

            AbnormalCardHelper.CreateCard(
                "wstl_ozmaPumpkin", "Pumpkin",
                "An orange gourd.",
                0, 1, 0, 0,
                Artwork.ozmaPumpkin,
                abilities: new() { TargetGainSigils.ability },
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "wstl_ozmaPumpkinJack");
        }
    }
}