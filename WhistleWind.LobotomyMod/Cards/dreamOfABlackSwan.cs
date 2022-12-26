using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
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
            LobotomyCardHelper.CreateCard(
                "wstl_dreamOfABlackSwan", "Dream of a Black Swan",
                "The sister of six brothers. She worked tirelessly to protect them, all for naught.",
                atk: 2, hp: 5,
                blood: 3, bones: 0, energy: 0,
                Artwork.dreamOfABlackSwan, Artwork.dreamOfABlackSwan_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: LobotomyCardHelper.RiskLevel.Waw);
        }
    }
}