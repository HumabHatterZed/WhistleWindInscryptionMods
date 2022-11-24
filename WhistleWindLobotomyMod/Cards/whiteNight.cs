﻿using DiskCardGame;
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
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                ImmuneToInstaDeath.specialAbility
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
                "wstl_whiteNight", "WhiteNight",
                "The time has come.",
                atk: 0, hp: 666,
                blood: 0, bones: 0, energy: 0,
                Artwork.whiteNight, Artwork.whiteNight_emission, titleTexture: Artwork.whiteNight_title,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, onePerDeck: true);
        }
    }
}