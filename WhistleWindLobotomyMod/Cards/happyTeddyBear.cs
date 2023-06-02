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
        private void Card_HappyTeddyBear_T0406()
        {
            List<Ability> abilities = new() { Ability.GuardDog };

            CreateCard(
                "wstl_happyTeddyBear", "Happy Teddy Bear",
                "Its memories began with a warm hug.",
                atk: 1, hp: 6,
                blood: 0, bones: 6, energy: 0,
                Artwork.happyTeddyBear, Artwork.happyTeddyBear_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He,
                evolveName: "Big, {0}");
        }
    }
}