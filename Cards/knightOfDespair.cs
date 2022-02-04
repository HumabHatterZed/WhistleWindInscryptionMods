using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void KnightOfDespair_O0173()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.SplitStrike
            };

            WstlUtils.Add(
                "wstl_knightOfDespair", "The Knight of Despair",
                "Failing to protect, she realised nothing was ever truly upheld.",
                4, 2, 0, 0,
                Resources.knightOfDespair,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(),
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}