using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
                atk: 1, hp: 1,
                blood: 0, bones: 3, energy: 0,
                Artwork.youreBald, Artwork.youreBald_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Zayin);
        }
    }
}