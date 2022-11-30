using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleGuardian_T0346()
        {
            List<Ability> abilities = new()
            {
                Apostle.ability
            };
            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Terrain
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance,
                EventBackground.appearance
            };
            LobotomyCardHelper.CreateCard(
                "wstl_apostleGuardian", "Guardian Apostle",
                "The time has come.",
                atk: 4, hp: 6,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleGuardian, Artwork.apostleGuardian_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances);
        }
    }
}