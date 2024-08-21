using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreatePhoneMice()
        {
            // Phone Mouse
            CardManager.New(pluginPrefix, "mousePhone", "Phone Mouse", 0, 2, "A chatty little rodent. When it dies, backup follows swiftly.")
                .SetDefaultPart1Card().AddAct1()
                .SetEnergyCost(3)
                .SetPortraitAndEmission(GetTexture("mousePhone.png"), GetTexture("mousePhone_emission.png"))
                .SetPixelPortrait(GetTexture("mousePhone_pixel.png"))
                .AddAbilities(Ability.DrawRandomCardOnDeath)
                .AddTraits(Trait.SatisfiesRingTrial);

            CardInfo killer = CardManager.New(pluginPrefix, "killerMouse", "Killer Mouse", 1, 2, "DEATH FOLLOWS SWIFTLY WHEN THIS STABBY LITTLE MOUSE COMES A-CALLING.")
                .SetDefaultPart1Card().AddGrimora()
                .SetEnergyCost(3)
                .SetPortraitAndEmission(GetTexture("killerMouse.png"), GetTexture("killerMouse_emission.png"))
                .SetPixelPortrait(GetTexture("killerMouse_pixel.png"));

            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Slasher");
                Ability ability2 = ScrybeCompat.GetGrimoraAbility("Haunting Call");
                killer.AddAbilities(ability, ability2);
            }
            else
            {
                killer.AddAbilities(Ability.DoubleStrike);
            }
        }
    }
}
