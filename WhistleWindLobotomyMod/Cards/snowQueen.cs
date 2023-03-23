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
        private void Card_SnowQueen_F0137()
        {
            List<Ability> abilities = new() { FrostRuler.ability };
            List<Tribe> tribes = new() { TribeFae };

            CreateCard(
                "wstl_snowQueen", "The Snow Queen",
                "A queen from far away. Those who enter her palace never leave.",
                atk: 1, hp: 2,
                blood: 0, bones: 5, energy: 0,
                Artwork.snowQueen, Artwork.snowQueen_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He,
                evolveName: "[name]The Snow Empress");
        }
    }
}