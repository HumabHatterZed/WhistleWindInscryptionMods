using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_TheLittlePrinceMinion_O0466()
        {
            CardHelper.CreateCard(
                pluginPrefix,
                "wstl_theLittlePrinceMinion", "Spore Mold Creature",
                "A creature consumed by cruel, kind fungus.",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 0,
                Artwork.theLittlePrinceMinion, Artwork.theLittlePrinceMinion_emission,
                abilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}