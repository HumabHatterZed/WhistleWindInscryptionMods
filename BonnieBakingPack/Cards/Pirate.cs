using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreatePirates()
        {
            // Pirate
            CardManager.New(pluginPrefix, "pirate", "Pirate", 0, 1, "A scallywag with a great treasure. He reminds me of a certain someone...")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(1)
                .SetPortraitAndEmission(GetTexture("pirate.png"), GetTexture("pirate_emission.png"))
                .SetPixelPortrait(GetTexture("pirate_pixel.png"))
                .AddAbilities(Ability.Submerge, Ability.BoneDigger);

            CardInfo plunderer = CardManager.New(pluginPrefix, "plunderer", "Jolly Roger", 2, 2, "THERE IS NO GREATER JOY THAN THE FULFILLMENT OF GREED.")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(8)
                .SetPortraitAndEmission(GetTexture("plunderer.png"), GetTexture("plunderer_emission.png"))
                .AddAbilities(Ability.Loot);

            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Anchored");
                plunderer.AddAbilities(ability);
            }
        }
    }
}
