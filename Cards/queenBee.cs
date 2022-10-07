using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                0, 6, 2, 0,
                Artwork.queenBee, Artwork.queenBee_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}