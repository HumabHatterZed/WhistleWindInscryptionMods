using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_YoureBald_BaldIsAwesome()
        {
            List<Ability> abilities = new()
            {
                Ability.DrawCopy
            };
            CardHelper.CreateCard(
                "wstl_youreBald", "You're Bald...",
                "I've always wondered what it's like to be bald.",
                1, 1, 0, 3,
                Artwork.youreBald, Artwork.youreBald_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Zayin);
        }
    }
}