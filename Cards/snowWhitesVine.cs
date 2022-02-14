using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SnowWhitesVine_F0442()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp
            };

            List<Trait> traits = new()
            {
                Trait.Terrain
            };

            WstlUtils.Add(
                "wstl_snowWhitesVine", "Thorny Vines",
                "A vine.",
                0, 1, 0, 0,
                Resources.snowWhitesVine,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                appearanceBehaviour: CardUtils.getTerrainAppearance);
        }
    }
}