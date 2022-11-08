using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WillBeBadWolf_F0258()
        {
            List<Ability> abilities = new()
            {
                BitterEnemies.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Canine
            };

            CardHelper.CreateCard(
                "wstl_willBeBadWolf", "Big and Will be Bad Wolf",
                "It is no coincidence that wolves are the villains of so many tales.",
                atk: 3, hp: 2,
                blood: 2, bones: 0, energy: 0,
                Artwork.willBeBadWolf, Artwork.willBeBadWolf_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}