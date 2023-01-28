using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_RANDOM_PLACEHOLDER()
        {
            CardHelper.CreateCard(
                "wstl_RANDOM_PLACEHOLDER", "",
                "",
                baseAttack: 0, baseHealth: 0,
                0, 0,
                defaultTexture: Resources.RANDOM_PLACEHOLDER, gbcTexture: Resources.RANDOM_PLACEHOLDER_pixel,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}