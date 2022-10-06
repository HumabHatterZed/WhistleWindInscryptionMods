using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

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
                Resources.honouredMonk, Resources.honouredMonk_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, evolveName: "wstl_cloudedMonk", isDonator: true, riskLevel: 4);
        }
    }
}