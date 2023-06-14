﻿using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BehaviourAdjustment_O0996()
        {
            const string behaviourAdjustment = "behaviourAdjustment";

            CardInfo behaviour = NewCard(
                behaviourAdjustment,
                "Behaviour Adjustment",
                "A device made to 'fix' errant beasts. Its idea of 'fixing' might not be yours, however.",
                attack: 0, health: 1, blood: 3)
                .SetPortraits(behaviourAdjustment)
                .AddAbilities(Corrector.ability)
                .AddTribes(TribeMechanical);

            CreateCard(behaviour, CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}