using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleStaff_T0346()
        {
            Tribe customTribe = TribeDivine;
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
                ForcedWhiteEmission.appearance
            };
            CreateCard(
                "wstl_apostleStaff", "Staff Apostle",
                "The time has come.",
                atk: 3, hp: 6,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleStaff, Artwork.apostleStaff_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, modTypes: ModCardType.EventCard, customTribe: customTribe);

            abilities = new()
            {
                Ability.PreventAttack,
                Apostle.ability
            };
            CreateCard(
                "wstl_apostleStaffDown", "Staff Apostle",
                "The time has come.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleStaffDown, Artwork.apostleStaffDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, modTypes: ModCardType.EventCard, customTribe: customTribe);
        }
    }
}