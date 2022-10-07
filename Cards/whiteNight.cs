﻿using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                Trait.Uncuttable
            };
            List<CardMetaCategory> metaCategories = new()
            {
                CardHelper.CANNOT_BE_SACRIFICED
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };
            CardHelper.CreateCard(
                "wstl_whiteNight", "WhiteNight",
                "The time has come.",
                0, 666, 0, 0,
                Artwork.whiteNight, Artwork.whiteNight_emission, titleTexture: Artwork.whiteNight_title,
                abilities: abilities, specialAbilities: new(),
                metaCategories: metaCategories, tribes: new(), traits: traits,
                appearances: appearances, onePerDeck: true);
        }
    }
}