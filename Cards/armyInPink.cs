using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ArmyInPink_D01106()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.DrawCopy,
                Ability.Evolve
            };

            WstlUtils.Add(
                "wstl_armyInPink", "Army in Pink",
                "Helpful little soldiers.",
                2, 2, 2, 0,
                Resources.armyInPink,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                appearanceBehaviour: CardUtils.getRareAppearance,
                evolveId: new EvolveIdentifier("wstl_armyInBlack", 1));
        }
    }
}