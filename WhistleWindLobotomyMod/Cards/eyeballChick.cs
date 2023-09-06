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
        private void Card_EyeballChick()
        {
            const string eyeballChick = "eyeballChick";

            NewCard(eyeballChick, "Eyeball Chick",
                attack: 1, health: 3)
                .SetPortraits(eyeballChick)
                .AddAbilities(BindingStrike.ability)
                .AddTribes(Tribe.Bird)
                .Build();
        }
    }
}