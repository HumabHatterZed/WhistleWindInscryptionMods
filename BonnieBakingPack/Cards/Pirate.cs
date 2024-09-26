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
        private void CreatePirates()
        {
            // Pirate
            CardInfo pirate = CardManager.New(pluginPrefix, "pirate", "Pirate", 0, 1, "A scallywag with a great treasure. He reminds me of a certain someone...")
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

            CardInfo bot = CardManager.New(pluginPrefix, "pirateTrojan", "Trojan", 0, 2)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(2)
                .SetPortrait(GetTexture("pirateTrojan.png"));

            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Anchored", Ability.None);
                plunderer.AddAbilities(ability);
            }

            if (ScrybeCompat.P03Enabled)
            {
                if (OverrideAct1.Value.HasFlag(ActOverride.Act3))
                    pirate.AddMetaCategories(ScrybeCompat.NatureRegion);

                if (OverrideGrimora.Value.HasFlag(ActOverride.Act3))
                    plunderer.AddMetaCategories(ScrybeCompat.UndeadRegion);

                Ability ability = ScrybeCompat.GetP03Ability("Armor Giver", Ability.None);
                Ability ability2 = ScrybeCompat.GetP03Ability("Shield Absorption", Ability.None);
                bot.AddAbilities(ability, ability2);
                bot.AddMetaCategories(ScrybeCompat.NeutralRegion);
            }
            else
            {
                bot.AddAbilities(Ability.LatchBrittle);
            }
        }
    }
}
