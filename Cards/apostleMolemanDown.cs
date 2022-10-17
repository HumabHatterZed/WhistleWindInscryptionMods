using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ApostleMolemanDown_T0346()
        {
            List<Ability> abilities = new()
            {
                Ability.Reach,
                Ability.Evolve
            };
            List<Trait> traits = new()
            {
                Trait.Terrain
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance,
                RareEventBackground.appearance
            };

            CardHelper.CreateCard(
                "wstl_apostleMolemanDown", "Moleman Apostle",
                "The time has come.",
                0, 2, 0, 0,
                Artwork.apostleMolemanDown, Artwork.apostleMolemanDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, evolveName: "wstl_apostleMoleman", numTurns: 2);
        }
    }
}