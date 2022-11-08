using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleStaff_T0346()
        {
            List<Ability> abilities = new()
            {
                Ability.Sniper,
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
            CardHelper.CreateCard(
                "wstl_apostleStaff", "Staff Apostle",
                "The time has come.",
                atk: 3, hp: 6,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleStaff, Artwork.apostleStaff_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances);
        }
    }
}