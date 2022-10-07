﻿using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_TrainingDummy_00000()
        {
            CardHelper.CreateCard(
                "wstl_trainingDummy", "Standard Training-Dummy Rabbit",
                "A beast in the shape of a training dummy. The safest of these abnormal creatures.",
                0, 1, 0, 1,
                Artwork.trainingDummy, Artwork.trainingDummy_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Zayin);
        }
    }
}