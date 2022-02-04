using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BlockOfIce_F0137()
        {
            List<Trait> traits = new List<Trait>
            {
                Trait.Terrain
            };

            WstlUtils.Add(
                "wstl_blockOfIce", "Block of Ice",
                "The palace was cold and lonely.",
                1, 0, 0, 0,
                Resources.blockOfIce,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                appearanceBehaviour: CardUtils.getTerrainAppearance);
        }
    }
}