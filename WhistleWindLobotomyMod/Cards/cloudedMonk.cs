using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_CloudedMonk_D01110()
        {
            CardHelper.CreateCard(
                "wstl_cloudedMonk", "Clouded Monk",
                "A monk no more.",
                atk: 4, hp: 2,
                blood: 3, bones: 0, energy: 0,
                Artwork.cloudedMonk, Artwork.cloudedMonk_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}