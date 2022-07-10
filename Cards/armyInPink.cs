using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                Resources.armyInPink, Resources.armyInPink_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isRare: true, isDonator: true);
        }
    }
}