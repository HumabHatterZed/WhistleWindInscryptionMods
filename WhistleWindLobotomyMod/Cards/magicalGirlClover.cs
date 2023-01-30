﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MagicalGirlClover_O01111()
        {
            List<Ability> abilities = new()
            {
                Ability.DoubleStrike,
                Burning.ability,
                Burning.ability
            };
            List<Ability> abilities2 = new()
            {
                Burning.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Reptile
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {

            };
            LobotomyCardHelper.CreateCard(
                "wstl_servantOfWrath", "Servant of Wrath",
                "",
                atk: 2, hp: 4,
                blood: 1, bones: 0, energy: 0,
                Artwork.servantOfWrath, Artwork.servantOfWrath_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                onePerDeck: true);
            LobotomyCardHelper.CreateCard(
                "wstl_magicalGirlClover", "Magical Girl",
                "Blind protector of another world.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.magicalGirlClover, Artwork.magicalGirlClover_emission, pixelTexture: Artwork.magicalGirlClover_pixel,
                abilities: abilities2, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw,
                modTypes: LobotomyCardHelper.ModCardType.Ruina, customTribe: TribeFae);
        }
    }
}