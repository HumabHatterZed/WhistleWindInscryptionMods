using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void Frozenheart_F0137()
        {
            List<Ability> abilities = new List<Ability>
            {
                FrozenHeart.ability
            };

            List<Trait> traits = new List<Trait>
            {
                Trait.Terrain
            };

            WstlUtils.Add(
                "wstl_frozenHeart", "Frozen Heart",
                "The palace was cold and lonely.",
                1, 0, 0, 0,
                Resources.frozenHeart,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                appearanceBehaviour: CardUtils.getTerrainAppearance);
        }
    }
}