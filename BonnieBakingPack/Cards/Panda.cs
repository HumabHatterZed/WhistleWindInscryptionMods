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
        private void CreatePandas()
        {
            // Panda
            CardManager.New(pluginPrefix, "panda", "Panda", 1, 2, "A detective on the hunt for a killer. Armed and dangerous.")
                .SetDefaultPart1Card().AddAct1()
                .SetCost(1, 3)
                .SetPortraitAndEmission(GetTexture("panda.png"), GetTexture("panda_emission.png"))
                .SetAltPortrait(GetTexture("panda_alt.png"))
                .SetPixelPortrait(GetTexture("panda_pixel.png"))
                .SetPixelAlternatePortrait(GetTexture("panda_alt_pixel.png"))
                .AddSpecialAbilities(PandaAbility.SpecialAbility)
                .AddAbilities(Ability.Deathtouch);

            CardInfo dead = CardManager.New(pluginPrefix, "deadtective", "Deadtective", 1, 1, "A DETECTIVE THAT WAS HUNTED BY A KILLER. ARMLESS YET DANGEROUS.")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(4)
                .SetPortraitAndEmission(GetTexture("deadtective.png"), GetTexture("deadtective_emission.png"))
                .SetAltPortrait(GetTexture("deadtective_alt.png")).SetEmissiveAltPortrait(GetTexture("deadtective_alt_emission.png"))
                .AddSpecialAbilities(PandaAbility.SpecialAbility);

            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Soul Shot");
                dead.AddAbilities(ability);
            }
            else
            {
                dead.AddAbilities(Ability.ActivatedDealDamage);
            }

            CardInfo bot = CardManager.New(pluginPrefix, "pandat", "Pardan Panda", 1, 2)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(5)
                .SetPortrait(GetTexture("pandat.png"))
                .SetAltPortrait(GetTexture("pandat_alt.png"))
                .AddSpecialAbilities(PandaAbility.SpecialAbility);

            if (ScrybeCompat.P03Enabled)
            {
                Ability ability2 = ScrybeCompat.GetP03Ability("Nerf This!");
                CardAppearanceBehaviour.Appearance app = GuidManager.GetEnumValue<CardAppearanceBehaviour.Appearance>(ScrybeCompat.P03Guid, "ForceRevolverAppearance");
                bot.AddAbilities(Ability.Deathtouch, ability2).AddMetaCategories(ScrybeCompat.NatureRegion).AddAppearances(app);
            }
            else
            {
                bot.AddAbilities(Ability.Sniper, Ability.BuffEnemy);
            }
        }
    }
}
