using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void ArmyInBlack_D01106()
        {
            List<Ability> abilities = new()
            {
                Ability.GuardDog,
                Volatile.ability
            };

            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.RareCardBackground
            };

            WstlUtils.Add(
                "wstl_armyInBlack", "Army in Black",
                "Duty-bound.",
                2, 2, 2, 0,
                Resources.armyInBlack, Resources.armyInBlack_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances);
        }
    }
}