using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleMoleman_T0346()
        {
            string tribal = "divinebeast";
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
            LobotomyCardHelper.CreateCard(
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
                modTypes: LobotomyCardHelper.ModCardType.EventCard, tribal: tribal);

            abilities = new()
            {
                Ability.Reach,
                Ability.Evolve
            };
            traits = new()
            {
                Trait.Terrain
            };
            LobotomyCardHelper.CreateCard(
                "wstl_apostleMolemanDown", "Moleman Apostle",
                "The time has come.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleMolemanDown, Artwork.apostleMolemanDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, evolveName: "wstl_apostleMoleman", numTurns: 2,
                modTypes: LobotomyCardHelper.ModCardType.EventCard, tribal: tribal);
        }
    }
}