using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SpiderBud_O0243()
        {
            List<Ability> abilities = new()
            {
                BroodMother.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            LobotomyCardHelper.CreateCard(
                "wstl_spiderBud", "Spider Bud",
                "A grotesque mother of spiders. Its children are small but grow quickly.",
                atk: 0, hp: 2,
                blood: 0, bones: 4, energy: 0,
                Artwork.spiderBud, Artwork.spiderBud_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Teth);
        }
    }
}