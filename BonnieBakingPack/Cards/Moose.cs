using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateMoose()
        {
            // Moose
            CardInfo moose = CardManager.New(pluginPrefix, "moose", "Moose", 2, 8, "A mooseterious being with a knack for telling tales.")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(3)
                .SetPortraitAndEmission(GetTexture("moose.png"), GetTexture("moose_emission.png"))
                .SetPixelPortrait(GetTexture("moose_pixel.png"))
                .AddTribes(Tribe.Hooved)
                .AddAbilities(Ability.WhackAMole);

            CardInfo moosetro = CardManager.New(pluginPrefix, "moosetro", "Moosetro", 0, 1, "ALONGSIDE HIS SKELETAL ENTOURAGE, HE TRAVELS THE WORLD SPREADING BEAUTIFUL MOOSEIC.")
                .SetDefaultPart1Card().AddGrimora().SetRare()
                .SetEnergyCost(6)
                .SetPortraitAndEmission(GetTexture("moosetro.png"), GetTexture("moosetro_emission.png"))
                .AddAbilities(ScrybeCompat.GetGrimoraAbility("Sea Shanty", Ability.BuffNeighbours), Ability.SkeletonStrafe); ;

            CardInfo bot = CardManager.New(pluginPrefix, "digitalMhoost", "Digital Mhoost", 1, 3)
                .SetDefaultPart3Card().AddP03()
                .SetBonesCost(4)
                .SetPortrait(GetTexture("moosebot.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Phase Through", Ability.StrafePush));

            if (ScrybeCompat.P03Enabled)
            {
                if (OverrideAct1.Value.HasFlag(ActOverride.Act3))
                    moose.AddMetaCategories(ScrybeCompat.NatureRegion);

                if (OverrideGrimora.Value.HasFlag(ActOverride.Act3))
                    moosetro.AddMetaCategories(ScrybeCompat.UndeadRegion);

                bot.AddMetaCategories(ScrybeCompat.UndeadRegion);
            }
        }
    }
}
