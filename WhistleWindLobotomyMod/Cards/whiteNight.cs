using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WhiteNight_T0346()
        {
            List<Ability> abilities = new()
            {
                TrueSaviour.ability,
                Idol.ability
            };
            List<Tribe> tribes = new() { TribeDivine };

            List<Trait> traits = new()
            {
                TraitApostle,
                Trait.Uncuttable,
                AbnormalPlugin.ImmuneToInstaDeath
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhiteEmission.appearance
            };
            CreateCard(
                "wstl_whiteNight", "WhiteNight",
                "The time has come.",
                atk: 0, hp: 66,
                blood: 0, bones: 0, energy: 0,
                Artwork.whiteNight, Artwork.whiteNight_emission, titleTexture: Artwork.whiteNight_title,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                appearances: appearances, modTypes: ModCardType.EventCard, onePerDeck: true);
        }
    }
}