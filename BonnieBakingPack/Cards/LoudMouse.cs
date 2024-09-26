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
            
            CardInfo aka = CardManager.New(pluginPrefix, "akaMouso", "Aka Mouso", 2, 2, "RED PAPER OR BLUE PAPER?")
                .SetDefaultPart1Card().AddGrimora()
                .SetEnergyCost(5)
                .SetPortraitAndEmission(GetTexture("akaMouso.png"), GetTexture("akaMouso_emission.png"))
                .AddAbilities(ScrybeCompat.GetGrimoraAbility("Alternating Strike", Ability.DebuffEnemy));

            CardInfo bot = CardManager.New(pluginPrefix, "steambotWilly", "Steambot Willy", 3, 1)
                .SetDefaultPart3Card().AddP03().SetRare()
                .SetEnergyCost(6)
                .SetPortrait(GetTexture("steambotWilly.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Conduit Protector", Ability.BuffEnemy), Ability.Submerge);

            if (ScrybeCompat.P03Enabled)
            {
                if (OverrideAct1.Value.HasFlag(ActOverride.Act3))
                    loud.AddMetaCategories(ScrybeCompat.NatureRegion);

                if (OverrideGrimora.Value.HasFlag(ActOverride.Act3))
                    aka.AddMetaCategories(ScrybeCompat.UndeadRegion);

                bot.AddMetaCategories(ScrybeCompat.NatureRegion);
            }
        }
    }
}
