using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateDogs()
        {
            // Dog
            CardManager.New(pluginPrefix, "dog", "Dog", 0, 2, "A diligent, if unappreciated, worker.")
                .SetDefaultPart1Card().AddAct1()
                .SetEnergyCost(2).AddTribes(Tribe.Canine)
                .SetPortraitAndEmission(GetTexture("dog.png"), GetTexture("dog_emission.png"))
                .SetPixelPortrait(GetTexture("dog_pixel.png"))
                .AddAbilities(Ability.GuardDog, Ability.Reach);

            CardManager.New(pluginPrefix, "doggone", "Doggone", 0, 2, "AN INSUBSTANTIAL EXISTENCE WHOSE ONLY CLAIM TO MEMORY IS THE SPACE IT TAKES UP. SURPRISINGLY SOLID.")
                .SetDefaultPart1Card().AddGrimora()
                .SetEnergyCost(2)
                .SetPortraitAndEmission(GetTexture("doggone.png"), GetTexture("doggone_emission.png"))
                .AddAbilities(Ability.GuardDog, Ability.Reach);

            CardInfo bot = CardManager.New(pluginPrefix, "dogbot", "K9", 0, 2)
                .SetDefaultPart3Card().AddP03()
                .SetBloodCost(1)
                .SetPortrait(GetTexture("dogbot.png"));

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
