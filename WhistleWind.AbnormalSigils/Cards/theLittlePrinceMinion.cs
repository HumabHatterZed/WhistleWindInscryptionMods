using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_TheLittlePrinceMinion_O0466()
        {
            AbnormalCardHelper.CreateCard(
                "wstl_theLittlePrinceMinion", "Spore Mold Creature",
                "A creature consumed by cruel, kind fungus.",
                0, 0, 0, 0,
                Artwork.theLittlePrinceMinion, Artwork.theLittlePrinceMinion_emission,
                abilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}