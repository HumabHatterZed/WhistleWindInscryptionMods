using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WillBeBadWolf_F0258()
        {
            List<Ability> abilities = new() { BitterEnemies.ability };
            List<Tribe> tribes = new() { Tribe.Canine };
            
            CreateCard(
                "wstl_willBeBadWolf", "Big and Will Be Bad Wolf",
                "It is no coincidence that wolves are the villains of so many tales.",
                atk: 4, hp: 2,
                blood: 3, bones: 0, energy: 0,
                Artwork.willBeBadWolf, Artwork.willBeBadWolf_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw);
        }
    }
}