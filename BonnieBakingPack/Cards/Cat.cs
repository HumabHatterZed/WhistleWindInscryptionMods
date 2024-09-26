using DiskCardGame;
using GrimoraMod;
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
        private void CreateCats()
        {
            // Cat
            CardInfo cat = CardManager.New(pluginPrefix, "cat", "Cat", 1, 1, "A refined cat with elite tastes, in more ways than one.")
                .SetDefaultPart1Card().AddAct1()
                .SetBonesCost(4)
                .SetPortraitAndEmission(GetTexture("cat.png"), GetTexture("cat_emission.png"))
                .SetPixelPortrait(GetTexture("cat_pixel.png"))
                .AddAbilities(Ability.Morsel);

            CardInfo nine = CardManager.New(pluginPrefix, "nine", "Nine", 1, 1, "A REFINED FOX WAITING PATIENTLY FOR ITS TIME TO COME.")
                .SetDefaultPart1Card().AddGrimora().SetRare()
                .SetBonesCost(2)
                .SetPortraitAndEmission(GetTexture("nine.png"), GetTexture("nine_emission.png"))
                .AddAbilities(Ability.DrawCopyOnDeath)
                .AddSpecialAbilities(NineAbility.SpecialAbility);

            CardInfo bot = CardManager.New(pluginPrefix, "felinebot", "F4T C4T", 0, 2)
                .SetDefaultPart3Card().AddP03()
                .SetBloodCost(1)
                .SetPortrait(GetTexture("felinebot.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Macabre Growth", Ability.ActivatedStatsUpEnergy));

            if (ScrybeCompat.P03Enabled)
            {
                if (OverrideAct1.Value.HasFlag(ActOverride.Act3))
                    cat.AddMetaCategories(ScrybeCompat.NatureRegion);

                if (OverrideGrimora.Value.HasFlag(ActOverride.Act3))
                    nine.AddMetaCategories(ScrybeCompat.UndeadRegion);

                Ability ability = ScrybeCompat.GetP03RunAbility("Mine Cryptocurrency", Ability.CreateBells);
                bot.AddAbilities(ability);
                bot.AddMetaCategories(ScrybeCompat.NeutralRegion);
            }
        }
    }
}
