using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateCats()
        {
            // Cat
            CardManager.New(pluginPrefix, "cat", "Cat", 1, 1, "A refined cat with elite tastes, in more ways than one.")
                .SetDefaultPart1Card().AddAct1()
                .SetBonesCost(4)
                .SetPortraitAndEmission(GetTexture("cat.png"), GetTexture("cat_emission.png"))
                .SetPixelPortrait(GetTexture("cat_pixel.png"))
                .AddAbilities(Ability.Morsel);

            CardManager.New(pluginPrefix, "nine", "Nine", 1, 1, "A REFINED FOX WAITING PATIENTLY FOR ITS TIME TO COME.")
                .SetDefaultPart1Card().AddGrimora().SetRare()
                .SetBonesCost(2)
                .SetPortraitAndEmission(GetTexture("nine.png"), GetTexture("nine_emission.png"))
                .AddAbilities(Ability.DrawCopyOnDeath)
                .AddSpecialAbilities(NineAbility.SpecialAbility);

            CardInfo bot = CardManager.New(pluginPrefix, "felinebot", "F4T C47", 0, 2)
                .SetDefaultPart3Card().AddP03()
                .SetBloodCost(1)
                .SetPortrait(GetTexture("felinebot.png"));

            if (ScrybeCompat.P03Enabled)
            {
                Ability ability = ScrybeCompat.GetP03Ability("Macabre Growth");
                Ability ability2 = ScrybeCompat.GetP03Ability("Mine Cryptocurrency");
                bot.AddMetaCategories(ScrybeCompat.NeutralRegion);
                bot.AddAbilities(ability, ability2);
            }
            else
            {
                bot.AddAbilities(Ability.ActivatedStatsUp);
            }
        }
    }
}
