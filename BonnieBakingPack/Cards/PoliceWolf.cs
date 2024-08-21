using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreatePoliceWolves()
        {
            // Police Wolf
            CardManager.New(pluginPrefix, "policeWolf", "Police Wolf", 2, 2, "An officer of the law, quick to respond to any trouble.")
                .SetDefaultPart1Card().AddAct1()
                .SetEnergyCost(6)
                .SetPortraitAndEmission(GetTexture("policeWolf.png"), GetTexture("policeWolf_emission.png"))
                .SetPixelPortrait(GetTexture("policeWolf_pixel.png"))
                .AddTribes(Tribe.Canine)
                .AddTraits(Trait.Wolf)
                .AddAbilities(Ability.CorpseEater);

            CardManager.New(pluginPrefix, "spiritWolf", "Spirit Wolf", 2, 1, "THE DEGENERATED SPIRIT OF A ONCE-FEARSOME PREDATOR OF THE FOREST.")
                .SetDefaultPart1Card().AddGrimora()
                .SetEnergyCost(4)
                .SetPortraitAndEmission(GetTexture("spiritWolf.png"), GetTexture("spiritWolf_emission.png"))
                .AddTraits(Trait.Wolf);

            CardInfo bot = CardManager.New(pluginPrefix, "copstable", "Cop.stable", 1, 1)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(3)
                .SetPortrait(GetTexture("firewolf.png"));

            if (ScrybeCompat.P03Enabled)
            {
                Ability ability = ScrybeCompat.GetP03Ability("Electric");
                bot.AddAbilities(ability).AddMetaCategories(ScrybeCompat.NeutralRegion);
            }
            else
            {
                bot.AddAbilities(Ability.DoubleStrike);
            }
        }
    }
}
