using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_Pumpkin_F04116()
        {
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