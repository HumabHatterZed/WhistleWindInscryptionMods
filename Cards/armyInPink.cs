using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ArmyInPink_D01106()
        {
            List<Ability> abilities = new()
            {
                Ability.DrawCopyOnDeath,
                Protector.ability
            };

            WstlUtils.Add(
                "wstl_armyInPink", "Army in Pink",
                "The human heart is pink. They wear its colour as to blend in with your mind.",
                2, 2, 2, 0,
                Resources.armyInPink,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                emissionTexture: Resources.armyInPink_emission,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}