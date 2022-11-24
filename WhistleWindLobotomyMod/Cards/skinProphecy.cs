﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SkinProphecy_T0990()
        {
            List<Ability> abilities = new()
            {
                Witness.ability
            };
            CardHelper.CreateCard(
                "wstl_skinProphecy", "Skin Prophecy",
                "A holy book. Its believers wrapped it in skin to preserve its sanctity.",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.skinProphecy, Artwork.skinProphecy_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}