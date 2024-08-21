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
                .AddAbilities(Ability.Reach, Ability.GuardDog);

            CardManager.New(pluginPrefix, "doggone", "Doggone", 0, 2, "AN INSUBSTANTIAL EXISTENCE WHOSE ONLY CLAIM TO MEMORY IS THE SPACE IT TAKES UP. SURPRISINGLY SOLID.")
                .SetDefaultPart1Card().AddGrimora()
                .SetEnergyCost(2)
                .SetPortraitAndEmission(GetTexture("doggone.png"), GetTexture("doggone_emission.png"))
                .AddAbilities(Ability.GuardDog, Ability.Reach);
        }
    }
}
