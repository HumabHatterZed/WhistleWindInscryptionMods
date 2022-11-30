using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
            LobotomyCardHelper.CreateCard(
                "wstl_apostleMolemanDown", "Moleman Apostle",
                "The time has come.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleMolemanDown, Artwork.apostleMolemanDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, evolveName: "wstl_apostleMoleman", numTurns: 2);
        }
    }
}