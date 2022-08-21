using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void FleshIdol_T0979()
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
                Resources.fleshIdol, Resources.fleshIdol_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 4);
        }
    }
}