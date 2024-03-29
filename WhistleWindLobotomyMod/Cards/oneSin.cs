﻿using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private const string oneSinName = "One Sin and Hundreds of Good Deeds";
        private const string oneSin = "oneSin";
        private void Card_OneSin_O0303()
        {
            NewCard(oneSin, oneSinName, "A floating skull. Its hollow sockets see through you.",
                attack: 0, health: 1, bones: 1, temple: CardTemple.Undead)
                .SetPortraits(oneSin)
                .AddAbilities(Martyr.ability)
                .AddTribes(TribeDivine)
                .SetDefaultEvolutionName(oneSinName)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}