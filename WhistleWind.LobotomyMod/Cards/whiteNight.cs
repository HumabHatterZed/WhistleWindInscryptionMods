using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

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
            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Terrain,
                AbnormalPlugin.ImmuneToInstaDeath
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };
            LobotomyCardHelper.CreateCard(
                "wstl_whiteNight", "WhiteNight",
                "The time has come.",
                atk: 0, hp: 66,
                blood: 0, bones: 0, energy: 0,
                Artwork.whiteNight, Artwork.whiteNight_emission, titleTexture: Artwork.whiteNight_title,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, modTypes: LobotomyCardHelper.ModCardType.EventCard, onePerDeck: true,
                customTribe: TribeDivine);
        }
    }
}