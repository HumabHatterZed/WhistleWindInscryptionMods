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
        private void CreateBunnies()
        {
            // Bunny
            CardInfo bunny = CardManager.New(pluginPrefix, "bunny", "Bunny", 1, 2, "A delivery driver with a missing sister. Perhaps you've seen her?")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(1)
                .SetPortraitAndEmission(GetTexture("bunny.png"), GetTexture("bunny_emission.png"))
                .SetPixelPortrait(GetTexture("bunny_pixel.png"))
                .AddAbilities(Ability.GainBattery);

            CardInfo duck = CardManager.New(pluginPrefix, "duckit", "Duckit", 1, 2, "AN ENIGMATIC, TWO-FACED CREATURE. PERHAPS YOU KNOW ITS TRUE IDENTITY?")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(2).SetEnergyCost(2)
                .SetPortraitAndEmission(GetTexture("duckit.png"), GetTexture("duckit_emission.png"))
                .AddSpecialAbilities(DuckRabbitAbility.SpecialAbility)
                .AddAbilities(
                    ScrybeCompat.GetGrimoraAbility("Random Ability", Ability.RandomAbility)
                    );

            CardInfo bot = CardManager.New(pluginPrefix, "bunbot", "Bunbot", 1, 3)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(5)
                .SetPortrait(GetTexture("bunbot.png"))
                .AddAbilities(
                    ScrybeCompat.GetP03Ability("Hopper", Ability.ExplodeOnDeath),
                    ScrybeCompat.GetP03Ability("Flammable", Ability.Strafe)
                    );

            if (ScrybeCompat.P03Enabled)
            {
                if (OverrideAct1.Value.HasFlag(ActOverride.Act3))
                    bunny.AddMetaCategories(ScrybeCompat.NatureRegion);

                if (OverrideGrimora.Value.HasFlag(ActOverride.Act3))
                    duck.AddMetaCategories(ScrybeCompat.UndeadRegion);

                bot.AddMetaCategories(ScrybeCompat.NatureRegion);
            }
        }
    }
}
