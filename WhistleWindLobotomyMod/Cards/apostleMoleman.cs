using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleMoleman_T0346()
        {
            List<Ability> abilities = new()
            {
                Apostle.ability,
                Ability.Reach,
                Ability.WhackAMole
            };
            List<Tribe> tribes = new() { TribeDivine };
            List<Trait> traits = new() { TraitApostle };

            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhiteEmission.appearance
            };
            CreateCard(
                "wstl_apostleMoleman", "Moleman Apostle",
                "The time has come.",
                atk: 1, hp: 8,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleMoleman, Artwork.apostleMoleman_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                appearances: appearances,
                choiceType: CardHelper.CardChoiceType.Rare,
                metaTypes: CardHelper.CardMetaType.NonChoice,
                modTypes: ModCardType.EventCard);

            CreateCard(
                "wstl_apostleMolemanDown", "Moleman Apostle",
                "The time has come.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleMolemanDown, Artwork.apostleMolemanDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                appearances: appearances, evolveName: "wstl_apostleMoleman", numTurns: 2,
                modTypes: ModCardType.EventCard);
        }
    }
}