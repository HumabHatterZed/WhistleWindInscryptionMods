using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

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
                Artwork.dreamOfABlackSwan, Artwork.dreamOfABlackSwan_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}