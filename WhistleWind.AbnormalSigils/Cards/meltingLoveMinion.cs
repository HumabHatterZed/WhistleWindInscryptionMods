﻿using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_MeltingLoveMinion_D03109()
        {
            const string meltingLoveMinion = "meltingLoveMinion";
            CreateCard(MakeCard(
                cardName: meltingLoveMinion,
                "Slime")
                .SetPortraits(meltingLoveMinion)
                .AddAbilities(Slime.ability));
        }
    }
}
