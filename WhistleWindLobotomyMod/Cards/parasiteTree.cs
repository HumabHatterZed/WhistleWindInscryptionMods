using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ParasiteTree_D04108()
        {
            List<Ability> abilities = new()
            {
                Gardener.ability
            };
            CardHelper.CreateCard(
                "wstl_parasiteTree", "Parasite Tree",
                "A beautiful tree. It wants only to help you and your beasts.",
                atk: 0, hp: 3,
                blood: 1, bones: 0, energy: 0,
                Artwork.parasiteTree, Artwork.parasiteTree_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw,
                metaTypes: CardHelper.MetaType.Donator);
        }
    }
}