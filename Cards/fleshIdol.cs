using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_FleshIdol_T0979()
        {
            List<Ability> abilities = new()
            {
                GroupHealer.ability,
                Ability.BuffEnemy
            };

            CardHelper.CreateCard(
                "wstl_fleshIdol", "Flesh Idol",
                "Prayer inevitably ends with the worshipper's despair.",
                0, 2, 0, 3,
                Artwork.fleshIdol, Artwork.fleshIdol_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}