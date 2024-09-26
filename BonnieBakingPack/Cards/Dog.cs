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
            CardInfo dog = CardManager.New(pluginPrefix, "dog", "Dog", 0, 2, "A diligent, if unappreciated, worker.")
                .SetDefaultPart1Card().AddAct1()
                .SetEnergyCost(2).AddTribes(Tribe.Canine)
                .SetPortraitAndEmission(GetTexture("dog.png"), GetTexture("dog_emission.png"))
                .SetPixelPortrait(GetTexture("dog_pixel.png"))
                .AddAbilities(Ability.GuardDog, Ability.Reach);

            CardInfo doggone = CardManager.New(pluginPrefix, "doggone", "Doggone", 0, 2, "AN INSUBSTANTIAL EXISTENCE WHOSE ONLY CLAIM TO MEMORY IS THE SPACE IT TAKES UP. SURPRISINGLY SOLID.")
                .SetDefaultPart1Card().AddGrimora()
                .SetEnergyCost(2)
                .SetPortraitAndEmission(GetTexture("doggone.png"), GetTexture("doggone_emission.png"))
                .AddAbilities(Ability.GuardDog, Ability.Reach);

            CardInfo bot = CardManager.New(pluginPrefix, "dogbot", "K9", 0, 2)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(3)
                .SetPortrait(GetTexture("dogbot.png"))
                .AddAbilities(
                    ScrybeCompat.GetP03Ability("Launch Self", Ability.DrawRandomCardOnDeath),
                    ScrybeCompat.GetP03Ability("Solar Heart", Ability.GuardDog)
                    );

            if (ScrybeCompat.P03Enabled)
            {
                if (OverrideAct1.Value.HasFlag(ActOverride.Act3))
                    dog.AddMetaCategories(ScrybeCompat.NatureRegion);

                if (OverrideGrimora.Value.HasFlag(ActOverride.Act3))
                    doggone.AddMetaCategories(ScrybeCompat.UndeadRegion);

                bot.AddMetaCategories(ScrybeCompat.NeutralRegion);
            }
        }
    }
}
