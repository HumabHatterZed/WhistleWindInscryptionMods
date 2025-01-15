using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateMeanMice()
        {
            // Mean Mouse
            CardInfo mean = CardManager.New(pluginPrefix, "mouseMean", "Mean Mouse", 1, 1, "Don't get too close to this mouse and its pepper spray.")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(1)
                .SetPortraitAndEmission(GetTexture("mouseMean.png"), GetTexture("mouseMean_emission.png"))
                .SetPixelPortrait(GetTexture("mouseMean_pixel.png"))
                .AddAbilities(Ability.Sentry);

            CardInfo napper = CardManager.New(pluginPrefixG, "mousenapper", "Mousenapper", 1, 2, "DON'T GET TOO CLOSE TO THIS MOUSE.")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(4)
                .SetPortraitAndEmission(GetTexture("mousenapper.png"), GetTexture("mousenapper_emission.png"))
                .SetPixelPortrait(GetTexture("mousenapper_pixel.png"))
                .AddAbilities(ScrybeCompat.GetGrimoraAbility("Hook Line And Sinker", Ability.DrawRabbits));

            CardInfo bot = CardManager.New(pluginPrefix3, "anonymouse", "Anonymouse", 1, 1)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(2)
                .SetPortrait(GetTexture("anonymouse.png"))
                .SetPixelPortrait(GetTexture("anonymouse_pixel.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Arsonist", Ability.TailOnHit));

            if (ScrybeCompat.P03Enabled)
            {
                mean.AddMetaCategories(ScrybeCompat.NatureRegion);
                napper.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot.AddMetaCategories(ScrybeCompat.NeutralRegion);
            }
        }
    }
}
