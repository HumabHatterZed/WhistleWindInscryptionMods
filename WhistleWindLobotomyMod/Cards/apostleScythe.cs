using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleScythe_T0346()
        {
            List<Ability> abilities = new()
            {
                Ability.DoubleStrike,
                Apostle.ability
            };
            List<Tribe> tribes = new() { TribeDivine };
            List<Trait> traits = new() { TraitApostle };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhiteEmission.appearance
            };
            CreateCard(
                "wstl_apostleScythe", "Scythe Apostle",
                "The time has come.",
                atk: 2, hp: 6,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleScythe, Artwork.apostleScythe_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                appearances: appearances, modTypes: ModCardType.EventCard);

            abilities = new()
            {
                Ability.PreventAttack,
                Apostle.ability
            };
            CreateCard(
                "wstl_apostleScytheDown", "Scythe Apostle",
                "The time has come.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleScytheDown, Artwork.apostleScytheDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                appearances: appearances, modTypes: ModCardType.EventCard);
        }
    }
}