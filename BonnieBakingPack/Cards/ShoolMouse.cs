using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using System.Collections.Generic;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateShoolMice(CardInfo mouse, CardInfo bot)
        {
            // Shool Mouse
            CardManager.New(pluginPrefix, "mouseShool", "Shool Mouse", 1, 1, "A young mouse, spirited and full of potential.")
                .AddAct1().SetDefaultPart1Card()
                .SetBloodCost(1)
                .SetPortraitAndEmission(GetTexture("mouseShool.png"), GetTexture("mouseShool_emission.png"))
                .SetPixelPortrait(GetTexture("mouseShool_pixel.png"))
                .AddAbilities(Ability.Evolve)
                .AddTraits(Trait.Juvenile).SetEvolve(mouse, 1);

            CardInfo ghool = CardManager.New(pluginPrefix, "mouseGhool", "Ghool Mouse", 0, 1, "A SKITTISH MOUSE, SPIRITLESS AND HOLLOW.")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(3)
                .SetPortraitAndEmission(GetTexture("mouseGhool.png"), GetTexture("mouseGhool_emission.png"))
                .SetPixelPortrait(GetTexture("mouseGhool_pixel.png"));
            
            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability skin = ScrybeCompat.GetGrimoraAbility("Skin Crawler");
                ghool.AddAbilities(skin);
            }
            else
            {
                ghool.AddAbilities(Ability.CorpseEater);
            }

            CardInfo bot1 = CardManager.New(pluginPrefix, "minorMousebot", "Litle M0U53", 1, 1)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(3)
                .SetPortrait(GetTexture("minorMousebot.png"))
                .AddTraits(Trait.Juvenile)
                .SetEvolve(bot, 1);

            if (ScrybeCompat.P03Enabled)
            {
                Ability ability = ScrybeCompat.GetP03Ability("Transforms When Powered");
                bot1.AddAbilities(ability).AddMetaCategories(ScrybeCompat.NatureRegion);
            }
            else
            {
                bot1.AddAbilities(Ability.Evolve);
            }
        }
    }
}
