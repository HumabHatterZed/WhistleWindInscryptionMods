using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateBunnies()
        {
            // Bunny
            CardManager.New(pluginPrefix, "bunny", "Bunny", 1, 2, "A delivery driver with a missing sister. Perhaps you've seen her?")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(1)
                .SetPortraitAndEmission(GetTexture("bunny.png"), GetTexture("bunny_emission.png"))
                .SetPixelPortrait(GetTexture("bunny_pixel.png"))
                .AddAbilities(Ability.GainBattery);

            CardInfo duck = CardManager.New(pluginPrefix, "duckit", "Duckit", 1, 2, "AN ENIGMATIC, TWO-FACED CREATURE. PERHAPS YOU KNOW ITS TRUE IDENTITY?")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(2).SetEnergyCost(2)
                .SetPortraitAndEmission(GetTexture("duckit.png"), GetTexture("duckit_emission.png"))
                .AddSpecialAbilities(DuckRabbitAbility.SpecialAbility);

            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Random Ability");
                duck.AddAbilities(ability);
            }
            else
            {
                duck.AddAbilities(Ability.RandomAbility);
            }

            CardInfo bot = CardManager.New(pluginPrefix, "bunbot", "Bunbot", 1, 2)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(4)
                .SetPortrait(GetTexture("bunbot.png"));

            if (ScrybeCompat.P03Enabled)
            {
                Ability ability = ScrybeCompat.GetP03Ability("Hopper");
                Ability ability2 = ScrybeCompat.GetP03Ability("Flammable");
                bot.AddAbilities(ability, ability2).AddMetaCategories(ScrybeCompat.NatureRegion);
            }
            else
            {
                bot.AddAbilities(Ability.Strafe, Ability.ExplodeOnDeath);
            }
        }
    }
}
