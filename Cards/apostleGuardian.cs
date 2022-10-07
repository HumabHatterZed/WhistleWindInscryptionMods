using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ApostleGuardian_T0346()
        {
            List<Ability> abilities = new()
            {
                Apostle.ability
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
                "wstl_apostleGuardian", "Guardian Apostle",
                "The time has come.",
                4, 6, 0, 0,
                Artwork.apostleGuardian, Artwork.apostleGuardian_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: metaCategories, tribes: new(), traits: traits,
                appearances: appearances);
        }
    }
}