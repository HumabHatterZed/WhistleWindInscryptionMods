using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleSpear_T0346()
        {
            List<Ability> abilities = new()
            {
                Piercing.ability,
                Apostle.ability
            };
            List<Tribe> tribes = new() { TribeDivine };
            List<Trait> traits = new() { TraitApostle };

            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhiteEmission.appearance
            };
            CreateCard(
                "wstl_apostleSpear", "Spear Apostle",
                "The time has come.",
                atk: 4, hp: 6,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleSpear, Artwork.apostleSpear_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                appearances: appearances, modTypes: ModCardType.EventCard);

            abilities = new()
            {
                Ability.PreventAttack,
                Apostle.ability
            };
            CreateCard(
                "wstl_apostleSpearDown", "Spear Apostle",
                "The time has come.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleSpearDown, Artwork.apostleSpearDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                appearances: appearances, modTypes: ModCardType.EventCard);
        }
    }
}