using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void KnightOfDespair_O0173()
        {
            List<Ability> abilities = new()
            {
                Ability.SplitStrike,
                Piercing.ability
            };

            WstlUtils.Add(
                "wstl_knightOfDespair", "The Knight of Despair",
                "Failing to protect, she realised nothing was ever truly upheld.",
                2, 4, 2, 0,
                Resources.knightOfDespair,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), emissionTexture: Resources.knightOfDespair_emission,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}