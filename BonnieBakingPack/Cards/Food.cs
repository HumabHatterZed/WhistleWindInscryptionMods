using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;

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

            // Act 3
            CardManager.New(pluginPrefix3, "redVelvet", "Extra RAM", 0, 0, "")
                .SetBonesCost(2).SetCardTemple(CardTemple.Tech)
                .SetPortrait(GetTexture("redVelvet_act3.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Upgrade", Ability.GainBattery))
                .SetTargetedSpell();

            // Grimora
            CardManager.New(pluginPrefixG, "redVelvet", "Dread Velvet", 1, 0, "")
                .SetBonesCost(2)
                .SetPortraitAndEmission(GetTexture("redVelvet_grimora.png"), GetTexture("redVelvet_grimora_emission.png"))
                .AddAbilities(GiveStats.AbilityID)
                .SetTargetedSpellStats()
                .SetTerrain(false);

            // Act 1
            CardManager.New(pluginPrefix, "whiteDonut", "White Donut", 0, 1, "")
                .SetBonesCost(1)
                .SetPortrait(GetTexture("whiteDonut.png"))
                .SetPixelPortrait(GetTexture("whiteDonut_pixel.png"))
                .AddAbilities(Ability.QuadrupleBones)
                .SetTerrain(false);

            // Act 3
            CardInfo red = CardManager.New(pluginPrefix3, "whiteDonut_red", "Red Noise", 0, 1, "")
                .SetBonesCost(2).SetCardTemple(CardTemple.Tech)
                .SetPortrait(GetTexture("whiteDonut_act3.png")).SetEmissivePortrait(GetTexture("whiteDonut_act3_red.png"))
                .AddAbilities(Ability.GainGemOrange, ScrybeCompat.GetP03Ability("Magic Dust", Ability.DrawRandomCardOnDeath))
                .SetGlobalSpell();

            // Act 3
            CardInfo green = CardManager.New(pluginPrefix3, "whiteDonut_green", "Green Noise", 0, 1, "")
                .SetBonesCost(2).SetCardTemple(CardTemple.Tech)
                .SetPortrait(GetTexture("whiteDonut_act3.png")).SetEmissivePortrait(GetTexture("whiteDonut_act3_green.png"))
                .AddAbilities(Ability.GainGemGreen, ScrybeCompat.GetP03Ability("Magic Dust", Ability.DrawRandomCardOnDeath))
                .SetGlobalSpell();

            // Act 3
            CardInfo blue = CardManager.New(pluginPrefix3, "whiteDonut_blue", "Blue Noise", 0, 1, "")
                .SetBonesCost(2).SetCardTemple(CardTemple.Tech)
                .SetPortrait(GetTexture("whiteDonut_act3.png")).SetEmissivePortrait(GetTexture("whiteDonut_act3_blue.png"))
                .AddAbilities(Ability.GainGemBlue, ScrybeCompat.GetP03Ability("Magic Dust", Ability.DrawRandomCardOnDeath))
                .SetGlobalSpell();

            // Act 3
            CardInfo white = CardManager.New(pluginPrefix3, "whiteDonut", "White Noise", 0, 1, "")
                .SetBonesCost(4).SetCardTemple(CardTemple.Tech)
                .SetPortrait(GetTexture("whiteDonut_act3.png"))
                .AddAbilities(Ability.GainGemTriple, ScrybeCompat.GetP03Ability("Magic Dust", Ability.DrawRandomCardOnDeath))
                .SetGlobalSpell();

            if (ScrybeCompat.P03Enabled)
            {
                ScrybeCompat.AddPart3Decal(red, red.GetEmissivePortrait().texture);
                ScrybeCompat.AddPart3Decal(green, green.GetEmissivePortrait().texture);
                ScrybeCompat.AddPart3Decal(blue, blue.GetEmissivePortrait().texture);
            }

            // Grimora
            CardManager.New(pluginPrefixG, "whiteDonut", "White Bonut", 0, 1, "")
                .SetBonesCost(1).SetCardTemple(CardTemple.Undead)
                .SetPortraitAndEmission(GetTexture("whiteDonut_grimora.png"), GetTexture("whiteDonut_grimora_emission.png"))
                .SetPixelPortrait(GetTexture("whiteDonut_pixel.png"))
                .AddAbilities(Ability.QuadrupleBones)
                .SetTerrain(true);

            CardManager.New(pluginPrefix, "pastry", "Pastry", 0, 2, "")
                .SetBonesCost(2)
                .SetPortrait(GetTexture("pastry.png"))
                .SetPixelPortrait(GetTexture("pastry_pixel.png"))
                .AddAbilities(GiveStats.AbilityID)
                .SetTargetedSpellStats()
                .SetTerrain(false);

            // Act 3
            CardManager.New(pluginPrefix3, "pastry", "PasteMe!", 0, 1, "")
                .SetBonesCost(3).SetCardTemple(CardTemple.Tech)
                .SetPortrait(GetTexture("pastry_act3.png"))
                .AddAbilities(GiveStats.AbilityID, ScrybeCompat.GetP03Ability("Iterate", Ability.DrawCopy));

            // Grimora
            CardManager.New(pluginPrefixG, "pastry", "Pastry", 0, 2, "")
                .SetBonesCost(2)
                .SetPortrait(GetTexture("pastry_grimora.png"), GetTexture("pastry_grimora_emission.png"))
                .AddAbilities(GiveStats.AbilityID)
                .SetTargetedSpellStats()
                .SetTerrain(false);

            CardManager.New(pluginPrefix, "meetBun", "Meet Bun", 0, 1, "")
                .SetBonesCost(3)
                .SetPortrait(GetTexture("meetBun.png"))
                .SetPixelPortrait(GetTexture("meetBun_pixel.png"))
                .AddAbilities(Ability.TripleBlood)
                .AddTraits(Trait.Goat);

            // Act 3
            CardManager.New(pluginPrefix3, "meetBun", "M33T BUN", 0, 1, "")
                .SetBonesCost(3).SetCardTemple(CardTemple.Tech)
                .SetPortrait(GetTexture("meetBun_act3.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Fully Loaded", Ability.DebuffEnemy), ScrybeCompat.GetP03Ability("Full of Blood", Ability.TripleBlood));

            // Grimora
            CardManager.New(pluginPrefixG, "meetBun", "Meet Bun", 0, 2, "")
                .SetBonesCost(3)
                .SetPortraitAndEmission(GetTexture("meetBun_grimora.png"), GetTexture("meetBun_grimora_emission.png"))
                .AddAbilities(ScrybeCompat.GetGrimoraAbility("Spirit Bearer", Ability.GainBattery))
                .SetTerrain(false);

            // Act 1
            CardManager.New(pluginPrefix, "scones", "Scones", 0, 1, "")
                .SetBonesCost(1)
                .AddAbilities(Ability.DrawCopy, GiveStats.AbilityID)
                .SetPortrait(GetTexture("scones.png"))
                .SetPixelPortrait(GetTexture("scones_pixel.png"))
                .SetTargetedSpellStats()
                .SetTerrain(false);

            // Act 3
            CardManager.New(pluginPrefix3, "scones", "Safety Cones", 0, 3, "")
                .SetBonesCost(3).SetCardTemple(CardTemple.Tech)
                .SetPortrait(GetTexture("scones_act3.png"));

            // Grimora
            CardManager.New(pluginPrefixG, "scones", "Scones", 0, 1, "")
                .SetBonesCost(1)
                .AddAbilities(Ability.DoubleDeath)
                .SetPortraitAndEmission(GetTexture("scones_grimora.png"), GetTexture("scones_grimora_emission.png"))
                .SetTerrain(false);

            CardManager.New(pluginPrefix, "eggTart", "Egg Tart", 0, 2, "")
                .SetBonesCost(2)
                .SetPortrait(GetTexture("eggTart.png"))
                .SetPixelPortrait(GetTexture("eggTart_pixel.png"))
                .AddAbilities(Ability.GainBattery)
                .SetTerrain(false);

            // Act 3
            CardManager.New(pluginPrefix3, "eggTart", "Egg.txt", 0, 0, "")
                .SetBonesCost(3).SetCardTemple(CardTemple.Tech)
                .SetPortrait(GetTexture("eggTart_act3.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Full of Guts", Ability.GainBattery))
                .SetGlobalSpell();

            // Grimora
            CardManager.New(pluginPrefixG, "eggTart", "Egg Tart", 0, 2, "")
                .SetBonesCost(2)
                .SetPortraitAndEmission(GetTexture("eggTart_grimora.png"), GetTexture("eggTart_grimora_emission.png"))
                .AddAbilities(ScrybeCompat.GetGrimoraAbility("Soul Sucker", Ability.GainBattery))
                .SetTerrain(false);
        }
    }
}
