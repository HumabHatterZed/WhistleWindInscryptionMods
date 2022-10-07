using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Ppodae_D02107()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy,
                Ability.Evolve
            };

            List<Tribe> tribes = new()
            {
                Tribe.Canine
            };
            CardHelper.CreateCard(
                "wstl_ppodae", "Ppodae",
                "An innocent little puppy.",
                1, 1, 0, 4,
                Resources.ppodae, Resources.ppodae_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isChoice: true, evolveName: "wstl_ppodaeBuff", isDonator: true, riskLevel: 2);
        }
    }
}