﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_HonouredMonk_D01110()
        {
            Tribe customTribe = TribeHumanoid;

            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            LobotomyCardHelper.CreateCard(
                "wstl_cloudedMonk", "Clouded Monk",
                "A monk no more.",
                atk: 4, hp: 2,
                blood: 3, bones: 0, energy: 0,
                Artwork.cloudedMonk, Artwork.cloudedMonk_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                customTribe: customTribe);
            LobotomyCardHelper.CreateCard(
                "wstl_honouredMonk", "Honoured Monk",
                "A monk seeking enlightenment through good deeds. But surely there's a quicker way to nirvana...",
                atk: 2, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.honouredMonk, Artwork.honouredMonk_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw,
                modTypes: LobotomyCardHelper.ModCardType.Donator, evolveName: "wstl_cloudedMonk",
                customTribe: customTribe);
        }
    }
}