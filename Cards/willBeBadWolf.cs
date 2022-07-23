using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void WillBeBadWolf_F0258()
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
                3, 2, 2, 0,
                Resources.willBeBadWolf, Resources.willBeBadWolf_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isChoice: true, riskLevel: 4);
        }
    }
}