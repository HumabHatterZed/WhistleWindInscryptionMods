﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_CanOfWellCheers_F0552()
        {
            CreateCard(
                "wstl_CRUMPLED_CAN", "Crumpled Can of WellCheers",
                "Soda can can soda dota 2 electric boo.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.skeleton_can,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "[name]Crumpled Can of Aged WellCheers");

            List<Ability> abilities = new()
            {
                Ability.Brittle,
                Ability.IceCube
            };
            CreateCard(
                "wstl_SKELETON_SHRIMP", "Skeleton Shrimp",
                "A dead shrimp man craving for a final drop of soda.",
                atk: 2, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.skeleton_shrimp, Artwork.skeleton_shrimp_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                iceCubeName: "wstl_CRUMPLED_CAN");

            abilities = new()
            {
                Ability.Strafe,
                Ability.Submerge
            };
            List<Tribe> tribes = new() { TribeMechanical };

            CreateCard(
                "wstl_canOfWellCheers", "Opened Can of WellCheers",
                "A vending machine dispensing ocean soda.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.canOfWellCheers, Artwork.canOfWellCheers_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Zayin,
                iceCubeName: "wstl_SKELETON_SHRIMP");
        }
    }
}