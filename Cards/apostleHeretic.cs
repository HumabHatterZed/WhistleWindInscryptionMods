using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ApostleHeretic_T0346()
        {
            List<Ability> abilities = new()
            {
                Confession.ability
            };
            List<Trait> traits = new()
            {
                Trait.Uncuttable
            };
            List<CardMetaCategory> metaCategories = new()
            {
                CardHelper.CANNOT_BE_SACRIFICED
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };

            CardHelper.CreateCard(
                "wstl_apostleHeretic", "Heretic",
                "The time has come.",
                0, 7, 0, 0,
                Artwork.apostleHeretic, Artwork.apostleHeretic_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: metaCategories, tribes: new(), traits: traits,
                appearances: appearances);
        }
    }
}