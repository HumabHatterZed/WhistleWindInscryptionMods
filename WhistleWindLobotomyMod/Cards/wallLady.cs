﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WallLady_F0118()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp
            };
            LobotomyCardHelper.CreateCard(
                "wstl_wallLady", "The Lady Facing the Wall",
                "A deep sorrow, grown to obsession. Perhaps it's best to leave her be.",
                atk: 1, hp: 2,
                blood: 0, bones: 4, energy: 0,
                Artwork.wallLady, Artwork.wallLady_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Teth,
                customTribe: TribeHumanoid);
        }
    }
}