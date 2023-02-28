using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleGuardian_T0346()
        {
            Tribe customTribe = TribeDivine;
            List<Ability> abilities = new()
            {
                Apostle.ability
            };
            List<Trait> traits = new()
            {
                TraitApostle
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhiteEmission.appearance
            };
            CreateCard(
                "wstl_apostleGuardian", "Guardian Apostle",
                "The time has come.",
                atk: 4, hp: 6,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleGuardian, Artwork.apostleGuardian_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, modTypes: ModCardType.EventCard,
                customTribe: customTribe);

            CreateCard(
                "wstl_apostleGuardianDown", "Guardian Apostle",
                "The time has come.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleGuardianDown, Artwork.apostleGuardianDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, evolveName: "wstl_apostleGuardian", numTurns: 2,
                modTypes: ModCardType.EventCard, customTribe: customTribe);
        }
    }
}