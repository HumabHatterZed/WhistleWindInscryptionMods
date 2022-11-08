using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
                atk: 2, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.honouredMonk, Artwork.honouredMonk_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw,
                metaTypes: CardHelper.MetaType.Donator, evolveName: "wstl_cloudedMonk");
        }
    }
}