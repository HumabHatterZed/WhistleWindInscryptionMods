using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleScythe_T0346()
        {
            Tribe customTribe = TribeDivine;
            List<Ability> abilities = new()
            {
                Ability.DoubleStrike,
                Apostle.ability
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
                "wstl_apostleScythe", "Scythe Apostle",
                "The time has come.",
                atk: 2, hp: 6,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleScythe, Artwork.apostleScythe_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, modTypes: LobotomyCardHelper.ModCardType.EventCard, customTribe: customTribe);

            abilities = new()
            {
                Ability.PreventAttack,
                Apostle.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_apostleScytheDown", "Scythe Apostle",
                "The time has come.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleScytheDown, Artwork.apostleScytheDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, modTypes: LobotomyCardHelper.ModCardType.EventCard, customTribe: customTribe);
        }
    }
}