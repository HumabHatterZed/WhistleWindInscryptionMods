using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
            LobotomyCardHelper.CreateCard(
                "wstl_apostleGuardianDown", "Guardian Apostle",
                "The time has come.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleGuardianDown, Artwork.apostleGuardianDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, evolveName: "wstl_apostleGuardian", numTurns: 2);
        }
    }
}