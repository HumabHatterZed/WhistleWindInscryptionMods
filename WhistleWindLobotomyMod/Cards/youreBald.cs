using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

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
            CreateCard(
                "wstl_youreBald", "You're Bald...",
                "I've always wondered what it's like to be bald.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 2,
                Artwork.youreBald, Artwork.youreBald_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Zayin,
                evolveName: "{0}");
        }
    }
}