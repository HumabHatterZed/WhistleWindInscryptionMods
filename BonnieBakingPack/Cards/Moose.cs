using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateMoose()
        {
            // Moose
            CardManager.New(pluginPrefix, "moose", "Moose", 2, 8, "A mooseterious being with a knack for telling tales.")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(3)
                .SetPortraitAndEmission(GetTexture("moose.png"), GetTexture("moose_emission.png"))
                .SetPixelPortrait(GetTexture("moose_pixel.png"))
                .AddTribes(Tribe.Hooved)
                .AddAbilities(Ability.WhackAMole);

            CardInfo moosetro = CardManager.New(pluginPrefix, "moosetro", "Moosetro", 0, 1, "ALONGSIDE HIS SKELETAL ENTOURAGE, HE TRAVELS THE WORLD SPREADING BEAUTIFUL MOOSEIC.")
                .SetDefaultPart1Card().AddGrimora().SetRare()
                .SetEnergyCost(6)
                .SetPortraitAndEmission(GetTexture("moosetro.png"), GetTexture("moosetro_emission.png"));

            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Sea Shanty");
                moosetro.AddAbilities(ability, Ability.SkeletonStrafe);
            }
            else
            {
                moosetro.AddAbilities(Ability.MoveBeside, Ability.BuffNeighbours);
            }


            CardInfo bot = CardManager.New(pluginPrefix, "moosebot", "M.0053", 1, 3)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(4)
                .SetPortrait(GetTexture("moose.png"));

            if (ScrybeCompat.P03Enabled)
            {
                Ability ability = ScrybeCompat.GetP03Ability("Phase Through");
                bot.AddAbilities(ability).AddMetaCategories(ScrybeCompat.NatureRegion);
            }
            else
            {
                bot.AddAbilities(Ability.StrafePush);
            }
        }
    }
}
