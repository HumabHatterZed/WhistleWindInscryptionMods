using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateFood()
        {
            CardManager.New(pluginPrefix, "redVelvet", "Red Velvet", 1, 0, "")
                .SetBonesCost(2)
                .SetPortrait(GetTexture("redVelvet.png"))
                .SetPixelPortrait(GetTexture("redVelvet_pixel.png"))
                .AddAbilities(GiveStats.AbilityID)
                .SetTargetedSpellStats()
                .SetTerrain(false);

            CardManager.New(pluginPrefix, "whiteDonut", "White Donut", 0, 1, "")
                .SetBonesCost(1)
                .SetPortrait(GetTexture("whiteDonut.png"))
                .SetPixelPortrait(GetTexture("whiteDonut_pixel.png"))
                .AddAbilities(Ability.QuadrupleBones)
                .SetTerrain(false);

            CardManager.New(pluginPrefix, "pastry", "Pastry", 0, 2, "")
                .SetBonesCost(2)
                .SetPortrait(GetTexture("pastry.png"))
                .SetPixelPortrait(GetTexture("pastry_pixel.png"))
                .AddAbilities(GiveStats.AbilityID)
                .SetTargetedSpellStats()
                .SetTerrain(false);
        
            CardManager.New(pluginPrefix, "meetBun", "Meet Bun", 0, 1, "")
                .SetBonesCost(3)
                .SetPortrait(GetTexture("meetBun.png"))
                .SetPixelPortrait(GetTexture("meetBun_pixel.png"))
                .AddAbilities(Ability.TripleBlood)
                .AddTraits(Trait.Goat);

            CardManager.New(pluginPrefix, "scones", "Scones", 0, 1, "")
                .SetBonesCost(1)
                .AddAbilities(Ability.DrawCopy, GiveStats.AbilityID)
                .SetPortrait(GetTexture("scones.png"))
                .SetPixelPortrait(GetTexture("scones_pixel.png"))
                .SetTargetedSpellStats()
                .SetTerrain(false);

            CardManager.New(pluginPrefix, "eggTart", "Egg Tart", 0, 2, "")
                .SetBonesCost(2)
                .SetPortrait(GetTexture("eggTart.png"))
                .SetPixelPortrait(GetTexture("eggTart_pixel.png"))
                .AddAbilities(Ability.GainBattery)
                .SetTerrain(false);

            CardManager.New(pluginPrefix, "whiteDonut_grimora", "White Bonut", 0, 1, "")
                .SetBonesCost(1).SetCardTemple(CardTemple.Undead)
                .SetPortraitAndEmission(GetTexture("whiteDonut_grimora.png"), GetTexture("whiteDonut_grimora_emission.png"))
                .SetPixelPortrait(GetTexture("whiteDonut_pixel.png"))
                .AddAbilities(Ability.QuadrupleBones);
        }
    }
}
