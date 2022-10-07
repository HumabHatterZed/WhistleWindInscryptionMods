using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_HonouredMonk_D01110()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };

            CardHelper.CreateCard(
                "wstl_honouredMonk", "Honoured Monk",
                "A monk seeking enlightenment through good deeds. But surely there's a quicker way to nirvana...",
                2, 1, 2, 0,
                Artwork.honouredMonk, Artwork.honouredMonk_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isDonator: true,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw,
                evolveName: "wstl_cloudedMonk");
        }
    }
}