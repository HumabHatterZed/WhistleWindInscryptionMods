﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
                atk: 4, hp: 2,
                blood: 3, bones: 0, energy: 0,
                Artwork.dreamingCurrent, Artwork.dreamingCurrent_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}