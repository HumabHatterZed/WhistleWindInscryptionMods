using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void HundredsGoodDeeds_O0303()
        {
            List<Ability> abilities = new()
            {
                Confession.ability
            };

            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Terrain
            };

            CardHelper.CreateCard(
                "wstl_hundredsGoodDeeds", "One Sin and Hundreds of Good Deeds",
                "Its hollow sockets see through you.",
                0, 777, 0, 0,
                Resources.hundredsGoodDeeds, Resources.hundredsGoodDeeds_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits, riskLevel: 1);
        }
    }
}