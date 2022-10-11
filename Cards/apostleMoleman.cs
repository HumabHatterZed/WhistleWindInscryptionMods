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
                Trait.Uncuttable,
                Trait.Terrain
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.NonChoice,
                CardHelper.MetaType.EventCard
            };

            CardHelper.CreateCard(
                "wstl_apostleMoleman", "Moleman Apostle",
                "The time has come.",
                1, 8, 0, 0,
                Artwork.apostleMoleman, Artwork.apostleMoleman_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances,
                choiceType: CardHelper.ChoiceType.Rare, metaTypes: metaTypes);
        }
    }
}