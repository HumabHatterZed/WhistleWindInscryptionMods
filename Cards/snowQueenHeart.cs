using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void SnowQueenIceHeart_F0137()
        {
            List<Ability> abilities = new()
            {
                FrozenHeart.ability
            };

            List<Trait> traits = new()
            {
                Trait.Terrain
            };

            WstlUtils.Add(
                "wstl_snowQueenIceHeart", "Frozen Heart",
                "The palace was cold and lonely.",
                0, 1, 0, 0,
                Resources.snowQueenIceHeart,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                appearanceBehaviour: CardUtils.getTerrainAppearance);
        }
    }
}