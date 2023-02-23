using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleMoleman_T0346()
        {
            Tribe customTribe = TribeDivine;
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
                ForcedWhiteEmission.appearance
            };
            CreateCard(
                "wstl_apostleMoleman", "Moleman Apostle",
                "The time has come.",
                atk: 1, hp: 8,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleMoleman, Artwork.apostleMoleman_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances,
                choiceType: CardHelper.CardChoiceType.Rare,
                metaTypes: CardHelper.CardMetaType.NonChoice,
                modTypes: ModCardType.EventCard, customTribe: customTribe);

            abilities = new()
            {
                Ability.Reach,
                Ability.Evolve
            };
            traits = new()
            {
                Trait.Terrain
            };
            CreateCard(
                "wstl_apostleMolemanDown", "Moleman Apostle",
                "The time has come.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleMolemanDown, Artwork.apostleMolemanDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, evolveName: "wstl_apostleMoleman", numTurns: 2,
                modTypes: ModCardType.EventCard, customTribe: customTribe);
        }
    }
}