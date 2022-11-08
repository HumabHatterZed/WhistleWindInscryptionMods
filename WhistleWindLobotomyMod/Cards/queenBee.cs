using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_QueenBee_T0450()
        {
            List<Ability> abilities = new()
            {
                QueenNest.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            CardHelper.CreateCard(
                "wstl_queenBee", "Queen Bee",
                "A monstrous amalgam of a hive and a bee.",
                atk: 0, hp: 6,
                blood: 2, bones: 0, energy: 0,
                Artwork.queenBee, Artwork.queenBee_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}