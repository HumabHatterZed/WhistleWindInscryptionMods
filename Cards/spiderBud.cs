using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpiderBud_O0243()
        {
            List<Ability> abilities = new()
            {
                BroodMother.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            CardHelper.CreateCard(
                "wstl_spiderBud", "Spider Bud",
                "A grotesque mother of spiders. Its children are small but grow quickly.",
                0, 2, 0, 4,
                Resources.spiderBud, Resources.spiderBud_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isChoice: true, riskLevel: 2);
        }
    }
}