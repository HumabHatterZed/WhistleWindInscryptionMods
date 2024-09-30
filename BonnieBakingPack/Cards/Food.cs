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

            CardManager.New(pluginPrefix, "redVelvet_act3", "Extra RAM", 0, 0, "")
                .SetBonesCost(2)
                .SetPortrait(GetTexture("redVelvet_act3.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Upgrade!", Ability.GainBattery))
                .SetTargetedSpell();

            CardManager.New(pluginPrefix, "whiteDonut", "White Donut", 0, 1, "")
                .SetBonesCost(1)
                .SetPortrait(GetTexture("whiteDonut.png"))
                .SetPixelPortrait(GetTexture("whiteDonut_pixel.png"))
                .AddAbilities(Ability.QuadrupleBones)
                .SetTerrain(false);

            CardManager.New(pluginPrefix, "whiteDonut_act3", "White Noise", 0, 1, "")
                .SetBonesCost(3)
                .SetPortrait(GetTexture("whiteDonut_act3.png"))
                .AddAbilities(GiveSigils.AbilityID, ScrybeCompat.GetP03Ability("Apotheosis", Ability.RandomAbility));

            CardManager.New(pluginPrefix, "whiteDonut_grimora", "White Bonut", 0, 1, "")
                .SetBonesCost(1).SetCardTemple(CardTemple.Undead)
                .SetPortraitAndEmission(GetTexture("whiteDonut_grimora.png"), GetTexture("whiteDonut_grimora_emission.png"))
                .SetPixelPortrait(GetTexture("whiteDonut_pixel.png"))
                .AddAbilities(Ability.QuadrupleBones);

            CardManager.New(pluginPrefix, "pastry", "Pastry", 0, 2, "")
                .SetBonesCost(2)
                .SetPortrait(GetTexture("pastry.png"))
                .SetPixelPortrait(GetTexture("pastry_pixel.png"))
                .AddAbilities(GiveStats.AbilityID)
                .SetTargetedSpellStats()
                .SetTerrain(false);

            CardManager.New(pluginPrefix, "pastry_act3", "PasteMe!", 0, 0, "")
                .SetEnergyCost(4)
                .SetPortrait(GetTexture("pastry_act3.png"))
                .AddAbilities(ScrybeCompat.GetP03ExpAbility("Sticker Lord", ScrybeCompat.GetP03Ability("Fully Loaded", Ability.DrawRabbits)))
                .SetTargetedSpell();

            CardManager.New(pluginPrefix, "meetBun", "Meet Bun", 0, 1, "")
                .SetBonesCost(3)
                .SetPortrait(GetTexture("meetBun.png"))
                .SetPixelPortrait(GetTexture("meetBun_pixel.png"))
                .AddAbilities(Ability.TripleBlood)
                .AddTraits(Trait.Goat);

            CardManager.New(pluginPrefix, "meetBun_act3", "M33T BUN", 0, 1, "")
                .SetBonesCost(2)
                .SetPortrait(GetTexture("meetBun_act3.png"))
                .AddAbilities(GiveSigils.AbilityID, ScrybeCompat.GetP03Ability("Full of Blood", Ability.Morsel))
                .SetTargetedSpell();

            CardManager.New(pluginPrefix, "scones", "Scones", 0, 1, "")
                .SetBonesCost(1)
                .AddAbilities(Ability.DrawCopy, GiveStats.AbilityID)
                .SetPortrait(GetTexture("scones.png"))
                .SetPixelPortrait(GetTexture("scones_pixel.png"))
                .SetTargetedSpellStats()
                .SetTerrain(false);

            CardManager.New(pluginPrefix, "scones_act3", "S Cones", 0, 2, "")
                .SetEnergyCost(1)
                .SetPortrait(GetTexture("scones_act3.png"))
                .AddAbilities(Ability.Reach);

            CardManager.New(pluginPrefix, "eggTart", "Egg Tart", 0, 2, "")
                .SetBonesCost(2)
                .SetPortrait(GetTexture("eggTart.png"))
                .SetPixelPortrait(GetTexture("eggTart_pixel.png"))
                .AddAbilities(Ability.GainBattery)
                .SetTerrain(false);

            CardManager.New(pluginPrefix, "eggTart_act3", "Egg.txt", 0, 0, "")
                .SetEnergyCost(2)
                .SetPortrait(GetTexture("eggTart_act3.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Slime Vandal", Ability.CreateEgg))
                .SetGlobalSpell();
        }
    }
}
