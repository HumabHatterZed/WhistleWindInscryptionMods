﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BehaviourAdjustment_O0996()
        {
            List<Ability> abilities = new()
            {
                Corrector.ability
            };
            CreateCard(
                "wstl_behaviourAdjustment", "Behaviour Adjustment",
                "A strange device made to 'fix' errant beasts. I do not see the point.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 3,
                Artwork.behaviourAdjustment, Artwork.behaviourAdjustment_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Teth);
        }
    }
}