using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void QueenBee_T0450()
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
                0, 5, 2, 0,
                Resources.queenBee, Resources.queenBee_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isChoice: true, riskLevel: 4);
        }
    }
}