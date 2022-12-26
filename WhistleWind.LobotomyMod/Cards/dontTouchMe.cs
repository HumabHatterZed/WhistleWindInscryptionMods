﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DontTouchMe_O0547()
        {
            List<Ability> abilities = new()
            {
                Punisher.ability,
                Ability.WhackAMole
            };
            LobotomyCardHelper.CreateCard(
                "wstl_dontTouchMe", "Don't Touch Me",
                "Don't touch it.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 5,
                Artwork.dontTouchMe, Artwork.dontTouchMe_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic,
                riskLevel: LobotomyCardHelper.RiskLevel.Zayin,
                metaTypes: CardHelper.CardMetaType.Terrain,
                customTribe: TribeMachine);
        }
    }
}