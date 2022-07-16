using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void MeltingLoveMinion_D03109()
        {
            List<Ability> abilities = new()
            {
                Slime.ability
            };

            CardHelper.CreateCard(
                "wstl_meltingLoveMinion", "Slime",
                "Don't let your beasts get too close now.",
                0, 0, 0, 0,
                Resources.meltingLoveMinion, Resources.meltingLoveMinion_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), riskLevel: 5);
        }
    }
}