using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_HundredsGoodDeeds_O0303()
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
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };

            CardHelper.CreateCard(
                "wstl_hundredsGoodDeeds", "One Sin and Hundreds of Good Deeds",
                "Its hollow sockets see through you.",
                0, 777, 0, 0,
                Artwork.hundredsGoodDeeds, Artwork.hundredsGoodDeeds_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances);
        }
    }
}