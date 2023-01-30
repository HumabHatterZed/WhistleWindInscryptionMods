using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_RANDOM_PLACEHOLDER()
        {
            LobotomyCardHelper.CreateCard(
                "wstl_RANDOM_PLACEHOLDER", "",
                "",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 0,
                Artwork.RANDOM_PLACEHOLDER, pixelTexture: Artwork.RANDOM_PLACEHOLDER_pixel,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), statIcon: SigilPower.icon);
        }
    }
}