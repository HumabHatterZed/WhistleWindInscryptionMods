﻿using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_DreamingCurrent_T0271()
        {
            List<Ability> abilities = new()
            {
                Ability.Submerge,
                Ability.StrafeSwap
            };

            CardHelper.CreateCard(
                "wstl_dreamingCurrent", "The Dreaming Current",
                "A sickly child. Everyday it was fed candy that let it see the ocean.",
                4, 2, 3, 0,
                Resources.dreamingCurrent, Resources.dreamingCurrent_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 4);
        }
    }
}