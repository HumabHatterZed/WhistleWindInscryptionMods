using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateMeanMice()
        {
            // Mean Mouse
            CardManager.New(pluginPrefix, "mouseMean", "Mean Mouse", 1, 1, "Don't get too close to this mouse and its pepper spray.")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(1)
                .SetPortraitAndEmission(GetTexture("mouseMean.png"), GetTexture("mouseMean_emission.png"))
                .SetPixelPortrait(GetTexture("mouseMean_pixel.png"))
                .AddAbilities(Ability.Sentry);

            CardInfo napper = CardManager.New(pluginPrefix, "mousenapper", "Mousenapper", 1, 2, "DON'T GET TOO CLOSE TO THIS MOUSE.")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(4)
                .SetPortraitAndEmission(GetTexture("mousenapper.png"), GetTexture("mousenapper_emission.png"))
                .SetPixelPortrait(GetTexture("mousenapper_pixel.png"));

            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Hook Line And Sinker");
                napper.AddAbilities(ability);
            }
            else
            {
                napper.AddAbilities(Ability.DrawRabbits);
            }
        }
    }
}
