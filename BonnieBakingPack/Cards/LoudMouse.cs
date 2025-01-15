using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateLoudMice()
        {
            // Loud Mouse
            CardInfo loud = CardManager.New(pluginPrefix, "mouseLoud", "Loud Mouse", 2, 1, "Some people don't know when to shut up.")
                .SetDefaultPart1Card().AddAct1()
                .SetBonesCost(3)
                .SetPortraitAndEmission(GetTexture("mouseLoud.png"), GetTexture("mouseLoud_emission.png"))
                .SetPixelPortrait(GetTexture("mouseLoud_pixel.png"))
                .AddAbilities(Ability.BuffEnemy);
            
            CardInfo aka = CardManager.New(pluginPrefixG, "akaMouso", "Aka Mouso", 2, 2, "RED PAPER OR BLUE PAPER?")
                .SetDefaultPart1Card().AddGrimora()
                .SetEnergyCost(5)
                .SetPortraitAndEmission(GetTexture("akaMouso.png"), GetTexture("akaMouso_emission.png"))
                .AddAbilities(ScrybeCompat.GetGrimoraAbility("Alternating Strike", Ability.DebuffEnemy));

            CardInfo bot = CardManager.New(pluginPrefix3, "steambotWilly", "Steambot Willy", 3, 1)
                .SetDefaultPart3Card().AddP03().SetRare()
                .SetEnergyCost(4)
                .SetPortrait(GetTexture("steambotWilly.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Fuel Strike", Ability.BuffEnemy), Ability.Submerge);

            ScrybeCompat.SetFuel(bot, 3);
            if (ScrybeCompat.P03Enabled)
            {
                loud.AddMetaCategories(ScrybeCompat.NatureRegion);
                aka.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot.AddMetaCategories(ScrybeCompat.NatureRegion);
            }
        }
    }
}
