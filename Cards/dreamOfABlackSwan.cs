using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_DreamOfABlackSwan_F0270()
        {
            List<Ability> abilities = new()
            {
                Nettles.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };
            CardHelper.CreateCard(
                "wstl_dreamOfABlackSwan", "Dream of a Black Swan",
                "The sister of six brothers. She worked tirelessly to protect them, all for naught.",
                2, 5, 3, 0,
                Resources.dreamOfABlackSwan, Resources.dreamOfABlackSwan_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isRare: true, riskLevel: 4);
        }
    }
}