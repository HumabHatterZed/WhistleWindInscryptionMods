using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ApostleMoleman_T0346()
        {
            List<Ability> abilities = new()
            {
                Apostle.ability,
                Ability.Reach,
                Ability.WhackAMole
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
                "wstl_apostleMoleman", "Moleman Apostle",
                "The time has come.",
                1, 8, 0, 0,
                Artwork.apostleMoleman, Artwork.apostleMoleman_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: metaCategories, tribes: new(), traits: traits,
                appearances: appearances,
                choiceType: CardHelper.ChoiceType.Rare, metaType: CardHelper.MetaType.NonChoice);
        }
    }
}