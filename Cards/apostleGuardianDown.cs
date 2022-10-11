using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ApostleGuardianDown_T0346()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<Trait> traits = new()
            {
                Trait.Terrain
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance,
                EventBackground.appearance
            };

            CardHelper.CreateCard(
                "wstl_apostleGuardianDown", "Guardian Apostle",
                "The time has come.",
                0, 2, 0, 0,
                Artwork.apostleGuardianDown, Artwork.apostleGuardianDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, evolveName: "wstl_apostleGuardian");
        }
    }
}