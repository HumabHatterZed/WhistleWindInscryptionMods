﻿using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void TrainingDummy_00000()
        {
            WstlUtils.Add(
                "wstl_trainingDummy", "Standard Training-Dummy Rabbit",
                "A creature in the shape of a training dummy. Perhaps the safest of these strange beasts.",
                1, 0, 0, 1,
                Resources.trainingDummy,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.trainingDummy_emission);
        }
    }
}