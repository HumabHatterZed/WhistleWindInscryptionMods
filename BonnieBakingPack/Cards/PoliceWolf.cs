using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreatePoliceWolves()
        {
            CardInfo wolf = CardManager.New(pluginPrefix, "policeWolf", "Police Wolf", 2, 2, "An officer of the law, quick to respond to any trouble.")
                .SetDefaultPart1Card().AddAct1()
                .SetEnergyCost(6)
                .SetPortraitAndEmission(GetTexture("policeWolf.png"), GetTexture("policeWolf_emission.png"))
                .SetPixelPortrait(GetTexture("policeWolf_pixel.png"))
                .AddTribes(Tribe.Canine)
                .AddTraits(Trait.Wolf)
                .AddAbilities(Ability.CorpseEater);

            CardInfo spirit = CardManager.New(pluginPrefix, "spiritWolf", "Spirit Wolf", 2, 1, "THE DEGENERATED SPIRIT OF A ONCE-FEARSOME PREDATOR OF THE FOREST.")
                .SetDefaultPart1Card().AddGrimora()
                .SetEnergyCost(4)
                .SetPortraitAndEmission(GetTexture("spiritWolf.png"), GetTexture("spiritWolf_emission.png"))
                .AddTraits(Trait.Wolf);

            CardInfo bot = CardManager.New(pluginPrefix, "copstable", "Cop.stable", 1, 1)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(3)
                .SetPortrait(GetTexture("firewolf.png"))
                .SetPixelPortrait(GetTexture("firewolf_pixel.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Electric", Ability.DoubleStrike));

            if (ScrybeCompat.P03Enabled)
            {
                if (OverrideAct1.Value.HasFlag(ActOverride.Act3))
                    wolf.AddMetaCategories(ScrybeCompat.NatureRegion);

                if (OverrideGrimora.Value.HasFlag(ActOverride.Act3))
                    spirit.AddMetaCategories(ScrybeCompat.UndeadRegion);

                bot.AddMetaCategories(ScrybeCompat.NeutralRegion);
            }
        }
    }
}
